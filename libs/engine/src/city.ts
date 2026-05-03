/* =============================================================================
 * File:           web/packages/engine/src/city.ts
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-05-03
 * Last Update:    2026-05-03
 * Version:        0.1.0
 *
 * Description:
 *   City lifecycle helpers: yields from worked tiles, growth threshold,
 *   production tick, and the bookkeeping needed by the turn pipeline.
 *
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

import { BIOMES, FACTIONS, UNITS } from "@nyrvexa/content";
import type { CityInstance, UnitId, WorldSnapshot } from "@nyrvexa/protocol";
import { hexesInRange } from "./hex.js";
import { playerById, tileMap } from "./world.js";

export type CityYield = { food: number; production: number; gold: number; science: number };

/** Center yield = the tile under the city. Adjacent tiles (radius 2) are worked. */
export function cityYield(state: WorldSnapshot, city: CityInstance): CityYield {
  const tiles = tileMap(state);
  const player = playerById(state, city.ownerId);
  const faction = player ? FACTIONS[player.factionId] : null;
  let food = 0;
  let production = 0;
  let gold = 0;

  // City center always yields a flat baseline + the tile's gold contribution.
  food += 2;
  production += 1;

  // Work up to `population + 1` neighbouring tiles in radius 2.
  const slots = Math.min(city.population + 1, 6);
  let used = 0;
  const ring: { food: number; production: number; gold: number }[] = [];
  for (const h of hexesInRange({ q: city.q, r: city.r }, 2)) {
    if (h.q === city.q && h.r === city.r) continue;
    const t = tiles.get(`${h.q},${h.r}`);
    if (!t) continue;
    const b = BIOMES[t.biome];
    ring.push({ food: b.food, production: b.production, gold: b.gold });
  }
  // Take the top `slots` tiles by food+production, deterministic by sorted index.
  ring.sort((a, b) => (b.food + b.production) - (a.food + a.production));
  for (const r of ring) {
    if (used >= slots) break;
    food += r.food + (faction?.bonusFoodPerTile ?? 0);
    production += r.production;
    gold += r.gold;
    used++;
  }

  // Science = 25% of city production + 1 per population (libraries are post-v0.1).
  const science = Math.floor(production * 0.25) + city.population;

  return { food, production, gold, science };
}

/** Food required to grow from current population to next. */
export function growthThreshold(population: number): number {
  return 12 + population * 6;
}

/** Apply one turn of yields, growth, and production progress. */
export function tickCity(state: WorldSnapshot, city: CityInstance): { producedUnit: UnitId | null; yields: CityYield } {
  const y = cityYield(state, city);

  // Food → growth, surplus banked.
  city.foodStock += y.food;
  const threshold = growthThreshold(city.population);
  if (city.foodStock >= threshold) {
    city.foodStock -= threshold;
    city.population += 1;
  }

  // Production → current build.
  let producedUnit: UnitId | null = null;
  if (city.producing) {
    city.productionStock += y.production;
    const cost = UNITS[city.producing].cost;
    if (city.productionStock >= cost) {
      city.productionStock = 0;
      producedUnit = city.producing;
      // Keep producing the same thing by default.
    }
  }

  // Slow self-heal.
  if (city.hp < 100) city.hp = Math.min(100, city.hp + 2);

  return { producedUnit, yields: y };
}
