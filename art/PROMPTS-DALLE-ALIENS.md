<!-- =============================================================================
File:           art/PROMPTS-DALLE-ALIENS.md
Author:         USDTG GROUP TECHNOLOGY LLC
Developer:      Irfan Gedik
Created Date:   2026-05-04
Last Update:    2026-05-04
Version:        0.3.0

Description:
  Volume III — alien antagonist atlas. Comprehensive DALL-E 3 prompts
  for the only enemies in the USDTG game suite: alien creatures, their
  hive structures, alien-corrupted terrain, combat effects, battle
  cards, threat icons, and a 12-boss rotation for Nyrduel daily mode.

  This volume is THE conflict-side art for every USDTG game. Combat is
  human-versus-alien only — never human-versus-human, never against
  Earth animals. Aliens here are designed as "beautiful-and-dangerous"
  rather than "hideous-and-gross" so play feels strategic, not horror.

License:
  Proprietary. All rights reserved. See LICENSE in the repository root.
============================================================================= -->

# Nyrvexa / Nyrvexis / Nyrduel — Alien Antagonist Atlas (Volume III)

**Why this volume exists.** The studio's hard rule: combat in any USDTG
game is **only** humans defending against alien threats. No human-vs-
human combat, no Earth-animal antagonists, no mechanic that could
generate fear or hatred toward Earth life. This file gives you the full
art set for the alien side of every game.

**~100 prompts** picking up at #348 (where Volume II ended). Combined
with Volumes I + II, the full project has ~448 ready-to-paste prompts.

---

## Style anchor for alien content

The painted Studio-Ghibli world from V1 still applies — alien creatures
are painted in the same style, on the same world, just with a distinct
sub-palette. Use this **alien sub-style block** at the bottom of every
alien prompt **in addition to** the master style block from V1:

```
[ALIEN-STYLE-BLOCK] = (paste verbatim, after the master block)
The creature is unmistakably alien — clearly non-Earth biology, no
anthropomorphic features, no terrestrial-animal mimicry. Beautiful-
and-dangerous rather than gross or gore-laden. Cool palette of deep
violets, electric teals, void blacks, with bioluminescent highlights;
warm-orange accents only on threat indicators (eyes, tendril tips).
Crystalline, energy, or geometric-organic body plans preferred. The
creature reads as a formidable challenge, not a horror.
```

**Reminder of standard blocks** (use as before):

```
[STYLE-BLOCK] = (master Ghibli/painted style sentence)
[CHAR-POST]   = (Centered subject, isolated on plain white background…)
[TILE-POST]   = (Top-down 90-degree birds-eye view…)
```

Each alien prompt below uses **all three**: style, alien-style, and
appropriate post-block.

---

# SECTION 29 — Alien species (6 classes × 4 poses)

Six alien species spanning small swarmers, mid-tier melee, ranged, and
heavy bosses. Each in idle / attacking / hit / defeated poses so the
renderer can sequence them through combat. Faction-agnostic — these
are the universal threat across all USDTG games.

## 29.1 Skitterling (small swarmer)

Small, fast, low-HP, hunts in groups. Crystalline multi-legged body
with a single faint glowing core. Replaces "barbarian unit" archetypes
in any game that previously had small enemy fodder.

### #348 — Skitterling (idle)

**File:** `apps/web/public/aliens/skitterling-idle.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a small alien skitterling — six segmented crystalline
legs in a low spider-like crouch, a smooth violet-glass thorax, a
single faint teal bioluminescent core inside the torso, no head per
se, just three small black sensor pits at the front, gentle pulsing
glow, alert-but-still pose, full body visible.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #349 — Skitterling (attack)

**File:** `apps/web/public/aliens/skitterling-attack.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a skitterling mid-leap, all six legs splayed, the
violet thorax flared open to reveal an inner teal energy mouth, faint
trailing motion arcs, dynamic predatory pose, full body visible.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #350 — Skitterling (hit)

**File:** `apps/web/public/aliens/skitterling-hit.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a skitterling staggered, two legs buckled, the violet
crystalline thorax cracked along one side leaking faint teal light,
sensor pits dimmed, full body visible in unbalanced pose.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #351 — Skitterling (defeated)

**File:** `apps/web/public/aliens/skitterling-defeated.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a skitterling fallen on its side, legs curled
inward, crystalline thorax dim and dark, the bioluminescent core
fading, gentle dissolving particles rising from the body, no gore,
no liquid, no Earth-biology insides — just dimming alien crystal.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

## 29.2 Voidweaver (hovering ranged caster)

Mid-tier, low ground-presence, hovers above the ground. Hurls dark-
energy projectiles. Levitating mass of looping tentacles around a
central glowing eye-orb.

### #352 — Voidweaver (idle)

