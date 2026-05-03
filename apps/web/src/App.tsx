/* =============================================================================
 * File:           apps/web/src/App.tsx
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-05-03
 * Last Update:    2026-05-03
 * Version:        0.1.0
 *
 * Description:
 *   Top-level shell. Owns the WorldSnapshot, applies player commands,
 *   runs the AI turn (synchronously for v0.1; Web Worker offload comes
 *   in v0.2), and renders the canvas + HUD overlays.
 *
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

import { useCallback, useEffect, useMemo, useState } from "react";
import {
  applyCommand,
  cityAt,
  cloneWorld,
  commandAttack,
  commandMove,
  commandSettle,
  computeReach,
  endTurn,
  hexDistance,
  newGame,
  planTurn,
  recomputeFog,
  unitAt,
  unitById,
  cityById
} from "@nyrvexa/engine";
import { UNITS } from "@nyrvexa/content";
import type { FactionId, Hex, UnitId, WorldSnapshot } from "@nyrvexa/protocol";
import { GameCanvas, type SelectionState } from "./game/GameCanvas.js";
import { SetupScreen } from "./ui/SetupScreen.js";
import { TopBar } from "./ui/TopBar.js";
import { SelectionPanel } from "./ui/SelectionPanel.js";
import { WinModal } from "./ui/WinModal.js";
import { clearAutosave, loadAutosave, saveAutosave } from "./lib/profile.js";

const HUMAN_PLAYER_ID = 0;
const AI_PLAYER_ID = 1;

export function App() {
  const [state, setState] = useState<WorldSnapshot | null>(() => loadAutosave());
  const [selUnitId, setSelUnitId] = useState<number | null>(null);
  const [selCityId, setSelCityId] = useState<number | null>(null);
  const [hovered, setHovered] = useState<Hex | null>(null);
  const [thinking, setThinking] = useState(false);
  const [hint, setHint] = useState<string | null>(null);

  // Persist after every state change.
  useEffect(() => {
    if (state) saveAutosave(state);
  }, [state]);

  // Hint auto-dismiss (tutorial hints linger longer than transient errors).
  useEffect(() => {
    if (!hint) return;
    const isTutorial = hint.includes("settler") || hint.includes("End turn");
    const t = window.setTimeout(() => setHint(null), isTutorial ? 5500 : 2400);
    return () => window.clearTimeout(t);
  }, [hint]);

  const startGame = useCallback((opts: { player: FactionId; opponent: FactionId; seed: string }) => {
    const fresh = newGame({
      seed: opts.seed,
      width: 22,
      height: 16,
      players: [
        { isHuman: true, factionId: opts.player },
        { isHuman: false, factionId: opts.opponent }
      ]
    });
    setState(fresh);
    setSelUnitId(null);
    setSelCityId(null);
    setHint("Tap your settler, walk to a fertile tile, then Found city.");
  }, []);

  const startNewMatch = useCallback(() => {
    clearAutosave();
    setState(null);
  }, []);

  const reachableKeys = useMemo<ReadonlySet<string>>(() => {
    if (!state || selUnitId === null) return new Set();
    const unit = unitById(state, selUnitId);
    if (!unit || unit.ownerId !== HUMAN_PLAYER_ID) return new Set();
    if (unit.acted || unit.movesLeft <= 0) return new Set();
    const reach = computeReach(state, unit);
    return new Set(reach.cost.keys());
  }, [state, selUnitId]);

  const onHexClick = useCallback(
    (hex: Hex) => {
      if (!state || state.status.kind !== "playing" || thinking) return;
      const next = cloneWorld(state);
      const unitOnTile = unitAt(next, hex.q, hex.r);
      const cityOnTile = cityAt(next, hex.q, hex.r);

      // 1. If we have a unit selected and tile is reachable → move or attack.
      if (selUnitId !== null) {
        const u = unitById(next, selUnitId);
        if (u && u.ownerId === HUMAN_PLAYER_ID) {
          // Adjacent enemy → attack
          const adjacent = hexDistance({ q: u.q, r: u.r }, hex) === 1;
          const enemyHere =
            (unitOnTile && unitOnTile.ownerId !== HUMAN_PLAYER_ID) ||
            (cityOnTile && cityOnTile.ownerId !== HUMAN_PLAYER_ID);
          if (adjacent && enemyHere) {
            const result = commandAttack(next, u.id, hex.q, hex.r);
            if (result.kind === "rejected") setHint(result.reason);
            recomputeFog(next);
            setState(next);
            return;
          }
          if (reachableKeys.has(`${hex.q},${hex.r}`)) {
            const result = commandMove(next, u.id, hex.q, hex.r);
            if (result.kind === "rejected") setHint(result.reason);
            recomputeFog(next);
            setState(next);
            // Keep unit selected so player sees the new reach.
            return;
          }
        }
      }

      // 2. Otherwise select what's on the tile.
      if (unitOnTile && unitOnTile.ownerId === HUMAN_PLAYER_ID) {
        setSelUnitId(unitOnTile.id);
        setSelCityId(null);
        return;
      }
      if (cityOnTile && cityOnTile.ownerId === HUMAN_PLAYER_ID) {
        setSelCityId(cityOnTile.id);
        setSelUnitId(null);
        return;
      }
      if (unitOnTile) {
        setSelUnitId(unitOnTile.id);
        setSelCityId(null);
        return;
      }
      // Empty tile click — clear selection.
      setSelUnitId(null);
      setSelCityId(null);
    },
    [state, selUnitId, reachableKeys, thinking]
  );

  const onSettle = useCallback(() => {
    if (!state || selUnitId === null) return;
    const next = cloneWorld(state);
    const result = commandSettle(next, selUnitId);
    if (result.kind === "rejected") {
      setHint(result.reason);
      return;
    }
    recomputeFog(next);
    setState(next);
    setSelUnitId(null);
    if (result.kind === "settled") setSelCityId(result.cityId);
  }, [state, selUnitId]);

  const onProduceUnit = useCallback(
    (cityId: number, unit: UnitId) => {
      if (!state) return;
      const next = cloneWorld(state);
      const c = cityById(next, cityId);
      if (!c) return;
      c.producing = unit;
      setState(next);
      setHint(`${UNITS[unit].name} queued at ${c.name}.`);
    },
    [state]
  );

  const onEndTurn = useCallback(() => {
    if (!state || state.status.kind !== "playing" || thinking) return;
    setThinking(true);
    // Yield a frame so the spinner can render.
    setTimeout(() => {
      const next = cloneWorld(state);

      // ---- AI takes its turn first ----
      const aiCmds = planTurn(next, AI_PLAYER_ID);
      for (const cmd of aiCmds) applyCommand(next, cmd, AI_PLAYER_ID);

      // ---- Advance turn (yields, growth, fog, victory) ----
      endTurn(next);

      setState(next);
      setSelUnitId(null);
      setSelCityId(null);
      setThinking(false);
    }, 30);
  }, [state, thinking]);

  if (!state) {
    return <SetupScreen onStart={(o) => startGame({ player: o.player, opponent: o.opponent, seed: o.seed })} />;
  }

  const selUnit = selUnitId !== null ? unitById(state, selUnitId) : null;
  const selCity = selCityId !== null ? cityById(state, selCityId) : null;

  const selection: SelectionState = {
    selectedUnitId: selUnitId,
    selectedCityId: selCityId,
    hoveredHex: hovered,
    reachableKeys
  };

  return (
    <div className="app">
      <TopBar state={state} humanPlayerId={HUMAN_PLAYER_ID} onEndTurn={onEndTurn} isAiThinking={thinking} />
      <GameCanvas
        state={state}
        selection={selection}
        humanPlayerId={HUMAN_PLAYER_ID}
        onHexClick={onHexClick}
        onHexHover={setHovered}
      />
      <SelectionPanel
        state={state}
        selectedUnit={selUnit}
        selectedCity={selCity}
        humanPlayerId={HUMAN_PLAYER_ID}
        onSettle={onSettle}
        onProduceUnit={onProduceUnit}
      />
      {hint && <div className="hint">{hint}</div>}
      <WinModal state={state} humanPlayerId={HUMAN_PLAYER_ID} onAgain={startNewMatch} />
    </div>
  );
}
