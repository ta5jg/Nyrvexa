/* =============================================================================
 * File:           services/gateway/src/routes/leaderboard.ts
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-05-03
 * Last Update:    2026-05-03
 * Version:        0.1.0
 * ============================================================================= */

import type { FastifyInstance } from "fastify";
import { z } from "zod";
import type { Db } from "../db.js";

const SubmitBody = z
  .object({
    v: z.literal(1),
    user: z.string().min(1).max(64),
    displayName: z.union([z.string(), z.null()]).optional(),
    faction: z.string().min(1).max(32),
    outcome: z.enum(["victory", "defeat", "draw"]),
    turns: z.number().int().min(1).max(1000),
    score: z.number().int().min(0).max(1_000_000)
  })
  .strict();

const Query = z.object({ limit: z.coerce.number().int().min(1).max(100).default(50) });

export type LBDeps = { db: Db };

export function registerLeaderboard(app: FastifyInstance, deps: LBDeps): void {
  const insert = deps.db.prepare(
    `INSERT INTO results (user, display_name, faction, outcome, turns, score, at_ms)
     VALUES (?, ?, ?, ?, ?, ?, ?)`
  );
  const top = deps.db.prepare(
    `SELECT id, user, display_name, faction, outcome, turns, score, at_ms
     FROM results
     ORDER BY score DESC, at_ms ASC
     LIMIT ?`
  );

  app.post("/results", async (req, reply) => {
    const parsed = SubmitBody.safeParse(req.body);
    if (!parsed.success) {
      reply.code(400);
      return { v: 1, ok: false, error: "BAD_REQUEST" };
    }
    insert.run(
      parsed.data.user,
      parsed.data.displayName ?? null,
      parsed.data.faction,
      parsed.data.outcome,
      parsed.data.turns,
      parsed.data.score,
      Date.now()
    );
    return { v: 1, ok: true };
  });

  app.get("/leaderboard", async (req, reply) => {
    const parsed = Query.safeParse(req.query);
    if (!parsed.success) {
      reply.code(400);
      return { v: 1, ok: false, error: "BAD_REQUEST" };
    }
    const rows = top.all(parsed.data.limit) as Array<{
      id: number;
      user: string;
      display_name: string | null;
      faction: string;
      outcome: string;
      turns: number;
      score: number;
      at_ms: number;
    }>;
    return {
      v: 1,
      ok: true,
      top: rows.map((r, i) => ({
        rank: i + 1,
        user: r.user,
        displayName: r.display_name,
        faction: r.faction,
        outcome: r.outcome,
        turns: r.turns,
        score: r.score,
        atMs: r.at_ms
      })),
      total: rows.length
    };
  });
}
