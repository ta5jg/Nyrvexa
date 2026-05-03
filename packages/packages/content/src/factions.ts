/* =============================================================================
 * File:           web/packages/content/src/factions.ts
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-05-03
 * Last Update:    2026-05-03
 * Version:        0.1.0
 *
 * Description:
 *   Faction catalog — identity, color, starting bonus. v0.1 keeps each
 *   faction asymmetric but readable; deeper synergies arrive in v0.2.
 *
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

import type { FactionId } from "@nyrvexa/protocol";

export type FactionDef = {
  id: FactionId;
  name: string;
  /** Short tagline displayed in the picker. */
  tagline: string;
  /** Banner color — used for unit highlight + city marker rim. */
  color: string;
  /** Subtle accent color for HUD sections. */
  accent: string;
  /** Starting gold above baseline. */
  bonusGold: number;
  /** Starting science above baseline. */
  bonusScience: number;
  /** Bonus food yield per worked tile. */
  bonusFoodPerTile: number;
  /** % combat strength bonus when attacking. */
  bonusAtkPct: number;
  /** % combat strength bonus when defending. */
  bonusDefPct: number;
};

export const FACTIONS: Record<FactionId, FactionDef> = {
  aurei: {
    id: "aurei",
    name: "Aurei",
    tagline: "Sun-forged caravans of the inland plains.",
    color: "#ff5b3b",
    accent: "#ffce4d",
    bonusGold: 30,
    bonusScience: 0,
    bonusFoodPerTile: 0,
    bonusAtkPct: 5,
    bonusDefPct: 0
  },
  vesnar: {
    id: "vesnar",
    name: "Vesnar",
    tagline: "Forest-keepers, slow to anger, deeper than they look.",
    color: "#3fb583",
    accent: "#a7c46a",
    bonusGold: 0,
    bonusScience: 0,
    bonusFoodPerTile: 1,
    bonusAtkPct: 0,
    bonusDefPct: 10
  },
  thalassia: {
    id: "thalassia",
    name: "Thalassia",
    tagline: "Coastal scribes who turn distance into currency.",
    color: "#00d2c0",
    accent: "#4ce9d9",
    bonusGold: 0,
    bonusScience: 20,
    bonusFoodPerTile: 0,
    bonusAtkPct: 0,
    bonusDefPct: 0
  },
  kyron: {
    id: "kyron",
    name: "Kyron",
    tagline: "Hill-riders. First to strike, last to unmount.",
    color: "#a778ff",
    accent: "#c8a5ff",
    bonusGold: 0,
    bonusScience: 0,
    bonusFoodPerTile: 0,
    bonusAtkPct: 10,
    bonusDefPct: 0
  }
};

export function getFaction(id: FactionId): FactionDef {
  return FACTIONS[id];
}

export const FACTION_LIST: readonly FactionId[] = Object.keys(FACTIONS) as FactionId[];
