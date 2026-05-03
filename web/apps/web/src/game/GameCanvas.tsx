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

const HEX_SIZE = 36;

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

        // Center camera on the world initially.
        const center = worldCenter(props.state);
        cameraRef.current.x = app.canvas.clientWidth / 2 - center.x;
        cameraRef.current.y = app.canvas.clientHeight / 2 - center.y;
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

function worldCenter(state: WorldSnapshot): { x: number; y: number } {
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
  return { x: (minX + maxX) / 2, y: (minY + maxY) / 2 };
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
    const g = new Graphics();
    g.poly(hexPolygonPath(p.x, p.y, HEX_SIZE - 0.5));
    g.fill({ color: colorToHex(biome.color), alpha: visible ? 1 : 0.45 });
    g.poly(hexPolygonPath(p.x, p.y, HEX_SIZE - 0.5));
    g.stroke({ color: colorToHex(biome.outline), width: 1.5, alpha: visible ? 1 : 0.55 });

    // Subtle inner highlight (crystalline edge).
    g.poly(hexPolygonPath(p.x, p.y, HEX_SIZE - 4));
    g.stroke({ color: 0xffffff, width: 0.6, alpha: visible ? 0.07 : 0.03 });

    // Resource glyph as a small inner diamond.
    if (t.resource && visible) {
      const r = new Graphics();
      const rs = 5;
      r.poly([p.x, p.y - rs, p.x + rs, p.y, p.x, p.y + rs, p.x - rs, p.y]);
      r.fill({ color: resourceColor(t.resource) });
      r.stroke({ color: 0x0c0e14, width: 1 });
      tile.addChild(g, r);
      continue;
    }
    tile.addChild(g);
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

    // Outer ring
    const ring = new Graphics();
    ring.circle(p.x, p.y, HEX_SIZE * 0.55);
    ring.stroke({ color: colorToHex(factionColor), width: 4 });
    ring.circle(p.x, p.y, HEX_SIZE * 0.55);
    ring.fill({ color: 0x0c0e14, alpha: 0.35 });

    // Center cross emblem — original glyph (not a Civ-style turret)
    const emblem = new Graphics();
    const s = 8;
    emblem.moveTo(p.x - s, p.y).lineTo(p.x + s, p.y).moveTo(p.x, p.y - s).lineTo(p.x, p.y + s);
    emblem.stroke({ color: colorToHex(factionColor), width: 2.5 });
    // Diamond accent
    emblem.poly([p.x, p.y - 4, p.x + 4, p.y, p.x, p.y + 4, p.x - 4, p.y]);
    emblem.fill({ color: colorToHex(factionColor) });

    sprites.addChild(ring, emblem);

    // Population label
    const txt = new Text({
      text: `${c.name} · ${c.population}`,
      style: new TextStyle({
        fontFamily: "Inter, sans-serif",
        fontSize: 11,
        fontWeight: "800",
        fill: 0xf3f4f8,
        stroke: { color: 0x0c0e14, width: 3, join: "round" }
      })
    });
    txt.anchor.set(0.5, 1);
    txt.position.set(p.x, p.y + HEX_SIZE * 0.55 + 14);
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

function drawUnitGlyph(cx: number, cy: number, type: string, factionColor: string): Container {
  const c = new Container();
  const fc = colorToHex(factionColor);
  const g = new Graphics();
  const s = 12;

  switch (type) {
    case "settler": {
      // Asymmetric pennant shape — distinctive, not a generic pawn.
      g.poly([cx, cy - s, cx + s * 0.9, cy, cx, cy + s, cx - s * 0.45, cy + s * 0.5, cx - s * 0.45, cy - s * 0.5]);
      break;
    }
    case "warrior": {
      // Inverted shield — two layers.
      g.poly([cx, cy - s, cx + s * 0.85, cy - s * 0.4, cx + s * 0.55, cy + s, cx - s * 0.55, cy + s, cx - s * 0.85, cy - s * 0.4]);
      break;
    }
    case "scout": {
      // Long arrowhead — fast feel.
      g.poly([cx, cy - s * 1.05, cx + s * 0.5, cy + s * 0.4, cx, cy + s * 0.9, cx - s * 0.5, cy + s * 0.4]);
      break;
    }
    case "archer": {
      // Tall arc with a baseline.
      g.moveTo(cx - s, cy + s * 0.6);
      g.bezierCurveTo(cx - s, cy - s * 0.8, cx + s, cy - s * 0.8, cx + s, cy + s * 0.6);
      g.lineTo(cx - s, cy + s * 0.6);
      break;
    }
    case "horseman": {
      // Slanted parallelogram.
      g.poly([cx - s * 0.9, cy + s * 0.5, cx - s * 0.4, cy - s * 0.7, cx + s * 0.9, cy - s * 0.5, cx + s * 0.4, cy + s * 0.7]);
      break;
    }
    case "worker": {
      // Open square (tool icon).
      g.rect(cx - s * 0.7, cy - s * 0.7, s * 1.4, s * 1.4);
      break;
    }
    default: {
      g.circle(cx, cy, s);
    }
  }
  g.fill({ color: fc });
  g.stroke({ color: 0x0c0e14, width: 2, join: "round" });
  c.addChild(g);
  return c;
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
