/* =============================================================================
 * File:           web/packages/engine/src/map.ts
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-05-03
 * Last Update:    2026-05-03
 * Version:        0.1.0
 *
 * Description:
 *   Procedural map generation. Two-pass approach:
 *     1. Value noise → continent shape (water vs land) using a layered
 *        FNV-derived hash so output is deterministic given the seed.
 *     2. Per-tile classifier → biome (plain/forest/hill/mountain/desert/
 *        tundra) based on latitude + a separate noise field.
 *   Returns a list of tiles in axial (offset-shifted) coords centered on
 *   (0, 0). No external noise library — keeps the engine dependency-free.
 *
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

import type { BiomeId, ResourceId, Tile } from "@nyrvexa/protocol";
import { fnv1a32 } from "./rng.js";

export type MapOptions = {
  seed: string;
  width: number;
  height: number;
};

/** Quick value-noise: hash the integer point + octave offset → [0, 1). */
function pointNoise(seed: number, x: number, y: number, octave: number): number {
  const h = fnv1a32(`${seed}:${octave}:${x}:${y}`);
  return h / 0xffffffff;
}

/** Smoothstep-interpolated noise sampling at fractional coords. */
function smoothNoise(seed: number, x: number, y: number, octave: number): number {
  const x0 = Math.floor(x);
  const y0 = Math.floor(y);
  const x1 = x0 + 1;
  const y1 = y0 + 1;
  const fx = x - x0;
  const fy = y - y0;
  const sx = fx * fx * (3 - 2 * fx);
  const sy = fy * fy * (3 - 2 * fy);
  const n00 = pointNoise(seed, x0, y0, octave);
  const n10 = pointNoise(seed, x1, y0, octave);
  const n01 = pointNoise(seed, x0, y1, octave);
  const n11 = pointNoise(seed, x1, y1, octave);
  const ix0 = n00 * (1 - sx) + n10 * sx;
  const ix1 = n01 * (1 - sx) + n11 * sx;
  return ix0 * (1 - sy) + ix1 * sy;
}

/** Octave-summed noise — gives soft continent shapes. */
function fbm(seed: number, x: number, y: number, octaves: number): number {
  let amp = 1;
  let freq = 1;
  let sum = 0;
  let norm = 0;
  for (let i = 0; i < octaves; i++) {
    sum += amp * smoothNoise(seed, x * freq, y * freq, i);
    norm += amp;
    amp *= 0.5;
    freq *= 2;
  }
  return sum / norm;
}

/** Convert offset (col, row) where row is even/odd-shifted → axial (q, r). */
function offsetToAxial(col: number, row: number): { q: number; r: number } {
  // odd-r offset: shifts odd rows right.
  const q = col - ((row - (row & 1)) >> 1);
  const r = row;
  return { q, r };
}

export function generateMap(opts: MapOptions): Tile[] {
  const { width, height } = opts;
  const elevSeed = fnv1a32(opts.seed + ":elev");
  const moistSeed = fnv1a32(opts.seed + ":moist");
  const tempSeed = fnv1a32(opts.seed + ":temp");
  const resourceSeed = fnv1a32(opts.seed + ":res");

  const tiles: Tile[] = [];
  const cx = width / 2;
  const cy = height / 2;

  for (let row = 0; row < height; row++) {
    for (let col = 0; col < width; col++) {
      const { q, r } = offsetToAxial(col, row);

      // Continent shape — fbm with a radial dropoff so the world has clear edges.
      const elevation = fbm(elevSeed, col * 0.18, row * 0.18, 4);
      const dx = (col - cx) / cx;
      const dy = (row - cy) / cy;
      const radial = Math.sqrt(dx * dx + dy * dy);
      const continentBias = Math.max(0, 1 - radial * 0.9);
      const land = elevation * 0.55 + continentBias * 0.45;

      // Latitude-based temperature: poles cold, equator hot.
      const lat = Math.abs(row - cy) / cy;
      const temp = 1 - lat * 0.9 + (fbm(tempSeed, col * 0.12, row * 0.12, 2) - 0.5) * 0.3;
      const moist = fbm(moistSeed, col * 0.16, row * 0.16, 3);

      const biome = classify(land, temp, moist);

      // Resources sparsely scattered (~6% chance) and biome-correlated.
      const rRoll = pointNoise(resourceSeed, col, row, 1);
      const resource = rollResource(biome, rRoll);

      tiles.push({
        q,
        r,
        biome,
        seenMask: 0,
        visibleMask: 0,
        resource: resource ?? null
      });
    }
  }

  return tiles;
}

function classify(land: number, temp: number, moist: number): BiomeId {
  if (land < 0.42) return "water";
  if (land > 0.78) return "mountain";
  if (temp < 0.25) return "tundra";
  if (temp > 0.65 && moist < 0.35) return "desert";
  if (moist > 0.62) return "forest";
  if (land > 0.62) return "hill";
  return "plain";
}

function rollResource(biome: BiomeId, roll: number): ResourceId | null {
  if (roll < 0.92) return null;
  switch (biome) {
    case "plain":   return "wheat";
    case "forest":  return "wheat";
    case "hill":    return roll < 0.96 ? "iron" : "gold_ore";
    case "mountain":return "stone";
    case "desert":  return "gold_ore";
    case "tundra":  return null;
    case "water":   return null;
  }
}

/** Find a starting tile for a player — passable, non-water, with food yield. */
export function findStartTile(tiles: Tile[], existing: Array<{ q: number; r: number }>, minDist: number, salt: string): { q: number; r: number } | null {
  const tilesByKey = new Map(tiles.map((t) => [`${t.q},${t.r}`, t]));
  const candidates = tiles.filter(
    (t) => t.biome === "plain" || t.biome === "forest" || t.biome === "tundra"
  );
  const seedHash = fnv1a32(salt);
  // Deterministic pseudo-shuffle.
  const sorted = [...candidates].sort(
    (a, b) => fnv1a32(`${seedHash}:${a.q}:${a.r}`) - fnv1a32(`${seedHash}:${b.q}:${b.r}`)
  );
  for (const c of sorted) {
    let ok = true;
    for (const e of existing) {
      const dx = c.q - e.q;
      const dy = c.r - e.r;
      const dz = -dx - dy;
      const dist = Math.max(Math.abs(dx), Math.abs(dy), Math.abs(dz));
      if (dist < minDist) {
        ok = false;
        break;
      }
    }
    if (ok && tilesByKey.has(`${c.q},${c.r}`)) return { q: c.q, r: c.r };
  }
  return null;
}
