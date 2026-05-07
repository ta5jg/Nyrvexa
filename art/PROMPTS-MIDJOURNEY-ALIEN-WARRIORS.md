<!-- =============================================================================
File:           art/PROMPTS-MIDJOURNEY-ALIEN-WARRIORS.md
Author:         USDTG GROUP TECHNOLOGY LLC
Developer:      Irfan Gedik
Created Date:   2026-05-07
Last Update:    2026-05-07
Version:        0.5.0

Description:
  Midjourney v6 prompts for alien warriors, alien wildlife, and alien
  elite / boss creatures. Civilization-builder players engage these
  units in combat. Each design is a distinct individual — has form,
  posture, weapons or tools — but its biology is completely
  non-derivative of any Earth species.

  Pairs with PROMPTS-DALLE-ALIENS.md (the abstract architecture/
  phenomena set). Together they cover the full alien antagonist
  surface: phenomena & structures from the abstract atlas, named
  warrior classes & wildlife from this atlas.

License:
  Proprietary. All rights reserved. See LICENSE in the repository root.
============================================================================= -->

# Nyrvexa / Nyrvexis / Nyrduel — Alien Warrior Atlas (Midjourney)

This file is for **Midjourney v6**. Open Midjourney (Discord), `/imagine`,
paste any prompt below as-is. Each prompt is single-block, copy-paste
ready, includes the negative-prompt `--no` flag, and ends with the
correct aspect / version / style flags.

The design rule (see `feedback_aliens_non_biological.md`): aliens **may
be creatures with form, posture, weapons, agency** — they just must not
look like any Earth species. No spider, octopus, insect, lizard, mammal,
bird, fish, or plant body plans. Build aliens out of crystal / energy /
light / folded space / metal / geometric solids. Heads are not in the
"top" position; eyes are not animal-eye shaped; limbs (when present) are
energy ribbons, crystalline extensions, or geometric protrusions —
never anatomical arms or legs.

---

## How to use

1. Open Midjourney in Discord.
2. Type `/imagine` and paste a prompt below verbatim.
3. Pick the variation that best matches the brief. Upscale, save as
   PNG, then convert to WebP locally with `art/optimize-assets.sh`.
4. Drop the file into the path noted under each prompt.
5. Generate one full archetype set (idle / attack pose / hit pose) in
   the same Midjourney session — Midjourney's seed memory keeps style
   coherent across the set.

---

## Common style anchor (already baked into every prompt below)

Every prompt below includes this language so you don't have to add it:

- **Style:** Studio Ghibli + painted illustration, soft warm volumetric
  lighting, hand-painted texture, cohesive painted-fantasy palette of
  deep violets, electric teals, void blacks, with bioluminescent
  highlights and warm-orange threat accents.
- **Composition:** centered subject, isolated on plain neutral
  background, no other elements, no text, no watermark.
- **Negative prompt:** `--no humanoid face, mammalian, insectoid, spider,
  arachnid, octopus, cephalopod, tentacles, reptile, scales, lizard,
  bird, beak, feathers, fish, fins, plant, vines, animal eyes with
  pupils, gnashing teeth, claws, fur, exposed flesh, gore, blood,
  segmented body, chitin, exoskeleton, photorealistic, photo`
- **Flags:** `--ar 3:4 --v 6 --style raw`

---

# SECTION A — Twelve named alien warrior classes

These are combat units. Each has a clear silhouette so players can
identify them on a battlefield. Three poses each (idle / attack /
defeated) so the renderer can sequence states in combat.

## A.1 Aether Trooper (basic infantry)

Form: hovering humanoid-scale figure made of layered violet crystal
plate-armour. NO face: a glowing geometric glyph hovers where a head
would be. NO legs: gravitational anchor at the base. Holds a folded-
space rifle.

### #1 — Aether Trooper, idle

**File:** `apps/web/public/aliens/warriors/aether-trooper-idle.webp`

```
A painterly fantasy illustration of an alien warrior class called an
Aether Trooper. Hovering humanoid-scale figure with layered violet
crystal plate armour, no face, a softly glowing rotating geometric
glyph in the position where a head would normally be, no legs — the
torso ends at a gravitational anchor of swirling teal energy. Holds a
slim folded-space rifle of crystalline metal in both gauntleted shard-
hands. Standing-at-ease pose, calm posture, isolated on plain neutral
background. Studio Ghibli painted illustration style, soft warm
volumetric lighting, deep violet and electric teal palette with warm-
orange threat accents on the rifle muzzle, hand-painted texture, no
text, no watermark. --ar 3:4 --v 6 --style raw --no humanoid face,
mammalian, insectoid, spider, arachnid, octopus, cephalopod, tentacles,
reptile, scales, lizard, bird, beak, feathers, fish, fins, plant,
vines, animal eyes with pupils, gnashing teeth, claws, fur, exposed
flesh, gore, blood, segmented body, chitin, exoskeleton, photorealistic,
photo
```

### #2 — Aether Trooper, attack

**File:** `apps/web/public/aliens/warriors/aether-trooper-attack.webp`

```
A painterly fantasy illustration of an Aether Trooper alien warrior
mid-attack — hovering humanoid-scale figure in layered violet crystal
plate armour, no face but a brightly blazing geometric glyph in the
head position, gravitational anchor of swirling teal energy at the
base, folded-space rifle raised and discharging a horizontal beam of
indigo-violet energy with a white-teal core. Aggressive forward
projection pose, isolated on plain neutral background. Studio Ghibli
painted illustration style, soft warm volumetric lighting, deep violet
and electric teal palette, hand-painted texture, no text, no watermark.
--ar 3:4 --v 6 --style raw --no humanoid face, mammalian, insectoid,
spider, octopus, tentacles, reptile, scales, fish, plant, animal eyes
with pupils, teeth, claws, fur, segmented body, chitin, exoskeleton,
photorealistic, photo
```

### #3 — Aether Trooper, defeated

**File:** `apps/web/public/aliens/warriors/aether-trooper-defeated.webp`

