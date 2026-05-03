/* =============================================================================
 * File:           apps/web/src/lib/profile.ts
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-05-03
 * Last Update:    2026-05-03
 * Version:        0.1.0
 *
 * Description:
 *   Local profile + autosave helpers backed by localStorage.
 *
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

import type { WorldSnapshot } from "@nyrvexa/protocol";

const K_USER = "nyrvexa:user";
const K_NAME = "nyrvexa:name";
const K_AUTOSAVE = "nyrvexa:autosave";

function genId(): string {
  const buf = new Uint8Array(8);
  if (typeof crypto !== "undefined" && "getRandomValues" in crypto) crypto.getRandomValues(buf);
  else for (let i = 0; i < 8; i++) buf[i] = Math.floor(Math.random() * 256);
  return "vex_" + Array.from(buf, (b) => b.toString(16).padStart(2, "0")).join("");
}

export function getUserId(): string {
  if (typeof window === "undefined") return "ssr";
  let id = localStorage.getItem(K_USER);
  if (!id) {
    id = genId();
    localStorage.setItem(K_USER, id);
  }
  return id;
}

export function getDisplayName(): string {
  return localStorage.getItem(K_NAME) ?? "";
}

export function setDisplayName(name: string): void {
  const t = name.trim();
  if (!t) localStorage.removeItem(K_NAME);
  else localStorage.setItem(K_NAME, t.slice(0, 24));
}

export function loadAutosave(): WorldSnapshot | null {
  const raw = localStorage.getItem(K_AUTOSAVE);
  if (!raw) return null;
  try {
    return JSON.parse(raw) as WorldSnapshot;
  } catch {
    return null;
  }
}

export function saveAutosave(state: WorldSnapshot): void {
  try {
    localStorage.setItem(K_AUTOSAVE, JSON.stringify(state));
  } catch {
    // quota — ignore for v0.1
  }
}

export function clearAutosave(): void {
  localStorage.removeItem(K_AUTOSAVE);
}
