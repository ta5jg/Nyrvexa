/* =============================================================================
 * File:           services/gateway/src/index.ts
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-05-03
 * Last Update:    2026-05-03
 * Version:        0.1.0
 *
 * Description:
 *   Nyrvexa gateway entry. Boots Fastify, opens SQLite, registers routes,
 *   and (in production) serves the bundled web app from /apps/web/dist.
 *
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

import { existsSync } from "node:fs";
import { resolve } from "node:path";
import Fastify from "fastify";
import cors from "@fastify/cors";
import rateLimit from "@fastify/rate-limit";
import staticPlugin from "@fastify/static";
import { readEnv } from "./env.js";
import { openDb } from "./db.js";
import { registerHealth } from "./routes/health.js";
import { registerSaves } from "./routes/saves.js";
import { registerLeaderboard } from "./routes/leaderboard.js";

async function main() {
  const env = readEnv();
  const db = openDb(env.DB_PATH);

  const app = Fastify({
    logger: {
      level: env.NODE_ENV === "production" ? "info" : "debug",
      transport:
        env.NODE_ENV === "development"
          ? { target: "pino-pretty", options: { colorize: true, singleLine: true } }
          : undefined
    },
    trustProxy: env.TRUST_PROXY.length > 0 ? env.TRUST_PROXY.split(",").map((s) => s.trim()) : false,
    bodyLimit: 512 * 1024
  });

  await app.register(cors, {
    origin: env.CORS_ORIGIN === "*" ? true : env.CORS_ORIGIN.split(",").map((s) => s.trim()),
    methods: ["GET", "POST"]
  });

  await app.register(rateLimit, {
    max: 90,
    timeWindow: "1 minute",
    allowList: () => env.NODE_ENV === "test"
  });

  registerHealth(app);
  registerSaves(app, { db });
  registerLeaderboard(app, { db });

  // Serve the web bundle in production. Dev runs Vite on its own port.
  const webDist = resolve(process.cwd(), "../../apps/web/dist");
  if (env.NODE_ENV === "production" && existsSync(webDist)) {
    await app.register(staticPlugin, {
      root: webDist,
      prefix: "/",
      decorateReply: false
    });
    app.setNotFoundHandler((req, reply) => {
      if (req.url.startsWith("/health") || req.url.startsWith("/saves") || req.url.startsWith("/results") || req.url.startsWith("/leaderboard")) {
        reply.code(404).send({ error: "not found" });
        return;
      }
      reply.type("text/html").sendFile("index.html");
    });
  }

  app.addHook("onClose", async () => {
    db.close();
  });
  process.on("SIGTERM", () => void app.close());
  process.on("SIGINT", () => void app.close());

  await app.listen({ host: env.HOST, port: env.PORT });
  app.log.info({ host: env.HOST, port: env.PORT }, "nyrvexa gateway up");
}

main().catch((err) => {
  console.error("fatal:", err);
  process.exit(1);
});