```
A painterly fantasy illustration of a defeated Aether Trooper alien
warrior — humanoid-scale crystal plate armour broken and slumped, the
glyph in the head position dim and fragmented, the gravitational
anchor at the base extinguished so the figure now rests on the ground,
rifle fallen beside, painterly soft dispersing teal particles rising
from the seams of the armour. No body inside the armour, no organic
remains, no gore. Isolated on plain neutral background. Studio Ghibli
painted illustration style, soft warm volumetric lighting, deep violet
and electric teal palette, hand-painted texture, no text, no
watermark. --ar 3:4 --v 6 --style raw --no humanoid face, mammalian,
insectoid, organic remains, gore, blood, exposed flesh, animal anatomy,
photorealistic, photo
```

## A.2 Lattice Knight (heavy infantry)

Form: hovering geode-shaped torso of layered crystal plates, no head,
no legs (gravitational anchor). Holds a long energy-blade. Heavier
silhouette than Aether Trooper.

### #4 — Lattice Knight, idle

**File:** `apps/web/public/aliens/warriors/lattice-knight-idle.webp`

```
A painterly fantasy illustration of an alien warrior class called a
Lattice Knight. Heavy hovering geode-shaped torso of overlapping
hexagonal violet crystal plates, no head at all (where a head would
be there is just a wider top facet of the geode with a single deep
teal sensor-gem set into it), no legs — gravitational anchor of
violet swirling energy at the base. Wields a tall energy-blade of
white-teal light held vertically, hilt in armoured shard-grip
extension. Calm imposing standing-guard pose, isolated on plain
neutral background. Studio Ghibli painted illustration style, soft
warm volumetric lighting, deep violet and electric teal palette,
hand-painted texture, no text, no watermark. --ar 3:4 --v 6 --style
raw --no humanoid face, mammalian, insectoid, spider, octopus,
tentacles, reptile, scales, fish, plant, animal eyes with pupils,
teeth, claws, fur, segmented body, chitin, exoskeleton, photorealistic,
photo
```

### #5 — Lattice Knight, attack

**File:** `apps/web/public/aliens/warriors/lattice-knight-attack.webp`

```
A painterly fantasy illustration of a Lattice Knight alien warrior
mid-strike — heavy geode-shaped crystal torso lunging forward, the
sensor-gem at the top blazing teal, energy-blade swept in a wide
diagonal arc trailing painterly motion-blur, gravitational anchor at
the base elongated and tilted into the lunge. Dynamic forward attack
pose, isolated on plain neutral background. Studio Ghibli painted
illustration style, soft warm volumetric lighting, deep violet and
electric teal palette, hand-painted texture, no text, no watermark.
--ar 3:4 --v 6 --style raw --no humanoid face, mammalian, insectoid,
spider, octopus, tentacles, reptile, scales, fish, plant, animal eyes
with pupils, teeth, claws, fur, segmented body, chitin, exoskeleton,
photorealistic, photo
```

### #6 — Lattice Knight, defeated

**File:** `apps/web/public/aliens/warriors/lattice-knight-defeated.webp`

```
A painterly fantasy illustration of a fallen Lattice Knight alien
warrior — geode-shaped crystal torso cracked open along several
plates, the central sensor-gem dim, gravitational anchor extinguished
so the torso rests on the ground tilted, energy-blade fallen and
darkened beside, painterly soft dispersing teal particles rising from
the cracks. No biological remains inside the geode, no gore. Isolated
on plain neutral background. Studio Ghibli painted illustration style,
soft warm volumetric lighting, deep violet and electric teal palette,
hand-painted texture, no text, no watermark. --ar 3:4 --v 6 --style
raw --no humanoid face, organic remains, gore, blood, exposed flesh,
animal anatomy, photorealistic, photo
```

## A.3 Shard Skirmisher (fast scout)

Form: small triangular floating frame, with three razor-shaped
crystal shards orbiting at its perimeter. Speedy. No body proper,
no head — just the three orbiting shards around a glowing core
triangle.

### #7 — Shard Skirmisher, idle

**File:** `apps/web/public/aliens/warriors/shard-skirmisher-idle.webp`

```
A painterly fantasy illustration of an alien warrior class called a
Shard Skirmisher. Small floating triangular violet-and-teal frame at
chest height, with three sharp razor-shaped crystal shards orbiting
slowly at its perimeter. The triangular frame has a single faint
glowing teal core in its centre. No body of any kind, no head — the
entity IS the orbiting shard cluster. Calm hovering pose, isolated on
plain neutral background. Studio Ghibli painted illustration style,
soft warm volumetric lighting, deep violet and electric teal palette,
hand-painted texture, no text, no watermark. --ar 3:4 --v 6 --style
raw --no humanoid face, mammalian, insectoid, spider, octopus,
tentacles, reptile, fish, plant, animal eyes, teeth, claws, fur,
segmented body, chitin, exoskeleton, photorealistic, photo
```

### #8 — Shard Skirmisher, attack

**File:** `apps/web/public/aliens/warriors/shard-skirmisher-attack.webp`

```
A painterly fantasy illustration of a Shard Skirmisher alien warrior
mid-attack — triangular core frame angled forward, all three razor
shards launched outward in a coordinated forward volley with painterly
motion-trails, the core blazing bright teal with an aggressive
discharge glow. Dynamic precision-strike pose, isolated on plain
neutral background. Studio Ghibli painted illustration style, soft
warm volumetric lighting, deep violet and electric teal palette,
hand-painted texture, no text, no watermark. --ar 3:4 --v 6 --style
raw --no humanoid face, mammalian, insectoid, spider, octopus,
tentacles, reptile, fish, plant, animal eyes, teeth, claws, fur,
segmented body, chitin, exoskeleton, photorealistic, photo
```

### #9 — Shard Skirmisher, defeated

**File:** `apps/web/public/aliens/warriors/shard-skirmisher-defeated.webp`

```
A painterly fantasy illustration of a Shard Skirmisher alien warrior
defeated — triangular core frame cracked and dim, two of the orbiting
shards broken and tumbling, the third drifting away unmoored, central
core dark, painterly soft dispersing teal particles fading. No
biological remains, no gore. Isolated on plain neutral background.
Studio Ghibli painted illustration style, soft warm volumetric
lighting, deep violet and electric teal palette, hand-painted texture,
no text, no watermark. --ar 3:4 --v 6 --style raw --no organic remains,
gore, blood, animal anatomy, photorealistic, photo
```

