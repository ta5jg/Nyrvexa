<!-- =============================================================================
File:           infra/README.md
Author:         USDTG GROUP TECHNOLOGY LLC
Developer:      Irfan Gedik
Created Date:   2026-05-03
Last Update:    2026-05-03
Version:        0.1.0

Description:
  Operational runbook for the USDTG game stack on Hetzner.

License:
  Proprietary. All rights reserved. See LICENSE in the repository root.
============================================================================= -->

# Nyrvexa / USDTG Production Deploy

This `infra/` folder lives inside the Nyrvexa repo and orchestrates the
production deployment of the entire USDTG game suite (Nyrvexa + its two
sibling games). It is the single source of truth for Caddy config,
docker-compose stack, deploy script, Hetzner provisioning, and the
nyrvexa.com landing page.

```
/srv/
├── nyrvexis/        ← github.com/ta5jg/nyrvexis (game)
├── nyrduel/         ← github.com/ta5jg/nyrduel (game)
└── Nyrvexa/         ← github.com/ta5jg/Nyrvexa (this repo)
    └── infra/       ← deploy stack, run from here
```

Single Hetzner host running three games behind Caddy (HTTPS auto, gzip, HSTS):

| Domain         | Service                  | Port | Source                       |
| -------------- | ------------------------ | ---- | ---------------------------- |
| nyrvexis.com   | Nyrvexis web + gateway   | 8787 | `/srv/nyrvexis`              |
| nyrduel.com    | Nyrduel web + gateway    | 8788 | `/srv/nyrduel`               |
| nyrvexa.com    | Nyrvexa landing (static) | n/a  | `/srv/Nyrvexa/infra/landing` |

## One-time setup (do once per server)

### 1. Provision the box

Hetzner Cloud → **Add Server**:
- Image: **Ubuntu 24.04**
- Type: **CX22** (€4.51/mo, 2vCPU/4GB) — start here, scale up later
- Location: **Nuremberg or Helsinki** (low-latency to TR + EU)
- Networking: **Public IPv4 + IPv6**
- SSH key: **add yours** (also paste it into `cloud-init.yml` below)
- User data: paste the contents of [`cloud-init.yml`](./cloud-init.yml) — replace `SSH_PUBLIC_KEY_HERE` with your real key first

The cloud-init script installs Docker + Compose, locks down SSH, opens UFW for 22/80/443 only, creates a `deploy` user, and puts the box in a usable state.

### 2. Point DNS

For each domain at your registrar (or Cloudflare in DNS-only mode):

| Type | Name | Value          |
| ---- | ---- | -------------- |
| A    | @    | <SERVER_IPv4>  |
| A    | www  | <SERVER_IPv4>  |
| AAAA | @    | <SERVER_IPv6>  |
| AAAA | www  | <SERVER_IPv6>  |

Caddy will request Let's Encrypt certs automatically once DNS propagates (5–30 minutes).

> **Cloudflare:** Set DNS-only (grey cloud) until Caddy has issued certs at least once. Proxy-on can interfere with the ACME HTTP challenge.

### 3. Clone the repos under `/srv`

```bash
ssh deploy@<server-ip>
cd /srv
git clone https://github.com/ta5jg/nyrvexis.git
git clone https://github.com/ta5jg/nyrduel.git
git clone https://github.com/ta5jg/Nyrvexa.git
```

If `nyrvexis` does not have a Dockerfile yet, use `infra/sample-nyrvexis.Dockerfile` (see below) until one is added to the repo.

### 4. First deploy

From your laptop:

```bash
cd /Users/irfangedik/Nyrvexa/infra
SERVER=deploy@<server-ip> ./deploy.sh
```

The script:
1. SSHs in, fetches the latest commit on each repo's default branch
2. Builds the Docker images
3. Brings the stack up
4. Reloads Caddy
5. Health-checks each domain and reports success/failure

Allow ~2 minutes the first time (image build + first cert issuance).

## Daily deploy flow

Every subsequent deploy is one command:

```bash
SERVER=deploy@<server-ip> ./deploy.sh
```

That re-fetches each repo, rebuilds only what changed (Docker layer cache), restarts containers, and verifies health. ~10–30 seconds typically.

