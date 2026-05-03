# =============================================================================
# File:           web/Dockerfile
# Author:         USDTG GROUP TECHNOLOGY LLC
# Developer:      Irfan Gedik
# Created Date:   2026-05-03
# Last Update:    2026-05-03
# Version:        0.1.0
#
# Description:
#   Multi-stage build for the Nyrvexa web port. Final image runs the gateway,
#   which serves the compiled web bundle via @fastify/static.
#
# License:
#   Proprietary. All rights reserved. See LICENSE in the repository root.
# =============================================================================

FROM node:22.16-alpine AS deps
RUN corepack enable
WORKDIR /app
COPY pnpm-workspace.yaml package.json pnpm-lock.yaml* .npmrc* ./
COPY tsconfig.base.json ./
COPY packages ./packages
COPY services ./services
COPY apps ./apps
RUN pnpm install --no-frozen-lockfile

FROM deps AS build
RUN pnpm -r build

FROM node:22.16-alpine AS runtime
RUN corepack enable
WORKDIR /app
ENV NODE_ENV=production
ENV PORT=8789
ENV HOST=0.0.0.0
ENV DB_PATH=/app/data/nyrvexa.sqlite
COPY --from=build /app/services/gateway/dist ./services/gateway/dist
COPY --from=build /app/services/gateway/package.json ./services/gateway/
COPY --from=build /app/apps/web/dist ./apps/web/dist
COPY --from=build /app/packages ./packages
COPY --from=build /app/node_modules ./node_modules
COPY --from=build /app/services/gateway/node_modules ./services/gateway/node_modules
RUN mkdir -p /app/data
EXPOSE 8789
WORKDIR /app/services/gateway
CMD ["node", "dist/index.js"]
