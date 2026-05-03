/* =============================================================================
 * File:           web/packages/engine/src/save.ts
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-05-03
 * Last Update:    2026-05-03
 * Version:        0.1.0
 *
 * Description:
 *   World-snapshot serialization. v0.1 uses straight JSON (compact +
 *   debuggable). The Unity prototype's NYRVEXA_V1 plain-text format
 *   remains the design reference — same field set, different encoding.
 *
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

import type { WorldSnapshot } from "@nyrvexa/protocol";

export function serializeWorld(state: WorldSnapshot): string {
  return JSON.stringify(state);
}

export function deserializeWorld(json: string): WorldSnapshot {
  const parsed = JSON.parse(json) as WorldSnapshot;
  if (parsed.v !== 1) {
    throw new Error(`unsupported save version: ${parsed.v}`);
  }
  return parsed;
}

/** Quick deep clone for AI worker handoff (avoids shared-state bugs). */
export function cloneWorld(state: WorldSnapshot): WorldSnapshot {
  return JSON.parse(JSON.stringify(state)) as WorldSnapshot;
}
