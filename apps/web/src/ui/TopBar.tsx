/* =============================================================================
 * File:           apps/web/src/ui/TopBar.tsx
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-05-03
 * Last Update:    2026-05-03
 * Version:        0.1.0
 *
 * Description:
 *   Always-on top strip — turn counter, gold, science, current research.
 *
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

import { FACTIONS, TECHS } from "@nyrvexa/content";
import type { Player, WorldSnapshot } from "@nyrvexa/protocol";

export function TopBar({
  state,
  humanPlayerId,
  onEndTurn,
  isAiThinking
}: {
  state: WorldSnapshot;
  humanPlayerId: number;
  onEndTurn: () => void;
  isAiThinking: boolean;
}) {
  const player = state.players.find((p) => p.id === humanPlayerId)!;
  const faction = FACTIONS[player.factionId];

  return (
    <div className="topbar">
      <div className="topbar-brand" style={{ color: faction.color }}>
        <span style={{ width: 10, height: 10, background: faction.color, borderRadius: 2 }} />
        NYRVEXA
      </div>

      <div className="topbar-stats">
        <div className="stat">
          <span className="lab">Turn</span>
          <strong>{state.turn} / 60</strong>
        </div>
        <div className="stat">
          <span className="lab">Gold</span>
          <strong style={{ color: "var(--gold)" }}>{player.gold}</strong>
        </div>
        <div className="stat">
          <span className="lab">Cities</span>
          <strong>{state.cities.filter((c) => c.ownerId === humanPlayerId).length}</strong>
        </div>
        <div className="stat">
          <span className="lab">Units</span>
          <strong>{state.units.filter((u) => u.ownerId === humanPlayerId).length}</strong>
        </div>
        <ResearchChip player={player} />
      </div>

      <button className="btn btn-primary" onClick={onEndTurn} disabled={isAiThinking}>
        {isAiThinking ? "Foe thinking…" : "End turn ▸"}
      </button>
    </div>
  );
}

function ResearchChip({ player }: { player: Player }) {
  if (!player.researching) {
    return (
      <div className="stat">
        <span className="lab">Research</span>
        <strong style={{ color: "var(--text-muted)" }}>—</strong>
      </div>
    );
  }
  const tech = TECHS[player.researching];
  const progress = player.research[player.researching] ?? 0;
  return (
    <div className="stat">
      <span className="lab">Researching</span>
      <strong style={{ color: "var(--accent)" }}>
        {tech.name} {Math.floor((progress / tech.cost) * 100)}%
      </strong>
    </div>
  );
}
