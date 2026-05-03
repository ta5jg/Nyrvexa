/* =============================================================================
 * File:           apps/web/src/game/GameCanvas.tsx
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-05-03
 * Last Update:    2026-05-03
 * Version:        0.1.0
 *
 * Description:
 *   PixiJS-backed hex renderer. React owns the world state; this component
 *   is a thin mount that re-renders whenever the snapshot, selection, or
 *   camera changes. Custom drawing — no external sprite atlas — keeps the
 *   visual identity ours.
 *
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

import { useEffect, useRef } from "react";
import { Application, Container, Graphics, Text, TextStyle } from "pixi.js";
import { BIOMES, FACTIONS, UNITS } from "@nyrvexa/content";
import { hexToPixel, pixelToHex } from "@nyrvexa/engine";
import type { Hex, WorldSnapshot } from "@nyrvexa/protocol";

const HEX_SIZE = 46;

export type SelectionState = {
  selectedUnitId: number | null;
  selectedCityId: number | null;
  hoveredHex: Hex | null;
  reachableKeys: ReadonlySet<string>;
};

type Props = {
  state: WorldSnapshot;
  selection: SelectionState;
  humanPlayerId: number;
  onHexClick: (hex: Hex) => void;
  onHexHover: (hex: Hex | null) => void;
};

type PixiHandles = {
  app: Application;
  layers: {
    tile: Container;
    overlay: Container;
    fog: Container;
    sprites: Container;
    ui: Container;
  };
  worldRoot: Container;
};

export function GameCanvas(props: Props) {
  const hostRef = useRef<HTMLDivElement | null>(null);
  const handlesRef = useRef<PixiHandles | null>(null);
  const cameraRef = useRef({ x: 0, y: 0, zoom: 1 });

  // ---- Init Pixi once ----
  useEffect(() => {
    let cancelled = false;
    const host = hostRef.current;
    if (!host) return;

    const app = new Application();
    app
      .init({
        background: "#0c0e14",
        resizeTo: host,
        antialias: true,
        autoDensity: true,
        resolution: Math.min(window.devicePixelRatio || 1, 2)
      })
      .then(() => {
        if (cancelled) {
          app.destroy(true);
          return;
        }
        host.appendChild(app.canvas);

        const worldRoot = new Container();
        const tile = new Container();
        const overlay = new Container();
        const fog = new Container();
        const sprites = new Container();
        const ui = new Container();
        worldRoot.addChild(tile, overlay, fog, sprites);
        app.stage.addChild(worldRoot, ui);

        handlesRef.current = { app, layers: { tile, overlay, fog, sprites, ui }, worldRoot };

        attachInput(app.canvas, worldRoot, cameraRef, props);

        // Auto-fit camera so the whole world is visible at game start, with
        // a comfortable margin. After this, the user controls pan/zoom.
        const b = worldBounds(props.state);
        const padX = 60;
        const padY = 100;
        const cw = app.canvas.clientWidth;
        const ch = app.canvas.clientHeight;
        const zoom = Math.min(
          (cw - padX * 2) / Math.max(1, b.width),
          (ch - padY * 2) / Math.max(1, b.height),
          1.4
        );
        cameraRef.current.zoom = Math.max(0.4, zoom);
        cameraRef.current.x = cw / 2 - b.cx * cameraRef.current.zoom;
        cameraRef.current.y = ch / 2 - b.cy * cameraRef.current.zoom;
        worldRoot.position.set(cameraRef.current.x, cameraRef.current.y);
        worldRoot.scale.set(cameraRef.current.zoom);

        renderAll(handlesRef.current, props);
      });

    return () => {
      cancelled = true;
      const h = handlesRef.current;
      if (h) {
        h.app.destroy(true);
        handlesRef.current = null;
      }
      while (host.firstChild) host.removeChild(host.firstChild);
    };
    // Init only once.
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  // ---- Re-render when state / selection changes ----
  useEffect(() => {
    if (handlesRef.current) renderAll(handlesRef.current, props);
  }, [props]);

  return <div ref={hostRef} className="canvas-host" />;
}

// =============================================================================
// Rendering
// =============================================================================

function worldBounds(state: WorldSnapshot): { cx: number; cy: number; width: number; height: number } {
  let minX = Infinity;
  let minY = Infinity;
  let maxX = -Infinity;
  let maxY = -Infinity;
  for (const t of state.tiles) {
    const p = hexToPixel({ q: t.q, r: t.r }, HEX_SIZE);
    if (p.x < minX) minX = p.x;
    if (p.y < minY) minY = p.y;
    if (p.x > maxX) maxX = p.x;
    if (p.y > maxY) maxY = p.y;
  }
  return {
    cx: (minX + maxX) / 2,
    cy: (minY + maxY) / 2,
    width: (maxX - minX) + HEX_SIZE * 2,
    height: (maxY - minY) + HEX_SIZE * 2
  };
}

function hexPolygonPath(centerX: number, centerY: number, size: number): number[] {
  // Pointy-top hex; vertices start at top, clockwise.
  const pts: number[] = [];
  for (let i = 0; i < 6; i++) {
    const angle = (Math.PI / 180) * (60 * i - 30);
    pts.push(centerX + size * Math.cos(angle), centerY + size * Math.sin(angle));
  }
  return pts;
}

function colorToHex(c: string): number {
  return parseInt(c.replace("#", ""), 16);
}

function renderAll(h: PixiHandles, props: Props): void {
  const { state, selection, humanPlayerId } = props;
  const { tile, overlay, fog, sprites } = h.layers;

  tile.removeChildren();
  overlay.removeChildren();
  fog.removeChildren();
  sprites.removeChildren();

  const playerBit = 1 << humanPlayerId;

  // ---- Tiles ----
  for (const t of state.tiles) {
    const seen = (t.seenMask & playerBit) !== 0;
    const visible = (t.visibleMask & playerBit) !== 0;
    if (!seen) continue; // unseen tiles stay black
    const p = hexToPixel({ q: t.q, r: t.r }, HEX_SIZE);
    const biome = BIOMES[t.biome];
    const baseAlpha = visible ? 1 : 0.45;

    const g = new Graphics();
    // Slightly darker base for depth (drawn first).
    g.poly(hexPolygonPath(p.x, p.y, HEX_SIZE - 0.5));
    g.fill({ color: colorToHex(biome.outline), alpha: baseAlpha });

    // Top-most fill — slightly inset so the darker rim shows = 3D-ish edge.
    g.poly(hexPolygonPath(p.x, p.y, HEX_SIZE - 2));
    g.fill({ color: colorToHex(biome.color), alpha: baseAlpha });

    // Crisp outline so hex edges read on busy maps.
    g.poly(hexPolygonPath(p.x, p.y, HEX_SIZE - 0.5));
    g.stroke({ color: 0x0c0e14, width: 0.8, alpha: visible ? 0.45 : 0.25 });

    // Inner highlight along upper-left = subtle "lit from above".
    const lit = hexPolygonPath(p.x - 1, p.y - 1, HEX_SIZE - 5);
    g.poly(lit);
    g.stroke({ color: 0xffffff, width: 0.7, alpha: visible ? 0.10 : 0.04 });

    // Per-biome decoration drawn into the same Graphics (saves draw calls).
    // rngHash gives variety so identical biomes don't repeat literally.
    const rngHash = ((t.q * 73856093) ^ (t.r * 19349663)) >>> 0;
    decorateBiome(g, p.x, p.y, t.biome, baseAlpha, rngHash);

    tile.addChild(g);

    // Resource glyph (after decoration so it sits on top).
    if (t.resource && visible) {
      const r = new Graphics();
      const rs = 5;
      r.poly([p.x, p.y - rs, p.x + rs, p.y, p.x, p.y + rs, p.x - rs, p.y]);
      r.fill({ color: resourceColor(t.resource) });
      r.stroke({ color: 0x0c0e14, width: 1.2 });
      tile.addChild(r);
    }
  }

  // ---- Reachable overlay for selected unit ----
  if (selection.selectedUnitId !== null && selection.reachableKeys.size > 0) {
    for (const key of selection.reachableKeys) {
      const [q, r] = key.split(",").map(Number);
      const p = hexToPixel({ q: q!, r: r! }, HEX_SIZE);
      const g = new Graphics();
      g.poly(hexPolygonPath(p.x, p.y, HEX_SIZE - 4));
      g.fill({ color: 0x00d2c0, alpha: 0.18 });
      overlay.addChild(g);
    }
  }

  // ---- Hover ring ----
  if (selection.hoveredHex) {
    const p = hexToPixel(selection.hoveredHex, HEX_SIZE);
    const g = new Graphics();
    g.poly(hexPolygonPath(p.x, p.y, HEX_SIZE - 1));
    g.stroke({ color: 0xffffff, width: 2, alpha: 0.85 });
    overlay.addChild(g);
  }

  // ---- Selected unit ring ----
  if (selection.selectedUnitId !== null) {
    const u = state.units.find((uu) => uu.id === selection.selectedUnitId);
    if (u) {
      const p = hexToPixel({ q: u.q, r: u.r }, HEX_SIZE);
      const g = new Graphics();
      g.poly(hexPolygonPath(p.x, p.y, HEX_SIZE - 2));
      g.stroke({ color: 0xff5b3b, width: 3, alpha: 0.9 });
      overlay.addChild(g);
    }
  }

  // ---- Cities ----
  for (const c of state.cities) {
    const t = state.tiles.find((tt) => tt.q === c.q && tt.r === c.r);
    if (!t) continue;
    if ((t.seenMask & playerBit) === 0) continue;
    const p = hexToPixel({ q: c.q, r: c.r }, HEX_SIZE);
    const player = state.players.find((pp) => pp.id === c.ownerId);
    const factionColor = player ? FACTIONS[player.factionId].color : "#ffffff";
    sprites.addChild(drawCitySettlement(p.x, p.y, factionColor, c.population));

    // City label below the settlement.
    const txt = new Text({
      text: `${c.name} · ${c.population}`,
      style: new TextStyle({
        fontFamily: "Inter, sans-serif",
        fontSize: 12,
        fontWeight: "800",
        fill: 0xf3f4f8,
        stroke: { color: 0x0c0e14, width: 3, join: "round" }
      })
    });
    txt.anchor.set(0.5, 1);
    txt.position.set(p.x, p.y + HEX_SIZE * 0.5 + 18);
    sprites.addChild(txt);
  }

  // ---- Units ----
  for (const u of state.units) {
    const t = state.tiles.find((tt) => tt.q === u.q && tt.r === u.r);
    if (!t) continue;
    if ((t.visibleMask & playerBit) === 0 && u.ownerId !== humanPlayerId) continue;
    const p = hexToPixel({ q: u.q, r: u.r }, HEX_SIZE);
    const player = state.players.find((pp) => pp.id === u.ownerId);
    const factionColor = player ? FACTIONS[player.factionId].color : "#ffffff";
    const def = UNITS[u.type];

    const g = drawUnitGlyph(p.x, p.y, u.type, factionColor);
    sprites.addChild(g);

    // HP bar (only when wounded)
    if (u.hp < def.hp) {
      const barW = HEX_SIZE * 0.7;
      const pct = u.hp / def.hp;
      const bg = new Graphics();
      bg.rect(p.x - barW / 2, p.y - HEX_SIZE * 0.6, barW, 4);
      bg.fill({ color: 0x000000, alpha: 0.6 });
      const fg = new Graphics();
      fg.rect(p.x - barW / 2, p.y - HEX_SIZE * 0.6, barW * pct, 4);
      fg.fill({ color: pct > 0.6 ? 0x4dd680 : pct > 0.3 ? 0xffce4d : 0xff4566 });
      sprites.addChild(bg, fg);
    }

    // Acted indicator (gray-out)
    if (u.acted || u.movesLeft <= 0) {
      const dim = new Graphics();
      dim.poly(hexPolygonPath(p.x, p.y, HEX_SIZE - 4));
      dim.fill({ color: 0x000000, alpha: 0.32 });
      sprites.addChild(dim);
    }
  }

  // ---- Fog memory tint over seen-but-not-visible tiles ----
  for (const t of state.tiles) {
    const seen = (t.seenMask & playerBit) !== 0;
    const visible = (t.visibleMask & playerBit) !== 0;
    if (seen && !visible) {
      const p = hexToPixel({ q: t.q, r: t.r }, HEX_SIZE);
      const g = new Graphics();
      g.poly(hexPolygonPath(p.x, p.y, HEX_SIZE - 0.5));
      g.fill({ color: 0x000000, alpha: 0.32 });
      fog.addChild(g);
    }
  }
}

/**
 * Humanoid unit figure — head, body, and a per-class accessory (sword,
 * bow, hammer, etc). Drawn at integer pixel offsets so multiple units
 * remain crisp at different zoom levels.
 */
