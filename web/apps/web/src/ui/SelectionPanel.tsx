/* =============================================================================
 * File:           apps/web/src/ui/SelectionPanel.tsx
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-05-03
 * Last Update:    2026-05-03
 * Version:        0.1.0
 *
 * Description:
 *   Bottom-anchored panel that switches its content based on what is
 *   currently selected: a unit, a friendly city, or nothing.
 *
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

import { UNITS } from "@nyrvexa/content";
import type { CityInstance, UnitId, UnitInstance, WorldSnapshot } from "@nyrvexa/protocol";

export function SelectionPanel({
  state,
  selectedUnit,
  selectedCity,
  humanPlayerId,
  onSettle,
  onProduceUnit
}: {
  state: WorldSnapshot;
  selectedUnit: UnitInstance | null;
  selectedCity: CityInstance | null;
  humanPlayerId: number;
  onSettle: () => void;
  onProduceUnit: (cityId: number, unit: UnitId) => void;
}) {
  if (selectedUnit) {
    const def = UNITS[selectedUnit.type];
    const ownsIt = selectedUnit.ownerId === humanPlayerId;
    return (
      <div className="panel">
        <h4>
          {def.name}{" "}
          <span className="sub">{ownsIt ? "(yours)" : "(foe)"}</span>
        </h4>
        <div className="sub">{selectedUnit.acted ? "acted this turn" : `${selectedUnit.movesLeft} moves left`}</div>
        <div className="stats">
          <span className="chip hp">HP {selectedUnit.hp}/{def.hp}</span>
          {def.atk > 0 && <span className="chip atk">ATK {def.atk}</span>}
          <span className="chip def">DEF {def.def}</span>
          <span className="chip move">SPD {def.moves}</span>
        </div>
        {ownsIt && (
          <div className="panel-actions">
            {def.canSettle && !selectedUnit.acted && (
              <button className="btn btn-primary" onClick={onSettle}>
                Found city
              </button>
            )}
          </div>
        )}
      </div>
    );
  }

  if (selectedCity && selectedCity.ownerId === humanPlayerId) {
    return (
      <div className="panel">
        <h4>{selectedCity.name}</h4>
        <div className="sub">
          Population {selectedCity.population} · Building{" "}
          {selectedCity.producing ? UNITS[selectedCity.producing].name : "—"}
        </div>
        <div className="panel-actions">
          {(["settler", "warrior", "scout"] as UnitId[]).map((u) => (
            <button
              key={u}
              className="btn"
              onClick={() => onProduceUnit(selectedCity.id, u)}
              style={{
                borderColor: selectedCity.producing === u ? "var(--accent)" : undefined,
                color: selectedCity.producing === u ? "var(--accent)" : undefined
              }}
            >
              Build {UNITS[u].name}
            </button>
          ))}
        </div>
      </div>
    );
  }

  // Default: hint about what to do next.
  const myUnits = state.units.filter((u) => u.ownerId === humanPlayerId);
  const idleSettler = myUnits.find((u) => u.type === "settler" && !u.acted);
  return (
    <div className="panel">
      <h4>Your move</h4>
      <div className="sub">
        {idleSettler
          ? "Tap your settler, walk to a fertile tile, then Found city."
          : "Tap a unit to give it orders. End turn when you're satisfied."}
      </div>
    </div>
  );
}
