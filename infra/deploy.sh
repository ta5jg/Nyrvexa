#!/usr/bin/env bash
# =============================================================================
# File:           infra/deploy.sh
# Author:         USDTG GROUP TECHNOLOGY LLC
# Developer:      Irfan Gedik
# Created Date:   2026-05-03
# Last Update:    2026-05-03
# Version:        0.1.0
#
# Description:
#   One-shot production deploy. Runs locally; SSHes into the server, pulls
#   each game's repo, rebuilds + restarts the stack, then health-checks.
#   Idempotent: safe to run repeatedly. Default target is the SERVER var
#   below (override with: SERVER=user@ip ./deploy.sh).
#
# License:
#   Proprietary. All rights reserved. See LICENSE in the repository root.
# =============================================================================
set -euo pipefail

SERVER="${SERVER:-deploy@SERVER_IP_HERE}"
SRV_ROOT="${SRV_ROOT:-/srv}"

cyan()  { printf "\033[36m%s\033[0m\n" "$*"; }
green() { printf "\033[32m%s\033[0m\n" "$*"; }
red()   { printf "\033[31m%s\033[0m\n" "$*"; }

remote() { ssh -o StrictHostKeyChecking=accept-new "$SERVER" "$@"; }

cyan "==> 1/5 Pulling latest code on ${SERVER}"
remote bash -s <<EOF
set -euo pipefail
cd ${SRV_ROOT}
for repo in nyrvexis nyrduel Nyrvexa; do
  if [ -d "\$repo/.git" ]; then
    echo "  → updating \$repo"
    git -C "\$repo" fetch --quiet --all
    git -C "\$repo" reset --quiet --hard "origin/\$(git -C \"\$repo\" rev-parse --abbrev-ref HEAD)"
  else
    echo "  ⚠️  \$repo missing — clone it under ${SRV_ROOT} first."
  fi
done
EOF

cyan "==> 2/5 Building images"
remote "cd ${SRV_ROOT}/Nyrvexa/infra && docker compose -f docker-compose.prod.yml build --pull"

cyan "==> 3/5 Bringing up the stack"
remote "cd ${SRV_ROOT}/Nyrvexa/infra && docker compose -f docker-compose.prod.yml up -d"

cyan "==> 4/5 Reloading Caddy"
remote "cd ${SRV_ROOT}/Nyrvexa/infra && docker compose -f docker-compose.prod.yml exec -T caddy caddy reload --config /etc/caddy/Caddyfile" || true

cyan "==> 5/5 Health checks"
sleep 5
fail=0
for url in https://nyrvexis.com/ https://nyrduel.com/health https://nyrvexa.com/; do
  code=$(curl -sk -o /dev/null -w "%{http_code}" "$url" || echo "000")
  if [[ "$code" =~ ^(200|301|302)$ ]]; then
    green "  ✓ ${url} → ${code}"
  else
    red   "  ✗ ${url} → ${code}"
    fail=$((fail+1))
  fi
done

if [[ "$fail" -gt 0 ]]; then
  red "Deploy completed with ${fail} failing health check(s). Check logs:"
  red "  ssh ${SERVER} 'cd ${SRV_ROOT}/Nyrvexa/infra && docker compose -f docker-compose.prod.yml logs --tail=50'"
  exit 1
fi

green "✓ Deploy complete — all three sites reachable."
