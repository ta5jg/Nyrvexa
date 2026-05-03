/* =============================================================================
 * File:           web/packages/engine/src/combat.ts
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-05-03
 * Last Update:    2026-05-03
 * Version:        0.1.0
 *
 * Description:
 *   M1-style melee combat. Resolves a single attack: attacker and defender
 *   each roll an effective strength; the loser takes proportional HP loss,
 *   the winner takes a smaller hit. Mirrors the deterministic shape of the
 *   Unity prototype's MoveUnitCommand combat path.
 *
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

import { BIOMES, FACTIONS, UNITS } from "@nyrvexa/content";
import type { CityInstance, UnitInstance, WorldSnapshot } from "@nyrvexa/protocol";
import { makeRng } from "./rng.js";
import { tileAt } from "./world.js";

export type CombatResult = {
  attackerHpAfter: number;
  defenderHpAfter: number;
  attackerKilled: boolean;
  defenderKilled: boolean;
  /** Damage dealt to attacker. */
  attackerDamage: number;
  /** Damage dealt to defender. */
  defenderDamage: number;
  attackerStrength: number;
  defenderStrength: number;
};

export function effectiveStrength(
  state: WorldSnapshot,
  unit: UnitInstance,
  isAttacking: boolean
): number {
  const def = UNITS[unit.type];
  const base = isAttacking ? def.atk : def.def;
  if (base <= 0) return 0;
  const tile = tileAt(state, unit.q, unit.r);
  const biome = tile ? BIOMES[tile.biome] : null;
  const player = state.players.find((p) => p.id === unit.ownerId);
  const faction = player ? FACTIONS[player.factionId] : null;

  let strength = base;
  if (biome) strength += (base * biome.defensePct) / 100;
  if (faction) {
    const pct = isAttacking ? faction.bonusAtkPct : faction.bonusDefPct;
    strength += (base * pct) / 100;
  }
  // HP scaling: a wounded unit hits softer.
  strength *= unit.hp / def.hp;
  return Math.max(0.1, strength);
}

export function resolveAttack(
  state: WorldSnapshot,
  attackerId: number,
  defenderId: number,
  rngSalt: string
): CombatResult {
  const attacker = state.units.find((u) => u.id === attackerId);
  const defender = state.units.find((u) => u.id === defenderId);
  if (!attacker || !defender) {
    throw new Error("resolveAttack: missing combatants");
  }

  const aStr = effectiveStrength(state, attacker, true);
  const dStr = effectiveStrength(state, defender, false);
  const rng = makeRng(`${state.seed}:turn:${state.turn}:combat:${attackerId}:${defenderId}:${rngSalt}`);

  const aRoll = aStr * (0.85 + rng.next() * 0.3);
  const dRoll = dStr * (0.85 + rng.next() * 0.3);

  // Damage distribution: weaker side eats most.
  const total = aRoll + dRoll;
  const aDmg = Math.max(1, Math.round(8 * (dRoll / total)));
  const dDmg = Math.max(1, Math.round(8 * (aRoll / total)));

  const attackerHpAfter = Math.max(0, attacker.hp - aDmg);
  const defenderHpAfter = Math.max(0, defender.hp - dDmg);

  return {
    attackerHpAfter,
    defenderHpAfter,
    attackerKilled: attackerHpAfter === 0,
    defenderKilled: defenderHpAfter === 0,
    attackerDamage: aDmg,
    defenderDamage: dDmg,
    attackerStrength: aStr,
    defenderStrength: dStr
  };
}

/** A unit attacking an enemy city — same shape, but defender stat is the city's hp / 2. */
export function resolveCityAttack(
  state: WorldSnapshot,
  attackerId: number,
  city: CityInstance,
  rngSalt: string
): { attackerHpAfter: number; cityHpAfter: number; attackerKilled: boolean; cityCaptured: boolean } {
  const attacker = state.units.find((u) => u.id === attackerId);
  if (!attacker) throw new Error("resolveCityAttack: missing attacker");
  const aStr = effectiveStrength(state, attacker, true);
  const dStr = Math.max(2, city.hp * 0.4 + city.population * 1.2);
  const rng = makeRng(`${state.seed}:turn:${state.turn}:cityAttack:${attackerId}:${city.id}:${rngSalt}`);
  const aRoll = aStr * (0.85 + rng.next() * 0.3);
  const dRoll = dStr * (0.85 + rng.next() * 0.3);
  const total = aRoll + dRoll;
  const aDmg = Math.max(1, Math.round(6 * (dRoll / total)));
  const cDmg = Math.max(1, Math.round(8 * (aRoll / total)));
  const attackerHpAfter = Math.max(0, attacker.hp - aDmg);
  const cityHpAfter = Math.max(0, city.hp - cDmg);
  return {
    attackerHpAfter,
    cityHpAfter,
    attackerKilled: attackerHpAfter === 0,
    cityCaptured: cityHpAfter === 0
  };
}