**File:** `apps/web/public/aliens/voidweaver-idle.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of an alien voidweaver — a hovering levitating cluster
of seven smooth dark-violet tendrils orbiting a single large
slow-blinking teal eye-orb at its center, no body, no legs, faint
gravitational distortion around the orb, calm watchful pose,
suspended in air with wisps of cold mist below.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #353 — Voidweaver (attack)

**File:** `apps/web/public/aliens/voidweaver-attack.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a voidweaver casting — three tendrils thrust forward
together cradling a forming sphere of dark-violet energy with arcing
purple lightning over its surface, the central teal eye narrowed and
brightened, faint motion lines, dynamic ranged-attack pose.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #354 — Voidweaver (hit)

**File:** `apps/web/public/aliens/voidweaver-hit.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a voidweaver recoiling, tendrils flung outward in
disorder, the central eye-orb half-shut, faint cracks in its surface
leaking thin teal mist, painterly painterly motion shock pose.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #355 — Voidweaver (defeated)

**File:** `apps/web/public/aliens/voidweaver-defeated.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a voidweaver collapsed to the ground, tendrils
draped limply, the central eye-orb fully dark with hairline cracks,
faint dispersing teal vapor rising, no liquid, no gore.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

## 29.3 Hivekin (insectoid drone, melee)

Mid-tier melee. Bipedal-but-not-humanoid — walks on two thick chitin
legs, has multiple pairs of grasping limbs, no recognizable head, just
a forward-faced clustered eye-band. Faceless rather than monstrous.

### #356 — Hivekin (idle)

**File:** `apps/web/public/aliens/hivekin-idle.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of an alien hivekin drone — two thick black-chitin
digitigrade legs, a low-slung segmented body, four grasping limbs at
the torso (two ending in scythed blades, two in delicate grasper
hooks), no head — instead a forward-facing band of seven small
glowing teal eye-clusters, alert wary pose, full body visible.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #357 — Hivekin (attack)

**File:** `apps/web/public/aliens/hivekin-attack.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a hivekin mid-strike, both scythed front limbs
sweeping forward together, body torqued with momentum, the eye-band
bright and focused, painterly motion arc trailing the blades.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #358 — Hivekin (hit)

**File:** `apps/web/public/aliens/hivekin-hit.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a hivekin staggered backwards, one chitin leg
buckled, scythe-arms flung out for balance, eye-band partially
dimmed, painterly impact pose, full body visible.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #359 — Hivekin (defeated)

**File:** `apps/web/public/aliens/hivekin-defeated.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a hivekin sprawled on the ground, scythe limbs
folded inward, eye-band fully dark, painterly faint teal vapor
dispersing from the joint seams, chitin already starting to lose
shape and crumble — no liquid, no gore.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

## 29.4 Glyphbeast (large heavy charger)

Heavy mid-tier. Quadrupedal, oxen-sized, surface covered in shifting
glowing geometric symbols. Charges in a straight line. Not based on
any Earth animal — its anatomy is a thick stone-like mass on four
crystal trunk-legs, with mandible-shaped front symbol-plates.

### #360 — Glyphbeast (idle)

**File:** `apps/web/public/aliens/glyphbeast-idle.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of an alien glyphbeast — a heavy stone-like quadrupedal
mass on four short crystalline-trunk legs, smooth violet-grey
surface covered in slowly shifting glowing teal geometric glyph
patterns, two wide forward symbol-plates that resemble mandibles
without being insectoid, no eyes, no mouth, calm grounded pose,
full body visible.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #361 — Glyphbeast (charging)

**File:** `apps/web/public/aliens/glyphbeast-charge.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a glyphbeast in full charge, all four crystal-trunk
legs extended forward, glyph patterns on its body blazing bright
teal, mandible-plates flared open revealing bright energy inside,
painterly trail of dust and motion, full body visible in dynamic
forward thrust.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #362 — Glyphbeast (hit)

**File:** `apps/web/public/aliens/glyphbeast-hit.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a glyphbeast braced after impact, one crystal-trunk
leg cracked and faintly leaking teal mist, glyph patterns flickering
unevenly, head-mass tilted, painterly recoil pose, full body visible.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #363 — Glyphbeast (defeated)

**File:** `apps/web/public/aliens/glyphbeast-defeated.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a glyphbeast collapsed to the ground, all four
crystal-trunk legs splayed, glyph patterns extinguished and dim,
the front mandible-plates closed, painterly soft dispersing teal
particles rising from the seams, no gore.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

## 29.5 Riftborn (energy-being, ranged)

Pure-energy class. No solid body. Made of folded space, looking like
a vaguely figure-shaped distortion field with a glowing core and
trailing strands of starlight. Attacks from a distance with void-
beams. Phases between hexes.

### #364 — Riftborn (idle)

