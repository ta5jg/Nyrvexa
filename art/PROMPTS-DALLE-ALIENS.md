<!-- =============================================================================
File:           art/PROMPTS-DALLE-ALIENS.md
Author:         USDTG GROUP TECHNOLOGY LLC
Developer:      Irfan Gedik
Created Date:   2026-05-04
Last Update:    2026-05-06
Version:        0.4.0

Description:
  Volume III — alien antagonist atlas, REWRITTEN.

  Previous version (v0.3) leaned on insectoid / cephalopod / quadruped
  silhouettes — exactly the biophobic Earth-creature evocations the studio
  rule forbids. Every entity in this volume has been redesigned as
  architecture, geometry, energy, or phenomenon. No anatomy. No limbs.
  No eyes-as-eyes. No mouths. No "creatures."

  Combat is human-versus-alien only AND aliens are non-biological. Both
  rules apply simultaneously and govern every prompt below.

License:
  Proprietary. All rights reserved. See LICENSE in the repository root.
============================================================================= -->

# Nyrvexa / Nyrvexis / Nyrduel — Alien Antagonist Atlas (Volume III, redesigned)

The conflict-side art for every USDTG game. Combat antagonist is **always
non-biological**. Aliens here are deliberately strange machines, geometric
phenomena, or self-aware architectures — never anything a viewer could
read as a spider, octopus, insect, lizard, mammal, plant, or any Earth
life form. Mechanical threat (HP / damage / stats) is unchanged from the
gameplay layer; only the visual / narrative skin is non-biological.

~110 prompts total. Combined with Volumes I + II (348 prompts), the full
project has ~458 ready-to-paste DALL-E 3 prompts.

---

## Hard design rules (apply to EVERY prompt below)

The alien sub-style block in this volume **explicitly forbids** biological
moves. Use it verbatim:

```
[ALIEN-STYLE-BLOCK] = (paste verbatim, after the master block, on every alien prompt)

The entity is non-biological — strictly architecture, geometry, energy,
or phenomenon. AVOID: limbs of any kind (no legs, arms, tentacles,
mandibles, claws); anatomy (no chitin, exoskeleton, fangs, mouths,
animal-shaped eyes, ribs, organs, segmentation); Earth-creature
silhouettes (no insectoid, arachnid, cephalopod, reptilian, mammalian,
avian, amphibian, fish, plant body plans); Earth-creature movement
patterns (no scuttling, swarming, slithering, pouncing, stalking); body
horror (no viscera, blood, exposed flesh). Aim for: tessellated lattices,
prismatic gems, fractal surfaces, polygon clusters, faceted obelisks,
monoliths, towers, citadels, vaults, gates, pillars, spires, distortion
fields, singularities, gravitational anomalies, refraction patterns,
harmonic resonances, lightning columns, beams, plasma columns, event
horizons, magnetic field flexes, glyphs, sigils, runes, recursion marks.
Cool palette of deep violets, electric teals, void blacks, with
bioluminescent highlights; warm-orange accents only on threat-warning
indicators. The entity reads as strange machine or magical building —
never as a creature.
```

**Reminder of standard blocks** (use as before, from V1):

```
[STYLE-BLOCK] = (master Ghibli/painted style sentence)
[CHAR-POST]   = (Centered subject, isolated on plain white background…)
[TILE-POST]   = (Top-down 90-degree birds-eye view…)
```

Each alien prompt below uses **all three**: master style, alien-style,
and the appropriate post-block.

---

## The redesigned twelve

Each entity is a single-word noun drawn from architecture / geometry /
cosmology. No species labels. No Queen / Lord / Beast / Drone / etc.

| # | Name | Role | Form |
|---|------|------|------|
| 1 | **Lattice** | Fast swarmer-equivalent | Self-assembling tessellation cluster |
| 2 | **Crown** | Ranged caster | Singularity ringed by orbital arcs |
| 3 | **Choir** | Heavy melee equivalent | Group of resonant monoliths |
| 4 | **Citadel** | Tank | Architectural fortress entity |
| 5 | **Seam** | Phaser | Vertical tear in space |
| 6 | **Vault** | Defensive | Sealed prismatic gem fortress |
| 7 | **Gate** | Heavy striker | Circular event horizon |
| 8 | **Pillar** | Very fast | Rigid lightning column in frame |
| 9 | **Veil** | Defender | Refraction prism cloud |
| 10 | **Mark** | Balanced | Recursive fractal sigil |
| 11 | **Spoke** | Slow striker | Radial pulse engine |
| 12 | **Continent** | Boss tank | Planetary-scale glyph formation |

