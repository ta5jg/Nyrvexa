/* =============================================================================
 * File:           web/packages/engine/src/fog.ts
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-05-03
 * Last Update:    2026-05-03
 * Version:        0.1.0
 *
 * Description:
 *   Fog-of-war computation. Each tile carries two bitmasks: `seenMask`
 *   (ever seen by player N — keeps showing terrain memory) and
 *   `visibleMask` (currently visible — units update only inside this).
 *   Recomputed at the start of each player's turn.
 *
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

import { UNITS } from "@nyrvexa/content";
import type { WorldSnapshot } from "@nyrvexa/protocol";
import { hexesInRange } from "./hex.js";
import { tileMap } from "./world.js";

/** Recompute visibility for all players. Mutates the tiles in-place. */
export function recomputeFog(state: WorldSnapshot): void {
  // Clear current visibility for everyone.
  for (const t of state.tiles) t.visibleMask = 0;

  const tiles = tileMap(state);

  // Each unit illuminates `sight` radius around itself.
  for (const u of state.units) {
    const def = UNITS[u.type];
    const bit = 1 << u.ownerId;
    for (const h of hexesInRange({ q: u.q, r: u.r }, def.sight)) {
      const t = tiles.get(`${h.q},${h.r}`);
      if (!t) continue;
      t.visibleMask |= bit;
      t.seenMask |= bit;
    }
  }

  // Cities also reveal a small area.
  for (const c of state.cities) {
    const bit = 1 << c.ownerId;
    for (const h of hexesInRange({ q: c.q, r: c.r }, 2)) {
      const t = tiles.get(`${h.q},${h.r}`);
      if (!t) continue;
      t.visibleMask |= bit;
      t.seenMask |= bit;
    }
  }
}

/** True if a player can see a hex right now. */
export function isVisibleTo(state: WorldSnapshot, playerId: number, q: number, r: number): boolean {
  const t = state.tiles.find((tt) => tt.q === q && tt.r === r);
  return !!t && (t.visibleMask & (1 << playerId)) !== 0;
}

/** True if a player has ever seen a hex. */
export function hasSeen(state: WorldSnapshot, playerId: number, q: number, r: number): boolean {
  const t = state.tiles.find((tt) => tt.q === q && tt.r === r);
  return !!t && (t.seenMask & (1 << playerId)) !== 0;
}