function drawUnitGlyph(cx: number, cy: number, type: string, factionColor: string): Container {
  const c = new Container();
  const fc = colorToHex(factionColor);
  const skin = 0xf3d4a8;
  const dark = 0x14161f;
  const accent = 0x0c0e14;

  // Subtle ground shadow under the figure.
  const shadow = new Graphics();
  shadow.ellipse(cx, cy + 12, 9, 3);
  shadow.fill({ color: 0x000000, alpha: 0.32 });
  c.addChild(shadow);

  const fig = new Graphics();

  function head(x: number, y: number, r: number) {
    fig.circle(x, y, r + 0.6);
    fig.fill({ color: dark });
    fig.circle(x, y, r);
    fig.fill({ color: skin });
  }

  function torso(opts: { tone: number; accessory?: () => void }) {
    // Cape behind the body — faction colored.
    fig.poly([cx - 7, cy - 2, cx + 7, cy - 2, cx + 5, cy + 10, cx - 5, cy + 10]);
    fig.fill({ color: fc });
    // Tunic / armor on top of cape.
    fig.poly([cx - 5, cy - 2, cx + 5, cy - 2, cx + 4, cy + 8, cx - 4, cy + 8]);
    fig.fill({ color: opts.tone });
    fig.poly([cx - 5, cy - 2, cx + 5, cy - 2, cx + 4, cy + 8, cx - 4, cy + 8]);
    fig.stroke({ color: accent, width: 1.2 });
    // Belt
    fig.rect(cx - 4, cy + 5, 8, 1.5);
    fig.fill({ color: accent });
    // Legs
    fig.rect(cx - 3, cy + 8, 2, 4);
    fig.rect(cx + 1, cy + 8, 2, 4);
    fig.fill({ color: dark });
    if (opts.accessory) opts.accessory();
  }

  switch (type) {
    case "settler": {
      head(cx, cy - 8, 3.5);
      torso({
        tone: 0xc6a872,
        accessory: () => {
          // Pack on the back
          fig.rect(cx - 7, cy - 1, 4, 6);
          fig.fill({ color: 0x6b3f1a });
          fig.stroke({ color: accent, width: 1 });
          // Walking staff on the right
          fig.moveTo(cx + 6, cy - 6).lineTo(cx + 6, cy + 8);
          fig.stroke({ color: 0x6b3f1a, width: 1.3 });
        }
      });
      break;
    }
    case "warrior": {
      // Helm
      fig.poly([cx - 4, cy - 6, cx + 4, cy - 6, cx + 4, cy - 11, cx, cy - 13, cx - 4, cy - 11]);
      fig.fill({ color: 0x8a8d96 });
      fig.stroke({ color: accent, width: 1 });
      head(cx, cy - 7, 2.8);
      torso({
        tone: 0x8a8d96,
        accessory: () => {
          // Shield (left)
          fig.poly([cx - 11, cy - 1, cx - 5, cy - 1, cx - 5, cy + 8, cx - 8, cy + 11, cx - 11, cy + 8]);
          fig.fill({ color: fc });
          fig.stroke({ color: accent, width: 1.2 });
          // Cross emblem on shield
          fig.rect(cx - 8.5, cy + 1, 1.2, 6);
          fig.rect(cx - 10, cy + 3.5, 4, 1.2);
          fig.fill({ color: 0xffffff, alpha: 0.85 });
          // Sword (right)
          fig.rect(cx + 5, cy - 3, 1.5, 10);
          fig.fill({ color: 0xc0c4cc });
          fig.stroke({ color: accent, width: 0.6 });
          fig.rect(cx + 3.5, cy + 7, 4.5, 1.5);
          fig.fill({ color: 0xc0c4cc });
        }
      });
      break;
    }
    case "scout": {
      // Hood
      fig.poly([cx - 5, cy - 4, cx + 5, cy - 4, cx + 4, cy - 12, cx - 4, cy - 12]);
      fig.fill({ color: 0x2a3046 });
      head(cx, cy - 7, 2.7);
      // Eyes glint under hood
      fig.circle(cx - 1.4, cy - 7, 0.5);
      fig.circle(cx + 1.4, cy - 7, 0.5);
      fig.fill({ color: 0xffce4d });
      torso({
        tone: 0x2a3046,
        accessory: () => {
          // Spyglass / dagger pair
          fig.rect(cx + 5, cy + 1, 4, 1.2);
          fig.fill({ color: 0xc6a872 });
          fig.rect(cx - 8, cy + 2, 1.5, 4);
          fig.fill({ color: 0xc0c4cc });
        }
      });
      break;
    }
    case "archer": {
      head(cx, cy - 8, 3);
      torso({
        tone: 0x3f7a4d,
        accessory: () => {
          // Bow held forward
          fig.moveTo(cx - 9, cy - 4).bezierCurveTo(cx - 13, cy + 2, cx - 13, cy + 6, cx - 9, cy + 12);
          fig.stroke({ color: 0x6b3f1a, width: 1.4 });
          fig.moveTo(cx - 9, cy - 4).lineTo(cx - 9, cy + 12);
          fig.stroke({ color: 0x4a2a10, width: 0.7 });
          // Arrow nocked
          fig.moveTo(cx - 9, cy + 4).lineTo(cx + 4, cy + 4);
          fig.stroke({ color: 0x6b3f1a, width: 0.9 });
          // Quiver on back
          fig.rect(cx + 4, cy - 1, 3, 7);
          fig.fill({ color: 0x6b3f1a });
        }
      });
      break;
    }
    case "horseman": {
      // Horse body
      fig.ellipse(cx, cy + 6, 11, 5);
      fig.fill({ color: 0x6b3f1a });
      // Horse head + neck
      fig.poly([cx + 8, cy + 4, cx + 13, cy - 1, cx + 11, cy + 4, cx + 9, cy + 6]);
      fig.fill({ color: 0x6b3f1a });
      // Horse legs
      fig.rect(cx - 8, cy + 9, 1.6, 5);
      fig.rect(cx - 4, cy + 9, 1.6, 5);
      fig.rect(cx + 3, cy + 9, 1.6, 5);
      fig.rect(cx + 7, cy + 9, 1.6, 5);
      fig.fill({ color: 0x4a2a10 });
      // Rider torso
      fig.poly([cx - 3, cy - 2, cx + 5, cy - 2, cx + 4, cy + 5, cx - 2, cy + 5]);
      fig.fill({ color: fc });
      fig.stroke({ color: accent, width: 1 });
      // Rider head + helm
      head(cx + 1, cy - 5, 2.4);
      // Lance
      fig.moveTo(cx + 6, cy - 8).lineTo(cx + 14, cy - 12);
      fig.stroke({ color: 0xc0c4cc, width: 1.4 });
      break;
    }
    case "worker": {
      head(cx, cy - 7, 3);
      torso({
        tone: 0xc6a872,
        accessory: () => {
          // Hammer
          fig.rect(cx + 5, cy - 4, 1.4, 8);
          fig.fill({ color: 0x6b3f1a });
          fig.rect(cx + 3, cy - 5, 5, 2.5);
          fig.fill({ color: 0x8a8d96 });
        }
      });
      break;
    }
    default: {
      head(cx, cy - 7, 3);
      torso({ tone: fc });
    }
  }

  c.addChild(fig);
  return c;
}