Stats are owned by the engine; this file owns visuals only.

---

# SECTION 29 — Twelve entities × four states (48 prompts)

Each entity gets four state poses: **idle** (hovering / resting / inactive),
**active** (attacking / projecting / engaging), **disrupted** (taking damage),
**dispersing** (defeated / dissipating). Engine sequences these.

## 29.1 Lattice

### #348 — Lattice (idle)

**File:** `apps/web/public/aliens/lattice-idle.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Lattice — a floating cluster of seven self-organizing
hexagonal and triangular plates of violet-and-teal glass, suspended in
a loose orbital arrangement around an empty central point. The plates
rotate slowly relative to each other. Faint connecting energy lines
between plate vertices. No body, no creature. Static balance, calm.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #349 — Lattice (active)

**File:** `apps/web/public/aliens/lattice-active.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Lattice mid-strike — its plates have snapped into a tight
forward-pointing arrowhead formation, all vertices blazing teal,
radiating crystalline shards forward as a synchronized volley.
Energetic but rigid, mechanical, no organic motion.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #350 — Lattice (disrupted)

**File:** `apps/web/public/aliens/lattice-disrupted.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Lattice destabilised — three plates have cracked along a
vertex and drift out of alignment, the connecting energy lines
flickering, the cluster's symmetry broken. Mechanical fragility, no
biological wound.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #351 — Lattice (dispersing)

**File:** `apps/web/public/aliens/lattice-dispersing.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Lattice dissolving — plates separating and tumbling outward,
internal energy fading from teal to violet to dark, faint dispersing
particles, no liquid, no gore, just a structure quietly coming apart
back into geometry.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

## 29.2 Crown

### #352 — Crown (idle)

**File:** `apps/web/public/aliens/crown-idle.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Crown — a small intense singularity at the centre, surrounded
by five painterly orbital arcs of varying radii and tilts, each arc
trailing faint starlight motes. Pure gravitational architecture, no
body, no eye-orb, no anatomy. Calm rotational stability.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #353 — Crown (active)

**File:** `apps/web/public/aliens/crown-active.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Crown casting — the orbital arcs have aligned into a
forward-pointing focused conduit, channelling a beam of collapsed
mass-energy out the front. The central singularity shines bright
teal-white. Architectural precision, no creaturely gesture.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #354 — Crown (disrupted)

**File:** `apps/web/public/aliens/crown-disrupted.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Crown destabilised — orbital arcs unsynchronised and wobbling,
two arcs broken and trailing fragmenting starlight, central singularity
flickering. Mechanical disorder, no biological injury.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #355 — Crown (dispersing)

**File:** `apps/web/public/aliens/crown-dispersing.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Crown collapsing — orbital arcs unravelling outward, central
singularity fading and contracting to a single dark point, faint
final starlight motes scattering, no body to fall.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

## 29.3 Choir

### #356 — Choir (idle)

**File:** `apps/web/public/aliens/choir-idle.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Choir — five tall obsidian monoliths arranged in a row of
varying heights, each with a single vertical glowing teal seam running
top to bottom. Quiet harmonic vibration suggested by faint air
distortion between them. No legs, no faces, no creatures — just stones
that resonate.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #357 — Choir (active)

**File:** `apps/web/public/aliens/choir-active.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Choir resonating — the five monoliths' vertical seams now
blazing brilliant teal, harmonic concentric shockwave rings emanating
outward, a forward-directed harmonic concussion blast issuing from the
centre monolith. Pure mechanical sound-architecture, no movement.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #358 — Choir (disrupted)

**File:** `apps/web/public/aliens/choir-disrupted.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Choir disrupted — two of the monoliths have cracked, seam-
light flickering and disordered, harmonic field distorted, faint sour
ripple between the columns. Architectural damage, no anatomy.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #359 — Choir (dispersing)

**File:** `apps/web/public/aliens/choir-dispersing.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Choir falling — three monoliths toppled, seam-light fading
to dark, the remaining monoliths inert, faint last harmonic ripple
dispersing outward. No body, no gore — just stone going still.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

## 29.4 Citadel

### #360 — Citadel (idle)

