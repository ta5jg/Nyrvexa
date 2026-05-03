/* =============================================================================
 * File:           services/gateway/src/routes/health.ts
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-05-03
 * Last Update:    2026-05-03
 * Version:        0.1.0
 * ============================================================================= */

import type { FastifyInstance } from "fastify";

export function registerHealth(app: FastifyInstance): void {
  app.get("/health", async () => ({ ok: true, name: "nyrvexa-gateway", time: new Date().toISOString() }));
}
