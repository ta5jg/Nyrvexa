/* =============================================================================
 * File:           web/packages/engine/src/commands.ts
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-05-03
 * Last Update:    2026-05-03
 * Version:        0.1.0
 *
 * Description:
 *   Apply commands (from human input or AI) to a world snapshot. Each
 *   command function mutates the state in place and returns a tag for
 *   logging/UI feedback.
 *
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

import { BIOMES, UNITS } from "@nyrvexa/content";
import type { AiCommand, CityInstance, UnitId, UnitInstance, WorldSnapshot } from "@nyrvexa/protocol";
import { canReach, computeReach } from "./movement.js";
import { resolveAttack, resolveCityAttack } from "./combat.js";
import { cityAt, cityById, tileAt, unitAt, unitById } from "./world.js";

export type CommandLogEntry =
  | { kind: "moved"; unitId: number; toQ: number; toR: number; cost: number }
  | { kind: "settled"; unitId: number; cityId: number }
  | { kind: "combat"; attackerId: number; defenderId: number; attackerHp: number; defenderHp: number; attackerKilled: boolean; defenderKilled: boolean }
  | { kind: "cityAttack"; attackerId: number; cityId: number; cityHp: number; cityCaptured: boolean; attackerKilled: boolean }
  | { kind: "buildQueued"; cityId: number; unit: UnitId }
  | { kind: "researchSet"; tech: string }
  | { kind: "rejected"; reason: string };

let _newId = 1000; // placeholder; engine uses state.nextUnitId instead

function nextUnitId(state: WorldSnapshot): number {
  const id = state.nextUnitId++;
  return id;
}

function nextCityId(state: WorldSnapshot): number {
  const id = state.nextCityId++;
  return id;
}

function newCity(state: WorldSnapshot, ownerId: number, q: number, r: number, name: string): CityInstance {
  return {
    id: nextCityId(state),
    ownerId,
    name,
    q,
    r,
    population: 1,
    producing: "warrior",
    productionStock: 0,
    foodStock: 0,
    hp: 100
  };
}

const CITY_NAMES: Record<string, readonly string[]> = {
  aurei: ["Solanto", "Veridor", "Aevora", "Sumarad"],
  vesnar: ["Vinaroth", "Greenmere", "Oakhold", "Sylvane"],
  thalassia: ["Tidemark", "Ostrellis", "Pellaron", "Marindra"],
  kyron: ["Hethrar", "Korova", "Kalmoth", "Ydrun"]
};

function pickCityName(state: WorldSnapshot, ownerId: number): string {
  const player = state.players.find((p) => p.id === ownerId);
  const pool = (player ? CITY_NAMES[player.factionId] : null) ?? ["Capital"];
  const taken = new Set(state.cities.map((c) => c.name));
  for (const n of pool) if (!taken.has(n)) return n;
  return `${pool[0]} ${state.cities.length + 1}`;
}

export function applyCommand(
  state: WorldSnapshot,
  cmd: AiCommand,
  forPlayerId: number
): CommandLogEntry {
  switch (cmd.kind) {
    case "moveUnit":
      return commandMove(state, cmd.unitId, cmd.toQ, cmd.toR);
    case "settle":
      return commandSettle(state, cmd.unitId);
    case "attack":
      return commandAttack(state, cmd.unitId, cmd.targetQ, cmd.targetR);
    case "buildUnit":
      return commandBuild(state, cmd.cityId, cmd.unit);
    case "research": {
      const player = state.players.find((p) => p.id === forPlayerId);
      if (!player) return { kind: "rejected", reason: "no such player" };
      player.researching = cmd.tech;
      return { kind: "researchSet", tech: cmd.tech };
    }
  }
}

export function commandMove(state: WorldSnapshot, unitId: number, toQ: number, toR: number): CommandLogEntry {
  const u = unitById(state, unitId);
  if (!u) return { kind: "rejected", reason: "no such unit" };
  if (u.acted) return { kind: "rejected", reason: "unit already acted" };
  if (u.movesLeft <= 0) return { kind: "rejected", reason: "no moves left" };
  const tile = tileAt(state, toQ, toR);
  if (!tile) return { kind: "rejected", reason: "out of bounds" };
  if (!BIOMES[tile.biome].passable) return { kind: "rejected", reason: "impassable" };
  const reach = computeReach(state, u);
  if (!canReach(reach, toQ, toR)) return { kind: "rejected", reason: "out of range" };
  const cost = reach.cost.get(`${toQ},${toR}`)!;
  // Friendly unit on destination?
  const blocker = unitAt(state, toQ, toR);
  if (blocker && blocker.id !== u.id) {
    return { kind: "rejected", reason: "tile occupied" };
  }
  u.q = toQ;
  u.r = toR;
  u.movesLeft = Math.max(0, u.movesLeft - cost);
  return { kind: "moved", unitId, toQ, toR, cost };
}

export function commandSettle(state: WorldSnapshot, unitId: number): CommandLogEntry {
  const u = unitById(state, unitId);
  if (!u) return { kind: "rejected", reason: "no such unit" };
  if (UNITS[u.type].canSettle !== true) return { kind: "rejected", reason: "not a settler" };
  if (u.acted) return { kind: "rejected", reason: "already acted" };
  const tile = tileAt(state, u.q, u.r);
  if (!tile || !BIOMES[tile.biome].cityFoundable) {
    return { kind: "rejected", reason: "cannot found city here" };
  }
  if (cityAt(state, u.q, u.r)) return { kind: "rejected", reason: "city already exists" };
  // Cities must be at least 3 tiles apart (axial Chebyshev).
  for (const c of state.cities) {
    const dx = c.q - u.q;
    const dy = c.r - u.r;
    const dz = -dx - dy;
    if (Math.max(Math.abs(dx), Math.abs(dy), Math.abs(dz)) < 3) {
      return { kind: "rejected", reason: "too close to another city" };
    }
  }
  const city = newCity(state, u.ownerId, u.q, u.r, pickCityName(state, u.ownerId));
  state.cities.push(city);
  // Settler is consumed.
  state.units = state.units.filter((x) => x.id !== unitId);
  return { kind: "settled", unitId, cityId: city.id };
}

export function commandAttack(state: WorldSnapshot, unitId: number, targetQ: number, targetR: number): CommandLogEntry {
  const u = unitById(state, unitId);
  if (!u) return { kind: "rejected", reason: "no such unit" };
  if (UNITS[u.type].canAttack !== true) return { kind: "rejected", reason: "non-combat unit" };
  if (u.acted) return { kind: "rejected", reason: "already acted" };
  // Must be adjacent.
  const dx = targetQ - u.q;
  const dy = targetR - u.r;
  const dz = -dx - dy;
  if (Math.max(Math.abs(dx), Math.abs(dy), Math.abs(dz)) !== 1) {
    return { kind: "rejected", reason: "not adjacent" };
  }
  const targetUnit = unitAt(state, targetQ, targetR);
  const targetCity = cityAt(state, targetQ, targetR);

  if (targetUnit && targetUnit.ownerId !== u.ownerId) {
    const result = resolveAttack(state, u.id, targetUnit.id, "attack");
    u.hp = result.attackerHpAfter;
    targetUnit.hp = result.defenderHpAfter;
    if (result.attackerKilled) state.units = state.units.filter((x) => x.id !== u.id);
    if (result.defenderKilled) state.units = state.units.filter((x) => x.id !== targetUnit.id);
    if (!result.attackerKilled) {
      u.acted = true;
      u.movesLeft = 0;
      // Move into the now-empty tile if we killed.
      if (result.defenderKilled) {
        u.q = targetQ;
        u.r = targetR;
      }
    }
    return {
      kind: "combat",
      attackerId: unitId,
      defenderId: targetUnit.id,
      attackerHp: result.attackerHpAfter,
      defenderHp: result.defenderHpAfter,
      attackerKilled: result.attackerKilled,
      defenderKilled: result.defenderKilled
    };
  }

  if (targetCity && targetCity.ownerId !== u.ownerId) {
    const result = resolveCityAttack(state, u.id, targetCity, "cityAttack");
    u.hp = result.attackerHpAfter;
    targetCity.hp = result.cityHpAfter;
    if (result.attackerKilled) state.units = state.units.filter((x) => x.id !== u.id);
    if (result.cityCaptured) {
      // Transfer ownership; reset HP and pop loss.
      targetCity.ownerId = u.ownerId;
      targetCity.hp = 50;
      targetCity.population = Math.max(1, targetCity.population - 1);
      targetCity.producing = "warrior";
      targetCity.productionStock = 0;
      // Move the attacker into the captured city if alive.
      if (!result.attackerKilled) {
        u.q = targetCity.q;
        u.r = targetCity.r;
        u.acted = true;
        u.movesLeft = 0;
      }
    } else if (!result.attackerKilled) {
      u.acted = true;
      u.movesLeft = 0;
    }
    return {
      kind: "cityAttack",
      attackerId: unitId,
      cityId: targetCity.id,
      cityHp: result.cityHpAfter,
      cityCaptured: result.cityCaptured,
      attackerKilled: result.attackerKilled
    };
  }

  return { kind: "rejected", reason: "nothing to attack" };
}

export function commandBuild(state: WorldSnapshot, cityId: number, unit: UnitId): CommandLogEntry {
  const c = cityById(state, cityId);
  if (!c) return { kind: "rejected", reason: "no such city" };
  c.producing = unit;
  return { kind: "buildQueued", cityId, unit };
}

/** Spawn a freshly-built unit on the city's tile (or an empty neighbour). */
export function spawnUnit(state: WorldSnapshot, city: CityInstance, type: UnitId): UnitInstance | null {
  const def = UNITS[type];
  const candidates: Array<{ q: number; r: number }> = [
    { q: city.q, r: city.r },
    { q: city.q + 1, r: city.r },
    { q: city.q - 1, r: city.r },
    { q: city.q, r: city.r + 1 },
    { q: city.q, r: city.r - 1 },
    { q: city.q + 1, r: city.r - 1 },
    { q: city.q - 1, r: city.r + 1 }
  ];
  for (const c of candidates) {
    const tile = tileAt(state, c.q, c.r);
    if (!tile) continue;
    if (!BIOMES[tile.biome].passable) continue;
    if (unitAt(state, c.q, c.r)) continue;
    const unit: UnitInstance = {
      id: nextUnitId(state),
      ownerId: city.ownerId,
      type,
      q: c.q,
      r: c.r,
      hp: def.hp,
      movesLeft: def.moves,
      acted: false
    };
    state.units.push(unit);
    return unit;
  }
  return null;
}

// Touch the unused warning so the linter stays quiet about _newId.
void _newId;