**File:** `apps/web/public/aliens/citadel-idle.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Citadel — a multi-layered ziggurat-form fortress made of
self-shifting violet stone, each tier carved with abstract glowing
glyphs. No doors, no windows, no occupants — the structure itself is
the entity. A small glowing capstone spire on top. Imposing static
presence.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #361 — Citadel (active)

**File:** `apps/web/public/aliens/citadel-active.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Citadel projecting power — every glyph on every tier blazing
intense teal, the capstone spire emitting a vertical pillar of teal-
white light, the tiers slightly raised off the ground floating in
unison. Architectural threat, no movement of "limbs."

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #362 — Citadel (disrupted)

**File:** `apps/web/public/aliens/citadel-disrupted.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Citadel damaged — top tier broken at one corner with
fragments tumbling in suspended slow motion, several glyphs on the
sides extinguished, capstone tilted askew. Architectural ruin
beginning, no anatomy.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #363 — Citadel (dispersing)

**File:** `apps/web/public/aliens/citadel-dispersing.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Citadel collapsing — tiers slumping into each other, all
glyphs dim, the capstone fallen aside, painterly soft dispersing
violet particles rising from the seams, no liquid, no gore.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

## 29.5 Seam

### #364 — Seam (idle)

**File:** `apps/web/public/aliens/seam-idle.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Seam — a vertical jagged tear in space hovering above the
ground, indigo-violet visible inside the tear, faint gravitational
distortion shimmer around it, painterly starlight motes drifting near
its edges. No body, no creature — the tear itself is the entity.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #365 — Seam (active)

**File:** `apps/web/public/aliens/seam-active.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Seam discharging — the tear has widened slightly and projects
a horizontal beam of folded-space energy outward, edges crackling with
violet lightning, painterly dramatic distortion field expanded.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #366 — Seam (disrupted)

**File:** `apps/web/public/aliens/seam-disrupted.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Seam destabilised — its edges trembling and irregularly
flickering, distortion field uneven, faint sour-yellow flare across
the tear. Geometric instability, no anatomical wound.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #367 — Seam (dispersing)

**File:** `apps/web/public/aliens/seam-dispersing.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Seam closing — tear narrowing to a thin line, then to a
point, finally dissipating into a small puff of violet vapor. No body,
no gore — just a hole in space sealing itself shut.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

## 29.6 Vault

### #368 — Vault (idle)

**File:** `apps/web/public/aliens/vault-idle.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Vault — a large faceted prismatic octahedron of crystalline
violet-and-teal, sealed faces with internal facet lines visible, a
single contained light core glowing through the centre. No anatomy,
just a sealed crystal fortress entity.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #369 — Vault (active)

**File:** `apps/web/public/aliens/vault-active.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Vault projecting — a thin facet on the front face has opened
slightly to release a focused beam of refracted prismatic light, the
internal core blazing bright. Architectural defence, no creature
gesture.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #370 — Vault (disrupted)

**File:** `apps/web/public/aliens/vault-disrupted.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Vault damaged — one facet cracked with a thin line of light
leaking, internal facet structure visibly disordered, the sealed core
now flickering. Geometric flaw, no anatomy.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #371 — Vault (dispersing)

**File:** `apps/web/public/aliens/vault-dispersing.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Vault shattering — its octahedral form fragmenting into
geometric shards drifting outward, the contained core released and
fading, painterly motion arcs of light.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

## 29.7 Gate

### #372 — Gate (idle)

**File:** `apps/web/public/aliens/gate-idle.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Gate — a circular geometric ring containing an event horizon
of dark indigo, distant stars visible faintly inside the abyss. No
mouth, no petals, no teeth — purely a circular cosmic gate. Calm
rotational stability.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #373 — Gate (active)

**File:** `apps/web/public/aliens/gate-active.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Gate firing — the event horizon at its centre has compressed
and ejected a forward column of compressed space-mass like a piston
strike, the ring frame blazing teal. Mechanical and forceful.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #374 — Gate (disrupted)

**File:** `apps/web/public/aliens/gate-disrupted.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Gate destabilised — the ring is bent and irregular, the event
horizon inside flickering, distant stars inside no longer visible.
Geometric distortion, no biology.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #375 — Gate (dispersing)

**File:** `apps/web/public/aliens/gate-dispersing.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Gate closing — the ring contracting, the event horizon
collapsing inward into a single point, painterly final flash of light,
no body, no gore.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

## 29.8 Pillar

### #376 — Pillar (idle)

**File:** `apps/web/public/aliens/pillar-idle.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Pillar — a vertical rigid lightning column inside a geometric
metal frame, the lightning ordered into deliberate angular zigzag
shape rather than chaotic strikes, faint horizontal accent rings along
the frame indicating energy levels. No legs, no movement, no creature.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #377 — Pillar (active)

**File:** `apps/web/public/aliens/pillar-active.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Pillar discharging — the lightning column blazing brightly,
the frame ringing with bright resonance, a horizontal lightning bolt
arcing outward from the centre of the column. Architectural energy,
no creature gesture.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #378 — Pillar (disrupted)