## A.4 Glyph Caster (ranged caster)

Form: tall floating columnar entity. Body wrapped in flowing violet
ribbon-fabric. Where a head would be: a horizontal rotating ring of
glowing geometric glyphs, no face. Casts geometric attacks.

### #10 — Glyph Caster, idle

**File:** `apps/web/public/aliens/warriors/glyph-caster-idle.webp`

```
A painterly fantasy illustration of an alien warrior class called a
Glyph Caster. Tall floating columnar entity, body wrapped in flowing
violet ribbon-fabric that hangs and folds in painterly cloth, no
visible flesh underneath — just darkness inside the wrappings. Where
a head would be: a horizontal rotating ring of glowing teal geometric
glyphs, no face. Two long sleeves of the ribbon-fabric extend forward
holding a small floating prismatic geometry between them, like a
contained spell. Calm hovering casting-stance, isolated on plain
neutral background. Studio Ghibli painted illustration style, soft
warm volumetric lighting, deep violet and electric teal palette, hand-
painted texture, no text, no watermark. --ar 3:4 --v 6 --style raw
--no humanoid face, mammalian, insectoid, spider, octopus, tentacles,
reptile, fish, plant, animal eyes with pupils, teeth, claws, fur,
segmented body, chitin, exoskeleton, photorealistic, photo
```

### #11 — Glyph Caster, attack

**File:** `apps/web/public/aliens/warriors/glyph-caster-attack.webp`

```
A painterly fantasy illustration of a Glyph Caster alien warrior mid-
incantation — tall columnar entity wrapped in violet ribbon-fabric,
the rotating glyph-ring at the head position blazing bright, the
ribbon-fabric sleeves extended dramatically forward casting a forming
geometric construct of violet-and-teal energy with arcing electric
filaments — the spell about to release. Dynamic dramatic casting
pose, isolated on plain neutral background. Studio Ghibli painted
illustration style, soft warm volumetric lighting, deep violet and
electric teal palette, hand-painted texture, no text, no watermark.
--ar 3:4 --v 6 --style raw --no humanoid face, mammalian, insectoid,
spider, octopus, tentacles, reptile, fish, plant, animal eyes, teeth,
claws, fur, segmented body, chitin, exoskeleton, photorealistic, photo
```

### #12 — Glyph Caster, defeated

**File:** `apps/web/public/aliens/warriors/glyph-caster-defeated.webp`

```
A painterly fantasy illustration of a fallen Glyph Caster alien
warrior — tall columnar form collapsed forward, ribbon-fabric draped
limp on the ground, the glyph-ring at the head position broken into
disconnected fragments hovering dimly, no body inside the wrappings
since none was visible to begin with, painterly soft dispersing teal
particles. No biological remains, no gore. Isolated on plain neutral
background. Studio Ghibli painted illustration style, soft warm
volumetric lighting, deep violet and electric teal palette, hand-
painted texture, no text, no watermark. --ar 3:4 --v 6 --style raw
--no organic remains, gore, blood, exposed flesh, animal anatomy,
photorealistic, photo
```

## A.5 Hollow Sentinel (elite guard)

Form: a tall ornate suit-of-armour shape, but the armour is empty
inside — only starlight visible through the visor slit and the joint
seams. Carries a long crystal halberd.

### #13 — Hollow Sentinel, idle

**File:** `apps/web/public/aliens/warriors/hollow-sentinel-idle.webp`

```
A painterly fantasy illustration of an alien warrior class called a
Hollow Sentinel. A tall ornate elaborate full plate-armour suit in
violet-blackened metal with teal trim, but the suit is COMPLETELY
EMPTY inside — only deep starry violet void visible through the visor
slit, through the gauntlet finger gaps, and through every joint seam.
Holds a long crystal halberd vertically beside its standing pose. No
flesh, no creature inside, no gore — just an animate empty armour.
Stoic guard pose, isolated on plain neutral background. Studio Ghibli
painted illustration style, soft warm volumetric lighting, deep
violet-black palette with teal trim and starlight glints inside the
armour, hand-painted texture, no text, no watermark. --ar 3:4 --v 6
--style raw --no humanoid face inside helm, exposed flesh, gore,
blood, mammalian, insectoid, animal anatomy, organic creature,
photorealistic, photo
```

### #14 — Hollow Sentinel, attack

**File:** `apps/web/public/aliens/warriors/hollow-sentinel-attack.webp`

```
A painterly fantasy illustration of a Hollow Sentinel alien warrior
mid-strike — tall empty plate-armour suit (only starlight visible
through every gap), crystal halberd swept in a powerful diagonal arc
trailing painterly motion blur, the suit's joint-starlight blazing
brighter as if in combat-drive, dynamic aggressive forward stance,
isolated on plain neutral background. Studio Ghibli painted
illustration style, soft warm volumetric lighting, deep violet-black
palette with teal trim and starlight glints, hand-painted texture, no
text, no watermark. --ar 3:4 --v 6 --style raw --no humanoid face
inside helm, exposed flesh, gore, mammalian, insectoid, organic
creature, photorealistic, photo
```

### #15 — Hollow Sentinel, defeated

**File:** `apps/web/public/aliens/warriors/hollow-sentinel-defeated.webp`

```
A painterly fantasy illustration of a fallen Hollow Sentinel alien
warrior — empty plate-armour suit collapsed in a kneeling pose, halberd
fallen beside, the inner starlight that animated the suit now
extinguished and dispersing as faint teal vapour, suit visibly hollow
through the gaps. No body inside, no gore, no biological remains.
Isolated on plain neutral background. Studio Ghibli painted
illustration style, soft warm volumetric lighting, deep violet-black
palette, hand-painted texture, no text, no watermark. --ar 3:4 --v 6
--style raw --no organic remains, gore, blood, exposed flesh, animal
anatomy, photorealistic, photo
```

## A.6 Mirror Twin (synced pair)

Form: two perfectly identical translucent crystal humanoid-silhouettes
mirroring each other, sharing one consciousness. Move in perfect
unison. No faces — instead a single glowing rune at the head position
on each.

### #16 — Mirror Twin, idle

**File:** `apps/web/public/aliens/warriors/mirror-twin-idle.webp`

