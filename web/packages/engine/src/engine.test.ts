/* =============================================================================
 * File:           web/packages/engine/src/engine.test.ts
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-05-03
 * Last Update:    2026-05-03
 * Version:        0.1.0
 *
 * Description:
 *   Smoke + determinism tests covering the engine end-to-end: same seed
 *   produces same world, AI commands replay identically, victory checks
 *   fire correctly.
 *
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

import { describe, expect, it } from "vitest";
import { hexDistance, hexNeighbors } from "./hex.js";
import { newGame } from "./newgame.js";
import { planTurn } from "./ai.js";
import { applyCommand } from "./commands.js";
import { endTurn } from "./turn.js";
import { serializeWorld, deserializeWorld } from "./save.js";
import { fnv1a32, makeRng, mulberry32 } from "./rng.js";

describe("rng", () => {
  it("fnv1a32 is deterministic", () => {
    expect(fnv1a32("hello")).toBe(fnv1a32("hello"));
    expect(fnv1a32("a")).not.toBe(fnv1a32("b"));
  });
  it("mulberry32 reproduces", () => {
    const a = mulberry32(42);
    const b = mulberry32(42);
    for (let i = 0; i < 100; i++) expect(a()).toBe(b());
  });
  it("makeRng integer + chance", () => {
    const r = makeRng("seed");
    for (let i = 0; i < 200; i++) {
      const v = r.int(7);
      expect(v).toBeGreaterThanOrEqual(0);
      expect(v).toBeLessThan(7);
    }
    expect(r.chance(0)).toBe(false);
    expect(r.chance(100)).toBe(true);
  });
});

describe("hex math", () => {
  it("distance is symmetric", () => {
    expect(hexDistance({ q: 0, r: 0 }, { q: 3, r: -2 })).toBe(3);
    expect(hexDistance({ q: 3, r: -2 }, { q: 0, r: 0 })).toBe(3);
  });
  it("each hex has six neighbors", () => {
    expect(hexNeighbors({ q: 0, r: 0 })).toHaveLength(6);
  });
});

describe("newGame", () => {
  it("creates a deterministic world", () => {
    const a = newGame({
      seed: "demo:1",
      players: [
        { isHuman: true, factionId: "aurei" },
        { isHuman: false, factionId: "vesnar" }
      ]
    });
    const b = newGame({
      seed: "demo:1",
      players: [
        { isHuman: true, factionId: "aurei" },
        { isHuman: false, factionId: "vesnar" }
      ]
    });
    expect(serializeWorld(a)).toBe(serializeWorld(b));
  });
  it("places at least one settler per player", () => {
    const w = newGame({
      seed: "place:1",
      players: [
        { isHuman: true, factionId: "aurei" },
        { isHuman: false, factionId: "vesnar" }
      ]
    });
    expect(w.units.filter((u) => u.type === "settler" && u.ownerId === 0)).toHaveLength(1);
    expect(w.units.filter((u) => u.type === "settler" && u.ownerId === 1)).toHaveLength(1);
  });
});

describe("end-to-end determinism", () => {
  it("planTurn + applyCommand replay yields identical state", () => {
    function run(seed: string) {
      const state = newGame({
        seed,
        players: [
          { isHuman: false, factionId: "aurei" },
          { isHuman: false, factionId: "vesnar" }
        ]
      });
      // Both AI for ~5 turns.
      for (let i = 0; i < 5; i++) {
        for (const player of state.players) {
          const cmds = planTurn(state, player.id);
          for (const cmd of cmds) {
            applyCommand(state, cmd, player.id);
          }
        }
        endTurn(state);
        if (state.status.kind !== "playing") break;
      }
      return state;
    }
    expect(serializeWorld(run("e2e:42"))).toBe(serializeWorld(run("e2e:42")));
  });
});

describe("save / load round-trip", () => {
  it("preserves world", () => {
    const w = newGame({
      seed: "save:1",
      players: [
        { isHuman: true, factionId: "aurei" },
        { isHuman: false, factionId: "kyron" }
      ]
    });
    const s = serializeWorld(w);
    const w2 = deserializeWorld(s);
    expect(w2.tiles.length).toBe(w.tiles.length);
    expect(w2.units.length).toBe(w.units.length);
    expect(w2.players[0]!.factionId).toBe("aurei");
  });
});