**File:** `apps/web/public/aliens/pillar-disrupted.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Pillar destabilised — the column's lightning now broken into
disordered fragments, the frame bent on one side, accent rings dim.
Mechanical disruption, no biology.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #379 — Pillar (dispersing)

**File:** `apps/web/public/aliens/pillar-dispersing.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Pillar going dark — frame collapsed, lightning column
extinguished, faint last electric crackle dispersing. No body, no gore.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

## 29.9 Veil

### #380 — Veil (idle)

**File:** `apps/web/public/aliens/veil-idle.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Veil — a fanned-out series of translucent spectral planes
diverging from a small bright crystalline source point at the top,
each plane a different colour band like a prism's spectrum hung mid-
air. Painterly diffraction geometry, no creature.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #381 — Veil (active)

**File:** `apps/web/public/aliens/veil-active.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Veil refracting offensively — the spectral planes drawn
forward into a cone, focusing rainbow-band energy into a single
concentrated white-hot beam from the prism source. Pure optics, no
movement of body parts.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #382 — Veil (disrupted)

**File:** `apps/web/public/aliens/veil-disrupted.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Veil destabilised — spectral planes flickering and
overlapping irregularly, the crystalline source at the top cracked
revealing scattered light, painterly fragmented diffraction.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #383 — Veil (dispersing)

**File:** `apps/web/public/aliens/veil-dispersing.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Veil fading — spectral planes dimming and dispersing into
faint colour wisps, the source crystal extinguished, painterly final
prismatic shimmer.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

## 29.10 Mark

### #384 — Mark (idle)

**File:** `apps/web/public/aliens/mark-idle.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Mark — three nested triangular sigils of progressively
smaller size with a glowing central diamond, surrounded by an
enclosing dotted circle bearing runic ticks at the cardinal points.
The recursion mark IS the entity — no body wearing it.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #385 — Mark (active)

**File:** `apps/web/public/aliens/mark-active.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Mark resonating — every nested triangle blazing white-teal,
the central diamond pulsing in concentric expanding rings, the runic
circle rotating, painterly recursion-strike emanating outward. Pure
symbolic geometry.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #386 — Mark (disrupted)

**File:** `apps/web/public/aliens/mark-disrupted.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Mark broken — outermost triangle cracked along one edge, two
runic ticks on the circle extinguished, central diamond dim and
flickering. Geometric breakdown.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #387 — Mark (dispersing)

**File:** `apps/web/public/aliens/mark-dispersing.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Mark unravelling — triangles fading from the outside inward,
runic ticks all dim, central diamond dissolving into dispersing
particles, painterly soft fade.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

## 29.11 Spoke

### #388 — Spoke (idle)

**File:** `apps/web/public/aliens/spoke-idle.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Spoke — a central glowing hub with eight rigid energy
spokes radiating outward at perfectly even angles, each spoke ending
in a small energy capacitor node. A faint containment ring connects
the spoke tips. Pure radial machine geometry.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #389 — Spoke (active)

**File:** `apps/web/public/aliens/spoke-active.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Spoke pulsing — every capacitor node bright and discharging
a synchronised radial energy wave outward, the central hub blazing
white-teal, painterly visible shockwave ring. Mechanical, ordered.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #390 — Spoke (disrupted)

**File:** `apps/web/public/aliens/spoke-disrupted.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Spoke damaged — three of the eight spokes broken and
trailing, capacitor nodes on those spokes dim, hub flickering, the
containment ring partially severed. Mechanical injury.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #391 — Spoke (dispersing)

**File:** `apps/web/public/aliens/spoke-dispersing.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Spoke shutting down — spokes folding inward toward a dim
hub, capacitor nodes extinguished, painterly soft fade of remaining
energy.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

## 29.12 Continent

### #392 — Continent (idle)

**File:** `apps/web/public/aliens/continent-idle.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Continent — a vast irregular flat geometric mass anchored to
the ground, its surface carved with glowing teal fissure-glyphs in
abstract patterns (lines, polygons, concentric rings, recursive
shapes). Cartographic / continental in scale, no anatomy.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #393 — Continent (active)

**File:** `apps/web/public/aliens/continent-active.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Continent invoking — every fissure-glyph blazing intense
teal, vertical pillars of light rising from each glyph node, painterly
ground-shaking activation. Geographic-scale machine, no creature
gesture.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #394 — Continent (disrupted)

