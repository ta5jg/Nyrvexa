/* =============================================================================
 * File:           web/packages/engine/src/rng.ts
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-05-03
 * Last Update:    2026-05-03
 * Version:        0.1.0
 *
 * Description:
 *   Deterministic seeded RNG — FNV-1a 32-bit hash → mulberry32 PRNG. Same
 *   seed yields the same sequence on Node, browsers, and Web Workers.
 *
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

export type Rng = {
  next(): number;
  int(max: number): number;
  range(min: number, max: number): number;
  chance(pct: number): boolean;
  pick<T>(arr: readonly T[]): T;
};

export function fnv1a32(s: string): number {
  let h = 2166136261 >>> 0;
  for (let i = 0; i < s.length; i++) {
    h ^= s.charCodeAt(i);
    h = Math.imul(h, 16777619);
  }
  return h >>> 0;
}

export function mulberry32(seed: number): () => number {
  let s = seed >>> 0;
  return () => {
    s = (s + 0x6d2b79f5) | 0;
    let t = s;
    t = Math.imul(t ^ (t >>> 15), t | 1);
    t ^= t + Math.imul(t ^ (t >>> 7), t | 61);
    return ((t ^ (t >>> 14)) >>> 0) / 4294967296;
  };
}

export function makeRng(seed: string, salt = ""): Rng {
  const hash = fnv1a32(salt.length > 0 ? `${seed}:${salt}` : seed);
  const next = mulberry32(hash);
  return {
    next,
    int(max) { return Math.floor(next() * max); },
    range(min, max) { return min + Math.floor(next() * (max - min + 1)); },
    chance(pct) {
      if (pct <= 0) return false;
      if (pct >= 100) return true;
      return next() * 100 < pct;
    },
    pick(arr) {
      if (arr.length === 0) throw new Error("rng.pick on empty array");
      return arr[Math.floor(next() * arr.length)]!;
    }
  };
}