/**
 * City settlement — a small cluster of buildings: keep tower, two houses,
 * and a flag in the faction color. Population scales the visible roof
 * count so a metropolis looks denser than a hamlet.
 */
function drawCitySettlement(cx: number, cy: number, factionColor: string, population: number): Container {
  const c = new Container();
  const fc = colorToHex(factionColor);
  const stone = 0xc8c8d4;
  const stoneDark = 0x88889a;
  const wood = 0xa67250;
  const woodDark = 0x6b3f1a;
  const accent = 0x0c0e14;
  const roofA = 0xc0394a;
  const roofB = 0x935a2c;

  // Ground platform / clearing
  const ground = new Graphics();
  ground.ellipse(cx, cy + 12, 26, 6);
  ground.fill({ color: 0x000000, alpha: 0.42 });
  c.addChild(ground);

  const g = new Graphics();

  // ---- Side house (smaller) ----
  g.rect(cx - 18, cy + 4, 10, 8);
  g.fill({ color: wood });
  g.rect(cx - 18, cy + 4, 10, 8);
  g.stroke({ color: woodDark, width: 1 });
  g.poly([cx - 19, cy + 4, cx - 7, cy + 4, cx - 13, cy - 2]);
  g.fill({ color: roofB });
  g.stroke({ color: accent, width: 1 });
  // Door
  g.rect(cx - 14, cy + 8, 2, 4);
  g.fill({ color: woodDark });

  // ---- Keep tower (central, tallest) ----
  g.rect(cx - 4, cy - 8, 8, 18);
  g.fill({ color: stone });
  g.rect(cx - 4, cy - 8, 8, 18);
  g.stroke({ color: stoneDark, width: 1.2 });
  // Crenellations
  for (let i = 0; i < 4; i++) {
    g.rect(cx - 4 + i * 2, cy - 11, 1.2, 3);
    g.fill({ color: stone });
  }
  // Window slit
  g.rect(cx - 1, cy - 5, 2, 4);
  g.fill({ color: accent });
  // Door
  g.rect(cx - 1.5, cy + 6, 3, 4);
  g.fill({ color: woodDark });
  // Flag pole + faction flag
  g.moveTo(cx, cy - 11).lineTo(cx, cy - 18);
  g.stroke({ color: stoneDark, width: 1 });
  g.poly([cx, cy - 18, cx + 7, cy - 16, cx, cy - 14]);
  g.fill({ color: fc });
  g.stroke({ color: accent, width: 0.8 });

  // ---- Right house ----
  g.rect(cx + 8, cy + 2, 11, 10);
  g.fill({ color: wood });
  g.rect(cx + 8, cy + 2, 11, 10);
  g.stroke({ color: woodDark, width: 1 });
  // Pitched roof
  g.poly([cx + 7, cy + 2, cx + 20, cy + 2, cx + 13.5, cy - 4]);
  g.fill({ color: roofA });
  g.stroke({ color: accent, width: 1 });
  // Window
  g.rect(cx + 11, cy + 5, 2, 2);
  g.fill({ color: 0xffd166 });

  // ---- Optional satellite hut for larger cities ----
  if (population >= 3) {
    g.rect(cx - 10, cy + 8, 5, 4);
    g.fill({ color: wood });
    g.poly([cx - 11, cy + 8, cx - 4, cy + 8, cx - 7.5, cy + 5]);
    g.fill({ color: roofB });
  }

  c.addChild(g);
  return c;
}

