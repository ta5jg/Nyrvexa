<!-- =============================================================================
File:           art/PROMPTS-DALLE-V2.md
Author:         USDTG GROUP TECHNOLOGY LLC
Developer:      Irfan Gedik
Created Date:   2026-05-03
Last Update:    2026-05-03
Version:        0.2.0

Description:
  Expansion atlas. Picks up where PROMPTS-DALLE.md (#0–#175) leaves off
  and adds another 172 prompts for seasonal biomes, full unit pose sets,
  walk-cycle animation frames, civilian/diplomatic units, wonder
  buildings, weather, dawn/dusk lighting, discovery tiles, combat
  illustration cards, city interiors, naval units, religion / culture,
  and map-edge atmosphere.

  Use the same master style block + I-NEED-EXACTLY-THIS rule from
  PROMPTS-DALLE.md.

License:
  Proprietary. All rights reserved. See LICENSE in the repository root.
============================================================================= -->

# Nyrvexa — DALL-E 3 Atlas, Volume II

172 additional prompts continuing from `PROMPTS-DALLE.md` (which ends at #175). Generate these only after Volume I's core set is locked, otherwise style drift compounds.

**All prompts inherit:**
- The `I NEED EXACTLY THIS, DO NOT REWRITE:` prefix.
- The master style block (Studio Ghibli + cohesive palette) from Volume I.
- The post-blocks for character ("Centered subject, isolated on plain white background…") or hex tile ("Top-down 90-degree birds-eye view…") — applied as appropriate per asset type.

For brevity in this file, the standard blocks are abbreviated as **`[STYLE-BLOCK]`** and **`[CHAR-POST]`** / **`[TILE-POST]`**. Expand them inline when you paste into ChatGPT.

```
[STYLE-BLOCK] = (paste this, verbatim, in every prompt)
In the style of Studio Ghibli, hand-painted anime illustration with
painterly brushwork, soft warm volumetric lighting, gentle dramatic
atmosphere, clean linework, cohesive color palette tying every asset
to a single fantasy world. Saturated greens, sandy oranges, slate
mountain greys, deep painterly water blues, parchment-warm beige
neutrals.

[CHAR-POST] = (append on character/icon prompts)
Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.

[TILE-POST] = (append on hex tile prompts)
Top-down 90-degree birds-eye view, square 1:1 aspect, looking straight
down at the terrain from above, no horizon line, no characters.
```

---

# SECTION 16 — Seasonal biome variants

4 seasons × 4 biomes that change with seasons (plain, forest, hill, water). Mountain / desert / tundra are climate-fixed and use the Volume I tiles year-round. Generate in a single chat for cohesion.

### #176 — Plain (spring blooming)

**File:** `apps/web/public/biomes/seasons/plain-spring.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of an
open grassland in early spring, fresh green shoots breaking through
last winter's pale stubble, scattered patches of yellow buttercups
and white daisies, faint dew highlights, no people, no buildings,
square 1:1 aspect.

[STYLE-BLOCK]

[TILE-POST]
```

### #177 — Plain (summer baked)

**File:** `apps/web/public/biomes/seasons/plain-summer.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of an
open grassland in midsummer, sun-warmed yellow-green grass with
patches of golden seed heads, wind-rippled lines across the field,
no people, no buildings, square 1:1 aspect.

[STYLE-BLOCK]

[TILE-POST]
```

### #178 — Plain (autumn golden)

**File:** `apps/web/public/biomes/seasons/plain-autumn.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of an
open grassland in late autumn, painterly amber and bronze grass with
fallen leaves drifting across, a few seed-heads catching slanted
light, no people, no buildings, square 1:1 aspect.

[STYLE-BLOCK]

[TILE-POST]
```

### #179 — Plain (winter frosted)

**File:** `apps/web/public/biomes/seasons/plain-winter.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of an
open grassland in winter, frost-silvered dry grass tufts breaking
through a thin layer of snow, painterly cold blue shadows, no people,
no buildings, square 1:1 aspect.

[STYLE-BLOCK]

[TILE-POST]
```

### #180 — Forest (spring buds)

**File:** `apps/web/public/biomes/seasons/forest-spring.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
broadleaf forest in early spring, fresh light-green budding leaves
just opening across the canopy, painterly mist hugging the lower
trunks, no people, no buildings, square 1:1 aspect.

[STYLE-BLOCK]

[TILE-POST]
```

### #181 — Forest (summer dense)

**File:** `apps/web/public/biomes/seasons/forest-summer.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
broadleaf forest in deep summer, full lush dark-green canopies
overlapping, dappled sunlight punching through to the forest floor,
no people, no buildings, square 1:1 aspect.

[STYLE-BLOCK]

[TILE-POST]
```

### #182 — Forest (autumn fiery)

**File:** `apps/web/public/biomes/seasons/forest-autumn.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
broadleaf forest in autumn peak, painterly red, orange, gold and
russet canopies in close clusters, fallen leaves carpeting the
ground in clearings, no people, no buildings, square 1:1 aspect.

[STYLE-BLOCK]

[TILE-POST]
```

### #183 — Forest (winter snowed)

**File:** `apps/web/public/biomes/seasons/forest-winter.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
forest in winter, bare grey-brown branches dusted with snow,
evergreen pockets weighed down with white, painterly cold blue
shadows on the snowy floor, no people, no buildings, square 1:1
aspect.

[STYLE-BLOCK]

[TILE-POST]
```

### #184 — Hill (spring flowering)

**File:** `apps/web/public/biomes/seasons/hill-spring.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of
rolling hills in spring, fresh green slopes covered in bands of
wildflowers — purple lupines, yellow daffodils, white anemones —
casting soft shadows, no people, no buildings, square 1:1 aspect.

[STYLE-BLOCK]

[TILE-POST]
```

### #185 — Hill (summer dry)

**File:** `apps/web/public/biomes/seasons/hill-summer.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of
rolling hills in summer, sun-bleached grass on the crests, deeper
green in the gullies, scattered boulders, painterly warm long
shadows, no people, no buildings, square 1:1 aspect.

[STYLE-BLOCK]

[TILE-POST]
```

### #186 — Hill (autumn brown)

**File:** `apps/web/public/biomes/seasons/hill-autumn.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of
rolling hills in autumn, amber and rust grasses with dark exposed
earth showing in places, scattered fallen leaves drifting in the
gullies, no people, no buildings, square 1:1 aspect.

[STYLE-BLOCK]

[TILE-POST]
```

### #187 — Hill (winter snowed)

**File:** `apps/web/public/biomes/seasons/hill-winter.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of
rolling hills under a thick layer of fresh snow, slope contours
softened, exposed dark rock outcroppings here and there, painterly
cold blue shadows, no people, no buildings, square 1:1 aspect.

[STYLE-BLOCK]

[TILE-POST]
```

### #188 — Water (spring melt)

**File:** `apps/web/public/biomes/seasons/water-spring.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
freshwater lake in early spring, painterly cold-clear blue water
with last melting ice patches drifting across, faint reflective
ripples, no land visible, no characters, square 1:1 aspect.

[STYLE-BLOCK]

[TILE-POST]
```

### #189 — Water (summer warm)

**File:** `apps/web/public/biomes/seasons/water-summer.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
freshwater lake in deep summer, painterly warm-blue water reflecting
soft cumulus clouds, water lily clusters in one corner, gentle
ripples, no land visible, no characters, square 1:1 aspect.

[STYLE-BLOCK]

[TILE-POST]
```

### #190 — Water (autumn rain)

**File:** `apps/web/public/biomes/seasons/water-autumn.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
freshwater lake under autumn rainfall, painterly grey-blue water
with concentric rain ripples scattered across the surface, fallen
leaves floating, no land visible, no characters, square 1:1 aspect.

[STYLE-BLOCK]

[TILE-POST]
```

### #191 — Water (winter frozen)

**File:** `apps/web/public/biomes/seasons/water-winter.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
freshwater lake frozen over in winter, painterly cracked white-blue
ice with dusting of snow, dark fissure lines, no land visible, no
characters, square 1:1 aspect.

[STYLE-BLOCK]

[TILE-POST]
```

---

# SECTION 17 — Unit combat pose set

Each of the 6 unit classes in 4 poses (faction-agnostic; engine tints per faction at render time). 24 prompts.

### #192 — Settler (carrying tools)

**File:** `apps/web/public/units/poses/settler-build.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a settler kneeling and driving a wooden tent stake
into the ground with a small mallet, beige tunic and travel cloak,
focused expression, mid-action pose, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #193 — Settler (greeting)

**File:** `apps/web/public/units/poses/settler-greet.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a settler standing tall with one arm raised in
welcoming greeting, beige tunic and travel cloak, walking staff in
the lowered hand, peaceful expression, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #194 — Warrior (attack swing)

**File:** `apps/web/public/units/poses/warrior-attack.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of an armored warrior mid-swing, sword sweeping
diagonally across, shield braced behind, helm with bronze brow,
fierce concentrated expression, dynamic full body in motion.

[STYLE-BLOCK]

[CHAR-POST]
```

### #195 — Warrior (block stance)

**File:** `apps/web/public/units/poses/warrior-block.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of an armored warrior in defensive crouch, kite shield
raised in front, sword held low, helm tilted forward, braced
expression, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #196 — Warrior (hit recoil)

**File:** `apps/web/public/units/poses/warrior-hit.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of an armored warrior staggered backwards from an
unseen impact, shield arm flung up, sword swinging away, helm tilted,
pained grimace, dynamic off-balance pose, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #197 — Warrior (defeated)

**File:** `apps/web/public/units/poses/warrior-defeated.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of an armored warrior on one knee, shield resting in
the dirt, sword tip pressed into the ground for support, helm
removed beside, exhausted expression, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #198 — Scout (scouting / pointing)

**File:** `apps/web/public/units/poses/scout-spot.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a hooded scout crouched low and pointing forward
into the distance, hand-axe at hip, hood casting shadow over face,
keen alert expression, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #199 — Scout (ranged dagger throw)

**File:** `apps/web/public/units/poses/scout-throw.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a hooded scout in mid throw of a dagger, body
twisted, off-hand braced behind, hood blown back slightly, focused
expression, dynamic full body in motion.

[STYLE-BLOCK]

[CHAR-POST]
```

### #200 — Scout (climbing / sneaking)

**File:** `apps/web/public/units/poses/scout-sneak.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a hooded scout creeping forward in a low silent
stalk, both hands braced as if balancing, dagger in mouth, full body
in stealthy motion.

[STYLE-BLOCK]

[CHAR-POST]
```

### #201 — Scout (defeated)

**File:** `apps/web/public/units/poses/scout-defeated.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a hooded scout slumped against a small boulder, hood
fallen back, dagger fallen from the hand, defeated expression, full
body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #202 — Archer (loose / firing)

**File:** `apps/web/public/units/poses/archer-loose.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of an archer in the moment of release, bow string
snapping forward, arrow streaking out, fingers splayed, intense
expression, dynamic full body, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #203 — Archer (drawing back)

**File:** `apps/web/public/units/poses/archer-draw.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of an archer drawing back the bow string with great
effort, arrow nocked, off-arm extended forward holding the bow,
focused calm expression, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #204 — Archer (running)

**File:** `apps/web/public/units/poses/archer-run.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of an archer running forward at speed, bow held in one
hand at the side, quiver bouncing on the back, urgent expression,
mid-stride full body in motion.

[STYLE-BLOCK]

[CHAR-POST]
```

### #205 — Archer (defeated)

**File:** `apps/web/public/units/poses/archer-defeated.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of an archer on the ground, bow broken beside, quiver
spilled with scattered arrows, exhausted defeated expression, full
body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #206 — Horseman (charging)

**File:** `apps/web/public/units/poses/horseman-charge.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
a mounted horseman in full gallop charge, lance lowered horizontal,
rider's cloak streaming back, horse's mane and tail flowing,
aggressive expression, dynamic full mount and rider in motion.

[STYLE-BLOCK]

[CHAR-POST]
```

### #207 — Horseman (rearing)

**File:** `apps/web/public/units/poses/horseman-rear.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
a mounted horseman with the horse rearing on its hind legs, rider
braced and holding aloft a sword, dramatic dynamic pose, full mount
and rider visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #208 — Horseman (trotting)

**File:** `apps/web/public/units/poses/horseman-trot.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
a mounted horseman at a steady trot, rider sitting upright with reins
in both hands, calm watchful expression, full mount and rider visible
in mid-stride.

[STYLE-BLOCK]

[CHAR-POST]
```

### #209 — Horseman (defeated)

**File:** `apps/web/public/units/poses/horseman-defeated.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
a horseman dismounted, kneeling next to a wounded horse lying on its
side, rider's helm in the dirt, exhausted defeated expression, full
scene visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #210 — Worker (chopping)

**File:** `apps/web/public/units/poses/worker-chop.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a worker mid-axe-swing into a wood log on a stump,
body torqued for the chop, sleeves rolled, focused expression, full
body in motion.

[STYLE-BLOCK]

[CHAR-POST]
```

### #211 — Worker (hammering)

**File:** `apps/web/public/units/poses/worker-hammer.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a worker striking a wooden post with a heavy
carpenter's hammer, both hands on the handle, knees flexed, focused
expression, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #212 — Worker (carrying load)

**File:** `apps/web/public/units/poses/worker-carry.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a worker carrying a heavy bundle of building supplies
on the shoulder, leaning into the weight, sturdy boots, determined
expression, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #213 — Worker (resting)

**File:** `apps/web/public/units/poses/worker-rest.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a worker sitting on a stone block, wiping brow with
the back of one hand, hammer leaning beside, weary content
expression, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #214 — Generic unit (victory cheer)

**File:** `apps/web/public/units/poses/generic-victory.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a generic warrior figure with both arms raised in
victorious cheer, sword held high, helm thrown back to reveal
elated expression, full body in triumphant pose.

[STYLE-BLOCK]

[CHAR-POST]
```

### #215 — Generic unit (fallen)

**File:** `apps/web/public/units/poses/generic-fallen.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a generic warrior figure lying on the ground in a
defeated pose, sword and shield fallen at the side, no blood, no
gore, painterly stoic dignity, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

---

# SECTION 18 — Walk cycle animation frames

4-frame walk cycle for each of the 6 unit types. Use these when the renderer animates movement between hexes. Faction-agnostic; engine tints per faction.

### #216 — Settler walk frame 1

**File:** `apps/web/public/units/walk/settler-1.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a settler walking forward, left leg fully extended
forward, right leg behind toes touching the ground, walking staff
planted alongside, mid-stride pose, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #217 — Settler walk frame 2

**File:** `apps/web/public/units/walk/settler-2.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a settler walking forward, both legs roughly level
beneath the body in a contact frame, walking staff lifted slightly,
full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #218 — Settler walk frame 3

**File:** `apps/web/public/units/walk/settler-3.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a settler walking forward, right leg fully extended
forward, left leg behind toes touching the ground, walking staff
planted alongside, mid-stride mirror of frame 1, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #219 — Settler walk frame 4

**File:** `apps/web/public/units/walk/settler-4.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a settler walking forward, both legs roughly level
beneath the body in a contact frame mirror of frame 2, walking
staff lifted slightly, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #220 — Warrior walk frame 1

**File:** `apps/web/public/units/walk/warrior-1.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of an armored warrior marching forward, left leg fully
extended forward, right leg behind toes touching, shield arm down,
sword held low, full body in mid-stride.

[STYLE-BLOCK]

[CHAR-POST]
```

### #221 — Warrior walk frame 2

**File:** `apps/web/public/units/walk/warrior-2.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of an armored warrior marching, both legs roughly
level, contact frame, shield slightly raised, sword swinging
counter-balance, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #222 — Warrior walk frame 3

**File:** `apps/web/public/units/walk/warrior-3.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of an armored warrior marching, right leg fully
extended forward, left leg behind, shield arm down, mirror of
frame 1, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #223 — Warrior walk frame 4

**File:** `apps/web/public/units/walk/warrior-4.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of an armored warrior marching, both legs roughly
level mirror of frame 2, shield slightly raised, sword swinging
opposite direction, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #224 — Scout walk frame 1

**File:** `apps/web/public/units/walk/scout-1.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a hooded scout walking forward, left leg fully
extended, body bent slightly forward in stealthy posture, hands at
hips, full body in mid-stride.

[STYLE-BLOCK]

[CHAR-POST]
```

### #225 — Scout walk frame 2

**File:** `apps/web/public/units/walk/scout-2.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a hooded scout walking, contact frame both legs
level, body bent forward, hood casting shadow, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #226 — Scout walk frame 3

**File:** `apps/web/public/units/walk/scout-3.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a hooded scout walking, right leg fully extended
mirror of frame 1, body bent forward, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #227 — Scout walk frame 4

**File:** `apps/web/public/units/walk/scout-4.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a hooded scout walking, contact frame mirror of
frame 2, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #228 — Archer walk frame 1

**File:** `apps/web/public/units/walk/archer-1.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of an archer walking forward, left leg extended, bow
held in one hand at the side, quiver on back, full body in mid-
stride.

[STYLE-BLOCK]

[CHAR-POST]
```

### #229 — Archer walk frame 2

**File:** `apps/web/public/units/walk/archer-2.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of an archer walking, contact frame both legs level,
bow swinging forward slightly, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #230 — Archer walk frame 3

**File:** `apps/web/public/units/walk/archer-3.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of an archer walking, right leg extended mirror of
frame 1, bow swinging back slightly, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #231 — Archer walk frame 4

**File:** `apps/web/public/units/walk/archer-4.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of an archer walking, contact frame mirror of frame 2,
full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #232 — Horseman trot frame 1

**File:** `apps/web/public/units/walk/horseman-1.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
a mounted horseman in a steady trot, horse's left foreleg and right
hindleg extended forward together, mane lifting, rider sitting
upright, full mount and rider visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #233 — Horseman trot frame 2

**File:** `apps/web/public/units/walk/horseman-2.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
a mounted horseman in a trot suspension frame, all four hooves
slightly off the ground, rider centered, full mount and rider
visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #234 — Horseman trot frame 3

**File:** `apps/web/public/units/walk/horseman-3.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
a mounted horseman in a steady trot, horse's right foreleg and left
hindleg extended forward together mirror of frame 1, rider sitting
upright, full mount and rider visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #235 — Horseman trot frame 4

**File:** `apps/web/public/units/walk/horseman-4.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
a mounted horseman in a trot suspension frame mirror of frame 2,
full mount and rider visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #236 — Worker walk frame 1

**File:** `apps/web/public/units/walk/worker-1.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a worker walking forward carrying a hammer at the
side, left leg extended, satchel on hip, full body in mid-stride.

[STYLE-BLOCK]

[CHAR-POST]
```

### #237 — Worker walk frame 2

**File:** `apps/web/public/units/walk/worker-2.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a worker walking, contact frame both legs level,
hammer swinging slightly, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #238 — Worker walk frame 3

**File:** `apps/web/public/units/walk/worker-3.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a worker walking, right leg extended mirror of
frame 1, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #239 — Worker walk frame 4

**File:** `apps/web/public/units/walk/worker-4.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a worker walking, contact frame mirror of frame 2,
full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

---

# SECTION 19 — Civilian / diplomatic units

Six new unit roles unlocked by tech / wonders. Each in 4 faction colors. 24 prompts. (Optional — only generate if you decide to add diplomacy mechanics in v0.3+.)

### #240 — Envoy (Aurei)

**File:** `apps/web/public/units/civilian/envoy-aurei.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of an Aurei diplomatic envoy — copper-orange formal robe
with ivory sash, gold chain of office, scroll case held in one hand,
no weapons, dignified bowing pose, calm expression, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #241 — Envoy (Vesnar)

**File:** `apps/web/public/units/civilian/envoy-vesnar.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a Vesnar diplomatic envoy — forest-green flowing
robe with deep brown trim, antler hairpiece, woven leaf-pattern
shoulder cloak, ceremonial staff with a carved acorn, gentle wise
expression, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #242 — Envoy (Thalassia)

**File:** `apps/web/public/units/civilian/envoy-thalassia.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a Thalassia diplomatic envoy — sea-teal formal robe
with silver embroidery, scholar's circlet, scroll cradled in arm,
silver inkpot on a chain, scholarly intelligent expression, full
body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #243 — Envoy (Kyron)

**File:** `apps/web/public/units/civilian/envoy-kyron.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a Kyron diplomatic envoy — violet formal cloak with
gold horse-emblem clasp, fur-trimmed shoulders, ceremonial dagger
sheathed at hip, proud upright pose, watchful expression, full body
visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #244 — Merchant (Aurei)

**File:** `apps/web/public/units/civilian/merchant-aurei.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of an Aurei merchant — copper-orange traveling cloak,
ivory tunic, leather purse at belt, brass weighing scale held in
hand, friendly haggling expression, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #245 — Merchant (Vesnar)

**File:** `apps/web/public/units/civilian/merchant-vesnar.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a Vesnar merchant — forest-green traveling cloak,
deep-brown apron, herb pouch at belt, basket of dried mushrooms and
fern bundles, kindly weathered expression, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #246 — Merchant (Thalassia)

**File:** `apps/web/public/units/civilian/merchant-thalassia.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a Thalassia merchant — sea-teal long coat with
silver buttons, parchment ledger held open, abacus tucked under
arm, calm tactical expression, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #247 — Merchant (Kyron)

**File:** `apps/web/public/units/civilian/merchant-kyron.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a Kyron merchant — violet riding coat with gold
trim, leather pack of trade goods at belt, gold coin pouch, shrewd
keen expression, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #248 — General (Aurei)

**File:** `apps/web/public/units/civilian/general-aurei.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of an Aurei general — full bronze plate armor over a
copper surcoat, ivory cape, crested helm with sun-shaped crest,
broadsword resting point-down on the ground, commanding stern
expression, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #249 — General (Vesnar)

**File:** `apps/web/public/units/civilian/general-vesnar.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a Vesnar general — moss-green lacquered scale armor,
antler-pattern helm, deep brown cloak, hand axe held across chest,
calm strategic expression, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #250 — General (Thalassia)

**File:** `apps/web/public/units/civilian/general-thalassia.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a Thalassia general — silver-bordered sea-teal
parade armor, plumed helm with silver feathers, ceremonial slim
sword held forward, intelligent imposing expression, full body
visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #251 — General (Kyron)

**File:** `apps/web/public/units/civilian/general-kyron.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a Kyron general — violet lamellar plate with gold
horse-emblem chestpiece, fur-shoulder cloak, curved sabre raised,
fierce commanding expression, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #252 — Priest (Aurei)

**File:** `apps/web/public/units/civilian/priest-aurei.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of an Aurei priest — flowing copper-orange robes with
gold sun embroidery, ivory headwrap, swinging gold incense censer,
serene expression, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #253 — Priest (Vesnar)

**File:** `apps/web/public/units/civilian/priest-vesnar.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a Vesnar druid-priest — moss-green robe of woven
leaves, antler crown, oak staff with hanging acorns, peaceful
profound expression, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #254 — Priest (Thalassia)

**File:** `apps/web/public/units/civilian/priest-thalassia.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a Thalassia priest — silver-trimmed sea-teal robes,
silver moon circlet, open book of prayers in hand, scholarly
reverent expression, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #255 — Priest (Kyron)

**File:** `apps/web/public/units/civilian/priest-kyron.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a Kyron shaman-priest — violet shoulder mantle of
woven horsehair, gold horse-skull headpiece, smoking sage bundle in
hand, mystical weathered expression, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #256 — Spy (Aurei)

**File:** `apps/web/public/units/civilian/spy-aurei.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of an Aurei spy — soft copper-toned traveler's clothes
that blend in, no visible faction insignia, hidden dagger barely
peeking from sleeve, alert hidden expression behind a half-mask,
full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #257 — Spy (Vesnar)

**File:** `apps/web/public/units/civilian/spy-vesnar.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a Vesnar spy — earthy green forager outfit, leather
belt with hidden tools, faint leaf-pattern markings, low hood, alert
quiet expression, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #258 — Spy (Thalassia)

**File:** `apps/web/public/units/civilian/spy-thalassia.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a Thalassia spy — neutral grey scholar's robe over
silver-grey light leather, scroll case as a disguise prop, hidden
silver dagger, bookish-looking but watchful expression, full body
visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #259 — Spy (Kyron)

**File:** `apps/web/public/units/civilian/spy-kyron.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a Kyron spy — neutral riding leathers without
faction colors, fur trim hood pulled low, two hidden throwing
knives at belt, hardened watchful expression, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #260 — Architect (Aurei)

**File:** `apps/web/public/units/civilian/architect-aurei.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of an Aurei master architect — copper-orange tunic,
ivory apron with chalk marks, leather tool belt with calipers and
ruler, blueprints rolled under one arm, thoughtful focused
expression, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #261 — Architect (Vesnar)

**File:** `apps/web/public/units/civilian/architect-vesnar.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a Vesnar master architect — forest-green vest over
brown work tunic, carved wooden builder's set-square, hammer at
belt, woven sapling models, careful patient expression, full body
visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #262 — Architect (Thalassia)

**File:** `apps/web/public/units/civilian/architect-thalassia.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a Thalassia master architect — sea-teal scholar's
coat, silver compass on a chain, abacus and brass dividers, scroll
of geometric drawings, calm precise expression, full body visible.

[STYLE-BLOCK]

[CHAR-POST]
```

### #263 — Architect (Kyron)

**File:** `apps/web/public/units/civilian/architect-kyron.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric character
illustration of a Kyron master architect — violet workshop coat with
gold trim, leather work apron, mason's hammer, scrolls of fortress
designs tucked under arm, intense determined expression, full body
visible.

[STYLE-BLOCK]

[CHAR-POST]
```

---

# SECTION 20 — Wonder buildings

12 wonders, each unique world treasures that boost a faction. Top-down 3/4 isometric, square aspect.

### #264 — Library of Thalassia

**File:** `apps/web/public/wonders/library.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of a great
domed marble library, silver-leaf flourishes, twin reading-arcades
flanking, scrolls and books visible through tall arched windows, no
characters, isolated on plain white background, square 1:1 aspect.

[STYLE-BLOCK]

[CHAR-POST]
```

### #265 — Aurei Sun Temple

**File:** `apps/web/public/wonders/sun-temple.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of an
Aurei sun temple — circular ivory-marble columns supporting a copper
dome topped by a gold sunburst, polished steps leading up, no
characters, isolated on plain white background, square 1:1 aspect.

[STYLE-BLOCK]

[CHAR-POST]
```

### #266 — Vesnar Sacred Grove

**File:** `apps/web/public/wonders/sacred-grove.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of a
Vesnar sacred grove — a circle of seven ancient massive oak trees
around a moss-covered carved stone altar, painterly soft sun
filtering through leaves, no characters, isolated on plain white
background, square 1:1 aspect.

[STYLE-BLOCK]

[CHAR-POST]
```

### #267 — Kyron Stable Hall

**File:** `apps/web/public/wonders/stable-hall.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of a
Kyron grand stable hall — a long stone-and-timber hall with rows of
horse stalls, a paddock attached, gold-pennanted roof, no characters,
isolated on plain white background, square 1:1 aspect.

[STYLE-BLOCK]

[CHAR-POST]
```

### #268 — Stonehenge

**File:** `apps/web/public/wonders/stonehenge.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of a
mysterious circle of standing stones — fifteen weathered grey
monoliths in a precise ring on a grass platform, painterly long
shadows, no characters, isolated on plain white background, square
1:1 aspect.

[STYLE-BLOCK]

[CHAR-POST]
```

### #269 — Hanging Gardens

**File:** `apps/web/public/wonders/hanging-gardens.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of
terraced hanging gardens — five layered stone terraces overflowing
with cascading flowering vines, fountains spilling between layers,
painterly lush color, no characters, isolated on plain white
background, square 1:1 aspect.

[STYLE-BLOCK]

[CHAR-POST]
```

### #270 — Great Pyramid

**File:** `apps/web/public/wonders/pyramid.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of a great
sandstone pyramid in a desert setting, gold-tipped capstone catching
sun, weathered steps along one face, no characters, isolated on
plain white background, square 1:1 aspect.

[STYLE-BLOCK]

[CHAR-POST]
```

### #271 — Lighthouse

**File:** `apps/web/public/wonders/lighthouse.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of a tall
white-stone coastal lighthouse with a brass crown lantern at the
top, the painterly faint glow of its beam, set on a rocky outcrop,
no characters, isolated on plain white background, square 1:1
aspect.

[STYLE-BLOCK]

[CHAR-POST]
```

### #272 — Coliseum

**File:** `apps/web/public/wonders/coliseum.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of a
grand stone amphitheater — concentric tiers of seating around an
oval arena, weathered marble columns ringing the upper level, no
characters, isolated on plain white background, square 1:1 aspect.

[STYLE-BLOCK]

[CHAR-POST]
```

### #273 — Statue Colossus

**File:** `apps/web/public/wonders/colossus.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of a
colossal bronze statue of a robed guardian figure, twice the height
of nearby buildings on its plaza pedestal, painterly soft sun
catching the bronze, no characters at base, isolated on plain white
background, square 1:1 aspect.

[STYLE-BLOCK]

[CHAR-POST]
```

### #274 — Forge of Eternal Fire

**File:** `apps/web/public/wonders/eternal-forge.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of a great
stone forge complex — twin tall chimneys with rising painterly smoke
plumes, glowing crucible at the heart visible through arched
openings, no characters, isolated on plain white background, square
1:1 aspect.

[STYLE-BLOCK]

[CHAR-POST]
```

### #275 — Sky Observatory

**File:** `apps/web/public/wonders/observatory.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of a tall
star-watching observatory — stacked octagonal stories, the topmost
opening into a slatted dome with a slit revealing a brass telescope
within, isolated on plain white background, no characters, square
1:1 aspect.

[STYLE-BLOCK]

[CHAR-POST]
```

---

# SECTION 21 — Weather overlays

Translucent painterly overlays that the renderer composites at low alpha across affected tiles. 8 prompts.

### #276 — Rain veil

**File:** `apps/web/public/weather/rain.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly overlay of
slanted rain streaks falling from the top across the frame, mostly
silver-blue thin lines with painterly variation, isolated on plain
white background, no characters, no terrain, square 1:1 aspect.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #277 — Heavy storm

**File:** `apps/web/public/weather/storm.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly overlay of
heavy diagonal driving rain with a single jagged white-yellow
lightning bolt cutting through the upper portion, painterly grey
storm haze, isolated on plain white background, no terrain, square
1:1 aspect.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #278 — Snow drift

**File:** `apps/web/public/weather/snow.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly overlay of
gentle falling snowflakes drifting at slight angle across the frame,
soft white particles of varied sizes, painterly cool blue tint,
isolated on plain white background, no terrain, square 1:1 aspect.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #279 — Blizzard

**File:** `apps/web/public/weather/blizzard.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly overlay of
a fierce blizzard — dense fast-moving snow streaks at sharp angle,
painterly opaque white-grey haze, faint visibility-reducing wind
lines, isolated on plain white background, no terrain, square 1:1
aspect.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #280 — Sandstorm

**File:** `apps/web/public/weather/sandstorm.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly overlay of
a desert sandstorm — sweeping warm-orange dust streaks driven by
wind, painterly opacity haze obscuring detail, isolated on plain
white background, no terrain, square 1:1 aspect.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #281 — Fog

**File:** `apps/web/public/weather/fog.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly overlay of
soft cool morning fog drifting across the lower half of the frame,
painterly translucent white-grey wisps, isolated on plain white
background, no terrain, square 1:1 aspect.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #282 — God-rays / sun shafts

**File:** `apps/web/public/weather/sunbeams.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly overlay of
warm golden volumetric sun shafts breaking diagonally across the
frame, soft glowing edges, painterly mote particles in the rays,
isolated on plain white background, no terrain, square 1:1 aspect.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #283 — Aurora glow

**File:** `apps/web/public/weather/aurora.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly overlay of
shifting aurora-borealis bands — soft green and violet ribbons
flowing across the upper portion, painterly translucent, isolated on
plain white background, no terrain, square 1:1 aspect.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

---

# SECTION 22 — Time of day biome variants

Dawn and dusk versions of the four most-used biomes (plain, forest, hill, water). 8 prompts.

### #284 — Plain (dawn)

**File:** `apps/web/public/biomes/dawn/plain.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of an
open grassland at dawn, painterly warm pink-gold tint across the
grass, long soft shadows from low sun, dew highlights, no people,
no buildings, square 1:1 aspect.

[STYLE-BLOCK]

[TILE-POST]
```

### #285 — Forest (dawn)

**File:** `apps/web/public/biomes/dawn/forest.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
forest at dawn, painterly low golden light filtering across the
canopy from one side, mist hugging the lower trunks, no people, no
buildings, square 1:1 aspect.

[STYLE-BLOCK]

[TILE-POST]
```

### #286 — Hill (dawn)

**File:** `apps/web/public/biomes/dawn/hill.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of
rolling hills at dawn, painterly warm-pink slopes catching the first
light, deep blue valleys still in shadow, no people, no buildings,
square 1:1 aspect.

[STYLE-BLOCK]

[TILE-POST]
```

### #287 — Water (dawn)

**File:** `apps/web/public/biomes/dawn/water.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
lake at dawn, painterly warm pink-gold reflections across the
surface, soft mist rising in patches, no land, no characters,
square 1:1 aspect.

[STYLE-BLOCK]

[TILE-POST]
```

### #288 — Plain (dusk)

**File:** `apps/web/public/biomes/dusk/plain.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of an
open grassland at dusk, painterly amber-orange tint across the
grass, very long indigo shadows from a low setting sun, no people,
no buildings, square 1:1 aspect.

[STYLE-BLOCK]

[TILE-POST]
```

### #289 — Forest (dusk)

**File:** `apps/web/public/biomes/dusk/forest.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
forest at dusk, painterly fiery orange light raking across the
canopy from one side, deepening blue shadows in the gaps, no
people, no buildings, square 1:1 aspect.

[STYLE-BLOCK]

[TILE-POST]
```

### #290 — Hill (dusk)

**File:** `apps/web/public/biomes/dusk/hill.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of
rolling hills at dusk, painterly amber-orange crests, deep purple-
blue valleys, painterly long shadows, no people, no buildings,
square 1:1 aspect.

[STYLE-BLOCK]

[TILE-POST]
```

### #291 — Water (dusk)

**File:** `apps/web/public/biomes/dusk/water.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
lake at dusk, painterly orange-gold reflections gradually deepening
to indigo across the surface, painterly soft ripples, no land, no
characters, square 1:1 aspect.

[STYLE-BLOCK]

[TILE-POST]
```

---

# SECTION 23 — Special discovery tiles

Rare tiles that the renderer can swap in for "first time you see this hex" wow moments. 12 prompts.

### #292 — Barbarian camp

**File:** `apps/web/public/discovery/barbarian-camp.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
rough barbarian camp on a grass clearing — three crude tents, a
fire pit with burning logs, racks of crude weapons, scattered bones,
no characters, painterly menacing atmosphere, square 1:1 aspect.

[STYLE-BLOCK]

[TILE-POST]
```

### #293 — Ruined city

**File:** `apps/web/public/discovery/ruined-city.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of an
ancient ruined city overgrown with vines and grass, broken stone
columns, collapsed walls, a dry fountain at the center, painterly
soft melancholy atmosphere, no characters, square 1:1 aspect.

[STYLE-BLOCK]

[TILE-POST]
```

### #294 — Oasis spring

**File:** `apps/web/public/discovery/oasis-spring.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
hidden desert oasis — a circular crystalline-clear pool ringed by
date palms, lush green ferns at the water edge, painterly golden
warmth, no characters, square 1:1 aspect.

[STYLE-BLOCK]

[TILE-POST]
```

### #295 — Ancient temple ruin

**File:** `apps/web/public/discovery/temple-ruin.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
crumbling ancient temple in a forest clearing — broken stone steps
leading up to a half-fallen colonnade, mossy carvings of lost gods,
painterly hushed mystery, no characters, square 1:1 aspect.

[STYLE-BLOCK]

[TILE-POST]
```

### #296 — Sacred grove (wild)

**File:** `apps/web/public/discovery/sacred-grove-wild.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
hidden wild grove — a circle of glowing painterly faintly-luminous
trees around a clear pond reflecting starlight even in daytime,
ethereal golden particles in the air, painterly otherworldly
atmosphere, no characters, square 1:1 aspect.

[STYLE-BLOCK]

[TILE-POST]
```

### #297 — Ice cave

**File:** `apps/web/public/discovery/ice-cave.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of an
ice cave entrance carved into a glacier wall, painterly faint blue
glow from inside, scattered icicles around the opening, no
characters, square 1:1 aspect.

[STYLE-BLOCK]

[TILE-POST]
```

### #298 — Mineral cave

**File:** `apps/web/public/discovery/mineral-cave.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
hill cave entrance with crystal clusters glittering inside —
amethyst purple, citrine yellow, painterly inner glow, no
characters, square 1:1 aspect.

[STYLE-BLOCK]

[TILE-POST]
```

### #299 — Witch's hut

**File:** `apps/web/public/discovery/witch-hut.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
small thatched cottage in a dim forest clearing, smoke curling from
a crooked chimney, hanging dried herbs around the doorway, painterly
slightly mysterious atmosphere, no characters, square 1:1 aspect.

[STYLE-BLOCK]

[TILE-POST]
```

### #300 — Hidden lake

**File:** `apps/web/public/discovery/hidden-lake.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
small mountain-cradled lake, mirror-still painterly turquoise water
ringed by pine, a single lily floating, no boats, no characters,
square 1:1 aspect.

[STYLE-BLOCK]

[TILE-POST]
```

### #301 — Volcanic vent

**File:** `apps/web/public/discovery/volcanic-vent.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of an
active volcanic vent — a narrow fissure venting glowing red-orange
lava, painterly heat-haze rising, basalt rocks around, no
characters, square 1:1 aspect.

[STYLE-BLOCK]

[TILE-POST]
```

### #302 — Sky island

**File:** `apps/web/public/discovery/sky-island.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
floating island viewed from directly above — a small grass-and-
stone formation suspended in painterly pale clouds, faint shadow
hovering below, no characters, square 1:1 aspect.

[STYLE-BLOCK]

[TILE-POST]
```

### #303 — Crystal cluster

**File:** `apps/web/public/discovery/crystal-cluster.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
ground cluster of huge magic crystals breaking through the rock —
violet, teal, gold prismatic facets, painterly inner light, no
characters, square 1:1 aspect.

[STYLE-BLOCK]

[TILE-POST]
```

---

# SECTION 24 — Battle illustration cards

Cinematic side-illustrations shown in the combat-result modal. 12 prompts.

### #304 — Warrior vs Warrior clash

**File:** `apps/web/public/battle/warrior-clash.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A cinematic painterly
illustration of two armored warriors clashing swords mid-combat,
sparks at the blade-meeting point, dynamic dramatic angle, no faces
clearly visible (helmet visors), no faction colors, isolated on
plain white background, no shadow on the floor, no text.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #305 — Archer volley

**File:** `apps/web/public/battle/archer-volley.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A cinematic painterly
illustration of a line of archers loosing a volley — multiple
arrows streaking forward with painterly motion trails, focused
profiles of the archers, isolated on plain white background, no
shadow on the floor, no text.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #306 — Cavalry charge

**File:** `apps/web/public/battle/cavalry-charge.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A cinematic painterly
illustration of three horsemen mid-charge with lances lowered, dust
kicking up, manes streaming, painterly dynamic depth, isolated on
plain white background, no shadow on the floor, no text.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #307 — Scout ambush

**File:** `apps/web/public/battle/scout-ambush.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A cinematic painterly
illustration of a hooded scout dropping from above onto an unaware
warrior in a forest, dagger drawn, painterly motion blur, isolated
on plain white background, no shadow on the floor, no text.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #308 — Siege at dawn

**File:** `apps/web/public/battle/siege-dawn.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A cinematic painterly
illustration of a small siege at dawn — distant city walls with
banners and a foreground line of soldiers with shields and lances
forming up, painterly warm dawn light, no clear faction colors,
isolated on plain white background, no shadow on the floor, no
text.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #309 — Naval skirmish

**File:** `apps/web/public/battle/naval-skirmish.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A cinematic painterly
illustration of two small wooden warships side-by-side at sea —
arrows flying between, sails catching wind, painterly waves and
spray, isolated on plain white background, no shadow on the floor,
no text.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #310 — Forest ambush

**File:** `apps/web/public/battle/forest-ambush.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A cinematic painterly
illustration of warriors stepping out from between trees, arrows
flying from the treeline at unsuspecting targets, painterly green
dappled light, isolated on plain white background, no shadow on the
floor, no text.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #311 — Mountain pass clash

**File:** `apps/web/public/battle/mountain-clash.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A cinematic painterly
illustration of warriors fighting on a narrow mountain pass with
sheer cliffs on either side, swirling snow, painterly dramatic
light, isolated on plain white background, no shadow on the floor,
no text.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #312 — Desert duel

**File:** `apps/web/public/battle/desert-duel.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A cinematic painterly
illustration of two single warriors dueling in the open desert,
shimmering heat haze around them, kicked-up sand at their feet,
painterly orange-gold sun light, isolated on plain white background,
no shadow on the floor, no text.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #313 — Capture the gate

**File:** `apps/web/public/battle/gate-capture.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A cinematic painterly
illustration of warriors breaching a wooden city gate with a
battering ram, splinters flying, defenders silhouetted on the
walls above, painterly dramatic mid-action moment, isolated on
plain white background, no shadow on the floor, no text.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #314 — Aftermath / surrender

**File:** `apps/web/public/battle/surrender.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A cinematic painterly
illustration of a defeated commander kneeling and offering a sword
hilt-first to a victor, both figures silhouetted against painterly
sunset, isolated on plain white background, no shadow on the floor,
no text.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #315 — Rally

**File:** `apps/web/public/battle/rally.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A cinematic painterly
illustration of a general rallying their troops on a hilltop,
banner held aloft, soldiers turning to face them with weapons
raised in unison, painterly dramatic backlight, isolated on plain
white background, no shadow on the floor, no text.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

---

# SECTION 25 — City interior views

Used as backgrounds for the city-management panel. 8 prompts.

### #316 — Aurei city interior

**File:** `apps/web/public/cities/interior/aurei.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A wide painterly first-person
view inside an Aurei city — terracotta-roofed cottages along a paved
street, copper-orange banners hanging, warm golden afternoon light,
no characters, 16:9 wide aspect, no text.

[STYLE-BLOCK]
```

### #317 — Vesnar city interior

**File:** `apps/web/public/cities/interior/vesnar.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A wide painterly first-person
view inside a Vesnar city — timber longhouses around a central
council fire, leaves drifting through dappled sun, deep green
banners, no characters, 16:9 wide aspect, no text.

[STYLE-BLOCK]
```

### #318 — Thalassia city interior

**File:** `apps/web/public/cities/interior/thalassia.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A wide painterly first-person
view inside a Thalassia city — white-stuccoed houses around a
plaza fountain, distant harbor visible at the edge, sea-teal banners
fluttering, no characters, 16:9 wide aspect, no text.

[STYLE-BLOCK]
```

### #319 — Kyron city interior

**File:** `apps/web/public/cities/interior/kyron.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A wide painterly first-person
view inside a Kyron camp — a circle of yurts around a central
chieftain's hall, horses grazing at the edges, painterly warm
evening light, violet-and-gold banners, no characters, 16:9 wide
aspect, no text.

[STYLE-BLOCK]
```

### #320 — Generic market square

**File:** `apps/web/public/cities/interior/market.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A wide painterly view of a
busy market square at midday — colored awnings over stalls, baskets
of fruit, bolts of cloth, hanging meat, painterly market warmth, no
clear characters, 16:9 wide aspect, no text.

[STYLE-BLOCK]
```

### #321 — Generic forge

**File:** `apps/web/public/cities/interior/forge.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A wide painterly view of an
inside of a stone forge — glowing crucible, anvil with a hammer
beside, racks of finished weapons against the wall, painterly warm
fire light, no characters, 16:9 wide aspect, no text.

[STYLE-BLOCK]
```

### #322 — Generic library hall

**File:** `apps/web/public/cities/interior/library.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A wide painterly view of the
inside of a tall library hall — stone columns flanking towering
shelves of scrolls and books, dust motes in painterly amber light
streaming from high windows, no characters, 16:9 wide aspect, no
text on the books or scrolls.

[STYLE-BLOCK]
```

### #323 — Generic temple sanctum

**File:** `apps/web/public/cities/interior/temple.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A wide painterly view inside a
small stone temple sanctum — a single tall altar with a soft
glowing brass bowl on top, decorative pillars, painterly soft
warm light from above, no characters, 16:9 wide aspect, no text.

[STYLE-BLOCK]
```

---

# SECTION 26 — Naval units

Sea-faring units unlocked by tech. Each in 4 faction colors. 8 prompts.

### #324 — Galley (Aurei)

**File:** `apps/web/public/units/naval/galley-aurei.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric painterly
illustration of an Aurei galley — single-tier wooden warship with
copper-trimmed hull, ivory sail bearing the Aurei sun emblem, oars
extended, painterly painterly light catching the wave foam, isolated
on plain white background, no shadow on the floor, no text.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #325 — Galley (Vesnar)

**File:** `apps/web/public/units/naval/galley-vesnar.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric painterly
illustration of a Vesnar galley — wooden hull with carved leaf-pattern
bow ornament, deep-green sail bearing the Vesnar leaf emblem, oars
extended, isolated on plain white background, no shadow on the floor,
no text.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #326 — Galley (Thalassia)

**File:** `apps/web/public/units/naval/galley-thalassia.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric painterly
illustration of a Thalassia galley — sleek silver-trimmed wooden hull,
sea-teal sail bearing the quill-and-wave emblem, twin tiers of oars,
isolated on plain white background, no shadow on the floor, no text.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #327 — Galley (Kyron)

**File:** `apps/web/public/units/naval/galley-kyron.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric painterly
illustration of a Kyron galley — heavy wooden hull with fur-trimmed
gunwale, violet sail bearing the gold horse emblem, oars extended,
isolated on plain white background, no shadow on the floor, no text.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #328 — Trireme (Aurei)

**File:** `apps/web/public/units/naval/trireme-aurei.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric painterly
illustration of an Aurei trireme — three-tiered wooden warship with
bronze ram at prow, copper-orange dual sails, isolated on plain white
background, no shadow on the floor, no text.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #329 — Trireme (Vesnar)

**File:** `apps/web/public/units/naval/trireme-vesnar.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric painterly
illustration of a Vesnar trireme — three-tiered wooden warship with
carved oak ram, deep-green dual sails with woven leaf trim, isolated
on plain white background, no shadow on the floor, no text.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #330 — Trireme (Thalassia)

**File:** `apps/web/public/units/naval/trireme-thalassia.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric painterly
illustration of a Thalassia trireme — silver-trimmed three-tiered
warship with elegant slim ram, sea-teal dual sails with silver
embroidery, isolated on plain white background, no shadow on the
floor, no text.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #331 — Trireme (Kyron)

**File:** `apps/web/public/units/naval/trireme-kyron.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric painterly
illustration of a Kyron trireme — heavily-armored three-tiered
warship with gold-trimmed iron ram, violet sails with gold horse
emblems, isolated on plain white background, no shadow on the floor,
no text.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

---

# SECTION 27 — Religion / culture buildings

Faction-specific holy sites and cultural buildings. 8 prompts.

### #332 — Aurei sun shrine

**File:** `apps/web/public/holy/aurei-shrine.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of a
small Aurei sun shrine — a circular ivory pavilion with a polished
copper sun-disc set in the central wall, four columns supporting a
painted ceiling, no characters, isolated on plain white background,
square 1:1 aspect.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #333 — Vesnar druid stones

**File:** `apps/web/public/holy/vesnar-stones.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of a
Vesnar druid stone circle — eight standing moss-covered carved
monoliths around a small grass-tufted central stone altar, no
characters, isolated on plain white background, square 1:1 aspect.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #334 — Thalassia academy

**File:** `apps/web/public/holy/thalassia-academy.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of a
Thalassia academy — a small marble building with silver-leaf trim,
slim columns flanking a tall reading-arcade entrance, scrolls
visible inside, no characters, isolated on plain white background,
square 1:1 aspect.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #335 — Kyron horse-spirit altar

**File:** `apps/web/public/holy/kyron-altar.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of a
Kyron horse-spirit altar — a carved stone block with a brass horse
statue on top, ringed by hanging ribbons of violet and gold cloth
on poles, no characters, isolated on plain white background, square
1:1 aspect.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #336 — Theater

**File:** `apps/web/public/culture/theater.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of a
small open-air theater — semi-circular tiered seating around a
central wooden stage, painted backdrop curtain, no characters,
isolated on plain white background, square 1:1 aspect.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #337 — University

**File:** `apps/web/public/culture/university.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of a
small university quadrangle — four stone-and-timber lecture halls
arranged around a central garden with a sundial, no characters,
isolated on plain white background, square 1:1 aspect.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #338 — Bath house

**File:** `apps/web/public/culture/bath.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of a
small Roman-inspired stone bath house — a tiled pool inside an
arched colonnade, painterly steam rising, no characters, isolated
on plain white background, square 1:1 aspect.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #339 — Granary

**File:** `apps/web/public/culture/granary.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of a
small stone-and-timber granary — round building with a conical
thatched roof, ladder up the side to a loft door, sacks of grain
visible inside, no characters, isolated on plain white background,
square 1:1 aspect.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

---

# SECTION 28 — Map atmosphere & frame

Atmospheric pieces that dress the edges and overlay of the world map. 8 prompts.

### #340 — Map edge cloud cover

**File:** `apps/web/public/atmosphere/edge-clouds.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A horizontal painterly band of
fluffy soft white-and-pink low clouds drifting across, designed to
sit at the edge of a top-down map fading the world's edge into
mystery, isolated on plain white background, no terrain, no
characters, no text.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #341 — Distant mountain silhouette

**File:** `apps/web/public/atmosphere/mountain-silhouette.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A horizontal painterly band of
distant blue-grey mountain silhouettes layered into hazy distance,
designed to sit at the edge of a top-down map, isolated on plain
white background, no terrain, no characters, no text.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #342 — Sea horizon line

**File:** `apps/web/public/atmosphere/sea-horizon.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A horizontal painterly band of
deep ocean blue with a single faint distant ship silhouette,
designed to sit at the edge of a top-down map, isolated on plain
white background, no terrain, no characters, no text.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #343 — Compass rose

**File:** `apps/web/public/atmosphere/compass.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly compass rose — an
eight-point star design in copper and ivory ink, with a small
flourish of vines at the cardinal points, suitable to overlay on a
parchment map corner, isolated on plain white background, no
shadow, no text.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #344 — Parchment map texture

**File:** `apps/web/public/atmosphere/parchment.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly aged
parchment texture, warm beige with faint stains and edge wear,
suitable as a UI background or tooltip backing, isolated, no text,
no characters, square 1:1 aspect.

[STYLE-BLOCK]
```

### #345 — Vignette ring

**File:** `apps/web/public/atmosphere/vignette.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly vignette
ring — soft warm-amber edges fading into transparent center, gentle
brushwork edge, isolated on plain white background, square 1:1
aspect, no characters, no text.

[STYLE-BLOCK]
```

### #346 — Day-night transition gradient

**File:** `apps/web/public/atmosphere/day-night-gradient.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A horizontal painterly band
gradient from warm-pink dawn on the left through midday gold to
deep-blue twilight on the right, suitable for time-of-day overlay,
no terrain, no characters, no text.

[STYLE-BLOCK]
```

### #347 — Aerial flock of birds

**File:** `apps/web/public/atmosphere/birds.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly small V-shaped
flock of distant birds in flight, soft silhouettes against
imaginary sky, isolated on plain white background, no clouds, no
terrain, no text.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

---

# Production tips for Volume II

1. Generate **after** Volume I is complete — the cohesive style anchor depends on the V1 set.
2. **Group by chat:** S16 (seasons) in one chat, S17 + S18 (combat poses + walk frames) in another, S19 (civilians) separately, etc.
3. **Walk-cycle frames** (S18) need particular care — generate frames 1-4 for one unit class in a single message bundle so DALL-E can reference each frame against the others. Otherwise frames look mismatched.
4. **Wonders** (S20) and **city interiors** (S25) are big set-pieces — generate fewer per chat (3-4) so each gets full attention.
5. **Faction skins matter:** if you only generate 1 of 4 faction variants per role and need to skip a faction, that's fine — engine will fall back to the generic version.
6. **Optional:** if any whole section feels like premature scope (S19 civilians, S22 dawn/dusk, S25 interiors), skip it — every section is independently optional.

# Final tally across both volumes

|  | Volume I | Volume II | Total |
|---|---|---|---|
| Hex tile biomes | 21 | 16 (seasons) + 8 (dawn/dusk) | **45** |
| Resources | 5 | — | 5 |
| Units (idle / faction skins) | 24 | — | 24 |
| Unit poses | — | 24 | 24 |
| Unit walk frames | — | 24 | 24 |
| Civilian/diplomatic units | — | 24 | 24 |
| Cities (faction × stage) | 16 | — | 16 |
| Faction emblems + banners | 8 | — | 8 |
| Tech icons | 8 | — | 8 |
| UI + HUD glyphs | 21 | — | 21 |
| Effects | 10 | — | 10 |
| Splash + portraits + endgame | 10 | — | 10 |
| Decorative tile variants | 15 | 12 (discovery) | 27 |
| Polish (panels/frames) | 7 | 8 (atmosphere) | 15 |
| Wildlife | 10 | — | 10 |
| Worker improvements | 8 | — | 8 |
| Biome edge transitions | 5 | — | 5 |
| Night biomes | 7 | — | 7 |
| Wonder buildings | — | 12 | 12 |
| Weather overlays | — | 8 | 8 |
| Battle illustration cards | — | 12 | 12 |
| City interior views | — | 8 | 8 |
| Naval units | — | 8 | 8 |
| Religion + culture | — | 8 | 8 |

Volume I prompts: **#0 – #175** (176 total).
Volume II prompts: **#176 – #347** (172 total).
**Grand total: 348 prompts** for the full painted Nyrvexa.

When you have a set of assets ready and dropped into the right paths, ping me — I'll wire `apps/web/src/lib/assets.ts` and the renderer to consume them; switching from hand-drawn Pixi Graphics to your painted DALL-E art is roughly a one-day code job once the files are in place.
