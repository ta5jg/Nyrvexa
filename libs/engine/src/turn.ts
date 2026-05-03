/* =============================================================================
 * File:           web/packages/engine/src/turn.ts
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-05-03
 * Last Update:    2026-05-03
 * Version:        0.1.0
 *
 * Description:
 *   End-of-turn pipeline. After every player has finished their turn, this
 *   advances the world: city tick, research progress, fog recompute, and
 *   victory check. Player turns themselves are processed via applyCommand
 *   in commands.ts; this module handles only the between-turn bookkeeping.
 *
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

import { FACTIONS, UNITS } from "@nyrvexa/content";
import type { CityInstance, TechId, UnitId, WorldSnapshot } from "@nyrvexa/protocol";
import { spawnUnit } from "./commands.js";
import { tickCity } from "./city.js";
import { recomputeFog } from "./fog.js";
import { tickResearch } from "./research.js";
import { evaluateVictory } from "./score.js";

export type TurnLog = {
  turn: number;
  perPlayer: Array<{
    playerId: number;
    techsFinished: TechId[];
    unitsBuilt: Array<{ cityId: number; type: UnitId }>;
    foodGained: number;
    productionGained: number;
    goldGained: number;
    scienceGained: number;
  }>;
};

export function endTurn(state: WorldSnapshot): TurnLog {
  const log: TurnLog = { turn: state.turn, perPlayer: [] };

  // ---- 1. City production + yields per player ----
  for (const player of state.players) {
    const myCities = state.cities.filter((c) => c.ownerId === player.id);
    let food = 0;
    let production = 0;
    let gold = 0;
    let science = 0;
    const unitsBuilt: Array<{ cityId: number; type: UnitId }> = [];
    for (const c of myCities) {
      const result = tickCity(state, c);
      food += result.yields.food;
      production += result.yields.production;
      gold += result.yields.gold;
      science += result.yields.science;
      if (result.producedUnit) {
        const spawned = spawnUnit(state, c, result.producedUnit);
        if (spawned) unitsBuilt.push({ cityId: c.id, type: result.producedUnit });
      }
    }
    player.gold += gold;
    // Faction science bonus only on turn 1 — handled at newgame setup.
    const techsFinished: TechId[] = [];
    const finished = tickResearch(player, science);
    if (finished) techsFinished.push(finished);
    log.perPlayer.push({
      playerId: player.id,
      techsFinished,
      unitsBuilt,
      foodGained: food,
      productionGained: production,
      goldGained: gold,
      scienceGained: science
    });
  }

  // ---- 2. Reset unit movement + acted flags ----
  for (const u of state.units) {
    const def = UNITS[u.type];
    u.movesLeft = def.moves;
    u.acted = false;
    if (u.hp < def.hp) u.hp = Math.min(def.hp, u.hp + 2); // slow heal
  }

  // ---- 3. Advance turn counter, recompute fog, check victory ----
  state.turn += 1;
  recomputeFog(state);
  state.status = evaluateVictory(state);

  // Suppress unused-var warning.
  void FACTIONS;
  void ((c: CityInstance) => c);

  return log;
}
