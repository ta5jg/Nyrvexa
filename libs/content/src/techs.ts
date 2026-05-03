/* =============================================================================
 * File:           web/packages/content/src/techs.ts
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-05-03
 * Last Update:    2026-05-03
 * Version:        0.1.0
 *
 * Description:
 *   Tech catalog — small linear-ish tree for v0.1. Each tech has a science
 *   cost and a list of prereqs. Effects are looked up elsewhere; the catalog
 *   is just the dependency graph + cost.
 *
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

import type { TechId, UnitId } from "@nyrvexa/protocol";

export type TechDef = {
  id: TechId;
  name: string;
  cost: number;
  prereqs: TechId[];
  unlocksUnits: UnitId[];
  /** Short description shown in the tech tree UI. */
  blurb: string;
};

export const TECHS: Record<TechId, TechDef> = {
  agriculture: {
    id: "agriculture",
    name: "Agriculture",
    cost: 30,
    prereqs: [],
    unlocksUnits: ["worker"],
    blurb: "Workers improve land. Cities feed more citizens."
  },
  bronze_working: {
    id: "bronze_working",
    name: "Bronze Working",
    cost: 50,
    prereqs: ["agriculture"],
    unlocksUnits: ["archer"],
    blurb: "Smelt copper and tin. Archers join your armies."
  },
  the_wheel: {
    id: "the_wheel",
    name: "The Wheel",
    cost: 60,
    prereqs: ["agriculture"],
    unlocksUnits: [],
    blurb: "Roads cut movement cost between your cities."
  },
  horseback_riding: {
    id: "horseback_riding",
    name: "Horseback Riding",
    cost: 80,
    prereqs: ["the_wheel"],
    unlocksUnits: ["horseman"],
    blurb: "Horsemen — fast strikers that punch deep."
  },
  masonry: {
    id: "masonry",
    name: "Masonry",
    cost: 70,
    prereqs: ["bronze_working"],
    unlocksUnits: [],
    blurb: "City walls. Defenders gain a stout bonus."
  },
  writing: {
    id: "writing",
    name: "Writing",
    cost: 90,
    prereqs: ["bronze_working"],
    unlocksUnits: [],
    blurb: "Libraries multiply your science output."
  },
  mathematics: {
    id: "mathematics",
    name: "Mathematics",
    cost: 130,
    prereqs: ["writing", "masonry"],
    unlocksUnits: [],
    blurb: "Catapults and finance. Engineering opens up."
  },
  currency: {
    id: "currency",
    name: "Currency",
    cost: 100,
    prereqs: ["bronze_working"],
    unlocksUnits: [],
    blurb: "Markets — gold compounds in your cities."
  }
};

export function getTech(id: TechId): TechDef {
  return TECHS[id];
}

// (Unit-unlock resolution lives in the engine; see UNITS[].requires lookups
// against player.techs there.)


/** All techs whose prereqs are satisfied by the given known set. */
export function availableTechs(known: TechId[]): TechId[] {
  const set = new Set(known);
  return (Object.keys(TECHS) as TechId[]).filter((id) => {
    if (set.has(id)) return false;
    return TECHS[id].prereqs.every((p) => set.has(p));
  });
}
