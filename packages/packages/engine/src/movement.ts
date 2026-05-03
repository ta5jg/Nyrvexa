/* =============================================================================
 * File:           web/packages/engine/src/movement.ts
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-05-03
 * Last Update:    2026-05-03
 * Version:        0.1.0
 *
 * Description:
 *   Pathfinding + reachability for hex-based unit movement. Dijkstra over
 *   biome move costs, capped by the unit's remaining movement points.
 *   Returns the set of reachable tiles (with their cost) and a path
 *   reconstructor to trace from origin to a chosen target.
 *
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

import { BIOMES } from "@nyrvexa/content";
import type { Hex, UnitInstance, WorldSnapshot } from "@nyrvexa/protocol";
import { hexKey, hexNeighbors } from "./hex.js";
import { tileMap, unitAt } from "./world.js";

export type ReachMap = {
  /** key (`q,r`) → total cost from origin to enter this tile */
  cost: Map<string, number>;
  /** key → previous tile key (for path reconstruction) */
  prev: Map<string, string>;
};

/**
 * Compute all hexes the unit can reach this turn, respecting move costs and
 * impassable terrain. Friendly units block; enemy units don't (handled in
 * combat instead).
 */
export function computeReach(state: WorldSnapshot, unit: UnitInstance): ReachMap {
  const tiles = tileMap(state);
  const cost = new Map<string, number>();
  const prev = new Map<string, string>();
  const start: Hex = { q: unit.q, r: unit.r };
  const startKey = hexKey(start);
  cost.set(startKey, 0);

  // Tiny priority queue — flat sorted array is fine at v0.1 sizes.
  const queue: Array<{ h: Hex; c: number }> = [{ h: start, c: 0 }];

  while (queue.length > 0) {
    queue.sort((a, b) => a.c - b.c);
    const { h, c } = queue.shift()!;
    if (c > unit.movesLeft) continue;

    for (const n of hexNeighbors(h)) {
      const nk = hexKey(n);
      const t = tiles.get(nk);
      if (!t) continue;
      const biome = BIOMES[t.biome];
      if (!biome.passable) continue;
      const blocker = unitAt(state, n.q, n.r);
      // Friendly unit on the destination blocks the path-through.
      if (blocker && blocker.id !== unit.id && blocker.ownerId === unit.ownerId) continue;
      const newCost = c + biome.moveCost;
      if (newCost > unit.movesLeft) continue;
      const existing = cost.get(nk);
      if (existing !== undefined && existing <= newCost) continue;
      cost.set(nk, newCost);
      prev.set(nk, hexKey(h));
      queue.push({ h: n, c: newCost });
    }
  }

  return { cost, prev };
}

/** Return true if the unit can reach (q, r) this turn. */
export function canReach(reach: ReachMap, q: number, r: number): boolean {
  return reach.cost.has(`${q},${r}`);
}

/** Reconstruct the path from origin to (q, r). Empty if unreachable. */
export function reconstructPath(reach: ReachMap, origin: Hex, target: Hex): Hex[] {
  const targetKey = hexKey(target);
  if (!reach.cost.has(targetKey)) return [];
  const out: Hex[] = [];
  let curKey = targetKey;
  while (curKey !== hexKey(origin)) {
    const [q, r] = curKey.split(",").map(Number);
    out.push({ q: q!, r: r! });
    const p = reach.prev.get(curKey);
    if (!p) break;
    curKey = p;
  }
  out.push({ ...origin });
  return out.reverse();
}