**File:** `apps/web/public/aliens/continent-disrupted.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Continent damaged — large cracks in its mass with glyphs
disrupted along the fissure lines, several light pillars extinguished,
painterly grey scarring across the surface.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #395 — Continent (dispersing)

**File:** `apps/web/public/aliens/continent-dispersing.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric illustration of
an alien Continent quieting — all glyphs extinguished, the mass
becoming inert grey stone, painterly faint last energy wisps rising
from the deepest fissures, no body, no gore.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

---

# SECTION 30 — Alien structures (architectural, not biological)

Static structures the alien presence builds on the world map. All
architecture, no "hives" with biological connotations.

### #396 — Spawning rift

**File:** `apps/web/public/aliens/structures/rift.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of a tall
narrow vertical tear in space anchored to the ground, indigo-violet
visible inside, faint geometric polygons drifting through, no creature
silhouettes, no organic edges — purely a geometric portal, isolated
on plain white background, no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #397 — Lattice nexus (large central structure)

**File:** `apps/web/public/aliens/structures/nexus.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of a vast
crystalline lattice nexus — a central tall faceted spire supported by
six radiating geometric strut-arms anchored to the ground at sharp
angles, faint teal vein-light pulsing along the struts, no biological
forms anywhere, isolated on plain white background, no shadow on the
floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #398 — Sentinel pillar

**File:** `apps/web/public/aliens/structures/sentinel.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of a tall
faceted obsidian sentinel pillar with a single slowly rotating teal
focal lens at the top, smaller hovering polygon shards orbiting the
pillar at mid-height. Architectural watchtower, no eye, no creature,
isolated on plain white background, no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #399 — Conduit lattice

**File:** `apps/web/public/aliens/structures/conduit.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of a low
ground-anchored cluster of three glassy energy spires connected to
each other by glowing teal beam-paths and to the ground by geometric
mountings (no organic roots), isolated on plain white background, no
shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #400 — Dormant capsule

**File:** `apps/web/public/aliens/structures/dormant-capsule.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of a small
inert geometric capsule — a closed crystalline cube on a square stone
plinth, the cube dark with only the faintest internal teal pulse.
Architectural quiet, no biological pod-feel, isolated on plain white
background, no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #401 — Active waypoint

**File:** `apps/web/public/aliens/structures/waypoint-active.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of an
active alien waypoint — three connected obsidian arches forming a
triangular footprint, each arch bearing glowing teal glyphs, central
floor disc rotating slowly with a focused beam of light upward. Pure
architecture, isolated on plain white background, no shadow on the
floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #402 — Standing array (mid-game alien presence)

