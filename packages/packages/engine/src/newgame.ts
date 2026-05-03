/* =============================================================================
 * File:           web/packages/engine/src/newgame.ts
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-05-03
 * Last Update:    2026-05-03
 * Version:        0.1.0
 *
 * Description:
 *   Bootstrap a fresh WorldSnapshot from a seed + player list. Generates
 *   the map, places starting settlers + warriors at well-separated
 *   locations, applies faction starting bonuses, and computes initial
 *   fog-of-war.
 *
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

import { FACTIONS, UNITS } from "@nyrvexa/content";
import type { FactionId, Player, UnitInstance, WorldSnapshot } from "@nyrvexa/protocol";
import { recomputeFog } from "./fog.js";
import { findStartTile, generateMap } from "./map.js";

export type NewGameOptions = {
  seed: string;
  width?: number;
  height?: number;
  players: Array<{ isHuman: boolean; factionId: FactionId }>;
};

const DEFAULTS = { width: 16, height: 12 };

export function newGame(opts: NewGameOptions): WorldSnapshot {
  const width = opts.width ?? DEFAULTS.width;
  const height = opts.height ?? DEFAULTS.height;
  const tiles = generateMap({ seed: opts.seed, width, height });

  const players: Player[] = opts.players.map((p, i) => {
    const faction = FACTIONS[p.factionId];
    return {
      id: i,
      isHuman: p.isHuman,
      factionId: p.factionId,
      gold: 50 + faction.bonusGold,
      science: faction.bonusScience,
      researching: null,
      research: {},
      techs: []
    };
  });

  const state: WorldSnapshot = {
    v: 1,
    seed: opts.seed,
    turn: 1,
    width,
    height,
    tiles,
    units: [],
    cities: [],
    players,
    status: { kind: "playing" },
    nextUnitId: 1,
    nextCityId: 1
  };

  // Place a settler + warrior for each player at well-separated start tiles.
  const placed: Array<{ q: number; r: number }> = [];
  const minDist = Math.max(5, Math.floor(Math.min(width, height) / 2));
  for (const player of players) {
    const start = findStartTile(tiles, placed, minDist, `${opts.seed}:start:${player.id}`);
    if (!start) {
      throw new Error(`newGame: could not find a start tile for player ${player.id}`);
    }
    placed.push(start);
    const settlerDef = UNITS.settler;
    const warriorDef = UNITS.warrior;
    const settler: UnitInstance = {
      id: state.nextUnitId++,
      ownerId: player.id,
      type: "settler",
      q: start.q,
      r: start.r,
      hp: settlerDef.hp,
      movesLeft: settlerDef.moves,
      acted: false
    };
    state.units.push(settler);

    // Adjacent warrior to escort the settler.
    const guardCandidates = [
      { q: start.q + 1, r: start.r },
      { q: start.q - 1, r: start.r },
      { q: start.q, r: start.r + 1 },
      { q: start.q, r: start.r - 1 },
      { q: start.q + 1, r: start.r - 1 },
      { q: start.q - 1, r: start.r + 1 }
    ];
    for (const c of guardCandidates) {
      const t = tiles.find((tt) => tt.q === c.q && tt.r === c.r);
      if (!t) continue;
      // Skip impassable
      const passable = t.biome !== "water" && t.biome !== "mountain";
      if (!passable) continue;
      const warrior: UnitInstance = {
        id: state.nextUnitId++,
        ownerId: player.id,
        type: "warrior",
        q: c.q,
        r: c.r,
        hp: warriorDef.hp,
        movesLeft: warriorDef.moves,
        acted: false
      };
      state.units.push(warrior);
      break;
    }
  }

  recomputeFog(state);
  return state;
}
