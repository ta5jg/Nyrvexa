/* =============================================================================
 * File:           web/packages/engine/src/index.ts
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-05-03
 * Last Update:    2026-05-03
 * Version:        0.1.0
 *
 * Description:
 *   Public surface of @nyrvexa/engine — re-exports the deterministic 4X
 *   simulation (RNG, hex math, map gen, world helpers, fog, movement,
 *   combat, city, research, score, commands, turns, AI, save/load).
 *
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

export * from "./rng.js";
export * from "./hex.js";
export * from "./world.js";
export * from "./map.js";
export * from "./fog.js";
export * from "./movement.js";
export * from "./combat.js";
export * from "./city.js";
export * from "./research.js";
export * from "./score.js";
export * from "./commands.js";
export * from "./turn.js";
export * from "./newgame.js";
export * from "./ai.js";
export * from "./save.js";
