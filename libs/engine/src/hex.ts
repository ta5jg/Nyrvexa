/* =============================================================================
 * File:           web/packages/engine/src/hex.ts
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-05-03
 * Last Update:    2026-05-03
 * Version:        0.1.0
 *
 * Description:
 *   Axial hex coordinate math (q, r). Distance, neighbors, ring, range,
 *   line — all the standard ops a 4X needs. Pointy-top orientation.
 *   Reference: https://www.redblobgames.com/grids/hexagons/
 *
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

import type { Hex } from "@nyrvexa/protocol";

export function hexEquals(a: Hex, b: Hex): boolean {
  return a.q === b.q && a.r === b.r;
}

export function hexKey(h: Hex): string {
  return `${h.q},${h.r}`;
}

export function hexFromKey(k: string): Hex {
  const [q, r] = k.split(",").map(Number);
  return { q: q!, r: r! };
}

/** Axial → cube. */
function toCube(h: Hex): { x: number; y: number; z: number } {
  return { x: h.q, z: h.r, y: -h.q - h.r };
}

export function hexDistance(a: Hex, b: Hex): number {
  const ac = toCube(a);
  const bc = toCube(b);
  return Math.max(Math.abs(ac.x - bc.x), Math.abs(ac.y - bc.y), Math.abs(ac.z - bc.z));
}

export const HEX_DIRECTIONS: readonly Hex[] = [
  { q: 1, r: 0 },
  { q: 1, r: -1 },
  { q: 0, r: -1 },
  { q: -1, r: 0 },
  { q: -1, r: 1 },
  { q: 0, r: 1 }
] as const;

export function hexAdd(a: Hex, b: Hex): Hex {
  return { q: a.q + b.q, r: a.r + b.r };
}

export function hexNeighbors(h: Hex): Hex[] {
  return HEX_DIRECTIONS.map((d) => hexAdd(h, d));
}

/** All hexes within `radius` of center, inclusive (center itself included). */
export function hexesInRange(center: Hex, radius: number): Hex[] {
  const result: Hex[] = [];
  for (let q = -radius; q <= radius; q++) {
    const rMin = Math.max(-radius, -q - radius);
    const rMax = Math.min(radius, -q + radius);
    for (let r = rMin; r <= rMax; r++) {
      result.push({ q: center.q + q, r: center.r + r });
    }
  }
  return result;
}

/** Hexes on the ring at exactly `radius` from center. */
export function hexRing(center: Hex, radius: number): Hex[] {
  if (radius === 0) return [{ ...center }];
  const result: Hex[] = [];
  let h = hexAdd(center, scale(HEX_DIRECTIONS[4]!, radius));
  for (let i = 0; i < 6; i++) {
    for (let j = 0; j < radius; j++) {
      result.push(h);
      h = hexAdd(h, HEX_DIRECTIONS[i]!);
    }
  }
  return result;
}

function scale(h: Hex, k: number): Hex {
  return { q: h.q * k, r: h.r * k };
}

/** Convert axial coordinates to pixel center for pointy-top hexes of size `size`. */
export function hexToPixel(h: Hex, size: number): { x: number; y: number } {
  const x = size * Math.sqrt(3) * (h.q + h.r / 2);
  const y = size * 1.5 * h.r;
  return { x, y };
}

/** Pixel coordinate → nearest hex. */
export function pixelToHex(x: number, y: number, size: number): Hex {
  const q = ((Math.sqrt(3) / 3) * x - (1 / 3) * y) / size;
  const r = ((2 / 3) * y) / size;
  return hexRound({ q, r });
}

/** Round fractional axial coords to the nearest integer hex. */
export function hexRound(h: { q: number; r: number }): Hex {
  let x = h.q;
  let z = h.r;
  let y = -x - z;
  let rx = Math.round(x);
  let ry = Math.round(y);
  let rz = Math.round(z);
  const xDiff = Math.abs(rx - x);
  const yDiff = Math.abs(ry - y);
  const zDiff = Math.abs(rz - z);
  if (xDiff > yDiff && xDiff > zDiff) rx = -ry - rz;
  else if (yDiff > zDiff) ry = -rx - rz;
  else rz = -rx - ry;
  return { q: rx, r: rz };
}

/** Linear interpolation between two hexes. */
export function hexLerp(a: Hex, b: Hex, t: number): { q: number; r: number } {
  return { q: a.q * (1 - t) + b.q * t, r: a.r * (1 - t) + b.r * t };
}

/** Hex line from `a` to `b`, inclusive of both endpoints. */
export function hexLine(a: Hex, b: Hex): Hex[] {
  const n = hexDistance(a, b);
  if (n === 0) return [{ ...a }];
  const out: Hex[] = [];
  for (let i = 0; i <= n; i++) {
    out.push(hexRound(hexLerp(a, b, i / n)));
  }
  return out;
}
