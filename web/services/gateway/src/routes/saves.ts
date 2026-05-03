/* =============================================================================
 * File:           services/gateway/src/routes/saves.ts
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-05-03
 * Last Update:    2026-05-03
 * Version:        0.1.0
 *
 * Description:
 *   Cloud-save endpoints. Body sizes are capped (a v0.1 world is ~80 KB
 *   serialized) and rate-limited so the route can't be abused as cheap
 *   storage.
 *
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

import type { FastifyInstance } from "fastify";
import { z } from "zod";
import type { Db } from "../db.js";

const SaveBody = z
  .object({
    v: z.literal(1),
    user: z.string().min(1).max(64),
    slot: z.string().regex(/^[A-Za-z0-9_-]{1,32}$/),
    state: z.unknown()
  })
  .strict();

const MAX_PAYLOAD_BYTES = 256 * 1024;

export type SaveDeps = { db: Db };

export function registerSaves(app: FastifyInstance, deps: SaveDeps): void {
  const upsert = deps.db.prepare(`
    INSERT INTO saves (user, slot, payload, updated_ms) VALUES (?, ?, ?, ?)
    ON CONFLICT(user, slot) DO UPDATE SET payload = excluded.payload, updated_ms = excluded.updated_ms
  `);
  const fetch = deps.db.prepare(`SELECT payload, updated_ms FROM saves WHERE user = ? AND slot = ?`);

  app.post("/saves", async (req, reply) => {
    const parsed = SaveBody.safeParse(req.body);
    if (!parsed.success) {
      reply.code(400);
      return { v: 1, ok: false, error: "BAD_REQUEST" };
    }
    const json = JSON.stringify(parsed.data.state);
    if (json.length > MAX_PAYLOAD_BYTES) {
      reply.code(413);
      return { v: 1, ok: false, error: "PAYLOAD_TOO_LARGE" };
    }
    const now = Date.now();
    upsert.run(parsed.data.user, parsed.data.slot, json, now);
    return { v: 1, ok: true, updatedAtMs: now };
  });

  app.get<{ Params: { user: string; slot: string } }>("/saves/:user/:slot", async (req, reply) => {
    const row = fetch.get(req.params.user, req.params.slot) as
      | { payload: string; updated_ms: number }
      | undefined;
    if (!row) {
      reply.code(404);
      return { v: 1, ok: false, error: "NOT_FOUND" };
    }
    return {
      v: 1,
      ok: true,
      state: JSON.parse(row.payload),
      updatedAtMs: row.updated_ms
    };
  });
}
