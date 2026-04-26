// Copyright (c) 2026 Irfan Gedik. All rights reserved.
// This file is part of Nyrvexa. Licensing: see LICENSE in the repository root (TBD).
//


using System;
using System.Globalization;
using System.Text;
using Nyrvexa.Simulation.Core;
using Nyrvexa.Simulation.Data;
using Nyrvexa.Simulation.Economy;
using Nyrvexa.Simulation.State;
using Nyrvexa.Simulation.World;

namespace Nyrvexa.Simulation.Persistence
{
    public sealed class PlainTextGameSerializer : IGameStateSerializer
    {
        private const string Magic = "NYRVEXA_V1";
        private const string TilesKey = "TILES";
        private static readonly DefId ResFood = new("res.food");
        private static readonly DefId ResProd = new("res.prod");

        public string SerializeGameState(GameStateRoot state)
        {
            if (state == null) throw new ArgumentNullException(nameof(state));
            var inv = CultureInfo.InvariantCulture;
            var sb = new StringBuilder();
            var h = state.Header;
            sb.AppendLine(Magic);
            sb.Append("schema=").AppendLine(h.SchemaVersion.ToString(inv));
            sb.Append("seed=").AppendLine(h.WorldSeed.ToString(inv));
            sb.Append("turn=").AppendLine(h.TurnIndex.ToString(inv));
            sb.Append("rng=").AppendLine(h.RngState64.ToString(inv));
            sb.Append("victor=").AppendLine(h.DeclaredVictorFactionIndex.ToString(inv));
            sb.Append("width=").AppendLine(state.World.Width.ToString(inv));
            sb.Append("height=").AppendLine(state.World.Height.ToString(inv));
            sb.Append("nextid=").AppendLine(state.Registry.GetNextIdSlot().ToString(inv));
            sb.Append("idgen=").AppendLine(state.Registry.Generation.ToString(inv));
            sb.AppendLine(TilesKey);
            var tiles = state.World.Tiles;
            if (tiles != null)
            {
                for (int i = 0; i < tiles.Length; i++)
                {
                    var t = tiles[i];
                    if (i > 0) sb.Append(';');
                    sb.Append(t.BiomeId).Append(',').Append(t.OwnerFactionIndex).Append(',').Append(t.ExploredMask);
                }
            }
            sb.AppendLine();
            sb.Append("factions=").AppendLine(state.Factions.Count.ToString(inv));
            for (int i = 0; i < state.Factions.Count; i++)
            {
                var f = state.Factions[i];
                var name = (f.DebugName ?? "").Replace("\n", " ").Replace("|", "_");
                sb.Append("f=").Append(i).Append("|").AppendLine(name);
            }
            for (int i = 0; i < state.Factions.Count; i++)
            {
                var f = state.Factions[i];
                f.ResourceStock.TryGetValue(ResFood, out var yiy);
                f.ResourceStock.TryGetValue(ResProd, out var ur);
                var act = f.ActiveResearchId.IsValid ? f.ActiveResearchId.Key : "-";
                sb.Append("feco=").Append(i).Append('|').Append(yiy).Append('|').Append(ur)
                    .Append('|').Append(f.StoredResearchPoints).Append('|').Append(act)
                    .Append('|').Append(f.ActiveResearchProgress).Append('|');
                var u = 0;
                foreach (var id in f.UnlockedTechnologies)
                {
                    var k = id.Key ?? string.Empty;
                    if (string.IsNullOrEmpty(k)) continue;
                    if (u++ > 0) sb.Append(',');
                    sb.Append(k.Replace(",", "_", StringComparison.Ordinal));
                }
                sb.AppendLine();
            }
            int nF = state.Factions.Count;
            state.Diplomacy.EnsureSize(nF);
            if (nF > 0)
            {
                sb.Append("diplon=").AppendLine(nF.ToString(inv));
                var sbt = new StringBuilder();
                var sbg = new StringBuilder();
                for (int a = 0; a < nF; a++)
                {
                    for (int b = 0; b < nF; b++)
                    {
                        if (a != 0 || b != 0)
                        {
                            sbt.Append(',');
                            sbg.Append(',');
                        }
                        state.Diplomacy.GetPair(a, b, out var tr, out var te);
                        sbt.Append(tr);
                        sbg.Append(te);
                    }
                }
                sb.Append("diplot=").AppendLine(sbt.ToString());
                sb.Append("diplog=").AppendLine(sbg.ToString());
            }
            sb.Append("cities=").AppendLine(state.Cities.Count.ToString(inv));
            foreach (var kv in state.Cities)
            {
                var c = kv.Value;
                var nm = c.Name?.Replace("\n", " ") ?? "X";
                sb.Append("c=").Append(kv.Key.Index).Append('.').Append(kv.Key.Generation)
                    .Append('|').Append(c.FactionIndex).Append('|').Append(c.TileIndex)
                    .Append('|').Append(nm)
                    .Append("|").Append(c.Population);
                for (int b = 0; b < c.BuildingIds.Count; b++)
                    sb.Append('|').Append(c.BuildingIds[b].Key);
                sb.AppendLine();
            }
            sb.Append("units=").AppendLine(state.Units.Count.ToString(inv));
            foreach (var kv in state.Units)
            {
                var u = kv.Value;
                sb.Append("u=").Append(kv.Key.Index).Append('.').Append(kv.Key.Generation)
                    .Append('|').Append(u.FactionIndex).Append('|').Append(u.TileIndex)
                    .Append('|').Append(u.Archetype.Key)
                    .Append('|').Append(u.MovementPoints).Append('|').Append(u.MaxMovement)
                    .Append('|').Append(u.VisionRadius)
                    .AppendLine();
            }
            CommandJournalWire.Append(sb, state);
            sb.AppendLine();
            return sb.ToString();
        }

