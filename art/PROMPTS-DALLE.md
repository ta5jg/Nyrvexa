<!-- =============================================================================
File:           art/PROMPTS-DALLE.md
Author:         USDTG GROUP TECHNOLOGY LLC
Developer:      Irfan Gedik
Created Date:   2026-05-03
Last Update:    2026-05-03
Version:        0.1.0

Description:
  Full DALL-E 3 prompt atlas for the Nyrvexa web 4X. Every visible asset
  in the game has a copy-paste prompt below, organized by category.
  Sub-200 prompts; aim is one cohesive painted world from a single style
  recipe so the map, units, and cities feel like the same universe.

License:
  Proprietary. All rights reserved. See LICENSE in the repository root.
============================================================================= -->

# Nyrvexa — DALL-E 3 Prompt Atlas

A full per-asset prompt set for the painterly rebuild of Nyrvexa visuals. Total ~170 prompts. Generate them in batches of 5–10 in the same ChatGPT (or Bing Image Creator) conversation so DALL-E's internal "previous reference" memory keeps the style cohesive.

---

## How to use this file

### Three-rule ritual

1. **Bypass ChatGPT's prompt rewriting.** Always prepend each prompt with `I NEED EXACTLY THIS, DO NOT REWRITE:` so DALL-E doesn't substitute its own wording and break style continuity.
2. **No real transparent PNG support in DALL-E.** Where a transparent background is needed (units, icons), ask for `plain white background, no shadow on the floor`, then run the result through [remove.bg](https://www.remove.bg) before dropping into the project.
3. **Same chat = same style.** Generate all assets in a single category in a single ChatGPT conversation. DALL-E silently references prior images you've made in the same chat. New chat → style drift.

### The master style block

Append this exact block to **every** prompt below. It is the glue that ties every asset to the same painted world. Don't paraphrase — DALL-E is sensitive to repetition.

```
In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.
```

When generating a unit/icon (transparent target), append also:

```
Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

When generating a hex tile (top-down terrain), append also:

```
Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

### File naming + integration

Drop generated assets into the path noted under each prompt. After all assets are in, edit `apps/web/src/lib/assets.ts` (creating it if missing) to point biome / unit / city draws at the new sprites. The renderer is already structured around an asset map; once filled, Pixi will replace its hand-drawn graphics with your painted tiles.

```
apps/web/public/
├── biomes/         # 7 hex-tile terrains (× variants)
├── resources/      # 5 resource overlay icons
├── units/          # 6 unit portraits × 4 factions
├── cities/         # 4 factions × 4 city stages
├── factions/       # 4 banners + 4 emblems
├── tech/           # 8 tech tree icons
├── ui/             # generic UI glyphs
├── effects/        # combat / movement bursts
└── splash/         # title + faction portraits + endgame
```

### Optimization (mandatory before commit)

DALL-E delivers 1–3 MB PNGs. Convert to WebP @ q85 for ~10× smaller files:

```bash
# Tiles (square 512)
for f in apps/web/public/biomes/*.png; do
  sips -s format jpeg -s formatOptions 86 --resampleHeightWidthMax 512 \
    "$f" --out "${f%.png}.jpg"
done

# Units (transparent 384) — keep PNG for alpha
for f in apps/web/public/units/*.png; do
  sips --resampleHeightWidthMax 384 "$f" --out "$f"
done
```

(macOS `sips` doesn't always emit WebP. JPG for terrain, PNG for units with alpha is fine.)

---

## Style anchor — generate once, study, then proceed

Before mass-producing assets, run prompt **#0** below. Study the result; if the style isn't what you want, refine the master block, regenerate, and only then start. Nothing wastes more credits than realizing 80 generations in that the style is wrong.

### #0 — Style anchor reference

**File:** `apps/web/public/splash/style-anchor.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly anime fantasy
landscape — a peaceful river valley at golden hour, distant
forest-covered hills under fluffy white clouds, a single small
hilltop village with terracotta-roofed cottages and a stone keep
flying a copper-orange banner, foreground wildflowers, soft volumetric
sunbeams, no characters in frame, 16:9 cinematic landscape composition.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.
```

If you like this image, every other prompt in this file inherits its style. If not, tweak the style block before continuing.

---

# SECTION 1 — Hex tile biomes (top-down terrain)

The hex map shows each tile from straight above. Generate **square** images; the renderer will mask them to hex shape. ~3 variants per biome so adjacent tiles don't look identical.

## 1.1 Plains

### #1 — Plain (open grassland)

**File:** `apps/web/public/biomes/plain-01.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down birds-eye view
of an open grassland tile, soft golden-green grass with subtle wind
patterns, scattered tiny wildflowers in yellow and white, a faint
dirt path crossing diagonally, no characters, no buildings, square
1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

### #2 — Plain (lush meadow)

**File:** `apps/web/public/biomes/plain-02.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a lush
flowering meadow, deep green grass dotted with white daisies, blue
forget-me-nots, and small clusters of red poppies, a meandering brook
in one corner, painterly highlights catching the petals, no people, no
buildings, square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

### #3 — Plain (sun-baked savanna)

**File:** `apps/web/public/biomes/plain-03.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of dry
sun-baked savanna grass, warm yellow-green tufts, a pair of acacia
trees casting long soft shadows, a winding herd path of trodden earth,
painterly afternoon warmth, no people, no buildings, square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

## 1.2 Forest

### #4 — Forest (broadleaf)

**File:** `apps/web/public/biomes/forest-01.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a dense
broadleaf forest canopy, layered green oak and maple crowns, dappled
sunlight breaking through to a mossy ground in places, a single tiny
clearing with a fallen log, painterly autumn-tinged edges, no people,
no buildings, square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

### #5 — Forest (deep evergreen)

**File:** `apps/web/public/biomes/forest-02.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a deep
pine forest, tall conical evergreens packed densely, deep cool greens
and shadowed blues between trunks, a thin winding trail of needles
underfoot, painterly highlights on tip-tops, no people, no buildings,
square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

### #6 — Forest (autumn)

**File:** `apps/web/public/biomes/forest-03.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of an
autumn forest canopy, mixed reds, oranges, deep yellows, scattered
green holdouts, a small misty clearing with a stone outcropping,
painterly leaf textures, no people, no buildings, square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

## 1.3 Hill

### #7 — Hill (rolling green)

**File:** `apps/web/public/biomes/hill-01.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of rolling
green hills, gentle sweeping curves casting soft shadows, scattered
boulders and a few twisted oaks, a faint shepherd's path winding
between hilltops, painterly afternoon light, no people, no buildings,
square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

### #8 — Hill (rocky bluffs)

**File:** `apps/web/public/biomes/hill-02.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of stony
windswept bluffs, exposed slate and granite outcroppings, sparse hardy
grass tufts and heather, a few bone-bleached driftwood logs, painterly
overcast light, no people, no buildings, square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

### #9 — Hill (terraced)

**File:** `apps/web/public/biomes/hill-03.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
terraced agricultural hill, layered rice or wheat paddies stepping
down a gentle slope, low stone retaining walls between terraces, a
narrow water channel running along one terrace, painterly soft sun,
no people, no buildings beyond the terraces, square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

## 1.4 Mountain

### #10 — Mountain (snow peak)

**File:** `apps/web/public/biomes/mountain-01.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
single jagged snow-capped mountain peak, dark slate-grey rock walls
streaked with white snow, a small frozen tarn at the base, painterly
shadows in the crevices, no people, no buildings, square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

### #11 — Mountain (ridge cluster)

**File:** `apps/web/public/biomes/mountain-02.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
cluster of rocky mountain ridges, three or four sharp peaks rising
together, deep dark valleys between them, sparse pine clinging to the
flanks, painterly cool light, no people, no buildings, square 1:1
aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

### #12 — Mountain (volcano)

**File:** `apps/web/public/biomes/mountain-03.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of an
active volcano peak, dark basalt slopes streaked with cooled lava
trails in red and orange, a smouldering caldera at the summit emitting
a thin painterly smoke plume, no people, no buildings, square 1:1
aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

## 1.5 Water

### #13 — Water (deep ocean)

**File:** `apps/web/public/biomes/water-01.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of deep
open ocean, painterly dark teal and indigo waves with white foam crests
and subtle highlight ripples, no land, no boats, no creatures, just the
surface of the sea seen from directly above, square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

### #14 — Water (shallow coast)

**File:** `apps/web/public/biomes/water-02.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
shallow tropical coastal lagoon, painterly turquoise gradient water
revealing pale sandy seafloor in the lighter areas, a small coral patch
in the corner, gentle wave patterns, no land visible, square 1:1
aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

### #15 — Water (lake)

**File:** `apps/web/public/biomes/water-03.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
freshwater inland lake surface, painterly soft blue with reflective
patches mirroring sky, a few lily pads grouped at one edge, very
gentle ripples, no shoreline visible, no characters, square 1:1
aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

## 1.6 Desert

### #16 — Desert (dune sea)

**File:** `apps/web/public/biomes/desert-01.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
warm sandy desert, painterly golden dune ridges casting long soft
shadows, faint wind ripples on the leeward slopes, a single bleached
bone half-buried in the foreground sand, no people, no buildings,
square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

### #17 — Desert (rocky wastes)

**File:** `apps/web/public/biomes/desert-02.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
cracked red rocky desert, painterly umber and rust earth with deep
sun-cracks, scattered boulders casting hard shadows, sparse desert
shrubs in the corners, no people, no buildings, square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

### #18 — Desert (oasis)

**File:** `apps/web/public/biomes/desert-03.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
small desert oasis, a circular pool of painterly turquoise water
ringed by tall date palms, lush green grass at the water's edge fading
quickly into golden sand, no people, no buildings, square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

## 1.7 Tundra

### #19 — Tundra (snowfield)

**File:** `apps/web/public/biomes/tundra-01.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of an open
arctic snowfield, painterly soft white with subtle blue shadows, a few
scattered black rock outcroppings half-buried in snow, faint animal
tracks crossing diagonally, no people, no buildings, square 1:1
aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

### #20 — Tundra (frozen taiga)

**File:** `apps/web/public/biomes/tundra-02.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of frozen
northern taiga, dark frosted spruce trees casting long blue shadows on
white snow, a frozen creek running through, painterly cold light, no
people, no buildings, square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

### #21 — Tundra (glacier edge)

**File:** `apps/web/public/biomes/tundra-03.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of the
edge of a glacier, painterly cracked blue-white ice with deep crevasses
and fresh snow, a thin meltwater stream tracing across, no people, no
buildings, square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

---

# SECTION 2 — Resource overlays

Resources sit on top of biome tiles. They should read as small, framed icons that compose well over any biome below. Generate with white background and run through remove.bg.

### #22 — Wheat field

**File:** `apps/web/public/resources/wheat.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A small painterly icon of a
golden wheat field bundle, rendered top-down birds-eye-view as if
slotting into a 64x64 hex tile space, ripe wheat heads in tied sheaves
casting gentle shadow, isolated on plain white background, no shadow
on the floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.
```

### #23 — Iron deposit

**File:** `apps/web/public/resources/iron.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A small painterly icon of an
exposed iron ore vein in dark grey rock, top-down view, with three
or four chunks of metallic blue-grey ore visible, ready to be mined,
isolated on plain white background, no shadow on the floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.
```

### #24 — Horse herd

**File:** `apps/web/public/resources/horse.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A small painterly icon of a tiny
herd of three brown horses grazing in tall green grass, viewed from
above, painterly impressions rather than detailed anatomy, isolated on
plain white background, no shadow on the floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.
```

### #25 — Gold ore

**File:** `apps/web/public/resources/gold_ore.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A small painterly icon of a gold
ore vein, glittering yellow nuggets embedded in cracked grey rock,
viewed from above, painterly highlight on the metal, isolated on plain
white background, no shadow on the floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.
```

### #26 — Stone quarry

**File:** `apps/web/public/resources/stone.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A small painterly icon of a stone
quarry, three or four cut grey limestone blocks stacked against a
quarry face, viewed from above, painterly chisel marks on the blocks,
isolated on plain white background, no shadow on the floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.
```

---

# SECTION 3 — Unit sprites

Six unit classes × four factions. Each unit appears as a **3/4 isometric character portrait** from a slightly raised camera so you can read both their stance and their kit. Generate with white background, run through remove.bg.

**Faction palette references** (use the named color in each prompt):
- **Aurei** — copper-orange and ivory; warm, sun-forged.
- **Vesnar** — forest-green and deep brown; quiet woodfolk.
- **Thalassia** — sea-teal and silver; coastal scribes.
- **Kyron** — violet and gold; hill-riders.

## 3.1 Settler (4 factions)

### #27 — Settler (Aurei)

**File:** `apps/web/public/units/settler-aurei.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a young Aurei settler — copper-orange tunic with ivory
trim, beige travel cloak, leather pack on back, walking staff in right
hand, sturdy boots, mid-step pose as if walking forward, calm hopeful
expression, isolated on plain white background, no shadow on the
floor, no text, full body visible from above-shoulders.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #28 — Settler (Vesnar)

**File:** `apps/web/public/units/settler-vesnar.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a young Vesnar settler — forest-green tunic with deep
brown trim, woven leaf-pattern cloak, leather pack on back, walking
staff carved from a sapling, sturdy boots, mid-step pose, gentle
expression, isolated on plain white background, no shadow on the floor,
no text, full body visible.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #29 — Settler (Thalassia)

**File:** `apps/web/public/units/settler-thalassia.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a young Thalassia settler — sea-teal tunic with silver
trim, white linen overcloak, scroll case slung at belt, walking staff,
sandals strapped over wrappings, mid-step pose, scholarly calm
expression, isolated on plain white background, no shadow on the floor,
no text, full body visible.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #30 — Settler (Kyron)

**File:** `apps/web/public/units/settler-kyron.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a young Kyron settler — violet tunic with gold trim,
fur-lined hood thrown back, leather pack on back, walking staff with a
small brass bell, sturdy riding boots, mid-step pose, weathered
confident expression, isolated on plain white background, no shadow on
the floor, no text, full body visible.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

## 3.2 Warrior (4 factions)

### #31 — Warrior (Aurei)

**File:** `apps/web/public/units/warrior-aurei.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of an Aurei warrior — polished bronze breastplate over a
copper-orange tunic, ivory cloak, kite shield bearing a sun-gold cross
emblem, straight short sword pointed down, helmet with a bronze brow,
ready stance, determined expression, isolated on plain white
background, no shadow on the floor, no text, full body visible.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #32 — Warrior (Vesnar)

**File:** `apps/web/public/units/warrior-vesnar.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a Vesnar warrior — green-lacquered scale armor with
deep brown leather strapping, mossy hooded cloak, round wooden shield
with a carved leaf emblem, hand axe in right hand, leather boots,
grounded stance, watchful expression, isolated on plain white
background, no shadow on the floor, no text, full body visible.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #33 — Warrior (Thalassia)

**File:** `apps/web/public/units/warrior-thalassia.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a Thalassia warrior — silvery scale armor with sea-teal
underrobe, white sash, oval shield etched with a wave-and-quill emblem,
slim short sword, sandals over greaves, balanced stance, calm
intelligent expression, isolated on plain white background, no shadow
on the floor, no text, full body visible.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #34 — Warrior (Kyron)

**File:** `apps/web/public/units/warrior-kyron.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a Kyron warrior — violet lamellar armor with gold
trim, fur shoulder pauldrons, round shield bearing a charging-stallion
emblem, curved sabre, fur-lined boots, aggressive forward stance,
fierce expression, isolated on plain white background, no shadow on
the floor, no text, full body visible.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

## 3.3 Scout (4 factions)

### #35 — Scout (Aurei)

**File:** `apps/web/public/units/scout-aurei.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of an Aurei scout — light copper-trimmed leather armor,
deep ivory hood casting partial face shadow, spyglass at belt, short
hand-axe at hip, soft boots, crouched alert stance, sharp watchful
expression, isolated on plain white background, no shadow on the floor,
no text, full body visible.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #36 — Scout (Vesnar)

**File:** `apps/web/public/units/scout-vesnar.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a Vesnar scout — leaf-pattern cloak with a deep hood,
forest-green light leather, long hunting knife, low-cut soft boots,
treading-quietly stance, attentive expression with bright eyes
catching light under the hood, isolated on plain white background, no
shadow on the floor, no text, full body visible.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #37 — Scout (Thalassia)

**File:** `apps/web/public/units/scout-thalassia.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a Thalassia scout — sea-teal traveler's cloak with
silver hood lining, scroll case at belt, slim throwing dagger, soft
sandals, alert listening stance, scholarly-but-sharp expression,
isolated on plain white background, no shadow on the floor, no text,
full body visible.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #38 — Scout (Kyron)

**File:** `apps/web/public/units/scout-kyron.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a Kyron scout — violet riding leathers with gold studs,
fur-trimmed hood, twin throwing daggers crossed at the back, light
riding boots, hunched-low stalking stance, intent narrowed-eye
expression, isolated on plain white background, no shadow on the floor,
no text, full body visible.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

## 3.4 Archer (4 factions)

### #39 — Archer (Aurei)

**File:** `apps/web/public/units/archer-aurei.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of an Aurei archer — copper-orange tunic with ivory
gambeson under, leather bracer on the bow arm, recurve bow drawn with
arrow nocked, quiver at hip, soft boots, aiming stance, focused
expression, isolated on plain white background, no shadow on the floor,
no text, full body visible.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #40 — Archer (Vesnar)

**File:** `apps/web/public/units/archer-vesnar.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a Vesnar archer — forest-green tunic, brown leather
bracer, hooded cape, longbow drawn with arrow nocked, woven quiver of
fletched arrows on back, leather boots, calm steady aiming stance,
quiet expression, isolated on plain white background, no shadow on the
floor, no text, full body visible.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #41 — Archer (Thalassia)

**File:** `apps/web/public/units/archer-thalassia.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a Thalassia archer — slim sea-teal robes over silver
greaves, composite recurve bow drawn, narrow quiver of silver-fletched
arrows, sandals over wrappings, balanced shooter stance, precise calm
expression, isolated on plain white background, no shadow on the floor,
no text, full body visible.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #42 — Archer (Kyron)

**File:** `apps/web/public/units/archer-kyron.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a Kyron horseback-style archer — violet riding gambeson
with gold trim, fur-shouldered cloak, short composite bow drawn,
quiver of barbed arrows at saddle position even though dismounted,
riding boots, twisted half-seated firing stance, fierce expression,
isolated on plain white background, no shadow on the floor, no text,
full body visible.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

## 3.5 Horseman (4 factions)

### #43 — Horseman (Aurei)

**File:** `apps/web/public/units/horseman-aurei.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an Aurei horseman — a chestnut warhorse with copper barding, mounted
rider in copper-orange surcoat over scale armor, crested helm, lance
held forward, sword at hip, ivory caparison embroidered with a sunburst,
mid-charge canter pose, isolated on plain white background, no shadow
on the floor, no text, full mount and rider visible.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #44 — Horseman (Vesnar)

**File:** `apps/web/public/units/horseman-vesnar.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
a Vesnar horseman — a dappled grey forest horse, mounted rider in
green-brown leathers and a leaf-pattern cloak, light helm, hand-axe
ready, hunting horn at belt, mid-canter pose with horse's mane flowing,
isolated on plain white background, no shadow on the floor, no text,
full mount and rider visible.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #45 — Horseman (Thalassia)

**File:** `apps/web/public/units/horseman-thalassia.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
a Thalassia horseman — a slim white horse with silver tack, mounted
rider in sea-teal cloak over silver scale, slim lance, oval shield
strapped at saddle, mid-trot poised pose, isolated on plain white
background, no shadow on the floor, no text, full mount and rider
visible.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #46 — Horseman (Kyron)

**File:** `apps/web/public/units/horseman-kyron.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
a Kyron horseman — a black warhorse with gold-trimmed violet caparison,
mounted rider in violet lamellar armor with gold trim and a fur-shoulder
cloak, long lance held overhead, curved sabre at hip, full charge gallop
pose, isolated on plain white background, no shadow on the floor, no
text, full mount and rider visible.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

## 3.6 Worker (4 factions)

### #47 — Worker (Aurei)

**File:** `apps/web/public/units/worker-aurei.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of an Aurei worker — copper-orange short tunic, ivory
apron, sleeves rolled, carpenter's hammer in right hand and a small
clay water jug in left, soft boots, working stance with one foot
forward, friendly resolute expression, isolated on plain white
background, no shadow on the floor, no text, full body visible.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #48 — Worker (Vesnar)

**File:** `apps/web/public/units/worker-vesnar.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a Vesnar worker — green tunic with deep brown apron,
sleeves rolled, axe with a curved haft, satchel of seedlings on the
back, leather boots, planting stance with one foot forward, peaceful
expression, isolated on plain white background, no shadow on the
floor, no text, full body visible.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #49 — Worker (Thalassia)

**File:** `apps/web/public/units/worker-thalassia.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a Thalassia worker — sea-teal short tunic, silver-belt
apron, rolled scrolls in hand, surveyor's stake in the other, sandals
over wrappings, surveying stance, contemplative expression, isolated
on plain white background, no shadow on the floor, no text, full body
visible.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #50 — Worker (Kyron)

**File:** `apps/web/public/units/worker-kyron.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a Kyron worker — violet short tunic with gold cuffs,
fur cloak draped on one shoulder, pickaxe over shoulder, leather
gauntlets, riding boots, hauling-stance with weight on the back foot,
hardy expression, isolated on plain white background, no shadow on the
floor, no text, full body visible.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

---

# SECTION 4 — City buildings (4 factions × 4 stages)

Cities grow from a hamlet (1 small house) to a capital (walled keep + multiple buildings + faction banner). Each faction has its own architectural identity. Top-down isometric view, 3/4 perspective, square aspect.

## 4.1 Aurei (sun-forged caravans)

### #51 — Aurei hamlet

**File:** `apps/web/public/cities/aurei-1-hamlet.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of a tiny
Aurei hamlet — a single round terracotta-roofed cottage with whitewashed
walls, a small kitchen garden, a copper-orange pennant on a pole, set on
a small grass platform, isolated on plain white background, no shadow
on the floor, no text, no characters, square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.
```

### #52 — Aurei town

**File:** `apps/web/public/cities/aurei-2-town.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of a small
Aurei town — three terracotta-roofed cottages clustered around a
central well, a small wooden granary, a copper-orange pennant on a
central pole, dirt paths winding between, isolated on plain white
background, no shadow on the floor, no text, no characters, square 1:1
aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.
```

### #53 — Aurei city

**File:** `apps/web/public/cities/aurei-3-city.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of an Aurei
city — a central stone keep with a copper-roofed tower, surrounded by
five or six terracotta-roofed cottages, a small marketplace with
colored awnings, a granary, a smithy with a thin smoke plume, paved
inner road, copper-orange banners on poles, isolated on plain white
background, no shadow on the floor, no text, no characters, square 1:1
aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.
```

### #54 — Aurei capital

**File:** `apps/web/public/cities/aurei-4-capital.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of the
Aurei capital — a walled hilltop city, central stone keep with a tall
copper-roofed tower flying a great copper-orange banner with a sun
emblem, multiple terracotta-roofed houses inside the walls, a temple
with ivory columns, a marketplace, gatehouse with twin towers, paved
streets, isolated on plain white background, no shadow on the floor,
no text, no characters, square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.
```

## 4.2 Vesnar (forest-keepers)

### #55 — Vesnar hamlet

**File:** `apps/web/public/cities/vesnar-1-hamlet.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of a tiny
Vesnar hamlet — a single timber longhouse with a moss-grown thatched
roof, carved wooden totem pole beside the door, vegetable patches in
the back, a small green pennant on a pole, set on a forest clearing
platform, isolated on plain white background, no shadow on the floor,
no text, no characters, square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.
```

### #56 — Vesnar town

**File:** `apps/web/public/cities/vesnar-2-town.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of a small
Vesnar town — three timber longhouses with moss-thatched roofs around
a central council fire ring, a small smokehouse, totem pole, woven
wicker fences, footpaths through ferns, green pennant, isolated on
plain white background, no shadow on the floor, no text, no characters,
square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.
```

### #57 — Vesnar city

**File:** `apps/web/public/cities/vesnar-3-city.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of a Vesnar
city — a great timber hall raised on stone foundations with carved
beam ends, surrounded by five or six longhouses with moss-thatched
roofs, a council fire ring, a forge inside a half-timbered shed,
totems, woven palisade, footpaths through wildflowers, deep green
banners, isolated on plain white background, no shadow on the floor,
no text, no characters, square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.
```

### #58 — Vesnar capital

**File:** `apps/web/public/cities/vesnar-4-capital.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of the
Vesnar capital — a great walled forest stronghold, central timber hall
on a stone plinth flying a great deep-green banner with a leaf-and-tree
emblem, multiple longhouses with moss-thatched roofs, a sacred grove
inside the walls, totem-flanked gatehouse, woven palisade with watch
platforms, footpaths winding through flowering shrubs, isolated on
plain white background, no shadow on the floor, no text, no characters,
square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.
```

## 4.3 Thalassia (coastal scribes)

### #59 — Thalassia hamlet

**File:** `apps/web/public/cities/thalassia-1-hamlet.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of a tiny
Thalassia hamlet — a single white-stuccoed cottage with a sea-teal
tile roof, a small herb garden, a stone well, a teal pennant with
silver trim, set on a small platform overlooking a faint hint of
coastline at the edge, isolated on plain white background, no shadow
on the floor, no text, no characters, square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.
```

### #60 — Thalassia town

**File:** `apps/web/public/cities/thalassia-2-town.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of a small
Thalassia town — three white-stuccoed cottages with sea-teal tile
roofs around a paved plaza with a fountain, a low-domed library
building, a stone well, a small dock at the edge, teal-and-silver
pennant, isolated on plain white background, no shadow on the floor,
no text, no characters, square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.
```

### #61 — Thalassia city

**File:** `apps/web/public/cities/thalassia-3-city.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of a
Thalassia city — a domed marble library at the center, surrounded by
five or six white-stuccoed houses with sea-teal tile roofs, a paved
plaza with a fountain, a small harbor with two moored boats at the
edge, a temple with slender silver-leafed columns, a sea-teal banner
with a quill-and-wave emblem, isolated on plain white background, no
shadow on the floor, no text, no characters, square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.
```

### #62 — Thalassia capital

**File:** `apps/web/public/cities/thalassia-4-capital.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of the
Thalassia capital — a walled coastal city, the great domed library at
the heart with a tall silver spire flying a vast sea-teal banner with
silver quill-and-wave emblem, multiple white-stuccoed houses, a
columned forum, an academy with reading-arcades, a deepwater harbor
with three moored ships at the edge, paved streets, gatehouse with
slim towers, isolated on plain white background, no shadow on the
floor, no text, no characters, square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.
```

## 4.4 Kyron (hill-riders)

### #63 — Kyron hamlet

**File:** `apps/web/public/cities/kyron-1-hamlet.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of a tiny
Kyron hamlet — a single round felt yurt with a violet-and-gold trim
banner, a fenced-off horse paddock with one tied horse, a stone fire
ring, set on a low grassy hill, isolated on plain white background,
no shadow on the floor, no text, no characters, square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.
```

### #64 — Kyron town

**File:** `apps/web/public/cities/kyron-2-town.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of a small
Kyron town — three felt yurts arranged around a central horse paddock
with several horses, a wooden archery range, a smoked-meat rack, a
violet-and-gold pennant on a tall pole, footpaths between, set on a
low grassy hill, isolated on plain white background, no shadow on the
floor, no text, no characters, square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.
```

### #65 — Kyron city

**File:** `apps/web/public/cities/kyron-3-city.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of a Kyron
city — a great chieftain's yurt with a violet-and-gold trim and a
brass eagle finial, surrounded by five or six smaller yurts, a large
fenced horse paddock with seven horses, an archery training ground, a
forge under an open shelter, lookout post, footpaths, multiple
violet-and-gold pennants, set on a hilltop, isolated on plain white
background, no shadow on the floor, no text, no characters, square 1:1
aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.
```

### #66 — Kyron capital

**File:** `apps/web/public/cities/kyron-4-capital.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of the
Kyron capital — a fortified hill camp with stone-and-timber walls, the
great chieftain's yurt at the heart with a tall pole flying a vast
violet-and-gold banner with a charging-stallion emblem, multiple
yurts inside the walls, a stable longhouse with eight horses, an
archery training ground, a war forge, lookout towers along the wall,
gatehouse with iron-banded doors, isolated on plain white background,
no shadow on the floor, no text, no characters, square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.
```

---

# SECTION 5 — Faction emblems and banners

Used in the HUD top bar, faction selection screen, and as overlay marks on units / cities. Drawn as **flat circular emblems** so they read at any size.

### #67 — Aurei sun emblem

**File:** `apps/web/public/factions/aurei-emblem.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A circular faction emblem on a
plain white background — a stylized painterly sun with eight rays in
copper-orange and ivory, a single radiant eye at the center, framed by
a thin gold border ring, no text, no shadow.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #68 — Aurei banner

**File:** `apps/web/public/factions/aurei-banner.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A vertical hanging cloth banner —
copper-orange field with ivory border, the Aurei sun emblem painted
in gold at the center, frayed bottom edge, soft fabric folds catching
light, isolated on plain white background, no shadow on the floor, no
text, no flagpole.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #69 — Vesnar leaf emblem

**File:** `apps/web/public/factions/vesnar-emblem.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A circular faction emblem on a
plain white background — a stylized painterly oak leaf in deep forest
green with golden veins, a small acorn at the leaf's base, framed by
a thin brown border ring, no text, no shadow.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #70 — Vesnar banner

**File:** `apps/web/public/factions/vesnar-banner.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A vertical hanging cloth banner —
deep forest-green field with brown border, the Vesnar oak-leaf emblem
painted in gold at the center, woven texture visible, soft fabric
folds catching light, isolated on plain white background, no shadow on
the floor, no text, no flagpole.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #71 — Thalassia quill emblem

**File:** `apps/web/public/factions/thalassia-emblem.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A circular faction emblem on a
plain white background — a stylized painterly quill pen overlaid on a
cresting wave, both in silver, on a sea-teal field, framed by a thin
silver border ring, no text, no shadow.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #72 — Thalassia banner

**File:** `apps/web/public/factions/thalassia-banner.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A vertical hanging cloth banner —
sea-teal field with silver trim, the Thalassia quill-and-wave emblem
in silver at the center, fine silk texture, soft fabric folds catching
light, isolated on plain white background, no shadow on the floor, no
text, no flagpole.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #73 — Kyron stallion emblem

**File:** `apps/web/public/factions/kyron-emblem.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A circular faction emblem on a
plain white background — a stylized painterly silhouette of a charging
stallion in gold against a violet field, mane streaming back, framed
by a thin gold border ring, no text, no shadow.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #74 — Kyron banner

**File:** `apps/web/public/factions/kyron-banner.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A vertical hanging cloth banner —
violet field with gold trim, the Kyron golden stallion emblem
prominently at the center, slightly weathered fabric texture with
campaign wear, soft fabric folds catching light, isolated on plain
white background, no shadow on the floor, no text, no flagpole.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

---

# SECTION 6 — Tech tree icons

Square painterly icons; the HUD frames them in stone-and-gold borders. White background, run through remove.bg.

### #75 — Agriculture

**File:** `apps/web/public/tech/agriculture.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of an
agricultural scythe leaning against a bound bundle of golden wheat, a
simple wooden plow handle visible beside, painterly soft sun, isolated
on plain white background, no shadow on the floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #76 — Bronze working

**File:** `apps/web/public/tech/bronze_working.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of a
smith's anvil with a hot bronze ingot resting on it, a hammer leaning
against the side, glowing forge embers in the background mist,
isolated on plain white background, no shadow on the floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #77 — The wheel

**File:** `apps/web/public/tech/the_wheel.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of a
sturdy wooden wagon wheel propped upright on a dirt path, faint cart
ruts trailing into the corners, painterly afternoon sun, isolated on
plain white background, no shadow on the floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #78 — Horseback riding

**File:** `apps/web/public/tech/horseback_riding.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of a
prancing horse silhouette in front of a sun-warm hill, a cinched
saddle with reins draped, painterly grass tufts at the base, isolated
on plain white background, no shadow on the floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #79 — Masonry

**File:** `apps/web/public/tech/masonry.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of a
stack of cut grey stone blocks with a stonemason's chisel and mallet
leaning against them, faint chip dust motes in the air, isolated on
plain white background, no shadow on the floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #80 — Writing

**File:** `apps/web/public/tech/writing.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of an
open scroll with painted ink markings and a quill pen, an ink pot
beside, a leather book in the background, soft library glow, isolated
on plain white background, no shadow on the floor, no text inside the
illustration itself (only the painted-on scroll is fine).

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #81 — Mathematics

**File:** `apps/web/public/tech/mathematics.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of a
brass astrolabe leaning against a stack of geometry-proof scrolls, a
pair of dividers across them, painterly twilight blue tint, isolated
on plain white background, no shadow on the floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #82 — Currency

**File:** `apps/web/public/tech/currency.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of a
small leather coin pouch spilling three or four gold coins onto a
wooden countertop, a bronze scale of weights visible behind, painterly
warm market light, isolated on plain white background, no shadow on
the floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

---

# SECTION 7 — UI glyphs

Generic icons used in the HUD: action buttons, status indicators, notifications. Square 1:1 aspect, isolated on white.

### #83 — Action: Move

**File:** `apps/web/public/ui/action-move.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of a pair
of stylized boot footprints in fresh dirt heading forward, painterly
dust kick at the back foot, isolated on plain white background, no
shadow on the floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #84 — Action: Attack

**File:** `apps/web/public/ui/action-attack.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of two
crossed straight swords with a small painterly impact spark at the
crossing point, weathered blades, leather-wrapped grips, isolated on
plain white background, no shadow on the floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #85 — Action: Settle

**File:** `apps/web/public/ui/action-settle.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of a
small wooden tent stake driven into the ground next to a simple
campfire and a folded canvas, painterly evening sun, isolated on plain
white background, no shadow on the floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #86 — Action: End turn

**File:** `apps/web/public/ui/action-end-turn.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of an
hourglass tipped on its side with the last grains of golden sand
falling, a small painterly star spark, isolated on plain white
background, no shadow on the floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #87 — Stat: HP

**File:** `apps/web/public/ui/stat-hp.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of a
small heart-shaped red gem set in a brass frame, painterly sparkle on
the gem, isolated on plain white background, no shadow on the floor,
no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #88 — Stat: ATK

**File:** `apps/web/public/ui/stat-atk.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of a
single straight-bladed sword tip pointed up with a small ember spark
at the point, weathered grip, isolated on plain white background, no
shadow on the floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #89 — Stat: DEF

**File:** `apps/web/public/ui/stat-def.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of a
small kite shield with brass rivets and a faint cross emblem, faintly
worn from use, isolated on plain white background, no shadow on the
floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #90 — Stat: SPD

**File:** `apps/web/public/ui/stat-spd.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of a
single feathered arrow in flight with painterly motion lines trailing,
isolated on plain white background, no shadow on the floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #91 — Stat: SCIENCE

**File:** `apps/web/public/ui/stat-science.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of an
open book with a small candle's flame casting warm light on the
parchment pages, a quill resting in the gutter, isolated on plain
white background, no shadow on the floor, no text on the book itself
(blank parchment is fine).

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #92 — Stat: GOLD

**File:** `apps/web/public/ui/stat-gold.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of three
gold coins stacked unevenly with a single coin tilted on top, brass
warm glow, isolated on plain white background, no shadow on the floor,
no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #93 — Stat: FOOD

**File:** `apps/web/public/ui/stat-food.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of a
loaf of crusty rustic bread next to a wedge of cheese, painterly warm
kitchen light, isolated on plain white background, no shadow on the
floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #94 — Stat: PRODUCTION

**File:** `apps/web/public/ui/stat-production.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of a
blacksmith's hammer crossed over a pair of tongs, painterly forge
glow behind, isolated on plain white background, no shadow on the
floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #95 — Indicator: Crit

**File:** `apps/web/public/ui/indicator-crit.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of a
spiky golden sunburst star with a small impact center dot, dramatic
painterly highlight, isolated on plain white background, no shadow on
the floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #96 — Indicator: Selection ring

**File:** `apps/web/public/ui/indicator-selection.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of a
hexagonal ring outline rendered in glowing copper-orange runes, faint
inner glow, runic detail along the ring, isolated on plain white
background, no shadow on the floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #97 — Indicator: Reachable

**File:** `apps/web/public/ui/indicator-reachable.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of a
hexagonal ring outline rendered in glowing teal mist, soft inner haze,
faint sparkles, isolated on plain white background, no shadow on the
floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #98 — Indicator: Acted (faded)

**File:** `apps/web/public/ui/indicator-acted.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of a
small simple Z-shaped sleep glyph in dark slate, soft sleepy haze
around it, isolated on plain white background, no shadow on the floor,
no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #99 — Notification: Tech discovered

**File:** `apps/web/public/ui/notif-tech.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of a
glowing scroll being unrolled with a soft golden discovery sparkle
above it, isolated on plain white background, no shadow on the floor,
no text on the scroll.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #100 — Notification: City founded

**File:** `apps/web/public/ui/notif-city.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of a
small house silhouette beside a planted banner pole, a bright sunburst
behind, isolated on plain white background, no shadow on the floor, no
text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #101 — Notification: City captured

**File:** `apps/web/public/ui/notif-capture.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of a
banner pole tipping over while a torch burns next to it, painterly
embers in the air, isolated on plain white background, no shadow on
the floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #102 — Notification: Combat won

**File:** `apps/web/public/ui/notif-combat-win.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of a
laurel wreath with a pair of crossed swords behind, painterly gold
glow, isolated on plain white background, no shadow on the floor, no
text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #103 — Notification: Combat lost

**File:** `apps/web/public/ui/notif-combat-loss.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of a
broken sword half buried in muddy ground, painterly grey ash drifting,
isolated on plain white background, no shadow on the floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

---

# SECTION 8 — Combat & movement effects

Translucent overlays that appear briefly during combat or movement. Generate as **single transparent-PNG bursts** so the renderer can composite them at any tile.

### #104 — Sword slash

**File:** `apps/web/public/effects/slash.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A single sweeping sword slash
arc rendered as a glowing white-orange streak with painterly speed
trails, motion-blurred, no characters, no weapon, just the slash
energy itself, isolated on plain white background, no shadow on the
floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #105 — Arrow flight trail

**File:** `apps/web/public/effects/arrow-trail.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A long horizontal arrow with a
painterly speed trail behind it, fletching streaked with motion blur,
faint copper-orange highlight on the head, isolated on plain white
background, no shadow on the floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #106 — Hit impact

**File:** `apps/web/public/effects/hit.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A burst of three or four spike-
shaped painterly red shards radiating from a central impact point,
small dust puff at the base, isolated on plain white background, no
shadow on the floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #107 — Crit burst

**File:** `apps/web/public/effects/crit-burst.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: An explosive painterly golden
star-shaped burst with eight long rays, white-hot center, lightning-
fork edges, isolated on plain white background, no shadow on the floor,
no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #108 — Smoke puff

**File:** `apps/web/public/effects/smoke.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly soft grey-white
smoke puff with curling tendrils, slightly translucent edges, isolated
on plain white background, no shadow on the floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #109 — Dust kick

**File:** `apps/web/public/effects/dust.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly low-rolling dust
cloud kicked up by movement, warm sandy beige with painterly highlight
edges, isolated on plain white background, no shadow on the floor, no
text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #110 — Discovery sparkle

**File:** `apps/web/public/effects/discovery.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly soft cluster of
golden-white sparkles with two larger central stars, faint magic glow,
isolated on plain white background, no shadow on the floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #111 — Movement footstep ghost

**File:** `apps/web/public/effects/footstep.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly faint pair of boot
prints in soft earth, slight motion-blur on the rearmost print,
isolated on plain white background, no shadow on the floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #112 — Heal pulse

**File:** `apps/web/public/effects/heal.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly soft white-green
healing pulse, ring expanding outward with faint petals or leaves
floating up, isolated on plain white background, no shadow on the
floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #113 — Capital banner unfurl

**File:** `apps/web/public/effects/banner.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly large flag mid-
unfurl with mid-air ripple, viewed from a 3/4 angle, generic blank
fabric (no faction colors — engine will tint), isolated on plain
white background, no shadow on the floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

---

# SECTION 9 — Splash, faction portraits, endgame

Big set-piece illustrations that appear once: title screen, faction picker portraits, victory / defeat / draw screens.

### #114 — Title screen background

**File:** `apps/web/public/splash/title.jpg`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A wide cinematic painted scene of
a high mountain meadow at golden hour, four distant hilltop banners
in copper-orange, forest-green, sea-teal, and violet visible across the
horizon, soft volumetric sunbeams between distant peaks, no characters,
no text, 16:9 wide aspect, sense of an empire being founded in the
gold of dawn.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.
```

### #115 — Faction portrait — Aurei

**File:** `apps/web/public/splash/portrait-aurei.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A waist-up character portrait of
the Aurei faction leader — a regal middle-aged ruler in copper-orange
robes with ivory trim, gold sun-shaped circlet, gentle confident
expression, distant sun-lit plains visible behind through a soft
window, painterly warm light on the face, isolated against a soft
neutral background, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.
```

### #116 — Faction portrait — Vesnar

**File:** `apps/web/public/splash/portrait-vesnar.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A waist-up character portrait of
the Vesnar faction leader — a quiet weathered woodland matriarch in
forest-green robes with deep brown trim, antler-crown headdress, gentle
unwavering gaze, soft forest light filtering through window-leaves
behind her, painterly cool-warm light on the face, isolated against a
soft neutral background, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.
```

### #117 — Faction portrait — Thalassia

**File:** `apps/web/public/splash/portrait-thalassia.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A waist-up character portrait of
the Thalassia faction leader — a calm scholarly figure in sea-teal
robes with silver embroidery, silver circlet, holding an open book,
distant sea-port visible through a window arch behind, painterly
silvery cool light on the face, isolated against a soft neutral
background, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.
```

### #118 — Faction portrait — Kyron

**File:** `apps/web/public/splash/portrait-kyron.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A waist-up character portrait of
the Kyron faction leader — a battle-tested chieftain in violet
lamellar with gold trim, fur shoulder cloak, gold horse-emblem brooch,
weathered determined expression, distant hilltop encampment with horse
banners visible behind, painterly dramatic warm-evening light on the
face, isolated against a soft neutral background, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.
```

### #119 — Endgame: Victory (domination)

**File:** `apps/web/public/splash/end-domination.jpg`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A wide cinematic painted scene
of a victorious army's banner planted on a windswept hilltop at sunrise,
distant cities visible across a peaceful land below, painterly golden
volumetric sunbeams, no faces visible (cropped or silhouetted), 16:9
wide aspect, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.
```

### #120 — Endgame: Victory (score)

**File:** `apps/web/public/splash/end-score.jpg`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A wide cinematic painted scene of
a thriving prosperous capital city at sunset, painterly market stalls,
distant farmlands and ports, scrolls and gold piled on a foreground
table, gentle golden hour atmosphere, no characters, 16:9 wide aspect,
no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.
```

### #121 — Endgame: Defeat

**File:** `apps/web/public/splash/end-defeat.jpg`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A wide cinematic painted scene
of a fallen city at dusk, broken banner flapping in the wind, distant
smoke rising from rooftops, painterly heavy clouds with a single
break of warm light, no characters, 16:9 wide aspect, melancholic but
not gory, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.
```

### #122 — Endgame: Draw

**File:** `apps/web/public/splash/end-draw.jpg`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A wide cinematic painted scene of
two facing armies' banners across a still misty valley at dawn, neither
side advancing, peaceful tension, painterly silver-blue light, no
characters, 16:9 wide aspect, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.
```

### #123 — Loading screen vignette

**File:** `apps/web/public/splash/loading.jpg`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A wide cinematic painted scene of
an unrolling old hand-drawn world map on a wooden desk, an inkpot and
quill at the corner, a brass compass beside, painterly warm candle
light, 16:9 wide aspect, no text on the map, no characters.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.
```

---

# SECTION 10 — Decorative tile variants (optional polish)

Special tile illustrations that appear rarely on the map for "discovery moments." Generate sparingly; the renderer will pick them with low probability.

### #124 — Plain with deer herd

**File:** `apps/web/public/biomes/plain-deer.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of an
open grassland with a small herd of three brown deer grazing, soft
green grass with wildflowers, painterly afternoon shadows, no people,
no buildings, square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

### #125 — Forest with wolf pack

**File:** `apps/web/public/biomes/forest-wolves.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
clearing in a pine forest with a small pack of three grey wolves
moving silently between trees, painterly cool light, dappled shadows,
no people, no buildings, square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

### #126 — Forest with ancient ruin

**File:** `apps/web/public/biomes/forest-ruin.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
forest clearing containing a circle of crumbling moss-covered stone
ruins half-claimed by vines, painterly forgotten atmosphere, no
people, no active buildings, square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

### #127 — Mountain with eagle nest

**File:** `apps/web/public/biomes/mountain-eagle.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
mountain peak with an eagle in flight circling above the rocky summit,
painterly cool shadows, no people, no buildings, square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

### #128 — Water with whale

**File:** `apps/web/public/biomes/water-whale.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of deep
ocean with a great whale's silhouette just below the surface, painterly
ripple ring around its blowhole, no boats, no people, square 1:1
aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

### #129 — Water with sailing ship

**File:** `apps/web/public/biomes/water-ship.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of open
water with a single small wooden sailing ship cutting across, painterly
foam wake behind, white sail catching wind, no characters visible,
square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

### #130 — Desert with caravan trail

**File:** `apps/web/public/biomes/desert-caravan.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of dunes
with a long line of camel hoof prints curving across them, painterly
afternoon shadows, no people, no caravan visible (just the trail),
square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

### #131 — Desert with ruin

**File:** `apps/web/public/biomes/desert-ruin.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of dunes
with a half-buried ruined column and broken stone tiles, painterly
sand drifts encroaching, no people, no active buildings, square 1:1
aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

### #132 — Tundra with caribou

**File:** `apps/web/public/biomes/tundra-caribou.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of an
arctic snowfield with a small group of three caribou grazing in
patches of exposed grass, painterly cold blue shadows, no people,
no buildings, square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

### #133 — Tundra with frozen wreck

**File:** `apps/web/public/biomes/tundra-wreck.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of an
arctic snowfield with a half-buried abandoned ship's prow protruding
from the ice at an angle, painterly icy haze around it, no people,
no active buildings, square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

### #134 — Hill with windmill

**File:** `apps/web/public/biomes/hill-windmill.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
green rolling hill with a small white-and-orange windmill on its crest,
painterly vanes catching the light, no people, no other buildings,
square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

### #135 — Hill with shrine

**File:** `apps/web/public/biomes/hill-shrine.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a hill
crest with a tiny stone shrine — three pillars under a triangular roof,
a few wildflowers around it, painterly soft sun, no people, no other
buildings, square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

### #136 — Plain with stone circle

**File:** `apps/web/public/biomes/plain-circle.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of an
open plain with a small ancient stone circle of seven monoliths,
weathered grey stone, faint moss, painterly mid-afternoon shadows,
no people, no buildings, square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

### #137 — Plain with road

**File:** `apps/web/public/biomes/plain-road.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of an
open plain with a packed dirt road curving from one corner to the
opposite, faint cart tracks down its center, painterly sun, no
people, no buildings, square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

### #138 — Forest with hermit cottage

**File:** `apps/web/public/biomes/forest-hermit.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
small clearing in a deep pine forest with a single tiny moss-roofed
hermit's cottage and a thin smoke plume from its chimney, painterly
quiet atmosphere, no people visible, square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

---

# SECTION 11 — Polish + framing

Decorative UI frames, dialog boxes, panel borders. These dress the React HUD around the canvas.

### #139 — HUD top bar parchment

**File:** `apps/web/public/ui/hud-bar.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A horizontal painterly parchment
strip with weathered edges and faint warm staining, suitable as a HUD
background bar, no text, no decorative elements, plain centered
parchment band, isolated on plain white background, no shadow on the
floor.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #140 — Panel frame ornament

**File:** `apps/web/public/ui/panel-frame.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly ornamental
gold filigree corner — a quarter-turn flourish suitable for placing at
the corner of a UI panel, painterly metallic shine, isolated on plain
white background, no shadow on the floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #141 — Tooltip background

**File:** `apps/web/public/ui/tooltip.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A small painterly weathered
parchment tooltip card shape, slightly torn lower edge, soft drop
shadow, isolated on plain white background, no shadow on the floor, no
text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #142 — Button frame: Action

**File:** `apps/web/public/ui/btn-action.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A horizontal painterly button
shape, polished bronze frame with a sun-orange interior, gently
beveled edges, no text, isolated on plain white background, no shadow
on the floor.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #143 — Button frame: End turn

**File:** `apps/web/public/ui/btn-end-turn.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A horizontal painterly button
shape, deep teal-and-silver frame with a midnight-blue interior, faint
star pattern inside, no text, isolated on plain white background, no
shadow on the floor.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #144 — Mini-map frame

**File:** `apps/web/public/ui/minimap-frame.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A small square painterly frame
suitable for a mini-map widget, weathered bronze edge with rivets at
each corner, hollow center, no text, isolated on plain white
background, no shadow on the floor.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #145 — Health bar frame

**File:** `apps/web/public/ui/hp-frame.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A horizontal narrow painterly
frame for a health bar, bronze-edged with empty interior, suitable for
overlaying on top of unit sprites, no text, isolated on plain white
background, no shadow on the floor.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

---

# SECTION 12 — Wildlife & ambient creatures (drop on plain/forest tiles)

Small creatures rendered as ~64px painterly icons. Drop them at low probability on appropriate biomes for atmosphere.

### #146 — Stag

**File:** `apps/web/public/wildlife/stag.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A small painterly stag with full
antlers standing alert, brown-red coat, viewed from a 3/4 angle,
isolated on plain white background, no shadow on the floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #147 — Fox

**File:** `apps/web/public/wildlife/fox.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A small painterly red fox in a
crouching ready posture, fluffy tail curved, white chest, sharp eyes,
viewed from a 3/4 angle, isolated on plain white background, no shadow
on the floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #148 — Eagle

**File:** `apps/web/public/wildlife/eagle.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A small painterly golden eagle
in flight, wings fully spread, sharp talons trailing, viewed from
slightly above the bird, isolated on plain white background, no shadow
on the floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #149 — Wolf

**File:** `apps/web/public/wildlife/wolf.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A small painterly grey wolf,
shoulders raised, gold eyes, mid-pace forward step, viewed from a 3/4
angle, isolated on plain white background, no shadow on the floor, no
text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #150 — Bear

**File:** `apps/web/public/wildlife/bear.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A small painterly brown bear on
all fours, alert head raised, thick winter coat, viewed from a 3/4
angle, isolated on plain white background, no shadow on the floor, no
text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #151 — Boar

**File:** `apps/web/public/wildlife/boar.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A small painterly wild boar with
short tusks, bristly back, low charging stance, dark-brown hide,
viewed from a 3/4 angle, isolated on plain white background, no shadow
on the floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #152 — Hawk

**File:** `apps/web/public/wildlife/hawk.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A small painterly hawk perched
on a wooden branch, wings folded, sharp profile view, viewed from a
slight 3/4 angle, isolated on plain white background, no shadow on the
floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #153 — Camel

**File:** `apps/web/public/wildlife/camel.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A small painterly dromedary
camel walking forward, sandy beige coat, viewed from a 3/4 angle,
isolated on plain white background, no shadow on the floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #154 — Whale (surface breach)

**File:** `apps/web/public/wildlife/whale.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A small painterly humpback whale
breaching the ocean surface, water splash around it, viewed from a 3/4
angle slightly above, isolated on plain white background, no shadow on
the floor, no text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #155 — Fish school (water)

**File:** `apps/web/public/wildlife/fish.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A small painterly cluster of
silver-flashing fish swimming together near the surface, viewed from
above, isolated on plain white background, no shadow on the floor, no
text.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

---

# SECTION 13 — Tile improvements (built by Workers)

When workers improve a tile (farm, mine, road, lumber camp), the tile gets a small structure on top.

### #156 — Farm

**File:** `apps/web/public/improvements/farm.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A small painterly farm
improvement — a wooden field fence with a haystack, a scarecrow, and
two rows of wheat, isolated on plain white background, no shadow on
the floor, no text, no characters.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #157 — Mine

**File:** `apps/web/public/improvements/mine.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A small painterly mine
improvement — a wooden mineshaft entry built into a rocky face, an
iron-banded cart on rails, a pickaxe leaning by the entrance, isolated
on plain white background, no shadow on the floor, no text, no
characters.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #158 — Lumber camp

**File:** `apps/web/public/improvements/lumber.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A small painterly lumber camp
improvement — a stack of cut logs, an axe in a stump, a small open
shed with a workbench, isolated on plain white background, no shadow
on the floor, no text, no characters.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #159 — Pasture

**File:** `apps/web/public/improvements/pasture.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A small painterly pasture
improvement — a wooden corral fence with three brown horses inside, a
trough at one corner, isolated on plain white background, no shadow on
the floor, no text, no characters.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #160 — Road segment

**File:** `apps/web/public/improvements/road.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A small painterly straight cobble
road segment running edge to edge, light grass at each side, painterly
warm sun, isolated on plain white background, no shadow on the floor,
no text, no characters.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #161 — Watchtower

**File:** `apps/web/public/improvements/watchtower.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A small painterly stone
watchtower with a wooden roof and a small lantern at the top, viewed
from a 3/4 angle, isolated on plain white background, no shadow on the
floor, no text, no characters.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #162 — Fishing wharf

**File:** `apps/web/public/improvements/wharf.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A small painterly fishing wharf
improvement — a short wooden dock with two moored fishing skiffs, nets
hung to dry, viewed from a 3/4 angle, isolated on plain white
background, no shadow on the floor, no text, no characters.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #163 — Trade post

**File:** `apps/web/public/improvements/trade-post.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A small painterly trade post
improvement — a striped tent over a wooden counter with crates
stacked beside, a balance scale on the counter, viewed from a 3/4
angle, isolated on plain white background, no shadow on the floor, no
text, no characters.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

---

# SECTION 14 — Tile-edge transitions (optional polish)

These are stitching pieces that smooth biome boundaries. Generate only if you want true painterly continuity. Each is a square edge tile with a specific transition.

### #164 — Plain → Forest edge

**File:** `apps/web/public/biomes/edges/plain-to-forest.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view showing
the boundary between open grassland on the left half and pine forest
on the right half, painterly transition with a few scattered young
trees in the middle, no people, no buildings, square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

### #165 — Plain → Water edge

**File:** `apps/web/public/biomes/edges/plain-to-water.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view showing
the boundary between green grassland on the left half and lake water
on the right half, sandy beach edge between them with reed clumps,
no people, no buildings, square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

### #166 — Plain → Desert edge

**File:** `apps/web/public/biomes/edges/plain-to-desert.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view showing
the boundary between green grassland on the left half and golden
desert on the right half, painterly transition with sun-baked yellow
grass blending into sand, no people, no buildings, square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

### #167 — Hill → Mountain edge

**File:** `apps/web/public/biomes/edges/hill-to-mountain.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view showing
the boundary between rolling green hills on the left half and rocky
mountain peaks on the right half, painterly transition with bare rock
breaking through grass, no people, no buildings, square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

### #168 — Forest → Tundra edge

**File:** `apps/web/public/biomes/edges/forest-to-tundra.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view showing
the boundary between dense pine forest on the left half and arctic
snowfield on the right half, painterly transition with snow-laden
branches and patches of bare snow, no people, no buildings, square
1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

---

# SECTION 15 — Day/Night variants (long-term polish)

If you want a day/night cycle, regenerate the 7 biomes at night with cool palette. Optional, ~15 prompts. Only generate after the daytime set is locked.

### #169 — Plain (night)

**File:** `apps/web/public/biomes/night/plain.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of an
open grassland at night, painterly cool moonlight on the grass, faint
white wildflowers glowing, fireflies in the air, no people, no
buildings, square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft cool moonlight volumetric lighting, gentle
dramatic atmosphere, clean linework, cohesive cool palette of deep
blues, indigos, painterly silver highlights.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

### #170 — Forest (night)

**File:** `apps/web/public/biomes/night/forest.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
pine forest canopy at night, painterly moonlight breaking through
between trees, faint glowing mushroom patches, no people, no
buildings, square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft cool moonlight volumetric lighting, gentle
dramatic atmosphere, clean linework, cohesive cool palette of deep
blues, indigos, painterly silver highlights.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

### #171 — Hill (night)

**File:** `apps/web/public/biomes/night/hill.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of rolling
hills at night, painterly moonlight casting long blue shadows down the
slopes, sparse silvered grass, no people, no buildings, square 1:1
aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft cool moonlight volumetric lighting, gentle
dramatic atmosphere, clean linework, cohesive cool palette of deep
blues, indigos, painterly silver highlights.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

### #172 — Mountain (night)

**File:** `apps/web/public/biomes/night/mountain.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of rocky
mountain peaks at night, painterly moonlight on the snow-caps, deep
blue shadows in crevices, no people, no buildings, square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft cool moonlight volumetric lighting, gentle
dramatic atmosphere, clean linework, cohesive cool palette of deep
blues, indigos, painterly silver highlights.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

### #173 — Water (night)

**File:** `apps/web/public/biomes/night/water.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of deep
ocean at night, painterly indigo waves with silver-white moonlight
reflections, faint phosphorescent ripple highlights, no land, no boats,
square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft cool moonlight volumetric lighting, gentle
dramatic atmosphere, clean linework, cohesive cool palette of deep
blues, indigos, painterly silver highlights.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

### #174 — Desert (night)

**File:** `apps/web/public/biomes/night/desert.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of dunes
at night, painterly cool blue sand with deep indigo shadows on dune
ridges, scattered cool moonlight highlights, no people, no buildings,
square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft cool moonlight volumetric lighting, gentle
dramatic atmosphere, clean linework, cohesive cool palette of deep
blues, indigos, painterly silver highlights.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

### #175 — Tundra (night)

**File:** `apps/web/public/biomes/night/tundra.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of an
arctic snowfield under aurora borealis, painterly soft green and
violet light bands across the snow surface, faint long shadows from
distant rocks, no people, no buildings, square 1:1 aspect.

In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft cool moonlight volumetric lighting, gentle
dramatic atmosphere, clean linework, cohesive cool palette of deep
blues, indigos, painterly silver highlights.

Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

---

# Production workflow

1. **Run prompt #0 first** — judge the style anchor. If it doesn't match the painted-Ghibli vibe you want, tweak the master block before generating anything else.
2. **One ChatGPT conversation per section.** Each section in this file is designed to be a single chat: open a fresh chat, paste prompts in order, accept the best of 4 generations per prompt. Style continuity within a section is automatic. Style continuity across sections is automatic if you keep ChatGPT open across days.
3. **Backgrounds.** All "isolated on plain white background" outputs go through [remove.bg](https://www.remove.bg) before saving. Hex tile and splash backgrounds (full scenes) skip the bg removal.
4. **Optimize before commit.** Do the WebP/JPG resize step at the top of this file. The repo should not contain 3 MB raw DALL-E PNGs.
5. **Drop into the right path.** Each prompt has a `**File:**` line — that is the destination relative to repo root.
6. **Wire up `apps/web/src/lib/assets.ts`.** When all assets for a section are in, ping me and I'll generate the asset map + Pixi sprite renderer that consumes them. (The current renderer uses hand-drawn Graphics; switching to your painted assets is a one-day code job.)

# Optional v0.2 expansion (later)

When v0.1 ships and you want richer content:
- 4 seasons × 7 biomes = 28 seasonal tiles
- Per-faction unit variants for archer/horseman in motion poses (charge, shoot, retreat) — 24 more
- Per-faction tile improvements (Aurei farm vs Vesnar farm vs etc.) — 16 more
- Diplomacy portraits (envoy / merchant / general) per faction — 12 more
- Wonder buildings (one per tech, ~8 wonders)
- Weather overlays (rain, snow drift, sandstorm, fog) — 4 more

Total v0.2 add-on: ~90 more prompts. Same recipe, same style block.

---

**That's the full atlas.** Hit `prompt #0` first, judge the style, then march through. With ChatGPT Plus you can produce ~30 prompts per hour comfortably, so the 175-prompt set is roughly a focused weekend of generation.