```
A painterly fantasy illustration of an alien warrior class called
Mirror Twin. Two perfectly identical translucent crystal humanoid-
silhouettes standing facing the same direction, their poses mirrored
perfectly, no faces — instead a single glowing teal rune at the head
position of each. Both bodies are made of pale translucent violet-
crystal, faintly visible internal facets. They share one consciousness
and move in perfect unison. Standing-paired alert pose, isolated on
plain neutral background. Studio Ghibli painted illustration style,
soft warm volumetric lighting, deep violet and electric teal palette,
hand-painted texture, no text, no watermark. --ar 3:4 --v 6 --style
raw --no humanoid face, mammalian, insectoid, spider, octopus,
tentacles, reptile, fish, plant, animal eyes with pupils, teeth, claws,
fur, organic skin, photorealistic, photo
```

### #17 — Mirror Twin, attack

**File:** `apps/web/public/aliens/warriors/mirror-twin-attack.webp`

```
A painterly fantasy illustration of Mirror Twin alien warriors mid-
strike — both translucent crystal humanoid silhouettes mid-lunge in
perfect mirrored synchronisation, energy-ribbon arms extended outward
casting twin beams of teal energy that converge on a target, the
glowing runes at their head positions blazing bright. Dynamic dual-
strike pose, isolated on plain neutral background. Studio Ghibli
painted illustration style, soft warm volumetric lighting, deep
violet and electric teal palette, hand-painted texture, no text, no
watermark. --ar 3:4 --v 6 --style raw --no humanoid face, mammalian,
insectoid, spider, octopus, tentacles, reptile, fish, plant, animal
eyes, teeth, claws, fur, organic skin, photorealistic, photo
```

### #18 — Mirror Twin, defeated

**File:** `apps/web/public/aliens/warriors/mirror-twin-defeated.webp`

```
A painterly fantasy illustration of fallen Mirror Twin alien warriors
— both translucent crystal humanoid silhouettes collapsed in mirrored
poses, runes at head positions dim and broken, dispersing teal
particles rising from cracks in the crystal forms. No biological
remains, no gore. Isolated on plain neutral background. Studio Ghibli
painted illustration style, soft warm volumetric lighting, deep
violet and electric teal palette, hand-painted texture, no text, no
watermark. --ar 3:4 --v 6 --style raw --no organic remains, gore,
blood, exposed flesh, animal anatomy, photorealistic, photo
```

## A.7 Pearl Marshal (commander)

Form: a regal large floating sphere-body of polished violet-pearl
material, surrounded by 4-6 smaller orbital pearl satellites. A
hovering cape of folded space trails behind. Commands.

### #19 — Pearl Marshal, idle

**File:** `apps/web/public/aliens/warriors/pearl-marshal-idle.webp`

```
A painterly fantasy illustration of an alien warrior class called a
Pearl Marshal. A regal large floating sphere body of polished violet-
pearl material with subtle iridescence, surrounded by five smaller
orbital pearl satellites slowly circling around it at varied radii.
A long hovering cape made of folded space (dark indigo with starlight
motes inside) trails behind the main sphere. No face, no head per se;
the main pearl has a faint single glowing teal sigil etched on its
surface signifying authority. Calm imposing presiding pose, isolated
on plain neutral background. Studio Ghibli painted illustration style,
soft warm volumetric lighting, deep violet and electric teal palette,
hand-painted texture, no text, no watermark. --ar 3:4 --v 6 --style
raw --no humanoid, mammalian, insectoid, spider, octopus, tentacles,
reptile, fish, plant, animal eyes, teeth, claws, fur, organic
features, photorealistic, photo
```

### #20 — Pearl Marshal, attack

**File:** `apps/web/public/aliens/warriors/pearl-marshal-attack.webp`

```
A painterly fantasy illustration of a Pearl Marshal alien warrior in
command — regal large floating violet-pearl sphere with the
authority-sigil blazing bright, all five orbital pearl satellites
launched forward in a coordinated forward strike with painterly
motion trails, hovering folded-space cape billowing dramatically
behind. Imposing strategic-attack pose, isolated on plain neutral
background. Studio Ghibli painted illustration style, soft warm
volumetric lighting, deep violet and electric teal palette, hand-
painted texture, no text, no watermark. --ar 3:4 --v 6 --style raw
--no humanoid, mammalian, insectoid, spider, octopus, tentacles,
reptile, fish, plant, animal eyes, teeth, claws, fur, organic
features, photorealistic, photo
```

### #21 — Pearl Marshal, defeated

**File:** `apps/web/public/aliens/warriors/pearl-marshal-defeated.webp`

```
A painterly fantasy illustration of a defeated Pearl Marshal alien
warrior — large violet-pearl sphere fractured and resting on the
ground, authority-sigil dim, three of the five orbital satellites
fallen alongside, two drifting away unmoored, the folded-space cape
collapsed into a faint indigo puddle. No biological remains, no gore.
Isolated on plain neutral background. Studio Ghibli painted
illustration style, soft warm volumetric lighting, deep violet and
electric teal palette, hand-painted texture, no text, no watermark.
--ar 3:4 --v 6 --style raw --no organic remains, gore, blood, animal
anatomy, photorealistic, photo
```

## A.8 Echo Walker (phaser)

Form: three semi-transparent overlapping copies of the same crystal
humanoid silhouette, slightly offset, all moving in slight time-lag
of each other. The "real" body is somewhere between the three echoes.

### #22 — Echo Walker, idle

**File:** `apps/web/public/aliens/warriors/echo-walker-idle.webp`

```
A painterly fantasy illustration of an alien warrior class called an
Echo Walker. Three semi-transparent overlapping copies of the same
crystal humanoid-silhouette form, each copy slightly offset to one
side and partially translucent, all in the same calm standing pose.
The crystal silhouettes have no faces, just smooth pale violet
surfaces with a single small glowing teal core in the centre of the
chest area visible through all three copies. Painterly time-lag
phasing effect, isolated on plain neutral background. Studio Ghibli
painted illustration style, soft warm volumetric lighting, deep
violet and electric teal palette, hand-painted texture, no text, no
watermark. --ar 3:4 --v 6 --style raw --no humanoid face, mammalian,
insectoid, spider, octopus, tentacles, reptile, fish, plant, animal
eyes, teeth, claws, fur, organic skin, photorealistic, photo
```