        public GameStateRoot DeserializeGameState(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) throw new ArgumentException(nameof(text));
            var inv = CultureInfo.InvariantCulture;
            var s = new GameStateRoot
            {
                Defs = EconomyDefCatalog.CreateDefault(),
                UnitDefs = UnitArchetypeCatalog.CreateDefault()
            };
            int w = 0, h2 = 0, schema = 1;
            uint nextid = 1, idgen = 1;
            string dplotCsv = null;
            string dplogCsv = null;
            var lines = text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length < 1 || !lines[0].StartsWith(Magic, StringComparison.Ordinal))
                throw new InvalidOperationException("Geçersiz sabit");
            int i = 1;
            for (; i < lines.Length; i++)
            {
                var L = lines[i].Trim();
                if (L == TilesKey) { i++; break; }
                if (!L.Contains("=", StringComparison.Ordinal)) continue;
                var kv = L.Split(new[] { '=' }, 2);
                if (kv.Length != 2) continue;
                var k = kv[0].Trim();
                var v = kv[1].Trim();
                switch (k.ToLowerInvariant())
                {
                    case "schema": schema = int.Parse(v, inv); s.Header.SchemaVersion = schema; break;
                    case "seed": s.Header.WorldSeed = ulong.Parse(v, inv); break;
                    case "turn": s.Header.TurnIndex = long.Parse(v, inv); break;
                    case "rng": s.Header.RngState64 = ulong.Parse(v, inv); break;
                    case "victor": s.Header.DeclaredVictorFactionIndex = int.Parse(v, inv); break;
                    case "width": w = int.Parse(v, inv); break;
                    case "height": h2 = int.Parse(v, inv); break;
                    case "nextid": nextid = uint.Parse(v, inv); break;
                    case "idgen": idgen = uint.Parse(v, inv); break;
                }
            }
            if (i < lines.Length)
            {
                var tl = lines[i].Trim();
                if (w > 0 && h2 > 0) s.World.Allocate(w, h2, 0);
                if (s.World.Tiles != null && !string.IsNullOrEmpty(tl))
                {
                    var parts = tl.Contains(";", StringComparison.Ordinal) ? tl.Split(';') : new[] { tl };
                    for (int t = 0; t < parts.Length && t < s.World.Tiles.Length; t++)
                    {
                        var t3 = parts[t].Split(',');
                        if (t3.Length < 3) continue;
                        s.World.Tiles[t] = new TileCell
                        {
                            BiomeId = ushort.Parse(t3[0], inv),
                            OwnerFactionIndex = sbyte.Parse(t3[1], inv),
                            ExploredMask = ushort.Parse(t3[2], inv)
                        };
                    }
                }
                i++;
            }
            s.Registry.ReseedForLoad(nextid, idgen);
            s.Factions.Clear();
            s.Cities.Clear();
            s.Units.Clear();
            s.UnitOnTile.Clear();
            for (; i < lines.Length; i++)
            {
                var L = lines[i].Trim();
                if (L.StartsWith("factions=", StringComparison.Ordinal))
                {
                }
                else if (L.StartsWith("f=", StringComparison.Ordinal))
                {
                    var p = L.Substring(2);
                    int bar = p.IndexOf('|', StringComparison.Ordinal);
                    if (bar > 0)
                    {
                        int fi = int.Parse(p.Substring(0, bar), inv);
                        while (s.Factions.Count <= fi) s.Factions.Add(new FactionState { Index = s.Factions.Count, CultureRoot = new DefId("culture.default") });
                        s.Factions[fi].DebugName = p.Substring(bar + 1);
                    }
                }
                else if (L.StartsWith("cities=", StringComparison.Ordinal)) { }
                else if (L.StartsWith("c=", StringComparison.Ordinal)) ParseCity(s, L, inv);
                else if (L.StartsWith("units=", StringComparison.Ordinal)) { }
                else if (L.StartsWith("u=", StringComparison.Ordinal)) ParseUnit(s, L, inv);
                else if (L.StartsWith("feco=", StringComparison.Ordinal)) ParseFactionEconomy(s, L, inv);
                else if (L.StartsWith("diplot=", StringComparison.Ordinal)) dplotCsv = L.Substring("diplot=".Length);
                else if (L.StartsWith("diplog=", StringComparison.Ordinal)) dplogCsv = L.Substring("diplog=".Length);
                else if (L.StartsWith("diplon=", StringComparison.Ordinal)) { }
                else if (L.StartsWith(CommandJournalWire.Key, StringComparison.Ordinal))
                    CommandJournalWire.ParseLineIntoState(L, s);
            }
            ApplyDiploFromCsv(s, dplotCsv, dplogCsv, inv);
            s.Diplomacy.EnsureSize(Math.Max(1, s.Factions.Count));
            return s;
        }

        private static void ApplyDiploFromCsv(GameStateRoot s, string tCsv, string gCsv, CultureInfo inv)
        {
            if (string.IsNullOrEmpty(tCsv) || string.IsNullOrEmpty(gCsv)) return;
            int n = s.Factions.Count;
            if (n < 1) return;
            var ts = tCsv.Split(',');
            var gs = gCsv.Split(',');
            s.Diplomacy.EnsureSize(n);
            int need = n * n;
            for (int k = 0; k < need && k < ts.Length && k < gs.Length; k++)
            {
                int a = k / n;
                int b = k % n;
                s.Diplomacy.SetRelation(a, b, int.Parse(ts[k].Trim(), inv), int.Parse(gs[k].Trim(), inv));
            }
        }

        private void ParseCity(GameStateRoot s, string L, CultureInfo inv)
        {
            var body = L.Substring(2);
            var p = body.Split('|');
            if (p.Length < 5) return;
            var idp = p[0].Split('.');
            var id = new EntityId(uint.Parse(idp[0], inv), idp.Length > 1 ? uint.Parse(idp[1], inv) : 1);
            int fac = int.Parse(p[1], inv);
            int tile = int.Parse(p[2], inv);
            var city = new CityState
            {
                FactionIndex = fac,
                TileIndex = tile,
                Name = p[3],
                Population = int.Parse(p[4], inv)
            };
            for (int b = 5; b < p.Length; b++) city.BuildingIds.Add(new DefId(p[b]));
            s.Cities[id] = city;
            if (fac >= 0 && fac < s.Factions.Count) s.Factions[fac].CityIds.Add(id);
            if (tile >= 0 && tile < s.World.Tiles.Length) s.World.SetOwner(tile, fac);
        }

        private static void ParseFactionEconomy(GameStateRoot s, string L, CultureInfo inv)
        {
            var body = L.Substring("feco=".Length);
            var p = body.Split('|');
            if (p.Length < 6) return;
            int fi = int.Parse(p[0].Trim(), inv);
            if (fi < 0 || fi >= s.Factions.Count) return;
            var f = s.Factions[fi];
            f.ResourceStock.Clear();
            f.ResourceStock[ResFood] = int.Parse(p[1].Trim(), inv);
            f.ResourceStock[ResProd] = int.Parse(p[2].Trim(), inv);
            f.StoredResearchPoints = int.Parse(p[3].Trim(), inv);
            var act = p[4].Trim();
            f.ActiveResearchId = act == "-" || string.IsNullOrEmpty(act) ? new DefId(string.Empty) : new DefId(act);
            f.ActiveResearchProgress = int.Parse(p[5].Trim(), inv);
            f.UnlockedTechnologies.Clear();
            if (p.Length > 6 && !string.IsNullOrEmpty(p[6].Trim()))
            {
                var parts = p[6].Split(',');
                for (int i = 0; i < parts.Length; i++)
                {
                    var k = parts[i].Trim();
                    if (!string.IsNullOrEmpty(k)) f.UnlockedTechnologies.Add(new DefId(k));
                }
            }
        }

        private void ParseUnit(GameStateRoot s, string L, CultureInfo inv)
        {
            var body = L.Substring(2);
            var p = body.Split('|');
            if (p.Length < 6) return;
            var idp = p[0].Split('.');
            var id = new EntityId(uint.Parse(idp[0], inv), idp.Length > 1 ? uint.Parse(idp[1], inv) : 1);
            var u = new UnitState
            {
                Id = id,
                FactionIndex = int.Parse(p[1], inv),
                TileIndex = int.Parse(p[2], inv),
                Archetype = new DefId(p[3]),
                MovementPoints = int.Parse(p[4], inv),
                MaxMovement = int.Parse(p[5], inv),
                VisionRadius = p.Length > 6 ? int.Parse(p[6], inv) : 1
            };
            s.Units[id] = u;
            s.UnitOnTile[u.TileIndex] = id;
        }
    }
}
