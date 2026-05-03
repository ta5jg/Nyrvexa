/* =============================================================================
 * File:           web/packages/content/src/units.ts
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-05-03
 * Last Update:    2026-05-03
 * Version:        0.1.0
 *
 * Description:
 *   Unit catalog — stats, costs, sight, abilities. Mirrors the M1 archetype
 *   layout from the Unity prototype's UnitArchetypeCatalog.
 *
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

import type { TechId, UnitId } from "@nyrvexa/protocol";

export type UnitDef = {
  id: UnitId;
  name: string;
  hp: number;
  /** Base attack strength. */
  atk: number;
  /** Base defense strength. */
  def: number;
  /** Movement points per turn. */
  moves: number;
  /** Tiles of fog-of-war reveal. */
  sight: number;
  /** Production points to build in a city. */
  cost: number;
  /** Tech required to unlock; null = available from turn 1. */
  requires: TechId | null;
  /** Special role tags. */
  role: "civilian" | "melee" | "ranged" | "scout";
  /** True if can settle a new city (consumes the unit). */
  canSettle?: boolean;
  /** True if can attack other units. */
  canAttack?: boolean;
};

export const UNITS: Record<UnitId, UnitDef> = {
  settler: {
    id: "settler",
    name: "Settler",
    hp: 10,
    atk: 0,
    def: 1,
    moves: 2,
    sight: 2,
    cost: 30,
    requires: null,
    role: "civilian",
    canSettle: true
  },
  warrior: {
    id: "warrior",
    name: "Warrior",
    hp: 20,
    atk: 6,
    def: 5,
    moves: 2,
    sight: 2,
    cost: 20,
    requires: null,
    role: "melee",
    canAttack: true
  },
  scout: {
    id: "scout",
    name: "Scout",
    hp: 12,
    atk: 2,
    def: 2,
    moves: 4,
    sight: 3,
    cost: 15,
    requires: null,
    role: "scout",
    canAttack: true
  },
  worker: {
    id: "worker",
    name: "Worker",
    hp: 10,
    atk: 0,
    def: 1,
    moves: 2,
    sight: 2,
    cost: 25,
    requires: "agriculture",
    role: "civilian"
  },
  archer: {
    id: "archer",
    name: "Archer",
    hp: 16,
    atk: 7,
    def: 3,
    moves: 2,
    sight: 3,
    cost: 25,
    requires: "bronze_working",
    role: "ranged",
    canAttack: true
  },
  horseman: {
    id: "horseman",
    name: "Horseman",
    hp: 24,
    atk: 8,
    def: 4,
    moves: 4,
    sight: 2,
    cost: 35,
    requires: "horseback_riding",
    role: "melee",
    canAttack: true
  }
};

export function getUnit(id: UnitId): UnitDef {
  return UNITS[id];
}