**File:** `apps/web/public/aliens/riftborn-idle.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of an alien riftborn — a humanoid-silhouette-shaped
field of folded space, no solid body, semi-transparent indigo-violet
distortion edges, three drifting strands of starlight orbiting a
single brilliant teal-white core where its chest would be, no
visible face, faint gravitational shimmer, hovering just above the
ground.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #365 — Riftborn (attack)

**File:** `apps/web/public/aliens/riftborn-attack.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a riftborn casting — extended both arm-shaped
distortions forward to channel a beam of indigo void-energy with
white-hot core, the three orbiting starlight strands now whipping
toward the same direction, intense painterly streaming light.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #366 — Riftborn (hit)

**File:** `apps/web/public/aliens/riftborn-hit.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a riftborn destabilized, the figure's distortion
field rippling and tearing in places, two starlight strands flung
outward, the central core flickering and dim, painterly fragmenting
silhouette pose.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #367 — Riftborn (defeated)

**File:** `apps/web/public/aliens/riftborn-defeated.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a riftborn collapsing into a small indigo singularity
hovering just off the ground, the figure dissolved, only the core
remains, faint final starlight mote dispersing outward, painterly
soft fade, no gore, no body.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

## 29.6 Maw of the Hive (heavy boss)

Endgame boss. A massive crystalline-organic structure-creature, root-
like base anchored in the ground, central mass a slowly rotating
crystalline orb, surrounded by smaller petal-mouths. Spawns smaller
aliens. Encountered as a static "boss tile" rather than a moving
unit. Multiple poses for stages of HP loss.

### #368 — Maw (idle)

**File:** `apps/web/public/aliens/maw-idle.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of an alien Maw of the Hive — a massive structure-
creature anchored in the ground by thick black-violet root-tendrils,
a slowly-rotating large crystalline orb at its center pulsing with
soft teal light, six smaller petal-mouths around the orb folded
closed, painterly aura of cold mist around the base, calm imposing
presence, no movement, full body visible.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #369 — Maw (attacking — petals open)

**File:** `apps/web/public/aliens/maw-attack.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of the Maw of the Hive in attack mode, the central
crystalline orb blazing bright teal, all six petal-mouths flung wide
open revealing inner star-points of light, root-tendrils flexed
slightly, painterly streaming energy filaments emerging from each
petal, dramatic dynamic pose.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #370 — Maw (wounded — half HP)

**File:** `apps/web/public/aliens/maw-wounded.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of the Maw of the Hive at half strength — three petal-
mouths broken or torn, the central orb cracked along one side
leaking a thin teal mist, two root-tendrils withered and curled,
remaining structure still imposing, painterly battle-damage pose.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #371 — Maw (defeated)

**File:** `apps/web/public/aliens/maw-defeated.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of the Maw of the Hive defeated — the central
crystalline orb shattered, all petal-mouths slumped or broken, root-
tendrils receded into the ground, painterly remaining mass crumbling
into faint dispersing teal particles, no gore, no liquid, dignified
ruin.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

---

# SECTION 30 — Alien hive structures

Static structures aliens build on the map. Capture/destroy these to
push the alien threat back.

### #372 — Spawning rift (alien spawn point)

**File:** `apps/web/public/aliens/structures/rift.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of an
alien spawning rift — a vertical jagged tear in space anchored to
the ground, swirling indigo-and-teal energy inside, faint silhouette
of a creature about to step through, painterly cool mist around the
base, isolated on plain white background, no shadow on the floor,
no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #373 — Hive nexus (large central structure)

**File:** `apps/web/public/aliens/structures/hive-nexus.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of an
alien hive nexus — a massive crystalline-organic spire reaching
upward, surface covered in slowly pulsing teal vein-lines, smaller
crystalline pods clustered at the base, painterly faint cold mist
around it, isolated on plain white background, no shadow on the
floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #374 — Brood pod (small spawning growth)

**File:** `apps/web/public/aliens/structures/brood-pod.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of a small
alien brood pod — an oval crystalline pod about hip-height with
faint teal light pulsing inside revealing a dark folded creature
silhouette within, anchored by short black tendril roots, isolated
on plain white background, no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #375 — Crystal sentinel (defensive emplacement)

**File:** `apps/web/public/aliens/structures/sentinel.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of an
alien crystal sentinel — a tall faceted crystalline pillar with a
single slowly-rotating teal eye-orb at the top, smaller hovering
shards orbiting the pillar at mid-height, painterly faint
gravitational distortion, isolated on plain white background, no
shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #376 — Conduit lattice (network node)

**File:** `apps/web/public/aliens/structures/conduit.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of an
alien conduit lattice — a low ground-anchored cluster of three
glassy energy spires connected to each other and to the ground by
glowing teal vein-tendrils, painterly cool light pulsing along the
veins, isolated on plain white background, no shadow on the floor,
no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #377 — Dormant pod (pre-activation)

**File:** `apps/web/public/aliens/structures/dormant-pod.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of a
dormant alien pod — closed crystalline egg-shaped object resting on
short root-feet, dark surface with only the faintest teal pulse
inside, painterly inert quiet atmosphere, isolated on plain white
background, no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #378 — Active hive (mid-game alien presence)

