/* =============================================================================
 * File:           web/packages/engine/src/score.ts
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-05-03
 * Last Update:    2026-05-03
 * Version:        0.1.0
 *
 * Description:
 *   Victory + score. v0.1 supports two endings:
 *     - Domination: only one player has cities left.
 *     - Score (turn limit): aggregate cities, units, techs, gold, pop.
 *   Ties break by score; equal scores end as draws.
 *
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

import type { GameStatus, WorldSnapshot } from "@nyrvexa/protocol";
import { citiesOf, unitsOf } from "./world.js";

export const TURN_LIMIT = 60;

export function scorePlayer(state: WorldSnapshot, playerId: number): number {
  const cities = citiesOf(state, playerId);
  const units = unitsOf(state, playerId);
  const player = state.players.find((p) => p.id === playerId);
  if (!player) return 0;
  let s = 0;
  for (const c of cities) s += 50 + c.population * 12;
  s += units.length * 6;
  s += player.techs.length * 25;
  s += Math.floor(player.gold / 4);
  return s;
}

export function evaluateVictory(state: WorldSnapshot): GameStatus {
  // Domination: zero out players with no cities + no settlers.
  const alive = state.players.filter((p) => {
    const cities = citiesOf(state, p.id).length;
    const hasSettler = unitsOf(state, p.id).some((u) => u.type === "settler");
    return cities > 0 || hasSettler;
  });
  if (alive.length === 1 && state.players.length > 1) {
    return { kind: "victory", winnerId: alive[0]!.id, reason: "domination" };
  }
  if (alive.length === 0 && state.players.length > 0) {
    return { kind: "draw" };
  }
  // Turn-limit score victory.
  if (state.turn >= TURN_LIMIT) {
    let bestId: number | null = null;
    let bestScore = -Infinity;
    let tied = false;
    for (const p of state.players) {
      const s = scorePlayer(state, p.id);
      if (s > bestScore) {
        bestScore = s;
        bestId = p.id;
        tied = false;
      } else if (s === bestScore) {
        tied = true;
      }
    }
    if (tied || bestId === null) return { kind: "draw" };
    return { kind: "victory", winnerId: bestId, reason: "score" };
  }
  return { kind: "playing" };
}
