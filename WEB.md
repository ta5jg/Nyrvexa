<!-- =============================================================================
File:           web/README.md
Author:         USDTG GROUP TECHNOLOGY LLC
Developer:      Irfan Gedik
Created Date:   2026-05-03
Last Update:    2026-05-03
Version:        0.1.0

Description:
  Nyrvexa web port — browser/mobile 4X.

License:
  Proprietary. All rights reserved. See LICENSE in the repository root.
============================================================================= -->

# Nyrvexa — Web Port

Browser/mobile 4X strategy. Map-first exploration, expansion, economy, and combat. TypeScript engine, custom PixiJS hex renderer, React HUD, deterministic AI in a Web Worker.

## Layout

```
web/
├── apps/web              # Vite + React + PixiJS frontend
├── services/gateway      # Fastify + SQLite (save/load, leaderboard)
└── packages/
    ├── engine            # Pure deterministic 4X simulation
    ├── content           # JSON-driven catalogs (biomes, units, techs, factions)
    └── protocol          # Wire types shared by web ↔ gateway
```

## Quick start

```bash
pnpm install
pnpm dev          # web → http://localhost:5175 + gateway → http://localhost:8789
```

Or individually:

```bash
pnpm dev:web
pnpm dev:gateway
```

## Scripts

| Command           | What it does                              |
| ----------------- | ----------------------------------------- |
| `pnpm dev`        | Run web + gateway in parallel             |
| `pnpm build`      | Build all packages                        |
| `pnpm test`       | Vitest across packages                    |
| `pnpm typecheck`  | tsc --noEmit across packages              |

## Aesthetic

Crystalline cartography — flat saturated biome colors with subtle texture, geometric SVG glyphs for units/cities, dark HUD with warm-orange + teal + violet accents (consistent with sibling games).

Original visual identity is a hard requirement: no Civ/Polytopia/Hearthstone clones.

## Design references

The Unity prototype (one level up in this repo) is the design source for mechanics. The web port is **not** a transpilation — it's a fresh implementation in TypeScript that preserves the deterministic gameplay shape (M1–M16 milestones, faction stats, biome economy, M1 skirmish combat).
