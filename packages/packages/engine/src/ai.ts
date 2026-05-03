/* =============================================================================
 * File:           web/packages/engine/src/ai.ts
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-05-03
 * Last Update:    2026-05-03
 * Version:        0.1.0
 *
 * Description:
 *   v0.1 AI — deterministic greedy / tactical. Priority order:
 *     1. Settlers found cities at their current tile if foundable; else
 *        move toward the nearest settle-eligible plain/forest/hill.
 *     2. Warriors near an enemy attack the weakest neighbour.
 *     3. Otherwise scouts/warriors march toward the nearest unscouted
 *        rim of the player's vision.
 *     4. Cities default to producing settler → warrior → warrior, then
 *        warrior loop.
 *     5. Research auto-picks cheapest available tech (handled in
 *        research.ts).
 *
 *   Output is a list of AiCommand objects; the consumer (turn pipeline
 *   or main thread) applies them in order.
 *
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

import { BIOMES, UNITS } from "@nyrvexa/content";
import type {
  AiCommand,
  CityInstance,
  Hex,
  UnitInstance,
  WorldSnapshot
} from "@nyrvexa/protocol";
import { hexDistance, hexNeighbors } from "./hex.js";
import { computeReach } from "./movement.js";
import { citiesOf, tileAt, tileMap, unitAt, unitsOf } from "./world.js";

/** Plan a turn's worth of commands for the given player. Pure function. */
export function planTurn(state: WorldSnapshot, playerId: number): AiCommand[] {
  const cmds: AiCommand[] = [];
  const myUnits = unitsOf(state, playerId);
  const myCities = citiesOf(state, playerId);

  // ---- Cities: production policy ----
  for (const c of myCities) {
    cmds.push({ kind: "buildUnit", cityId: c.id, unit: chooseProduction(state, c, playerId) });
  }

  // ---- Units: per-unit decisions ----
  for (const u of myUnits) {
    if (u.type === "settler") {
      const action = planSettler(state, u);
      if (action) cmds.push(action);
      continue;
    }
    if (UNITS[u.type].canAttack) {
      const attack = planAttack(state, u);
      if (attack) {
        cmds.push(attack);
        continue;
      }
    }
    const explore = planExplore(state, u, playerId);
    if (explore) cmds.push(explore);
  }

  return cmds;
}

function chooseProduction(state: WorldSnapshot, _city: CityInstance, playerId: number) {
  const owned = unitsOf(state, playerId);
  const settlers = owned.filter((u) => u.type === "settler").length;
  const cities = citiesOf(state, playerId).length;
  const warriors = owned.filter((u) => u.type === "warrior").length;
  const scouts = owned.filter((u) => u.type === "scout").length;

  // Aim for ~2 cities by mid game, then military.
  if (settlers === 0 && cities < 2) return "settler";
  if (warriors < cities + 1) return "warrior";
  if (scouts < 1) return "scout";
  return "warrior";
}

function planSettler(state: WorldSnapshot, u: UnitInstance): AiCommand | null {
  const tile = tileAt(state, u.q, u.r);
  // Settle here if foundable + far enough from existing cities.
  if (tile && BIOMES[tile.biome].cityFoundable) {
    const farEnough = state.cities.every((c) => hexDistance({ q: c.q, r: c.r }, { q: u.q, r: u.r }) >= 3);
    if (farEnough) return { kind: "settle", unitId: u.id };
  }
  // Otherwise step toward the nearest foundable plains/forest/hill.
  const target = findNearbyFoundable(state, u);
  if (target) {
    const next = stepToward(state, u, target);
    if (next) return { kind: "moveUnit", unitId: u.id, toQ: next.q, toR: next.r };
  }
  return null;
}

function planAttack(state: WorldSnapshot, u: UnitInstance): AiCommand | null {
  // Adjacent enemy unit?
  for (const n of hexNeighbors({ q: u.q, r: u.r })) {
    const enemy = unitAt(state, n.q, n.r);
    if (enemy && enemy.ownerId !== u.ownerId) {
      return { kind: "attack", unitId: u.id, targetQ: n.q, targetR: n.r };
    }
    const city = state.cities.find((c) => c.q === n.q && c.r === n.r);
    if (city && city.ownerId !== u.ownerId) {
      return { kind: "attack", unitId: u.id, targetQ: n.q, targetR: n.r };
    }
  }
  // Move toward nearest enemy unit/city we can see (visibleMask check).
  const target = findNearestVisibleEnemy(state, u);
  if (target) {
    const next = stepToward(state, u, target);
    if (next) return { kind: "moveUnit", unitId: u.id, toQ: next.q, toR: next.r };
  }
  return null;
}