### #23 — Echo Walker, attack

**File:** `apps/web/public/aliens/warriors/echo-walker-attack.webp`

```
A painterly fantasy illustration of an Echo Walker alien warrior
striking — three semi-transparent overlapping crystal humanoid
silhouettes mid-strike, each copy at a slightly different stage of
the same striking motion (like a stop-motion strobe), forming a
sweeping painterly phase trail. The chest cores blaze bright teal,
energy-ribbon strike emerging from the central body. Dynamic
phasing-strike pose, isolated on plain neutral background. Studio
Ghibli painted illustration style, soft warm volumetric lighting,
deep violet and electric teal palette, hand-painted texture, no text,
no watermark. --ar 3:4 --v 6 --style raw --no humanoid face,
mammalian, insectoid, spider, octopus, tentacles, reptile, fish,
plant, animal eyes, teeth, claws, fur, organic skin, photorealistic,
photo
```

### #24 — Echo Walker, defeated

**File:** `apps/web/public/aliens/warriors/echo-walker-defeated.webp`

```
A painterly fantasy illustration of a defeated Echo Walker alien
warrior — the three semi-transparent overlapping crystal humanoid
copies collapsed into a single faded silhouette resting on the
ground, the chest core dim, the offset copies dispersing as faint
violet vapour. No biological remains, no gore. Isolated on plain
neutral background. Studio Ghibli painted illustration style, soft
warm volumetric lighting, deep violet and electric teal palette,
hand-painted texture, no text, no watermark. --ar 3:4 --v 6 --style
raw --no organic remains, gore, blood, exposed flesh, animal anatomy,
photorealistic, photo
```

## A.9 Singular Bearer (heavy striker)

Form: tall humanoid-scale armoured silhouette holding/containing a
small but intense singularity at chest-level. The body of folded
black metal-fabric wraps around the singularity. Single visor slit
showing dark inside.

### #25 — Singular Bearer, idle

**File:** `apps/web/public/aliens/warriors/singular-bearer-idle.webp`

```
A painterly fantasy illustration of an alien warrior class called a
Singular Bearer. A tall humanoid-scale silhouette in a robe of folded
black metal-fabric, holding a small but intensely bright violet-and-
white singularity at chest level cradled between two armoured shard-
gauntlet hands. Where a head would be: a tall pointed hood with only
a thin horizontal teal-glowing visor slit visible inside. No face, no
flesh. Painterly gravitational distortion shimmer around the held
singularity. Standing braced pose, isolated on plain neutral
background. Studio Ghibli painted illustration style, soft warm
volumetric lighting, deep violet-black palette with brilliant white-
teal singularity highlight, hand-painted texture, no text, no
watermark. --ar 3:4 --v 6 --style raw --no humanoid face, mammalian,
insectoid, spider, octopus, tentacles, reptile, fish, plant, animal
eyes with pupils, teeth, claws, fur, exposed flesh, photorealistic,
photo
```

### #26 — Singular Bearer, attack

**File:** `apps/web/public/aliens/warriors/singular-bearer-attack.webp`

```
A painterly fantasy illustration of a Singular Bearer alien warrior
mid-strike — tall robed silhouette flinging the singularity it had
been cradling forward toward an unseen target, the singularity
trailing a brilliant white-violet streak of distorted space behind
it, robe billowing back from the throw motion, visor slit blazing
bright teal. Dynamic forward-throw pose, isolated on plain neutral
background. Studio Ghibli painted illustration style, soft warm
volumetric lighting, deep violet-black palette with brilliant white-
teal singularity highlight, hand-painted texture, no text, no
watermark. --ar 3:4 --v 6 --style raw --no humanoid face, mammalian,
insectoid, spider, octopus, tentacles, reptile, fish, plant, animal
eyes, teeth, claws, fur, exposed flesh, photorealistic, photo
```

### #27 — Singular Bearer, defeated

**File:** `apps/web/public/aliens/warriors/singular-bearer-defeated.webp`

```
A painterly fantasy illustration of a defeated Singular Bearer alien
warrior — tall robed silhouette collapsed forward, the singularity it
had been cradling now released and visible as a small dim point on
the ground beside, hood fallen back to reveal nothing inside, robe
draped lifeless. No body inside the robe, no organic remains, no gore.
Isolated on plain neutral background. Studio Ghibli painted
illustration style, soft warm volumetric lighting, deep violet-black
palette, hand-painted texture, no text, no watermark. --ar 3:4 --v 6
--style raw --no organic remains, gore, blood, exposed flesh, animal
anatomy, photorealistic, photo
```

## A.10 Voidsmith (caster / leader)

Form: large robe-wrapped entity, no visible body. Holds a long
"folding tool" — a tall instrument that bends space around it — like
a smith working invisible material. The head position is a perfect
black void inside the hood.

### #28 — Voidsmith, idle

**File:** `apps/web/public/aliens/warriors/voidsmith-idle.webp`

```
A painterly fantasy illustration of an alien warrior class called a
Voidsmith. A large floating entity wrapped in heavy violet-trimmed
black robes, no visible body underneath the robes. Holds a long
ornate ceremonial space-folding tool of dark crystal and hammered
metal at vertical resting position. The head position inside the deep
hood shows only a perfect black void with two small but bright teal
star-points where eyes would be (NOT animal eyes — just star-points
in the darkness). Calm dignified working-pose, isolated on plain
neutral background. Studio Ghibli painted illustration style, soft
warm volumetric lighting, deep violet-black palette with electric teal
star-glints, hand-painted texture, no text, no watermark. --ar 3:4
--v 6 --style raw --no humanoid face, mammalian, insectoid, spider,
octopus, tentacles, reptile, fish, plant, animal eyes with pupils,
teeth, claws, fur, exposed flesh, photorealistic, photo
```

### #29 — Voidsmith, attack

**File:** `apps/web/public/aliens/warriors/voidsmith-attack.webp`