**File:** `apps/web/public/aliens/structures/standing-array.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of an
active alien standing array — three connected geometric spires of
different heights with a glowing teal data-beam-network linking them,
faint hovering polygon shards drifting between, several closed
geometric capsules on the surrounding plinths. Architectural cluster,
no biological growth, isolated on plain white background, no shadow
on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

### #403 — Collapsed structure

**File:** `apps/web/public/aliens/structures/collapsed.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A 3/4 isometric view of a
defeated alien structure cluster — broken geometric spires fallen
and dim, extinguished beam-network, dispersing teal vapor rising from
the ruins, painterly aftermath. Architectural ruin, no biological
remains, isolated on plain white background, no shadow on the floor,
no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[CHAR-POST]
```

---

# SECTION 31 — Geometric corrupted biome variants

When alien presence settles on a tile, the terrain is geometrically
restructured, not biologically corrupted. No vein-tendrils that look
organic — instead crystalline lattices and polygon overlays.

### #404 — Restructured plain

**File:** `apps/web/public/biomes/altered/plain.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
plain altered by alien presence — green grass overlaid with a thin
glowing tessellated lattice of teal lines, scattered small floating
crystal polygons hovering just above the grass, no anatomical or
plant-like growths, no creatures, square 1:1 aspect.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[TILE-POST]
```

### #405 — Restructured forest

**File:** `apps/web/public/biomes/altered/forest.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
forest altered by alien presence — pine trees still standing but each
encased in a translucent geometric crystal sheath, ground patterned
with faint teal hex grid, no biological corruption, no creatures,
square 1:1 aspect.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[TILE-POST]
```

### #406 — Restructured hill

**File:** `apps/web/public/biomes/altered/hill.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of
rolling hills altered by alien presence — slope contours overlaid
with a perfectly regular tessellated polygon grid, faint hovering
crystal shards above the crests, no creatures, square 1:1 aspect.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[TILE-POST]
```

### #407 — Restructured mountain

**File:** `apps/web/public/biomes/altered/mountain.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of
mountain peaks altered by alien presence — natural rock partially
replaced by perfect crystalline obelisks at the summits, glowing teal
geometric seams running down the sides, no creatures, square 1:1
aspect.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[TILE-POST]
```

### #408 — Restructured water

**File:** `apps/web/public/biomes/altered/water.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of water
altered by alien presence — surface still water but overlaid with a
glowing geometric grid of teal lines that ripple in regular patterns,
small hovering polygon islands, no creatures, square 1:1 aspect.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[TILE-POST]
```

### #409 — Restructured desert

**File:** `apps/web/public/biomes/altered/desert.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
desert altered by alien presence — sandy ground with crystal shards
embedded everywhere in tessellated patterns, faint glowing geometric
grid under the surface, no creatures, square 1:1 aspect.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[TILE-POST]
```

### #410 — Restructured tundra

**File:** `apps/web/public/biomes/altered/tundra.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of a
tundra altered by alien presence — snow-covered ground with rising
crystalline obelisks instead of natural rock, glowing teal geometric
seams replacing frost lines, no creatures, square 1:1 aspect.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[TILE-POST]
```

### #411 — Geometric epicenter (rare)

**File:** `apps/web/public/biomes/altered/epicenter.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square top-down view of an
alien geometric epicenter — terrain centred on a small dimensional
seam glowing intense teal-and-violet, perfectly regular concentric
crystal rings radiating outward in geometric pattern, ground stripped
of all biome identity and replaced by abstract tessellation, no
creatures, square 1:1 aspect.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

[TILE-POST]
```

---

# SECTION 32 — Alien combat effects (energy / geometry only)

Translucent overlay effects. All purely energy / geometry — no acid
splashes, tendril whips, or anything resembling animal attacks.

### #412 — Void beam

**File:** `apps/web/public/aliens/effects/void-beam.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A horizontal painterly beam of
indigo-violet void energy with a brilliant white-teal core, arcing
electric tendrils along its length, painterly motion blur edges,
isolated on plain white background, no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #413 — Crystal shard volley

**File:** `apps/web/public/aliens/effects/shard-volley.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly fan of three or four
sharp violet-crystal shards in mid-flight with painterly motion
trails, faint teal glow at the leading edges, geometric, isolated on
plain white background, no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #414 — Harmonic shockwave

**File:** `apps/web/public/aliens/effects/harmonic-wave.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly expanding ring of
indigo-violet shockwave energy with a white-teal leading edge,
distortion ripple visible inside the ring, suggesting a sonic /
harmonic concussion, isolated on plain white background, no shadow on
the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #415 — Singularity collapse

**File:** `apps/web/public/aliens/effects/singularity-collapse.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly small intense
indigo-violet point at the centre surrounded by collapsing rings of
distorted space being pulled inward, suggesting a singularity-collapse
moment, isolated on plain white background, no shadow on the floor,
no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #416 — Dispersing particles (defeated)

**File:** `apps/web/public/aliens/effects/dispersing.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly cloud of softly
rising teal particles dispersing upward as if a structure has been
defeated and is dissolving back into ambient energy, no body remains,
isolated on plain white background, no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #417 — Geometric burst

**File:** `apps/web/public/aliens/effects/geometric-burst.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly burst of seven sharp
violet-and-teal crystal shards bursting outward from a central seed
point in a star pattern, all geometric, isolated on plain white
background, no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #418 — Containment field

**File:** `apps/web/public/aliens/effects/containment-field.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly translucent overlay
of indigo-violet electric arcs filling a circular area, painterly
threshold of containment effect, no organic forms inside, isolated on
plain white background, no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #419 — Void mist

**File:** `apps/web/public/aliens/effects/void-mist.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A painterly soft cloud of cool
dark-violet alien mist with faint embedded geometric shard glints,
slowly curling translucent edges, no creature silhouettes inside,
isolated on plain white background, no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

---

# SECTION 33 — Battle illustration cards (humans defending against phenomena)

Cinematic side illustrations for combat-result modals. Humans always
defending; aliens are phenomena/architectures, never creatures.

### #420 — Warrior facing a Lattice

**File:** `apps/web/public/battle-aliens/warrior-vs-lattice.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A cinematic painterly
illustration of a single armored warrior braced behind a kite shield
facing a hovering geometric Lattice cluster mid-strike, painterly
dynamic depth, warrior face hidden by helm visor, the alien is purely
geometric (no limbs, no creature shape), isolated on plain white
background, no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #421 — Archer line vs a Crown