**File:** `apps/web/public/aliens/structures/hive-active.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of an
active alien hive cluster — three connected crystalline-organic
spires of different heights, glowing teal vein-network linking
them, faint hovering shards drifting between, several closed
brood pods at the base, painterly low cold mist, isolated on plain
white background, no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #379 — Collapsed hive (defeated structure)

**File:** `apps/web/public/aliens/structures/hive-collapsed.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of a
collapsed alien hive — broken crystalline spires fallen and dim,
extinguished vein-network, dispersing teal vapor rising from the
ruins, painterly aftermath of victory, isolated on plain white
background, no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

---

# SECTION 31 — Alien-corrupted biome variants

When alien presence persists on a tile, the terrain is altered. These
are the corrupted variants the renderer can swap in.

### #380 — Corrupted plain

**File:** `apps/web/public/biomes/corrupted/plain.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
plain corrupted by alien influence — once-green grass turned dim
violet-grey, faint glowing teal vein-patterns running across the
ground, scattered small crystalline growths breaking the surface,
no characters, no buildings, square 1:1 aspect.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[TILE-POST]
```

### #381 — Corrupted forest

**File:** `apps/web/public/biomes/corrupted/forest.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
forest corrupted by alien influence — trees turned dark violet-black
with luminous teal sap-veins running up the trunks, the ground
between them seeded with small crystalline growths, no characters,
no buildings, square 1:1 aspect.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[TILE-POST]
```

### #382 — Corrupted hill

**File:** `apps/web/public/biomes/corrupted/hill.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of
rolling hills corrupted by alien influence — slopes turned dim
violet, exposed crystalline outcroppings glowing with teal veins,
faint hovering shards above the crests, no characters, no
buildings, square 1:1 aspect.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[TILE-POST]
```

### #383 — Corrupted mountain

**File:** `apps/web/public/biomes/corrupted/mountain.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
mountain corrupted by alien influence — peaks turned dark violet
basalt with glowing teal vein-cracks running down the sides,
crystalline spires protruding near the summit, painterly cold haze,
no characters, no buildings, square 1:1 aspect.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[TILE-POST]
```

### #384 — Corrupted water

**File:** `apps/web/public/biomes/corrupted/water.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of
water corrupted by alien influence — surface darkened to deep
purple-indigo, faint teal phosphorescent ripple patterns spreading
outward from a glowing point at the center, no land, no characters,
square 1:1 aspect.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[TILE-POST]
```

### #385 — Corrupted desert

**File:** `apps/web/public/biomes/corrupted/desert.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
desert corrupted by alien influence — sand turned cool grey-violet,
crystalline shards scattered everywhere casting long teal shadows,
faint glowing vein-patterns under the surface, no characters, no
buildings, square 1:1 aspect.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[TILE-POST]
```

### #386 — Corrupted tundra

**File:** `apps/web/public/biomes/corrupted/tundra.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
tundra corrupted by alien influence — snow tinted faintly violet,
black-violet crystal blooms rising through the ice, glowing teal
vein-patterns spreading like frost-roots, no characters, no
buildings, square 1:1 aspect.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[TILE-POST]
```

### #387 — Corruption epicenter (rare)

**File:** `apps/web/public/biomes/corrupted/epicenter.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of an
alien corruption epicenter — terrain centered on a small dimensional
rift glowing intense teal-and-violet, crystalline growths radiating
outward in a star pattern, ground stripped of all biome identity, no
characters, no buildings, square 1:1 aspect.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[TILE-POST]
```

---

# SECTION 32 — Alien combat effects

Translucent overlay effects that compose on top of the map during
alien attacks. Render with additive blending in Pixi.

### #388 — Void beam

**File:** `apps/web/public/aliens/effects/void-beam.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A horizontal painterly beam
of indigo-violet void energy with a brilliant white-teal core,
arcing electric tendrils along its length, painterly motion blur
edges, isolated on plain white background, no shadow on the floor,
no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #389 — Acid splash

**File:** `apps/web/public/aliens/effects/acid-splash.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly burst of glowing
teal alien fluid splashing outward from a central impact point —
not Earth-acid, more like radiant bioluminescent crystalline droplets
freezing mid-air, no liquid pooling, isolated on plain white
background, no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #390 — Shard volley

**File:** `apps/web/public/aliens/effects/shard-volley.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly fan of three or
four sharp violet-crystal shards in mid-flight with painterly motion
trails, faint teal glow at the leading edges, isolated on plain
white background, no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #391 — Tendril strike

**File:** `apps/web/public/aliens/effects/tendril-strike.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly single dark-violet
alien tendril whipped forward with a faint teal glowing tip, motion
blur trailing behind, no creature attached visible (just the
tendril), isolated on plain white background, no shadow on the floor,
no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #392 — Rift singularity

