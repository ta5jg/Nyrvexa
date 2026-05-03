/* =============================================================================
 * File:           web/packages/engine/src/world.ts
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-05-03
 * Last Update:    2026-05-03
 * Version:        0.1.0
 *
 * Description:
 *   World-snapshot accessors. The state is a plain JSON-compatible object
 *   (see @nyrvexa/protocol's WorldSnapshot). Mutators are tiny functions
 *   that return the new state — never mutate in place outside this module.
 *
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

import type {
  CityInstance,
  Hex,
  Player,
  Tile,
  UnitInstance,
  WorldSnapshot
} from "@nyrvexa/protocol";
import { hexKey } from "./hex.js";

export function tileAt(state: WorldSnapshot, q: number, r: number): Tile | null {
  // Linear scan is fine at v0.1 sizes (<200 tiles).
  return state.tiles.find((t) => t.q === q && t.r === r) ?? null;
}

export function tileMap(state: WorldSnapshot): Map<string, Tile> {
  const m = new Map<string, Tile>();
  for (const t of state.tiles) m.set(hexKey({ q: t.q, r: t.r }), t);
  return m;
}

export function unitAt(state: WorldSnapshot, q: number, r: number): UnitInstance | null {
  return state.units.find((u) => u.q === q && u.r === r) ?? null;
}

export function unitsOf(state: WorldSnapshot, ownerId: number): UnitInstance[] {
  return state.units.filter((u) => u.ownerId === ownerId);
}

export function cityAt(state: WorldSnapshot, q: number, r: number): CityInstance | null {
  return state.cities.find((c) => c.q === q && c.r === r) ?? null;
}

export function citiesOf(state: WorldSnapshot, ownerId: number): CityInstance[] {
  return state.cities.filter((c) => c.ownerId === ownerId);
}

export function playerById(state: WorldSnapshot, id: number): Player | null {
  return state.players.find((p) => p.id === id) ?? null;
}

export function unitById(state: WorldSnapshot, id: number): UnitInstance | null {
  return state.units.find((u) => u.id === id) ?? null;
}

export function cityById(state: WorldSnapshot, id: number): CityInstance | null {
  return state.cities.find((c) => c.id === id) ?? null;
}

export function isInBounds(state: WorldSnapshot, h: Hex): boolean {
  return tileAt(state, h.q, h.r) !== null;
}