## Inspecting / troubleshooting

```bash
# Logs
ssh deploy@<server-ip> 'cd /srv/Nyrvexa/infra && docker compose -f docker-compose.prod.yml logs --tail=100 -f'

# One service
ssh deploy@<server-ip> 'cd /srv/Nyrvexa/infra && docker compose -f docker-compose.prod.yml logs --tail=200 nyrduel'

# Caddy status (TLS certs etc.)
ssh deploy@<server-ip> 'cd /srv/Nyrvexa/infra && docker compose -f docker-compose.prod.yml exec caddy caddy fmt --overwrite /etc/caddy/Caddyfile && docker compose -f docker-compose.prod.yml exec caddy caddy reload --config /etc/caddy/Caddyfile'

# Restart a single service
ssh deploy@<server-ip> 'cd /srv/Nyrvexa/infra && docker compose -f docker-compose.prod.yml restart nyrduel'

# Disk + container resource usage
ssh deploy@<server-ip> 'docker stats --no-stream'
```

## Backups

SQLite files live in named volumes (`nyrvexis_data`, `nyrduel_data`). Quick weekly snapshot:

```bash
# On the server
cd /var/lib/docker/volumes
sudo tar czf /root/backups/nyr-$(date +%F).tgz \
  usdtg-stack_nyrvexis_data usdtg-stack_nyrduel_data
```

Set up a cron entry on the host once you have user data worth keeping:

```cron
0 3 * * 0 root tar czf /root/backups/nyr-$(date +\%F).tgz -C /var/lib/docker/volumes usdtg-stack_nyrvexis_data usdtg-stack_nyrduel_data
```

Hetzner volume snapshots (web UI) are an even simpler alternative.

## Switching nyrvexa.com to the playable web app

Once the Nyrvexa web port is shipped:

1. Add a `nyrvexa` service to `docker-compose.prod.yml` (build context: `../../Nyrvexa`).
2. In `Caddyfile`, replace the `nyrvexa.com` block's `root` + `file_server` with:
   ```
   reverse_proxy nyrvexa:8789
   ```
3. `./deploy.sh` to roll it out. Old landing files stay on disk as a fallback if you ever revert.

## Sample Dockerfile for nyrvexis

If `/srv/nyrvexis/Dockerfile` is missing, place this file there before the first deploy:

```Dockerfile
FROM node:22.16-alpine AS deps
RUN corepack enable
WORKDIR /app
COPY pnpm-workspace.yaml package.json pnpm-lock.yaml* .npmrc* ./
COPY . .
RUN pnpm install --frozen-lockfile

FROM deps AS build
RUN pnpm -r build || pnpm --filter "./services/gateway" build

FROM node:22.16-alpine AS runtime
RUN corepack enable
WORKDIR /app
ENV NODE_ENV=production
COPY --from=build /app /app
EXPOSE 8787
WORKDIR /app/services/gateway
CMD ["node", "dist/index.js"]
```

## Defaults shipped on Day 1

- Manual deploy via `./deploy.sh` (no GitHub Actions). Add CI/CD later when the workflow stabilizes.
- Email signups for nyrvexa.com land in nyrduel's SQLite (`signups` table). Export with:
  ```bash
  ssh deploy@<server-ip> 'docker compose -f /srv/Nyrvexa/infra/docker-compose.prod.yml exec nyrduel sqlite3 /app/data/nyrduel.sqlite \
    "SELECT email, source, datetime(at_ms/1000, '"'unixepoch'"') FROM signups ORDER BY at_ms DESC;"'
  ```

## Security checklist

- [x] SSH: key-only, root login disabled (cloud-init)
- [x] UFW: 22/80/443 only (cloud-init)
- [x] HTTPS: Caddy auto + HSTS (Caddyfile)
- [x] Headers: nosniff, frame-deny, referrer policy (Caddyfile)
- [ ] Rate-limit on `/signup` (TODO if abuse appears)
- [ ] Off-host backups (TODO once data matters)
- [ ] Monitoring (TODO — start with Hetzner's built-in graphs, add UptimeRobot for endpoint pings)