**File:** `apps/web/public/aliens/effects/singularity.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly small but intense
indigo-violet singularity with concentric rings of distorted space
and starlight motes spiralling inward, painterly gravitational lens
edges, isolated on plain white background, no shadow on the floor,
no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #393 — Dissolving particles (defeated alien)

**File:** `apps/web/public/aliens/effects/dissolve.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly cloud of softly
rising teal particles dispersing upward as if a creature has been
defeated and is dissolving into ambient energy, isolated on plain
white background, no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #394 — Energy shockwave

**File:** `apps/web/public/aliens/effects/shockwave.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly expanding ring of
indigo-violet shockwave energy with a white-teal leading edge,
distortion ripple visible inside the ring, isolated on plain white
background, no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #395 — Crystal growth burst

**File:** `apps/web/public/aliens/effects/growth-burst.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly burst of radiating
crystalline shards growing outward from a central seed point in a
star pattern, faint teal glow inside each shard, isolated on plain
white background, no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #396 — Static field

**File:** `apps/web/public/aliens/effects/static-field.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly translucent
overlay of indigo-violet static lightning arcs filling a circular
area, painterly threshold of containment effect, isolated on plain
white background, no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #397 — Dark mist

**File:** `apps/web/public/aliens/effects/mist.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly soft cloud of
cool dark-violet alien mist with faint teal embedded glints,
slowly curling translucent edges, isolated on plain white
background, no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

---

# SECTION 33 — Alien battle illustration cards

Replaces the human-vs-human battle cards from V2 with human-vs-alien
equivalents. Cinematic side illustrations for combat-result modals.

### #398 — Warrior facing skitterling pack

**File:** `apps/web/public/battle-aliens/warrior-vs-pack.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A cinematic painterly
illustration of a single armored warrior braced behind a kite shield
facing three onrushing alien skitterlings, painterly dynamic depth,
warrior's face hidden by helm visor (focus on the encounter, not
the warrior identity), isolated on plain white background, no shadow
on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #399 — Archer line vs voidweavers

**File:** `apps/web/public/battle-aliens/archer-vs-voidweaver.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A cinematic painterly
illustration of three archers loosing a volley of arrows at two
hovering voidweavers, painterly motion-trails behind the arrows,
voidweavers casting shock-distortion in defense, dramatic ranged
encounter, isolated on plain white background, no shadow on the
floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #400 — Cavalry charge into hivekin

**File:** `apps/web/public/battle-aliens/cavalry-vs-hivekin.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A cinematic painterly
illustration of three armored horsemen in full lance charge into a
line of alien hivekin drones, dust kicking up, hivekin scythe-arms
raised in defense, dynamic painterly forward depth, isolated on
plain white background, no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #401 — Glyphbeast charging defenders

**File:** `apps/web/public/battle-aliens/glyphbeast-vs-defenders.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A cinematic painterly
illustration of a charging glyphbeast bearing down on a small
shield-wall of three defenders bracing for the impact, painterly
glyph patterns blazing on the beast, dust trailing, dramatic
imminent-collision moment, isolated on plain white background, no
shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #402 — Riftborn ambush

**File:** `apps/web/public/battle-aliens/riftborn-ambush.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A cinematic painterly
illustration of two riftborn materializing simultaneously around a
lone scout in a forest, painterly distortion fields around the
riftborn, scout half-turning startled but composed, dramatic
ambush moment, isolated on plain white background, no shadow on
the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #403 — Maw of the Hive boss confrontation

**File:** `apps/web/public/battle-aliens/maw-confrontation.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A cinematic painterly
illustration of a lone champion warrior approaching a towering Maw
of the Hive, the warrior's silhouette small in the foreground
against the massive crystalline boss with its petals open and
glowing teal core, painterly dramatic scale contrast, isolated on
plain white background, no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #404 — City defense

**File:** `apps/web/public/battle-aliens/city-defense.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A cinematic painterly
illustration of human defenders along a city wall repelling an alien
incursion below — archers loosing volleys, warriors at the gate,
alien skitterlings and hivekin pressing in from the foreground,
painterly dramatic warm-orange torch light contrasting cold alien
glow, isolated on plain white background, no shadow on the floor,
no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #405 — Rift breach

**File:** `apps/web/public/battle-aliens/rift-breach.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A cinematic painterly
illustration of an alien rift tearing open in midair above a
landscape, the silhouettes of multiple emerging skitterlings and
hovering voidweavers stepping through, painterly glowing edges of
the rift, dramatic incursion moment, isolated on plain white
background, no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #406 — Hive fall

