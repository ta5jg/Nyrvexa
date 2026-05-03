/* =============================================================================
 * File:           web/packages/protocol/src/index.ts
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-05-03
 * Last Update:    2026-05-03
 * Version:        0.1.0
 *
 * Description:
 *   Wire contracts between Nyrvexa web client and gateway. Versioned with
 *   `v: 1` literal — bump on any breaking change so old clients fail loud
 *   instead of misinterpreting fields.
 *
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

export const PROTOCOL_VERSION = 1 as const;

// ---------------------------------------------------------------------------
// Identifiers
// ---------------------------------------------------------------------------

export const BIOME_IDS = ["plain", "forest", "hill", "mountain", "water", "desert", "tundra"] as const;
export type BiomeId = (typeof BIOME_IDS)[number];

export const UNIT_IDS = ["settler", "warrior", "scout", "worker", "archer", "horseman"] as const;
export type UnitId = (typeof UNIT_IDS)[number];

export const TECH_IDS = [
  "agriculture",
  "bronze_working",
  "the_wheel",
  "writing",
  "masonry",
  "horseback_riding",
  "currency",
  "mathematics"
] as const;
export type TechId = (typeof TECH_IDS)[number];

export const FACTION_IDS = ["aurei", "vesnar", "thalassia", "kyron"] as const;
export type FactionId = (typeof FACTION_IDS)[number];

// ---------------------------------------------------------------------------
// Hex coordinates (axial)
// ---------------------------------------------------------------------------

export type Hex = { q: number; r: number };

// ---------------------------------------------------------------------------
// World snapshot (for save/load + AI worker handoff)
// ---------------------------------------------------------------------------

export type Tile = {
  q: number;
  r: number;
  biome: BiomeId;
  /** Bitmask of player ids who have ever seen this tile (fog-of-war). */
  seenMask: number;
  /** Bitmask of player ids currently seeing this tile. */
  visibleMask: number;
  /** Optional resource bonus on this tile. */
  resource?: ResourceId | null;
};

export const RESOURCE_IDS = ["wheat", "iron", "horse", "gold_ore", "stone"] as const;
export type ResourceId = (typeof RESOURCE_IDS)[number];

export type UnitInstance = {
  id: number;
  ownerId: number;
  type: UnitId;
  q: number;
  r: number;
  hp: number;
  movesLeft: number;
  /** True after a unit acts (settles, attacks, or fortifies) and can't act again this turn. */
  acted: boolean;
};

export type CityInstance = {
  id: number;
  ownerId: number;
  name: string;
  q: number;
  r: number;
  population: number;
  /** Production currently being built; null when nothing queued. */
  producing: UnitId | null;
  /** Production points accumulated toward the current build. */
  productionStock: number;
  /** Food banked toward population growth. */
  foodStock: number;
  hp: number;
};

export type Player = {
  id: number;
  isHuman: boolean;
  factionId: FactionId;
  /** Cumulative resources. */
  gold: number;
  /** Cumulative science. Tech `progress` lives in `research`. */
  science: number;
  /** Tech currently being researched, or null. */
  researching: TechId | null;
  /** Per-tech progress in "science points" terms. */
  research: Partial<Record<TechId, number>>;
  /** Techs already discovered. */
  techs: TechId[];
};

export type GameStatus =
  | { kind: "playing" }
  | { kind: "victory"; winnerId: number; reason: "domination" | "score" }
  | { kind: "draw" };

export type WorldSnapshot = {
  v: 1;
  seed: string;
  turn: number;
  width: number;
  height: number;
  tiles: Tile[];
  units: UnitInstance[];
  cities: CityInstance[];
  players: Player[];
  status: GameStatus;
  nextUnitId: number;
  nextCityId: number;
};

// ---------------------------------------------------------------------------
// Save / load contracts
// ---------------------------------------------------------------------------

export type SaveRequest = {
  v: 1;
  user: string;
  slot: string;
  state: WorldSnapshot;
};

export type SaveResponse =
  | { v: 1; ok: true; updatedAtMs: number }
  | { v: 1; ok: false; error: string };

export type LoadResponse =
  | { v: 1; ok: true; state: WorldSnapshot; updatedAtMs: number }
  | { v: 1; ok: false; error: "NOT_FOUND" | "BAD_REQUEST" | "INTERNAL" };

// ---------------------------------------------------------------------------
// Leaderboard
// ---------------------------------------------------------------------------

export type LeaderboardEntry = {
  rank: number;
  user: string;
  displayName: string | null;
  factionId: FactionId;
  turns: number;
  score: number;
  atMs: number;
};

export type LeaderboardResponse = {
  v: 1;
  ok: true;
  top: LeaderboardEntry[];
  total: number;
};

// ---------------------------------------------------------------------------
// AI worker messages
// ---------------------------------------------------------------------------

export type AiRequest = {
  v: 1;
  state: WorldSnapshot;
  forPlayerId: number;
};

export type AiCommand =
  | { kind: "moveUnit"; unitId: number; toQ: number; toR: number }
  | { kind: "settle"; unitId: number }
  | { kind: "attack"; unitId: number; targetQ: number; targetR: number }
  | { kind: "buildUnit"; cityId: number; unit: UnitId }
  | { kind: "research"; tech: TechId };

export type AiResponse = {
  v: 1;
  forPlayerId: number;
  commands: AiCommand[];
};
