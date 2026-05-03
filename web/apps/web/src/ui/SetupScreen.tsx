/* =============================================================================
 * File:           apps/web/src/ui/SetupScreen.tsx
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-05-03
 * Last Update:    2026-05-03
 * Version:        0.1.0
 *
 * Description:
 *   New-game setup — pick faction, opponent faction, optional display name.
 *
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

import { useState } from "react";
import { FACTIONS, FACTION_LIST } from "@nyrvexa/content";
import type { FactionId } from "@nyrvexa/protocol";
import { getDisplayName, setDisplayName } from "../lib/profile.js";

export function SetupScreen({
  onStart
}: {
  onStart: (opts: { player: FactionId; opponent: FactionId; name: string; seed: string }) => void;
}) {
  const [player, setPlayer] = useState<FactionId>("aurei");
  const [opponent, setOpponent] = useState<FactionId>("vesnar");
  const [name, setName] = useState(() => getDisplayName());
  const seed = `nyrvexa:${new Date().toISOString().slice(0, 10)}:${Math.random().toString(16).slice(2, 8)}`;

  function start() {
    setDisplayName(name);
    onStart({ player, opponent, name, seed });
  }

  return (
    <div className="setup">
      <div className="setup-card">
        <h1 className="setup-title">Begin a new age</h1>
        <p className="setup-tag">Choose your faction. Your rival is the AI; the world is procedural.</p>

        <div className="faction-grid">
          {FACTION_LIST.map((id) => {
            const f = FACTIONS[id];
            return (
              <button
                key={id}
                type="button"
                className={`faction-card ${player === id ? "selected" : ""}`}
                onClick={() => {
                  setPlayer(id);
                  if (opponent === id) {
                    const next = FACTION_LIST.find((x) => x !== id) ?? id;
                    setOpponent(next);
                  }
                }}
                aria-pressed={player === id}
              >
                <div className="name">
                  <span className="swatch" style={{ background: f.color }} />
                  {f.name}
                </div>
                <div className="tag">{f.tagline}</div>
              </button>
            );
          })}
        </div>

        <div className="setup-row">
          <label htmlFor="opp">Opponent</label>
          <select
            id="opp"
            value={opponent}
            onChange={(e) => setOpponent(e.target.value as FactionId)}
            style={{
              flex: 2,
              padding: 10,
              background: "var(--bg)",
              border: "1px solid var(--line-strong)",
              borderRadius: 8,
              color: "var(--text)",
              fontSize: 14
            }}
          >
            {FACTION_LIST.filter((id) => id !== player).map((id) => (
              <option key={id} value={id}>
                {FACTIONS[id].name} — {FACTIONS[id].tagline}
              </option>
            ))}
          </select>
        </div>

        <div className="setup-row">
          <label htmlFor="nm">Display name</label>
          <input
            id="nm"
            type="text"
            placeholder="Anonymous strategist"
            value={name}
            maxLength={24}
            onChange={(e) => setName(e.target.value)}
          />
        </div>

        <button type="button" className="btn btn-primary" style={{ width: "100%" }} onClick={start}>
          Found your first city
        </button>

        <p style={{ color: "var(--text-muted)", fontSize: 11, marginTop: 14 }}>
          Drag the map to pan. Pinch or scroll to zoom. Tap a unit, then a destination to move.
        </p>
      </div>
    </div>
  );
}