**File:** `apps/web/public/battle-aliens/hive-fall.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A cinematic painterly
illustration of human warriors raising a banner of triumph over a
collapsed alien hive — the broken crystalline spires in the
background, dispersing teal particles, painterly dramatic dawn light
behind the figures, isolated on plain white background, no shadow on
the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #407 — Allied factions stand together

**File:** `apps/web/public/battle-aliens/alliance-stand.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A cinematic painterly
illustration of warriors from four different banners — copper-orange,
forest-green, sea-teal, and violet — standing shoulder to shoulder
on a hilltop facing an oncoming alien horde in the misty distance,
unified pose, painterly dramatic resolve, isolated on plain white
background, no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

---

# SECTION 34 — Alien threat UI icons

HUD glyphs that signal alien activity to the player.

### #408 — Threat: Rift opening

**File:** `apps/web/public/ui/aliens/threat-rift.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of a
small jagged tear-shaped rift symbol with indigo-violet inside
edged in glowing teal, isolated on plain white background, no
shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #409 — Threat: Incursion (warning)

**File:** `apps/web/public/ui/aliens/threat-warning.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of a
warning triangle with an indigo-violet alien silhouette inside,
faint teal glow around the triangle border, isolated on plain
white background, no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #410 — Threat: Corruption spread

**File:** `apps/web/public/ui/aliens/threat-corruption.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of a
spreading vein-pattern in dark-violet with teal glow tips, organic
fractal shape, isolated on plain white background, no shadow on
the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #411 — Status: Hive nearby

**File:** `apps/web/public/ui/aliens/threat-hive.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of a
small alien hive spire silhouette in dark violet with a teal core
glow, isolated on plain white background, no shadow on the floor,
no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #412 — Status: Wave incoming

**File:** `apps/web/public/ui/aliens/threat-wave.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of
three small advancing creature silhouettes in dark violet with
forward motion arrows behind, isolated on plain white background,
no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #413 — Status: Threat repelled

**File:** `apps/web/public/ui/aliens/threat-repelled.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of an
alien creature silhouette inside a circle with a clean diagonal
slash through it, painterly soft fade on the alien shape, isolated
on plain white background, no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #414 — Status: Hive destroyed

**File:** `apps/web/public/ui/aliens/threat-hive-down.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of a
broken/fallen hive spire silhouette with dispersing particles
rising, isolated on plain white background, no shadow on the floor,
no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #415 — Status: Containment field

**File:** `apps/web/public/ui/aliens/threat-contained.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of a
dotted hexagonal containment ring with a dim alien creature
silhouette inside, painterly faint glow on the ring, isolated on
plain white background, no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

---

# SECTION 35 — Daily-boss roster (Nyrduel)

Twelve unique alien bosses for Nyrduel's daily rotation. Each is a
larger-than-normal boss-class creature with distinct silhouette so
players can recognize "today's foe" at a glance. Drop into
`apps/nyrduel/public/aliens/bosses/` (or equivalent path in nyrduel's
public folder) and reference by `boss-{id}.png`.

### #416 — Boss: Skitterqueen

**File:** `apps/nyrduel/public/aliens/bosses/skitterqueen.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly portrait of an
alien Skitterqueen — a larger crystalline matriarch of the
skitterling line, twelve elongated legs in a regal arch, an oversized
violet crown-like thorax, three slowly-pulsing teal core lights
inside, painterly noble menacing pose, isolated on plain white
background, no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #417 — Boss: Voidking

**File:** `apps/nyrduel/public/aliens/bosses/voidking.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly portrait of an
alien Voidking — a vastly larger voidweaver, fifteen black-violet
tendrils spiralling around a central many-faceted teal eye-orb the
size of a stone, faint singularity warp behind it, painterly regal
ranged-caster pose, isolated on plain white background, no shadow
on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #418 — Boss: Hivelord

**File:** `apps/nyrduel/public/aliens/bosses/hivelord.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly portrait of an
alien Hivelord — a heavier hivekin variant, eight grasping limbs
instead of four, two pairs of mantis-blade arms, broader chitin
plating, a wider eye-band of fifteen glowing teal clusters, regal
predator stance, painterly imposing pose, isolated on plain white
background, no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #419 — Boss: Glyphtitan

**File:** `apps/nyrduel/public/aliens/bosses/glyphtitan.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly portrait of an
alien Glyphtitan — a vastly larger glyphbeast, six trunk-legs
instead of four, glyph patterns more elaborate and constantly
shifting, three forward symbol-plates, painterly stoic earth-shaking
pose, isolated on plain white background, no shadow on the floor,
no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #420 — Boss: Riftherald

