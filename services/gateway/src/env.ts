/* =============================================================================
 * File:           services/gateway/src/env.ts
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-05-03
 * Last Update:    2026-05-03
 * Version:        0.1.0
 * ============================================================================= */

import "dotenv/config";
import { z } from "zod";

const Schema = z.object({
  PORT: z.coerce.number().int().min(1).max(65535).default(8789),
  HOST: z.string().default("0.0.0.0"),
  DB_PATH: z.string().default("./data/nyrvexa.sqlite"),
  CORS_ORIGIN: z.string().default("*"),
  TRUST_PROXY: z.string().default(""),
  NODE_ENV: z.enum(["development", "production", "test"]).default("development")
});

export type Env = z.infer<typeof Schema>;

let cached: Env | null = null;

export function readEnv(): Env {
  if (cached) return cached;
  const parsed = Schema.safeParse(process.env);
  if (!parsed.success) {
    throw new Error(`invalid env: ${parsed.error.issues.map((i) => i.message).join("; ")}`);
  }
  cached = Object.freeze(parsed.data);
  return cached;
}