/* =============================================================================
 * Per-biome ornamentation drawn on top of the base hex fill. Each biome
 * gets a distinctive silhouette pattern so the map reads as terrain,
 * not as flat color cells.
 * ========================================================================== */
function decorateBiome(
  g: Graphics,
  cx: number,
  cy: number,
  biome: string,
  alpha: number,
  // Per-tile noise so identical biomes don't all look identical.
  rngHash: number
): void {
  // Cheap deterministic small-int helpers from rngHash.
  const r1 = ((rngHash * 9301 + 49297) % 233280) / 233280;
  const r2 = ((rngHash * 49979 + 73121) % 233280) / 233280;
  const r3 = ((rngHash * 1103 + 12345) % 233280) / 233280;

  switch (biome) {
    case "mountain": {
      // Layered range — back peaks shadowed, front peaks lit, snow caps.
      const back = 0x4a4651;
      const mid = 0x6e6a76;
      const front = 0x8c8896;
      const snow = 0xffffff;
      // Back row (silhouette)
      g.poly([cx - 18, cy + 12, cx - 6, cy - 8, cx + 4, cy + 4, cx + 14, cy - 4, cx + 22, cy + 12]);
      g.fill({ color: back, alpha });
      // Front-left peak
      g.poly([cx - 16, cy + 14, cx - 6, cy - 14, cx + 4, cy + 14]);
      g.fill({ color: front, alpha });
      g.poly([cx - 16, cy + 14, cx - 6, cy - 14, cx + 4, cy + 14]);
      g.stroke({ color: back, width: 1.2, alpha });
      g.poly([cx - 4.5, cy - 6, cx - 6, cy - 14, cx - 7.5, cy - 6]);
      g.fill({ color: snow, alpha: alpha * 0.95 });
      // Front-right peak
      g.poly([cx + 2, cy + 14, cx + 12, cy - 8, cx + 22, cy + 14]);
      g.fill({ color: mid, alpha });
      g.poly([cx + 2, cy + 14, cx + 12, cy - 8, cx + 22, cy + 14]);
      g.stroke({ color: back, width: 1.2, alpha });
      g.poly([cx + 10, cy - 1, cx + 12, cy - 8, cx + 14, cy - 1]);
      g.fill({ color: snow, alpha: alpha * 0.9 });
      // Subtle ridge highlights
      g.moveTo(cx - 14, cy + 12).lineTo(cx - 6, cy - 8).stroke({ color: 0xb8b4c0, width: 0.7, alpha: alpha * 0.6 });
      g.moveTo(cx + 4, cy + 12).lineTo(cx + 12, cy - 6).stroke({ color: 0xb8b4c0, width: 0.7, alpha: alpha * 0.5 });
      break;
    }
    case "forest": {
      // 5-7 conifers of mixed sizes — looks like a small grove, not 3 toy trees.
      const trunk = 0x3d2a16;
      const leaves1 = 0x254f2a;
      const leaves2 = 0x1a3826;
      const positions: Array<[number, number, number]> = [
        [-14, 4, 1.0],
        [-6, -6, 1.15],
        [2, 2, 0.95],
        [10, -8, 1.1],
        [14, 6, 1.0],
        [-4, 12, 0.85],
        [8, 12, 0.9]
      ];
      for (const [ox, oy, scale] of positions) {
        const h = 11 * scale;
        const w = 6 * scale;
        // Trunk
        g.rect(cx + ox - 1, cy + oy + h * 0.45, 2, h * 0.35);
        g.fill({ color: trunk, alpha });
        // Three layered triangles (bottom-up = darker to lighter for depth)
        g.poly([cx + ox - w, cy + oy + h * 0.5, cx + ox + w, cy + oy + h * 0.5, cx + ox, cy + oy - h * 0.1]);
        g.fill({ color: leaves2, alpha });
        g.poly([cx + ox - w * 0.85, cy + oy + h * 0.2, cx + ox + w * 0.85, cy + oy + h * 0.2, cx + ox, cy + oy - h * 0.4]);
        g.fill({ color: leaves1, alpha });
        g.poly([cx + ox - w * 0.65, cy + oy - h * 0.05, cx + ox + w * 0.65, cy + oy - h * 0.05, cx + ox, cy + oy - h * 0.7]);
        g.fill({ color: 0x4a8050, alpha });
      }
      break;
    }
    case "hill": {
      // Layered humps with grass tufts on top.
      const dark = 0x6f5731;
      const mid = 0x9a7c4a;
      g.ellipse(cx - 8, cy + 8, 13, 7);
      g.fill({ color: dark, alpha });
      g.ellipse(cx + 8, cy + 6, 15, 8);
      g.fill({ color: mid, alpha });
      g.ellipse(cx + 8, cy + 6, 15, 8);
      g.stroke({ color: dark, width: 0.8, alpha: alpha * 0.6 });
      // Grass tufts on top
      const tufts: Array<[number, number]> = [[-12, 2], [-4, 0], [4, -2], [12, 0], [16, 4]];
      for (const [ox, oy] of tufts) {
        g.moveTo(cx + ox, cy + oy + 2).lineTo(cx + ox - 1, cy + oy - 2);
        g.moveTo(cx + ox, cy + oy + 2).lineTo(cx + ox + 1, cy + oy - 2);
        g.stroke({ color: 0x547a3a, width: 0.9, alpha });
      }
      // Optional wildflower
      if (r1 > 0.5) {
        g.circle(cx - 4, cy - 1, 1.2);
        g.fill({ color: 0xffd166, alpha });
      }
      break;
    }
    case "water": {
      // Multiple wave lines + sparse foam dots; 1/4 chance of a tiny boat.
      const wave = 0xa8d1f0;
      const foam = 0xffffff;
      const rows: Array<[number, number, number]> = [
        [-16, -10, 30],
        [-14, -2, 28],
        [-16, 6, 32],
        [-14, 14, 28]
      ];
      for (const [x0, y0, len] of rows) {
        g.moveTo(cx + x0, cy + y0)
          .bezierCurveTo(cx + x0 + len * 0.25, cy + y0 - 3, cx + x0 + len * 0.5, cy + y0 + 3, cx + x0 + len * 0.75, cy + y0)
          .bezierCurveTo(cx + x0 + len * 0.85, cy + y0 - 1.5, cx + x0 + len, cy + y0 + 1, cx + x0 + len, cy + y0);
        g.stroke({ color: wave, width: 1.4, alpha: alpha * 0.7 });
      }
      // Foam specks
      const foams: Array<[number, number]> = [[-8, -4], [6, 3], [-2, 11]];
      for (const [ox, oy] of foams) {
        g.circle(cx + ox, cy + oy, 0.8);
        g.fill({ color: foam, alpha: alpha * 0.6 });
      }
      // Occasional skiff (deterministic per tile)
      if (r2 > 0.78) {
        const bx = cx;
        const by = cy + 4;
        g.poly([bx - 8, by, bx + 8, by, bx + 5, by + 3, bx - 5, by + 3]);
        g.fill({ color: 0x6b3f1a, alpha });
        g.moveTo(bx, by).lineTo(bx, by - 8);
        g.stroke({ color: 0x4a2a10, width: 1.2, alpha });
        g.poly([bx, by - 8, bx + 6, by - 4, bx, by - 1]);
        g.fill({ color: 0xfff0d4, alpha });
      }
      break;
    }
    case "desert": {
      // Multiple dunes, scattered grains, occasional cactus.
      g.moveTo(cx - 18, cy + 12).bezierCurveTo(cx - 6, cy + 4, cx + 4, cy + 14, cx + 10, cy + 6).bezierCurveTo(cx + 14, cy + 2, cx + 18, cy + 8, cx + 20, cy + 12);
      g.stroke({ color: 0xa3863e, width: 1.4, alpha });
      g.moveTo(cx - 20, cy - 4).bezierCurveTo(cx - 10, cy - 10, cx, cy - 2, cx + 10, cy - 8).bezierCurveTo(cx + 14, cy - 10, cx + 18, cy - 6, cx + 22, cy - 8);
      g.stroke({ color: 0xa3863e, width: 1, alpha: alpha * 0.6 });
      // Grain dots
      const dots: Array<[number, number]> = [[-10, 0], [-2, -4], [6, 2], [12, -2], [16, 6], [-12, 8]];
      for (const [ox, oy] of dots) {
        g.circle(cx + ox, cy + oy, 0.9);
        g.fill({ color: 0xfff0c5, alpha: alpha * 0.7 });
      }
      // Cactus (~30% of tiles)
      if (r1 > 0.7) {
        const bx = cx + 6;
        const by = cy - 2;
        g.rect(bx - 1.5, by - 8, 3, 12);
        g.fill({ color: 0x4a8050, alpha });
        g.rect(bx - 5, by - 4, 3, 5);
        g.fill({ color: 0x4a8050, alpha });
        g.rect(bx + 2, by - 6, 3, 6);
        g.fill({ color: 0x4a8050, alpha });
      }
      break;
    }
    case "tundra": {
      // Several frost cracks + a small pine + cold lichen specks.
      const specks: Array<[number, number]> = [[-12, -8], [-6, -3], [4, -7], [10, -2], [-3, 9], [8, 10], [14, 5]];
      for (const [ox, oy] of specks) {
        g.circle(cx + ox, cy + oy, 1);
        g.fill({ color: 0xffffff, alpha: alpha * 0.7 });
      }
      // Pine
      const px = cx + (r3 > 0.5 ? -6 : 6);
      const py = cy + 2;
      g.rect(px - 1, py + 4, 2, 5);
      g.fill({ color: 0x5b6864, alpha });
      g.poly([px - 5, py + 5, px + 5, py + 5, px, py - 4]);
      g.fill({ color: 0x4a5e58, alpha });
      g.poly([px - 4, py + 2, px + 4, py + 2, px, py - 7]);
      g.fill({ color: 0x3a4e48, alpha });
      // Frost crack
      g.moveTo(cx - 16, cy + 10).lineTo(cx - 8, cy + 6).lineTo(cx - 2, cy + 8).lineTo(cx + 6, cy + 4);
      g.stroke({ color: 0xffffff, width: 0.8, alpha: alpha * 0.5 });
      break;
    }
    case "plain": {
      // Tall grass blades + scattered wildflowers + the occasional grazing deer.
      const tufts: Array<[number, number]> = [
        [-14, -2], [-8, 6], [-2, -8], [4, 4], [10, -4], [14, 8], [-12, 10], [8, -10]
      ];
      for (const [ox, oy] of tufts) {
        g.moveTo(cx + ox, cy + oy + 3).lineTo(cx + ox - 1.5, cy + oy - 3);
        g.moveTo(cx + ox, cy + oy + 3).lineTo(cx + ox, cy + oy - 4);
        g.moveTo(cx + ox, cy + oy + 3).lineTo(cx + ox + 1.5, cy + oy - 3);
        g.stroke({ color: 0x547a3a, width: 0.9, alpha });
      }
      // Wildflowers
      const flowers: Array<[number, number, number]> = [[-6, 2, 0xffd166], [6, 8, 0xff6b6b], [12, -2, 0xa778ff]];
      for (const [ox, oy, color] of flowers) {
        if (((rngHash + ox + oy) % 7) < 3) {
          g.circle(cx + ox, cy + oy, 1.4);
          g.fill({ color, alpha });
        }
      }
      // Occasional deer (~12% of tiles)
      if (r2 > 0.88) {
        const bx = cx + 4;
        const by = cy + 2;
        // Body
        g.ellipse(bx, by, 5, 3);
        g.fill({ color: 0xa67250, alpha });
        // Head + neck
        g.circle(bx + 5, by - 3, 2);
        g.fill({ color: 0xa67250, alpha });
        g.moveTo(bx + 4, by - 1).lineTo(bx + 5, by - 3);
        g.stroke({ color: 0x8a5a3c, width: 1.5, alpha });
        // Legs
        g.moveTo(bx - 3, by + 2).lineTo(bx - 3, by + 5);
        g.moveTo(bx + 3, by + 2).lineTo(bx + 3, by + 5);
        g.stroke({ color: 0x8a5a3c, width: 1.2, alpha });
      }
      break;
    }
  }
}