```
A painterly fantasy illustration of a Voidsmith alien warrior at work
of war — large robed entity bringing the long space-folding tool down
in a mighty arc, the tool tearing painterly visible folds in space
forward toward an unseen target with a wave of violet-and-teal
energy. The hood's interior void with star-point eyes blazes bright.
Robes billowing dramatically. Isolated on plain neutral background.
Studio Ghibli painted illustration style, soft warm volumetric
lighting, deep violet-black palette with electric teal accents, hand-
painted texture, no text, no watermark. --ar 3:4 --v 6 --style raw
--no humanoid face, mammalian, insectoid, spider, octopus, tentacles,
reptile, fish, plant, animal eyes with pupils, teeth, claws, fur,
exposed flesh, photorealistic, photo
```

### #30 — Voidsmith, defeated

**File:** `apps/web/public/aliens/warriors/voidsmith-defeated.webp`

```
A painterly fantasy illustration of a fallen Voidsmith alien warrior
— large robe draped collapsed on the ground with no body inside, the
space-folding tool fallen and broken in half beside, the star-point
eyes inside the hood extinguished. Painterly faint dispersing teal
mist rising from the empty robe folds. No biological remains, no gore.
Isolated on plain neutral background. Studio Ghibli painted
illustration style, soft warm volumetric lighting, deep violet-black
palette, hand-painted texture, no text, no watermark. --ar 3:4 --v 6
--style raw --no organic remains, gore, blood, animal anatomy,
photorealistic, photo
```

## A.11 Fractal Dancer (elusive elite)

Form: shifts between geometric configurations during combat, like
animated origami in motion. No fixed body shape — sometimes a
floating bouquet of polygons, sometimes a tall column, sometimes
spread wide. Threatening through unpredictability.

### #31 — Fractal Dancer, idle

**File:** `apps/web/public/aliens/warriors/fractal-dancer-idle.webp`

```
A painterly fantasy illustration of an alien warrior class called a
Fractal Dancer. A floating cluster of constantly shifting violet-
and-teal geometric polygons (triangles, hexagons, rhombi) loosely
held together in a vaguely vertical formation, like animated paper
origami in slow motion. No fixed shape — the polygons drift between
arrangements. Faint glowing connecting energy threads between the
polygons. No head, no body, no face — the dancer IS the shifting
geometry. Calm slowly-shifting hover pose, isolated on plain neutral
background. Studio Ghibli painted illustration style, soft warm
volumetric lighting, deep violet and electric teal palette, hand-
painted texture, no text, no watermark. --ar 3:4 --v 6 --style raw
--no humanoid face, mammalian, insectoid, spider, octopus, tentacles,
reptile, fish, plant, animal eyes, teeth, claws, fur, organic
creature, photorealistic, photo
```

### #32 — Fractal Dancer, attack

**File:** `apps/web/public/aliens/warriors/fractal-dancer-attack.webp`

```
A painterly fantasy illustration of a Fractal Dancer alien warrior
mid-strike — the cluster of shifting violet-and-teal geometric
polygons has snapped into a forward-pointing arrowhead spear-formation,
all polygons aligned and blazing bright, painterly motion-trails of
energy threads behind. Dynamic precise-strike pose, isolated on plain
neutral background. Studio Ghibli painted illustration style, soft
warm volumetric lighting, deep violet and electric teal palette,
hand-painted texture, no text, no watermark. --ar 3:4 --v 6 --style
raw --no humanoid face, mammalian, insectoid, spider, octopus,
tentacles, reptile, fish, plant, animal eyes, teeth, claws, fur,
organic creature, photorealistic, photo
```

### #33 — Fractal Dancer, defeated

**File:** `apps/web/public/aliens/warriors/fractal-dancer-defeated.webp`

```
A painterly fantasy illustration of a defeated Fractal Dancer alien
warrior — the cluster of geometric polygons has lost its connecting
energy threads and the polygons are scattered on the ground in a
disordered pile, dim and inert, painterly soft dispersing teal vapour.
No biological remains, no gore. Isolated on plain neutral background.
Studio Ghibli painted illustration style, soft warm volumetric
lighting, deep violet and electric teal palette, hand-painted texture,
no text, no watermark. --ar 3:4 --v 6 --style raw --no organic
remains, gore, animal anatomy, photorealistic, photo
```

## A.12 Crystal Adept (versatile combat caster)

Form: tall hooded figure made of polished violet crystal. Multiple
small geometric "tools" hover at the belt. Robes of woven light. No
face — just a clean crystal mask where a face would be, with two
glowing teal slits as eye-positions.

### #34 — Crystal Adept, idle

**File:** `apps/web/public/aliens/warriors/crystal-adept-idle.webp`

```
A painterly fantasy illustration of an alien warrior class called a
Crystal Adept. A tall hooded figure carved entirely from polished
violet crystal, robes of woven light flowing down. Multiple small
geometric tools (a cube, a tetrahedron, a small ring) hover at the
belt. Where a face would be: a clean polished crystal mask with two
horizontal glowing teal slits in the eye positions — NOT animal eyes,
just clean horizontal slits of light. Calm scholarly stance, isolated
on plain neutral background. Studio Ghibli painted illustration style,
soft warm volumetric lighting, deep violet and electric teal palette,
hand-painted texture, no text, no watermark. --ar 3:4 --v 6 --style
raw --no humanoid skin, mammalian face, insectoid, spider, octopus,
tentacles, reptile, fish, plant, animal eyes with pupils, teeth,
claws, fur, exposed flesh, photorealistic, photo
```

### #35 — Crystal Adept, attack

**File:** `apps/web/public/aliens/warriors/crystal-adept-attack.webp`

```
A painterly fantasy illustration of a Crystal Adept alien warrior
mid-cast — tall hooded violet-crystal figure with the eye-slits in
its mask blazing bright teal, both crystal arms extended forward
casting a complex geometric construct of layered glyphs and rings,
the floating belt-tools all spinning rapidly. Robes of woven light
billowing dramatically. Isolated on plain neutral background. Studio
Ghibli painted illustration style, soft warm volumetric lighting, deep
violet and electric teal palette, hand-painted texture, no text, no
watermark. --ar 3:4 --v 6 --style raw --no humanoid skin, mammalian
face, insectoid, spider, octopus, tentacles, reptile, fish, plant,
animal eyes with pupils, teeth, claws, fur, exposed flesh,
photorealistic, photo
```

### #36 — Crystal Adept, defeated

**File:** `apps/web/public/aliens/warriors/crystal-adept-defeated.webp`