**File:** `apps/nyrduel/public/aliens/bosses/riftherald.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly portrait of an
alien Riftherald — a tall riftborn variant, the silhouette wider
and more elongated, seven orbiting starlight strands instead of
three, a halo-crown of folded space at the head-area, painterly
ethereal commanding pose, isolated on plain white background, no
shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #421 — Boss: Crystallarch

**File:** `apps/nyrduel/public/aliens/bosses/crystallarch.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly portrait of an
alien Crystallarch — a multi-faceted crystalline being shaped like
a tall obelisk with limbs, six radiating crystal arms ending in
sharp shard-tips, a single large prismatic core at the center
splitting light into rainbow refractions, painterly statuesque
pose, isolated on plain white background, no shadow on the floor,
no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #422 — Boss: Nullmaw

**File:** `apps/nyrduel/public/aliens/bosses/nullmaw.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly portrait of an
alien Nullmaw — a vast circular crystalline mouth structure
hovering above the ground, twelve petal-mouths around an inner
black-violet abyss, faint stars visible deep inside the central
hole, painterly imposing devourer pose, isolated on plain white
background, no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #423 — Boss: Stormwalker

**File:** `apps/nyrduel/public/aliens/bosses/stormwalker.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly portrait of an
alien Stormwalker — a tall lightning-cored being, four crystalline
legs, a torso of swirling indigo plasma with constant teal arcs,
two long whip-tendrils trailing electric discharge, painterly
charged aggressive pose, isolated on plain white background, no
shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #424 — Boss: Mistshaper

**File:** `apps/nyrduel/public/aliens/bosses/mistshaper.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly portrait of an
alien Mistshaper — a creature that is more vapor than substance, a
trailing cloud of dark-violet mist taking a vague larger-than-human
silhouette with two bright teal eye-points and faint suggested
arms made of denser mist, painterly elusive haunting pose, isolated
on plain white background, no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #425 — Boss: Echobreed

**File:** `apps/nyrduel/public/aliens/bosses/echobreed.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly portrait of an
alien Echobreed — a creature that appears to exist in three
overlapping translucent copies of itself slightly offset, basic
form is a slim crystalline humanoid-silhouette but clearly non-
human, single teal core visible through all three echoes, painterly
phasing dimensional pose, isolated on plain white background, no
shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #426 — Boss: Tidegrasp

**File:** `apps/nyrduel/public/aliens/bosses/tidegrasp.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly portrait of an
alien Tidegrasp — an aquatic-style alien with eleven long sinuous
tendrils, a flat fanged-flower-shaped central body with a teal
ring-mouth, faint phosphorescent dots along the tendrils, painterly
weightless drifting pose, isolated on plain white background, no
shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #427 — Boss: Worldscar

**File:** `apps/nyrduel/public/aliens/bosses/worldscar.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly portrait of an
alien Worldscar — a vast slow-moving entity, lower body anchored to
the ground like a small mountain of black-violet stone, upper body
splitting into five tower-like crystalline arms each with an
independent teal eye-orb at the tip, painterly cataclysmic
ancient-being pose, isolated on plain white background, no shadow
on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

---

# SECTION 36 — Alien faction symbols & threat banners

For HUD threat tracker, world map alien-presence indicator, and
defeat / victory banners.

### #428 — Alien threat sigil (master)

**File:** `apps/web/public/aliens/sigil.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A circular painterly sigil
representing the alien threat — a stylized geometric design of three
concentric jagged crystalline rings around a central teal eye-shape,
indigo-and-violet palette with teal glow accents, no text, no
shadow.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #429 — Alien banner / wave warning

**File:** `apps/web/public/aliens/banner-wave.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A vertical hanging cloth-like
banner — but the "fabric" is folded space, dark violet field with
teal threads of energy across it, the alien sigil at the center
flickering like a hologram, painterly warning unmistakable
otherworldly origin, isolated on plain white background, no shadow
on the floor, no text, no flagpole.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #430 — "Wave incoming" header banner

**File:** `apps/web/public/aliens/banner-incoming.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A horizontal painterly header
strip suitable as an in-game alert banner — dark indigo-violet
field with arcing teal lightning across, edges fraying into
dispersing particles, no text, isolated on plain white background,
no shadow on the floor, no watermark.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #431 — "Threat repelled" header banner

**File:** `apps/web/public/aliens/banner-repelled.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A horizontal painterly header
strip — warm copper-orange and ivory field with a stylized broken
alien sigil at the center, faint laurel flourish behind, no text,
isolated on plain white background, no shadow on the floor, no
watermark.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #432 — "Hive fallen" header banner

**File:** `apps/web/public/aliens/banner-hive-fallen.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A horizontal painterly header
strip — soft warm gold field with a fallen alien hive silhouette at
the center surrounded by a small wreath, painterly triumphant tone,
no text, isolated on plain white background, no shadow on the floor,
no watermark.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #433 — Cooperative alliance sigil

**File:** `apps/web/public/aliens/alliance-sigil.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A circular sigil representing
the four-faction human alliance against the alien threat — four
quadrants in copper-orange, forest-green, sea-teal, and violet,
united by a central white star, painterly heroic ornament, no text,
isolated on plain white background, no shadow.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