function planExplore(state: WorldSnapshot, u: UnitInstance, playerId: number): AiCommand | null {
  // Step into the nearest unseen tile we can reach this turn.
  const reach = computeReach(state, u);
  const tiles = tileMap(state);
  let bestKey: string | null = null;
  let bestCost = Infinity;
  for (const [key, cost] of reach.cost) {
    const [q, r] = key.split(",").map(Number);
    const t = tiles.get(`${q},${r}`);
    if (!t) continue;
    if ((t.seenMask & (1 << playerId)) !== 0) continue; // already seen
    if (!BIOMES[t.biome].passable) continue;
    if (cost < bestCost) {
      bestCost = cost;
      bestKey = key;
    }
  }
  if (bestKey) {
    const [q, r] = bestKey.split(",").map(Number);
    return { kind: "moveUnit", unitId: u.id, toQ: q!, toR: r! };
  }
  // No new ground reachable — wander toward nearest unexplored frontier.
  const frontier = findNearestUnexplored(state, u, playerId);
  if (frontier) {
    const next = stepToward(state, u, frontier);
    if (next) return { kind: "moveUnit", unitId: u.id, toQ: next.q, toR: next.r };
  }
  return null;
}

function findNearbyFoundable(state: WorldSnapshot, u: UnitInstance): Hex | null {
  let best: Hex | null = null;
  let bestDist = Infinity;
  for (const t of state.tiles) {
    if (!BIOMES[t.biome].cityFoundable) continue;
    if (state.cities.some((c) => c.q === t.q && c.r === t.r)) continue;
    const tooClose = state.cities.some(
      (c) => hexDistance({ q: c.q, r: c.r }, { q: t.q, r: t.r }) < 3
    );
    if (tooClose) continue;
    const d = hexDistance({ q: u.q, r: u.r }, { q: t.q, r: t.r });
    if (d < bestDist) {
      bestDist = d;
      best = { q: t.q, r: t.r };
    }
  }
  return best;
}

function findNearestVisibleEnemy(state: WorldSnapshot, u: UnitInstance): Hex | null {
  const ownerBit = 1 << u.ownerId;
  let best: Hex | null = null;
  let bestDist = Infinity;
  // Enemy units we can see right now.
  for (const e of state.units) {
    if (e.ownerId === u.ownerId) continue;
    const t = tileAt(state, e.q, e.r);
    if (!t || (t.visibleMask & ownerBit) === 0) continue;
    const d = hexDistance({ q: u.q, r: u.r }, { q: e.q, r: e.r });
    if (d < bestDist) {
      bestDist = d;
      best = { q: e.q, r: e.r };
    }
  }
  // Enemy cities we've ever seen.
  for (const c of state.cities) {
    if (c.ownerId === u.ownerId) continue;
    const t = tileAt(state, c.q, c.r);
    if (!t || (t.seenMask & ownerBit) === 0) continue;
    const d = hexDistance({ q: u.q, r: u.r }, { q: c.q, r: c.r });
    if (d < bestDist) {
      bestDist = d;
      best = { q: c.q, r: c.r };
    }
  }
  return best;
}

function findNearestUnexplored(state: WorldSnapshot, u: UnitInstance, playerId: number): Hex | null {
  const bit = 1 << playerId;
  let best: Hex | null = null;
  let bestDist = Infinity;
  for (const t of state.tiles) {
    if ((t.seenMask & bit) !== 0) continue;
    if (!BIOMES[t.biome].passable) continue;
    const d = hexDistance({ q: u.q, r: u.r }, { q: t.q, r: t.r });
    if (d < bestDist) {
      bestDist = d;
      best = { q: t.q, r: t.r };
    }
  }
  return best;
}

function stepToward(state: WorldSnapshot, u: UnitInstance, target: Hex): Hex | null {
  const reach = computeReach(state, u);
  // Walk the path; stop at the furthest reachable hex toward the target.
  // Greedy: pick reachable neighbour minimizing remaining distance to target.
  let best: { q: number; r: number; cost: number } | null = null;
  for (const [key, cost] of reach.cost) {
    const [q, r] = key.split(",").map(Number);
    if (q === u.q && r === u.r) continue;
    const dist = hexDistance({ q: q!, r: r! }, target);
    if (
      best === null ||
      dist < hexDistance({ q: best.q, r: best.r }, target) ||
      (dist === hexDistance({ q: best.q, r: best.r }, target) && cost < best.cost)
    ) {
      best = { q: q!, r: r!, cost };
    }
  }
  return best ? { q: best.q, r: best.r } : null;
}

