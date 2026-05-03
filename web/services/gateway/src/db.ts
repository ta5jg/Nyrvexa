/* =============================================================================
 * File:           services/gateway/src/db.ts
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-05-03
 * Last Update:    2026-05-03
 * Version:        0.1.0
 * ============================================================================= */

import { mkdirSync } from "node:fs";
import { dirname, isAbsolute, resolve } from "node:path";
import { DatabaseSync, type StatementSync } from "node:sqlite";

export type Db = DatabaseSync;
export type Stmt = StatementSync;

const SCHEMA = `
CREATE TABLE IF NOT EXISTS saves (
  id          INTEGER PRIMARY KEY AUTOINCREMENT,
  user        TEXT NOT NULL,
  slot        TEXT NOT NULL,
  payload     TEXT NOT NULL,
  updated_ms  INTEGER NOT NULL,
  UNIQUE(user, slot) ON CONFLICT REPLACE
);
CREATE INDEX IF NOT EXISTS idx_saves_user ON saves(user, updated_ms DESC);

CREATE TABLE IF NOT EXISTS results (
  id          INTEGER PRIMARY KEY AUTOINCREMENT,
  user        TEXT NOT NULL,
  display_name TEXT,
  faction     TEXT NOT NULL,
  outcome     TEXT NOT NULL CHECK (outcome IN ('victory','defeat','draw')),
  turns       INTEGER NOT NULL,
  score       INTEGER NOT NULL,
  at_ms       INTEGER NOT NULL
);
CREATE INDEX IF NOT EXISTS idx_results_score ON results(score DESC, at_ms ASC);
`;

export function openDb(filePath: string): Db {
  const inMem = filePath === ":memory:";
  let target = filePath;
  if (!inMem) {
    const abs = isAbsolute(filePath) ? filePath : resolve(process.cwd(), filePath);
    mkdirSync(dirname(abs), { recursive: true });
    target = abs;
  }
  const db = new DatabaseSync(target);
  db.exec("PRAGMA journal_mode = WAL;");
  db.exec("PRAGMA synchronous = NORMAL;");
  db.exec(SCHEMA);
  return db;
}
