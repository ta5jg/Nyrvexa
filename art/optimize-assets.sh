#!/usr/bin/env bash
# =============================================================================
# File:           art/optimize-assets.sh
# Author:         USDTG GROUP TECHNOLOGY LLC
# Developer:      Irfan Gedik
# Created Date:   2026-05-03
# Last Update:    2026-05-03
# Version:        0.1.0
#
# Description:
#   Batch-optimize DALL-E PNG output for the Nyrvexa web port. DALL-E only
#   emits PNG (sometimes 2-3 MB each). This script walks the asset tree,
#   resizes to web-sensible dimensions per asset category, and converts to
#   the smallest format that still preserves quality:
#     - Hex tile biomes:  JPG quality 86 @ 512px max  (no alpha needed)
#     - Splash/scenes:    JPG quality 86 @ 1600px max (no alpha needed)
#     - Cities + wonders: PNG @ 512px max             (alpha for placement)
#     - Units + UI:       PNG @ 384px max             (alpha mandatory)
#     - Effects/weather:  PNG @ 384px max             (alpha mandatory)
#
#   Idempotent: running twice on the same files is safe.
#
# Usage:
#   ./art/optimize-assets.sh                   # walk apps/web/public/
#   ./art/optimize-assets.sh path/to/folder    # walk a specific folder
#
# Requirements:
#   macOS sips (built-in). Linux: install ImageMagick and substitute
#   `magick` for the sips calls.
#
# License:
#   Proprietary. All rights reserved. See LICENSE in the repository root.
# =============================================================================
set -euo pipefail

ROOT="${1:-$(cd "$(dirname "$0")/.." && pwd)/apps/web/public}"

if [[ ! -d "$ROOT" ]]; then
  echo "error: $ROOT does not exist" >&2
  exit 1
fi

cyan()  { printf "\033[36m%s\033[0m\n" "$*"; }
green() { printf "\033[32m%s\033[0m\n" "$*"; }
dim()   { printf "\033[2m%s\033[0m\n" "$*"; }

# Categories that DON'T need transparency → convert to JPG, smaller files.
JPG_CATEGORIES=(
  "biomes"
  "splash"
  "cities/interior"
  "atmosphere"
  "battle"
)

# Everything else under public/ that is a PNG should stay PNG (with alpha)
# but get resized.

resize_jpg() {
  local in="$1"
  local out="$2"
  local maxdim="$3"
  sips -s format jpeg -s formatOptions 86 \
    --resampleHeightWidthMax "$maxdim" \
    "$in" --out "$out" >/dev/null
  rm -f "$in"
}

resize_png() {
  local file="$1"
  local maxdim="$2"
  sips --resampleHeightWidthMax "$maxdim" \
    "$file" --out "$file" >/dev/null
}

is_jpg_category() {
  local rel="$1"
  for c in "${JPG_CATEGORIES[@]}"; do
    if [[ "$rel" == "$c"* ]]; then return 0; fi
  done
  return 1
}

cyan "==> Scanning $ROOT"
total=0
saved=0

# Walk *.png files
while IFS= read -r -d '' f; do
  rel="${f#"$ROOT/"}"
  before=$(stat -f %z "$f")

  if is_jpg_category "$rel"; then
    if [[ "$rel" == splash/* || "$rel" == battle/* ]]; then
      maxdim=1600
    else
      maxdim=512
    fi
    out="${f%.png}.jpg"
    resize_jpg "$f" "$out" "$maxdim"
    after=$(stat -f %z "$out")
    dim "  $rel → $(basename "$out")  $((before/1024)) KB → $((after/1024)) KB"
  else
    if [[ "$rel" == units/* || "$rel" == ui/* || "$rel" == effects/* || "$rel" == weather/* ]]; then
      maxdim=384
    else
      maxdim=512
    fi
    resize_png "$f" "$maxdim"
    after=$(stat -f %z "$f")
    dim "  $rel  $((before/1024)) KB → $((after/1024)) KB"
  fi

  total=$((total + 1))
  saved=$((saved + before - after))
done < <(find "$ROOT" -name "*.png" -type f -print0)

if (( total > 0 )); then
  green "✓ Optimized $total assets, saved $((saved/1024/1024)) MB total."
else
  dim "No PNG files found under $ROOT — already optimized or empty."
fi

dim ""
dim "Next: run typecheck + build to confirm everything still loads."
dim "  pnpm -r typecheck && pnpm --filter @nyrvexa/web build"