```
A painterly fantasy illustration of a fallen Crystal Adept alien
warrior — tall hooded violet-crystal figure on one knee with crystal
mask cracked along one edge, eye-slits dim, robes of woven light
faded to grey, belt-tools fallen and inert at its base. No
biological remains inside the crystal, no gore. Isolated on plain
neutral background. Studio Ghibli painted illustration style, soft
warm volumetric lighting, deep violet and electric teal palette,
hand-painted texture, no text, no watermark. --ar 3:4 --v 6 --style
raw --no organic remains, gore, blood, exposed flesh, animal anatomy,
photorealistic, photo
```

---

# SECTION B — Alien wildlife / non-combatant creatures

These are environmental flavour — alien creatures the player encounters
in exploration but does NOT fight. Distinct from the Earth wildlife in
Volume I. Each is alien-biology only; no Earth-life evocation.

## B.1 Drift Mote (small floating glowing crystals)

### #37 — Drift Mote cluster

**File:** `apps/web/public/aliens/wildlife/drift-mote.webp`

```
A painterly fantasy illustration of alien wildlife — a small group of
six floating diamond-shaped translucent violet-and-teal crystal motes
about palm-sized, drifting harmlessly in mid-air with faint warm-glow
trails. They have no faces, no eyes, no limbs — just glowing crystal
shapes. Like alien fireflies but as polygons not insects. Calm
hovering pose, isolated on plain neutral background. Studio Ghibli
painted illustration style, soft warm volumetric lighting, deep
violet and electric teal palette, hand-painted texture, no text, no
watermark. --ar 1:1 --v 6 --style raw --no insect, firefly, animal
anatomy, mammalian, eyes with pupils, photorealistic, photo
```

## B.2 Glide Bell (alien atmospheric drifter)

### #38 — Glide Bell

**File:** `apps/web/public/aliens/wildlife/glide-bell.webp`

```
A painterly fantasy illustration of alien wildlife — a single floating
inverted-disk creature about meter-sized, the disk made of pale-
violet thin crystalline membrane, with seven hanging downward energy
ribbons of white-teal light beneath it. The disk hovers gently
through the air. No face, no eyes, no mouth — the creature is just
the disk and the hanging light-ribbons. NOT a jellyfish — the disk
is rigid crystal, not gelatinous, and the ribbons are pure light not
tentacles. Isolated on plain neutral background. Studio Ghibli
painted illustration style, soft warm volumetric lighting, deep
violet and electric teal palette, hand-painted texture, no text, no
watermark. --ar 1:1 --v 6 --style raw --no jellyfish, octopus,
tentacles, organic membrane, animal anatomy, eyes, mouth,
photorealistic, photo
```

## B.3 Crystal Boulder (large slow alien fauna)

### #39 — Crystal Boulder

**File:** `apps/web/public/aliens/wildlife/crystal-boulder.webp`

```
A painterly fantasy illustration of alien wildlife — a slow-moving
large boulder-form creature about 2 meters tall, made of stacked
faceted violet crystal blocks loosely cohering as a vertical mass,
balanced on a single broad gravitational suspension base of swirling
teal energy (no legs). No face, no eyes, no limbs — just the shifting
crystal mass on its base. A few small glowing teal pits on the surface
suggest passive sensory points. Calm sluggish hover-walking pose,
isolated on plain neutral background. Studio Ghibli painted
illustration style, soft warm volumetric lighting, deep violet and
electric teal palette, hand-painted texture, no text, no watermark.
--ar 1:1 --v 6 --style raw --no animal anatomy, mammalian, reptile,
legs, eyes with pupils, mouth, claws, fur, photorealistic, photo
```

## B.4 Echo Reed (alien field-growth, geometric "flora")

### #40 — Echo Reed field

**File:** `apps/web/public/aliens/wildlife/echo-reed.webp`

```
A painterly fantasy illustration of alien wildlife — a small field of
tall thin vibrating crystal columns about waist-height, each column
faceted violet-and-teal, growing in a loose cluster from a single
ground-anchor of glowing teal energy. The columns gently sway in
unison. NOT plants — they are pure crystal columns growing as alien
mineral lifeforms. No leaves, no roots, no flowers, no stems, no
biological tissue. Just glowing crystal stalks. Isolated on plain
neutral background. Studio Ghibli painted illustration style, soft
warm volumetric lighting, deep violet and electric teal palette,
hand-painted texture, no text, no watermark. --ar 1:1 --v 6 --style
raw --no plant, leaves, roots, flowers, vines, organic flora, fungus,
photorealistic, photo
```

## B.5 Lambent Walker (atmospheric large fauna)

### #41 — Lambent Walker

**File:** `apps/web/public/aliens/wildlife/lambent-walker.webp`

```
A painterly fantasy illustration of alien wildlife — a tall semi-
transparent column-creature about 3 meters tall, body of layered
translucent crystal shells nested inside each other with internal
warm-teal light source visible through the shells. The creature
moves slowly across the landscape via geometric translocation, with
no legs — just smooth gliding motion. No face, no eyes, no head — the
creature IS the column. A faint single glowing teal sigil orbits at
mid-height. Calm wandering pose, isolated on plain neutral background.
Studio Ghibli painted illustration style, soft warm volumetric
lighting, deep violet and electric teal palette, hand-painted texture,
no text, no watermark. --ar 1:1 --v 6 --style raw --no animal anatomy,
mammalian, reptile, legs, eyes, mouth, claws, fur, organic creature,
photorealistic, photo
```

## B.6 Sky Mote Shoal (flying group atmospheric fauna)

### #42 — Sky Mote Shoal

**File:** `apps/web/public/aliens/wildlife/sky-mote-shoal.webp`

```
A painterly fantasy illustration of alien wildlife — a coordinated
flying group of about twenty small diamond-shaped translucent crystal
beings, each palm-sized, flying together in a swirling V-formation
across an open painterly violet sky. They have no faces, no wings, no
limbs — just glowing geometric forms moving in perfect collective
flight via internal field-propulsion. Painterly motion trails of
warm-teal light behind the shoal. Isolated on plain neutral
background. Studio Ghibli painted illustration style, soft warm
volumetric lighting, deep violet and electric teal palette, hand-
painted texture, no text, no watermark. --ar 1:1 --v 6 --style raw
--no birds, fish, insect, swarm of bugs, animal anatomy, eyes, wings,
photorealistic, photo
```