---

# SECTION 37 — Alien projectile / spell effects

Eight specific spell-and-projectile arts the engine plays during
combat as overlays.

### #434 — Skitter-leap blur

**File:** `apps/web/public/aliens/spells/skitter-leap.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly low forward arc
of motion-blur trail in violet and teal, suggesting a small
creature's leap path, isolated on plain white background, no
shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #435 — Voidweaver bolt

**File:** `apps/web/public/aliens/spells/void-bolt.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly compact orb of
indigo void energy with white-teal core and crackling violet
lightning surface, mid-flight, faint motion trail behind, isolated
on plain white background, no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #436 — Hivekin slash arc

**File:** `apps/web/public/aliens/spells/hivekin-slash.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly sweeping arc of
violet-teal slash energy with a thin chitin-blade line through the
middle, dynamic motion-blur, isolated on plain white background, no
shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #437 — Glyphbeast charge dust

**File:** `apps/web/public/aliens/spells/glyphbeast-dust.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly low rolling dust
cloud trail kicked up by a heavy charging creature, dust tinted
faintly violet with embedded teal glyph fragments, isolated on
plain white background, no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #438 — Riftborn beam

**File:** `apps/web/public/aliens/spells/riftborn-beam.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly long horizontal
beam of folded-space energy, indigo-violet edges with a brilliant
white-teal core, distortion shimmer across the surface, isolated
on plain white background, no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #439 — Crystal eruption

**File:** `apps/web/public/aliens/spells/crystal-eruption.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly burst of seven
sharp violet-and-teal crystal shards bursting upward from a central
ground point in a fan, isolated on plain white background, no
shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #440 — Maw petal beams

**File:** `apps/web/public/aliens/spells/maw-petals.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly star-pattern of
six radiating teal energy beams emanating from a central dark
point, like the Maw of the Hive's signature attack viewed from
above, isolated on plain white background, no shadow on the floor,
no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #441 — Singularity collapse

**File:** `apps/web/public/aliens/spells/singularity-collapse.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly small intense
indigo-violet point at the center surrounded by collapsing rings of
distorted space being pulled inward, suggesting a singularity-
collapse moment, isolated on plain white background, no shadow on
the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

---

# Production tips for Volume III

1. **Generate Volume III in a fresh ChatGPT chat** (or several, one per section). Volume I and II established the human/Earth side; Volume III's alien sub-style needs its own anchor. Start with prompt #348 (Skitterling idle) and judge — if the alien-style block reads "alien, painted-Ghibli, beautiful-and-dangerous" rather than "horror movie", continue. Otherwise tighten the alien block.
2. **Pose sets matter.** When generating the four poses for one alien species, do them in a single message bundle so DALL-E aligns the silhouette across frames.
3. **Boss roster (#416–#427).** For Nyrduel, you only need one boss generated to flip the daily mode away from human-vs-human. The other 11 are content-rotation — generate over time.
4. **Avoid horror.** If a generation comes back with too much "guts/blood/teeth" energy, paste this revision tag at the end of the prompt:
   ```
   Avoid: viscera, gore, blood, exposed flesh, gnashing teeth, body-
   horror, screaming faces. Aim instead for: crystalline elegance,
   formidable presence, otherworldly beauty.
   ```
5. **Optimization.** Same `art/optimize-assets.sh` script — drop the alien PNGs into their folders and run it. Hive structures and battle cards will be auto-resized; alien creatures and effects keep alpha.

# Updated grand tally (Volumes I + II + III)

| Volume | Theme | Prompts |
|---|---|---|
| **I** | Painted world (human side) | 176 |
| **II** | Variants & polish (seasons, walks, civilians, wonders, etc.) | 172 |
| **III** | **Alien antagonist atlas** | 94 |
| **TOTAL** | | **442** |

Once the Volume III art ships, the studio rule "no human-vs-human / no Earth-life violence" is fully supported visually across every game in the suite.

---

## Where each prompt set ships

| Game | Folder for V3 art |
|---|---|
| **Nyrvexa** | `apps/web/public/aliens/`, `…/biomes/corrupted/`, `…/battle-aliens/`, `…/ui/aliens/` |
| **Nyrvexis** | mirror the same paths under nyrvexis's web app public folder |
| **Nyrduel** | `apps/nyrduel/public/aliens/bosses/` (boss roster); other alien art from V3 can be cherry-picked as needed |

When you have V3 assets generated and dropped in, ping me — I'll wire `lib/assets.ts` for each game and refactor the engines to swap "rival faction" out for "alien threat." That refactor is a one-day code job per game once art is ready.