**File:** `apps/web/public/battle-aliens/archer-vs-crown.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A cinematic painterly
illustration of three archers loosing a volley of arrows at a hovering
Crown — a singularity ringed by orbital arcs, painterly motion-trails
behind the arrows, the Crown casting a defensive distortion, dramatic
ranged encounter, isolated on plain white background, no shadow on
the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #422 — Cavalry charge into a Choir

**File:** `apps/web/public/battle-aliens/cavalry-vs-choir.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A cinematic painterly
illustration of three armored horsemen in full lance charge into a
row of vertical alien Choir monoliths emitting harmonic shockwaves,
dust kicking up, no creature opponents — only architecture, dynamic
forward depth, isolated on plain white background, no shadow on the
floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #423 — Defenders facing a Citadel

**File:** `apps/web/public/battle-aliens/defenders-vs-citadel.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A cinematic painterly
illustration of a small shield-wall of three defenders bracing as a
ziggurat-form alien Citadel projects a vertical pillar of light from
its capstone in front of them, painterly architectural threat, no
creatures, isolated on plain white background, no shadow on the floor,
no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #424 — Scout near a Seam

**File:** `apps/web/public/battle-aliens/scout-vs-seam.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A cinematic painterly
illustration of a hooded scout half-turned toward a vertical alien
Seam (tear in space) materialising in a forest beside them, painterly
distortion field around the seam, scout composed, no creature
opponent, isolated on plain white background, no shadow on the floor,
no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #425 — Champion approaching a Continent

**File:** `apps/web/public/battle-aliens/champion-vs-continent.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A cinematic painterly
illustration of a lone champion warrior approaching a towering vast
alien Continent — a planetary-scale glyph formation looming above —
the warrior small in the foreground against the immense glowing
geometric mass, painterly dramatic scale contrast, isolated on plain
white background, no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #426 — City defence

**File:** `apps/web/public/battle-aliens/city-defense.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A cinematic painterly
illustration of human defenders along a city wall repelling an alien
incursion below — archers loosing volleys, warriors at the gate,
hovering alien Lattice clusters and a Pillar pressing in from the
foreground, painterly dramatic warm-orange torch light contrasting
cold alien glow, all aliens are non-biological geometry, isolated on
plain white background, no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #427 — Gate opening

**File:** `apps/web/public/battle-aliens/gate-opening.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A cinematic painterly
illustration of a circular alien Gate event horizon opening above a
landscape, geometric polygons and crystal shards stepping through,
painterly glowing edges of the ring, dramatic incursion moment, no
creatures emerging, isolated on plain white background, no shadow on
the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #428 — Structure fall

**File:** `apps/web/public/battle-aliens/structure-fall.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A cinematic painterly
illustration of human warriors raising a banner of triumph over a
collapsed alien structure cluster — broken geometric spires in the
background, dispersing teal particles, painterly dramatic dawn light
behind the figures, isolated on plain white background, no shadow on
the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #429 — Allied factions stand together

**File:** `apps/web/public/battle-aliens/alliance-stand.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A cinematic painterly
illustration of warriors from four different banners — copper-orange,
forest-green, sea-teal, and violet — standing shoulder to shoulder on
a hilltop facing an oncoming sky filled with alien geometric Seams
and Crowns approaching from the misty distance, unified pose,
painterly dramatic resolve, isolated on plain white background, no
shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

---

# SECTION 34 — Threat UI icons

HUD glyphs. Icons read as symbols/architecture, never as creatures.

### #430 — Threat: Seam opening

**File:** `apps/web/public/ui/aliens/threat-seam.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of a
small jagged tear-shaped symbol with indigo-violet inside edged in
glowing teal, isolated on plain white background, no shadow on the
floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #431 — Threat: Incursion (warning)

**File:** `apps/web/public/ui/aliens/threat-warning.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of a
warning triangle with a small abstract geometric polygon inside,
faint teal glow around the triangle border, isolated on plain white
background, no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #432 — Threat: Restructuring spread

**File:** `apps/web/public/ui/aliens/threat-restructuring.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of a
spreading tessellated polygon pattern in dark-violet with teal glow
edges, isolated on plain white background, no shadow on the floor,
no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #433 — Status: Structure nearby