---

# SECTION C — Elite / boss-class alien warriors

Rare elite versions for late-game / boss encounters. Larger silhouettes,
more dramatic presence than the standard 12 warrior classes.

## C.1 Aether Sovereign (elite of Crystal Adept lineage)

### #43 — Aether Sovereign

**File:** `apps/web/public/aliens/elite/aether-sovereign.webp`

```
A painterly fantasy illustration of an elite alien warrior called an
Aether Sovereign. A tall regal hooded figure carved from polished
violet crystal, much larger and more ornate than a Crystal Adept,
with a crown of seven hovering geometric glyphs orbiting slowly above
the hood. Robes of woven white-teal light flow down in dramatic
folds. Crystal mask face has two long vertical glowing teal slits in
the eye positions (NOT animal eyes). Multiple geometric tools — cube,
tetrahedron, ring, prism — orbit at varying heights around the body.
Imposing commanding pose, isolated on plain neutral background.
Studio Ghibli painted illustration style, soft warm volumetric
lighting, deep violet and electric teal palette, hand-painted
texture, no text, no watermark. --ar 3:4 --v 6 --style raw --no
humanoid skin, mammalian face, insectoid, spider, octopus, tentacles,
reptile, fish, plant, animal eyes with pupils, teeth, claws, fur,
exposed flesh, photorealistic, photo
```

## C.2 Voidless (elite of Hollow Sentinel lineage, boss tank)

### #44 — Voidless

**File:** `apps/web/public/aliens/elite/voidless.webp`

```
A painterly fantasy illustration of an elite alien warrior called a
Voidless. A massive ornate full plate-armour suit standing 3 meters
tall in violet-blackened metal with elaborate teal trim and engraved
glyphs covering every surface, the suit COMPLETELY EMPTY inside but
with THREE deep starry voids visible inside — through the visor slit,
through the chest plate centre, and through one open gauntlet —
suggesting multiple inner singularities. Holds a vast crystal great-
halberd with both hands. No flesh, no creature inside the armour, no
gore — animate empty boss armour. Imposing commanding stance,
isolated on plain neutral background. Studio Ghibli painted
illustration style, soft warm volumetric lighting, deep violet-black
palette with teal trim and starlight glints, hand-painted texture, no
text, no watermark. --ar 3:4 --v 6 --style raw --no humanoid face
inside helm, exposed flesh, gore, blood, mammalian, insectoid,
organic creature, photorealistic, photo
```

## C.3 Apex Lattice (elite of Lattice Knight lineage)

### #45 — Apex Lattice

**File:** `apps/web/public/aliens/elite/apex-lattice.webp`

```
A painterly fantasy illustration of an elite alien warrior called an
Apex Lattice. A vast hovering geode-shaped torso, larger than a
standard Lattice Knight, of multi-layered hexagonal violet crystal
plates with intricate teal inlay patterns. No head — instead a wider
top-facet bears three sensor-gems instead of one. No legs — a
larger, more powerful gravitational anchor of swirling violet-and-
teal energy at the base. Wields TWO tall energy-blades (one in each
shard-grip extension) crossed in front in a battle-ready stance. Calm
imposing presence, isolated on plain neutral background. Studio
Ghibli painted illustration style, soft warm volumetric lighting, deep
violet and electric teal palette, hand-painted texture, no text, no
watermark. --ar 3:4 --v 6 --style raw --no humanoid face, mammalian,
insectoid, spider, octopus, tentacles, reptile, fish, plant, animal
eyes with pupils, teeth, claws, fur, organic creature, photorealistic,
photo
```

## C.4 Singularity Bearer (elite of Singular Bearer lineage)

### #46 — Singularity Bearer

**File:** `apps/web/public/aliens/elite/singularity-bearer.webp`

```
A painterly fantasy illustration of an elite alien warrior called a
Singularity Bearer. A vast humanoid-scale silhouette, taller and more
imposing than a standard Singular Bearer, in robes of folded black
metal-fabric edged with elaborate teal glyphs. Cradles THREE
brilliant violet-and-white singularities at chest-orbit level, the
three singularities spinning around each other in a triangular
formation. Where a head would be: a tall pointed regal hood with three
horizontal teal-glowing visor slits stacked vertically — not animal
eyes, just slits of light. Painterly heavy gravitational distortion
shimmer around the held singularities. Imposing pose, isolated on
plain neutral background. Studio Ghibli painted illustration style,
soft warm volumetric lighting, deep violet-black palette with
brilliant white-teal singularity highlights, hand-painted texture, no
text, no watermark. --ar 3:4 --v 6 --style raw --no humanoid face,
mammalian, insectoid, spider, octopus, tentacles, reptile, fish, plant,
animal eyes with pupils, teeth, claws, fur, exposed flesh,
photorealistic, photo
```

---

# Summary

- 12 alien warrior classes × 3 poses (idle / attack / defeated) = **36 prompts**
- 6 alien wildlife creatures × 1 = **6 prompts**
- 4 elite / boss-class alien warriors × 1 = **4 prompts**
- **Total: 46 Midjourney prompts**

Combined with the abstract atlas (96 prompts in `PROMPTS-DALLE-ALIENS.md`)
and the human-side V1+V2 atlas (348 prompts), the full project now has
~**490 ready-to-use prompts** across DALL-E and Midjourney.

## Quick reference card — the design rule

Aliens **may** be:
- Crystal warriors
- Hovering armoured forms
- Empty animate suits with starlight inside
- Robed entities with hidden voids inside
- Floating polygon clusters
- Hooded figures with crystal-mask faces and slit-eye-positions

Aliens **must not** be:
- Spiders, octopi, or any segmented arthropod
- Lizards, dragons, or scaled reptiles
- Mammals or anything with mammal eyes / fur / mammalian faces
- Birds with beaks and feathers
- Fish with fins
- Plants with vines or roots
- Any biological-feeling body plan recognisably from Earth

If a generation comes back looking like an Earth species, regenerate
with the negative-prompt list expanded — Midjourney's `--no` flag is
the lever that holds the line.
