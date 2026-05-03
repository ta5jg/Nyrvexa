/* =============================================================================
 * File:           web/packages/engine/src/research.ts
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-05-03
 * Last Update:    2026-05-03
 * Version:        0.1.0
 *
 * Description:
 *   Tech research progression. Each turn a player accumulates science
 *   points from cities + faction bonus; once `cost` is reached, the tech
 *   is added to the player's known set and a follow-on tech is auto-
 *   selected from `availableTechs` so research never stalls silently.
 *
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

import { TECHS, availableTechs } from "@nyrvexa/content";
import type { Player, TechId } from "@nyrvexa/protocol";

export function pickNextTech(player: Player): TechId | null {
  const avail = availableTechs(player.techs);
  if (avail.length === 0) return null;
  // Cheapest first so the AI/human always sees steady progress.
  avail.sort((a, b) => TECHS[a].cost - TECHS[b].cost);
  return avail[0]!;
}

/** Accumulate science into the current research; returns the tech finished this turn (if any). */
export function tickResearch(player: Player, sciencePoints: number): TechId | null {
  if (sciencePoints <= 0) return null;
  if (!player.researching) {
    player.researching = pickNextTech(player);
    if (!player.researching) return null;
  }
  const tech = player.researching;
  const current = player.research[tech] ?? 0;
  const next = current + sciencePoints;
  player.research[tech] = next;
  if (next >= TECHS[tech].cost) {
    player.techs.push(tech);
    delete player.research[tech];
    player.researching = pickNextTech(player);
    return tech;
  }
  return null;
}