**File:** `apps/web/public/ui/aliens/threat-structure.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of a
small alien spire silhouette in dark violet with a teal core glow,
isolated on plain white background, no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #434 — Status: Wave incoming

**File:** `apps/web/public/ui/aliens/threat-wave.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of three
small advancing geometric polygon silhouettes in dark violet with
forward motion arrows behind, isolated on plain white background, no
shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #435 — Status: Threat repelled

**File:** `apps/web/public/ui/aliens/threat-repelled.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of a
small alien geometric polygon silhouette inside a circle with a clean
diagonal slash through it, painterly soft fade on the polygon shape,
isolated on plain white background, no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #436 — Status: Structure destroyed

**File:** `apps/web/public/ui/aliens/threat-structure-down.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of a
broken/fallen alien spire silhouette with dispersing geometric
particles rising, isolated on plain white background, no shadow on
the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #437 — Status: Containment field

**File:** `apps/web/public/ui/aliens/threat-contained.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A square painterly icon of a
dotted hexagonal containment ring with a dim alien geometric shape
inside, painterly faint glow on the ring, isolated on plain white
background, no shadow on the floor, no text.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

---

# SECTION 35 — Alien sigils & banners

Used as faction-style emblem for the alien threat in HUD context.

### #438 — Alien threat sigil (master)

**File:** `apps/web/public/aliens/sigil.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A circular painterly sigil
representing the alien threat — a stylized geometric design of three
concentric jagged polygon rings around a central teal geometric mark
(diamond / triangle / circle nested), indigo-and-violet palette with
teal glow accents, no eye-shape, no creature reference, no text, no
shadow.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #439 — Alien wave-warning banner

**File:** `apps/web/public/aliens/banner-wave.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A vertical hanging banner where
the "fabric" is folded space, dark violet field with teal threads of
energy across it, the alien geometric sigil at the centre flickering
like a hologram, painterly warning unmistakable otherworldly origin,
isolated on plain white background, no shadow on the floor, no text,
no flagpole.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #440 — Cooperative alliance sigil

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

### #441 — "Wave incoming" header

**File:** `apps/web/public/aliens/banner-incoming.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A horizontal painterly header
strip — dark indigo-violet field with arcing teal lightning across,
edges fraying into dispersing geometric particles, no text, isolated
on plain white background, no shadow on the floor, no watermark.

[STYLE-BLOCK]

[ALIEN-STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #442 — "Threat repelled" header

**File:** `apps/web/public/aliens/banner-repelled.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A horizontal painterly header
strip — warm copper-orange and ivory field with a stylized broken
alien geometric sigil at the centre, faint laurel flourish behind, no
text, isolated on plain white background, no shadow on the floor, no
watermark.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

### #443 — "Structure fallen" header

**File:** `apps/web/public/aliens/banner-structure-fallen.png`

```
I NEED EXACTLY THIS, DO NOT REWRITE: A horizontal painterly header
strip — soft warm gold field with a fallen alien geometric spire
silhouette at the centre surrounded by a small wreath, painterly
triumphant tone, no text, isolated on plain white background, no
shadow on the floor, no watermark.

[STYLE-BLOCK]

Centered subject, isolated on plain white background, no shadow on the
floor, no text, no watermark.
```

---

## Updated grand tally (Volumes I + II + III-redesigned)

| Volume | Theme | Prompts |
|---|---|---|
| **I** | Painted world (human side) | 176 |
| **II** | Variants & polish (seasons, walks, civilians, wonders, etc.) | 172 |
| **III** | **Alien antagonist atlas — abstract entities, NO Earth-creature evocation** | 96 |
| **TOTAL** | | **444** |

---

## Production tips

1. **Generate in a fresh chat anchored to abstract aesthetic.** Run prompt
   #348 (Lattice idle) first. If the result has any limb-like protrusion,
   eye-like opening, or creature silhouette, regenerate — DALL-E may try
   to revert to "alien creature" defaults. The negative list in the
   alien-style block is what holds the line.
2. **Boss roster (#348–#395, idle states).** For Nyrduel, you only need
   the 12 idle portraits to start. The other states are for future polish.
3. **Sanity check after each generation:** could a stranger mistake this
   for a known Earth animal or animated creature? If yes, regenerate.
4. **Optimization** — same `art/optimize-assets.sh` script as before.

When V3 art ships, ping me and I'll wire `lib/assets.ts` and update the
renderer. Code-side refactor is a one-day job once art is in place.
