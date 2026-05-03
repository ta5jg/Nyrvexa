/* =============================================================================
 * File:           web/packages/content/src/biomes.ts
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-05-03
 * Last Update:    2026-05-03
 * Version:        0.1.0
 *
 * Description:
 *   Biome catalog — terrain stats, yields, movement, visual hints. The
 *   crystalline-cartography palette: each biome is a saturated, ownable
 *   color, not a photographic texture.
 *
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

import type { BiomeId } from "@nyrvexa/protocol";

export type BiomeDef = {
  id: BiomeId;
  name: string;
  /** Movement points spent to enter this tile. Impassable = 999. */
  moveCost: number;
  /** % defense bonus granted to a unit standing on this tile. */
  defensePct: number;
  /** Base food per turn (worked by adjacent city). */
  food: number;
  /** Base production per turn. */
  production: number;
  /** Base gold per turn. */
  gold: number;
  /** Whether a city can be founded here. */
  cityFoundable: boolean;
  /** Whether non-naval units can stand on this tile. */
  passable: boolean;
  /** Visual color — saturated flat palette. */
  color: string;
  /** Slightly darker variant for outline / contrast. */
  outline: string;
};

export const BIOMES: Record<BiomeId, BiomeDef> = {
  plain: {
    id: "plain",
    name: "Plain",
    moveCost: 1,
    defensePct: 0,
    food: 2,
    production: 1,
    gold: 0,
    cityFoundable: true,
    passable: true,
    color: "#a7c46a",
    outline: "#7a9849"
  },
  forest: {
    id: "forest",
    name: "Forest",
    moveCost: 2,
    defensePct: 25,
    food: 1,
    production: 2,
    gold: 0,
    cityFoundable: true,
    passable: true,
    color: "#3f7a4d",
    outline: "#2a5234"
  },
  hill: {
    id: "hill",
    name: "Hill",
    moveCost: 2,
    defensePct: 25,
    food: 1,
    production: 2,
    gold: 0,
    cityFoundable: true,
    passable: true,
    color: "#9a7c4a",
    outline: "#6f5731"
  },
  mountain: {
    id: "mountain",
    name: "Mountain",
    moveCost: 999,
    defensePct: 50,
    food: 0,
    production: 0,
    gold: 0,
    cityFoundable: false,
    passable: false,
    color: "#6e6a76",
    outline: "#4a4651"
  },
  water: {
    id: "water",
    name: "Sea",
    moveCost: 999,
    defensePct: 0,
    food: 1,
    production: 0,
    gold: 1,
    cityFoundable: false,
    passable: false,
    color: "#3c7ab0",
    outline: "#27548a"
  },
  desert: {
    id: "desert",
    name: "Desert",
    moveCost: 1,
    defensePct: 0,
    food: 0,
    production: 0,
    gold: 1,
    cityFoundable: true,
    passable: true,
    color: "#d9b86a",
    outline: "#a3863e"
  },
  tundra: {
    id: "tundra",
    name: "Tundra",
    moveCost: 1,
    defensePct: 10,
    food: 1,
    production: 1,
    gold: 0,
    cityFoundable: true,
    passable: true,
    color: "#bcc7c4",
    outline: "#8c9c98"
  }
};

export function getBiome(id: BiomeId): BiomeDef {
  return BIOMES[id];
}