function resourceColor(r: string): number {
  switch (r) {
    case "wheat": return 0xffce4d;
    case "iron": return 0xb6bbcd;
    case "horse": return 0xa778ff;
    case "gold_ore": return 0xffd166;
    case "stone": return 0x8c9c98;
  }
  return 0xffffff;
}

// =============================================================================
// Input
// =============================================================================

function attachInput(
  canvas: HTMLCanvasElement,
  world: Container,
  cameraRef: { current: { x: number; y: number; zoom: number } },
  props: Props
): void {
  let dragging = false;
  let lastX = 0;
  let lastY = 0;
  let movedDuringDrag = 0;
  let pinchStartDist = 0;
  let pinchStartZoom = 1;
  let downAt = 0;

  function applyCam() {
    const c = cameraRef.current;
    world.position.set(c.x, c.y);
    world.scale.set(c.zoom);
  }

  function clientToHex(clientX: number, clientY: number) {
    const rect = canvas.getBoundingClientRect();
    const lx = clientX - rect.left;
    const ly = clientY - rect.top;
    const c = cameraRef.current;
    return pixelToHex((lx - c.x) / c.zoom, (ly - c.y) / c.zoom, HEX_SIZE);
  }

  // ---- Mouse ----
  canvas.addEventListener("mousedown", (e) => {
    dragging = true;
    movedDuringDrag = 0;
    lastX = e.clientX;
    lastY = e.clientY;
    downAt = performance.now();
  });
  canvas.addEventListener("mousemove", (e) => {
    if (dragging) {
      const dx = e.clientX - lastX;
      const dy = e.clientY - lastY;
      lastX = e.clientX;
      lastY = e.clientY;
      cameraRef.current.x += dx;
      cameraRef.current.y += dy;
      movedDuringDrag += Math.abs(dx) + Math.abs(dy);
      applyCam();
    } else {
      props.onHexHover(clientToHex(e.clientX, e.clientY));
    }
  });
  canvas.addEventListener("mouseup", (e) => {
    dragging = false;
    const dt = performance.now() - downAt;
    if (movedDuringDrag < 6 && dt < 350) {
      props.onHexClick(clientToHex(e.clientX, e.clientY));
    }
  });
  canvas.addEventListener("mouseleave", () => {
    dragging = false;
    props.onHexHover(null);
  });

  canvas.addEventListener(
    "wheel",
    (e) => {
      e.preventDefault();
      const factor = Math.exp(-e.deltaY * 0.0015);
      zoomAt(e.clientX, e.clientY, factor);
    },
    { passive: false }
  );

  // ---- Touch ----
  canvas.addEventListener("touchstart", (e) => {
    if (e.touches.length === 1) {
      dragging = true;
      movedDuringDrag = 0;
      lastX = e.touches[0]!.clientX;
      lastY = e.touches[0]!.clientY;
      downAt = performance.now();
    } else if (e.touches.length === 2) {
      dragging = false;
      pinchStartDist = touchDistance(e);
      pinchStartZoom = cameraRef.current.zoom;
    }
  }, { passive: true });

  canvas.addEventListener(
    "touchmove",
    (e) => {
      if (e.touches.length === 1 && dragging) {
        e.preventDefault();
        const dx = e.touches[0]!.clientX - lastX;
        const dy = e.touches[0]!.clientY - lastY;
        lastX = e.touches[0]!.clientX;
        lastY = e.touches[0]!.clientY;
        cameraRef.current.x += dx;
        cameraRef.current.y += dy;
        movedDuringDrag += Math.abs(dx) + Math.abs(dy);
        applyCam();
      } else if (e.touches.length === 2) {
        e.preventDefault();
        const d = touchDistance(e);
        const target = pinchStartZoom * (d / pinchStartDist);
        const cx = (e.touches[0]!.clientX + e.touches[1]!.clientX) / 2;
        const cy = (e.touches[0]!.clientY + e.touches[1]!.clientY) / 2;
        setZoomAt(cx, cy, clamp(target, 0.4, 2.5));
      }
    },
    { passive: false }
  );

  canvas.addEventListener("touchend", (e) => {
    if (dragging && e.changedTouches.length === 1 && e.touches.length === 0) {
      dragging = false;
      const dt = performance.now() - downAt;
      if (movedDuringDrag < 8 && dt < 400) {
        props.onHexClick(clientToHex(e.changedTouches[0]!.clientX, e.changedTouches[0]!.clientY));
      }
    } else if (e.touches.length < 2) {
      pinchStartDist = 0;
    }
  });

  function zoomAt(clientX: number, clientY: number, factor: number) {
    const rect = canvas.getBoundingClientRect();
    const lx = clientX - rect.left;
    const ly = clientY - rect.top;
    const c = cameraRef.current;
    const newZoom = clamp(c.zoom * factor, 0.4, 2.5);
    const k = newZoom / c.zoom;
    c.x = lx - (lx - c.x) * k;
    c.y = ly - (ly - c.y) * k;
    c.zoom = newZoom;
    applyCam();
  }

  function setZoomAt(clientX: number, clientY: number, newZoom: number) {
    const rect = canvas.getBoundingClientRect();
    const lx = clientX - rect.left;
    const ly = clientY - rect.top;
    const c = cameraRef.current;
    const k = newZoom / c.zoom;
    c.x = lx - (lx - c.x) * k;
    c.y = ly - (ly - c.y) * k;
    c.zoom = newZoom;
    applyCam();
  }

  function touchDistance(e: TouchEvent): number {
    const dx = e.touches[0]!.clientX - e.touches[1]!.clientX;
    const dy = e.touches[0]!.clientY - e.touches[1]!.clientY;
    return Math.hypot(dx, dy);
  }

  function clamp(v: number, lo: number, hi: number) {
    return Math.max(lo, Math.min(hi, v));
  }
}
