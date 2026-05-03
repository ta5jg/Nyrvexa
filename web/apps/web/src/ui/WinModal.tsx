/* =============================================================================
 * File:           apps/web/src/ui/WinModal.tsx
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-05-03
 * Last Update:    2026-05-03
 * Version:        0.1.0
 *
 * Description:
 *   Game-over overlay. Shown when the world status is not "playing".
 *
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

import { FACTIONS } from "@nyrvexa/content";
import { scorePlayer } from "@nyrvexa/engine";
import type { WorldSnapshot } from "@nyrvexa/protocol";

export function WinModal({
  state,
  humanPlayerId,
  onAgain
}: {
  state: WorldSnapshot;
  humanPlayerId: number;
  onAgain: () => void;
}) {
  const status = state.status;
  if (status.kind === "playing") return null;
  let headline = "Stalemate";
  let detail = "The world endures, but neither side claims it.";
  let color = "var(--text)";
  if (status.kind === "victory") {
    const winner = state.players.find((p) => p.id === status.winnerId)!;
    const f = FACTIONS[winner.factionId];
    if (winner.id === humanPlayerId) {
      headline = "Victory";
      color = "var(--gold)";
      detail =
        status.reason === "domination"
          ? "Your rivals are no more. The land is yours."
          : "Time ran out, and your civilization stood tallest.";
    } else {
      headline = "Defeat";
      color = "var(--danger)";
      detail = `The ${f.name} outlasted you.`;
    }
  }

  const myScore = scorePlayer(state, humanPlayerId);

  return (
    <div className="modal-bg">
      <div className="modal">
        <h2 style={{ color }}>{headline}</h2>
        <div className="reason">{detail}</div>
        <div style={{ fontSize: 13, color: "var(--text-dim)", marginBottom: 18 }}>
          Final score: <strong style={{ color: "var(--text)" }}>{myScore}</strong>
        </div>
        <button className="btn btn-primary" onClick={onAgain} style={{ width: "100%" }}>
          New age
        </button>
      </div>
    </div>
  );
}
