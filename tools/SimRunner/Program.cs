/* =============================================================================
 * File:           tools/SimRunner/Program.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.10
 * 
 * Description:
 *   Unity yok: macOS / M2'de sadece dotnet run ile aynı simülasyonu çalıştır.
 *   Gerekli: .NET 8 SDK (ARM64).
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using System;
using System.IO;
using Nyrvexa.Simulation.Architecture;
using Nyrvexa.Simulation.Commands;
using Nyrvexa.Simulation.Core;
using Nyrvexa.Simulation.Data;
using Nyrvexa.Simulation.Diplomacy;
using Nyrvexa.Simulation.Events;
using Nyrvexa.Simulation.Loop;
using Nyrvexa.Simulation.Persistence;
using Nyrvexa.Simulation.Research;
using Nyrvexa.Simulation.Hex;
using Nyrvexa.Simulation.State;
using Nyrvexa.Simulation.World;
using Nyrvexa.Simulation.World.Generation;
using Nyrvexa.SimRunner;
using static Nyrvexa.Simulation.World.WorldMapDefaults;

/// <summary>
/// Unity yok: macOS / M2'de sadece <c>dotnet run</c> ile aynı simülasyonu çalıştır.
/// Gerekli: <see href="https://dotnet.microsoft.com/download">.NET 8 SDK</see> (ARM64).
/// </summary>
static class Program
{
    static int Main(string[] args)
    {
        int turns = 10;
        for (int i = 0; i < args.Length; i++)
        {
            var a = args[i];
            if (a.StartsWith("--turns=", StringComparison.Ordinal)
                && int.TryParse(a.AsSpan("--turns=".Length), out var t)
                && t > 0 && t <= 5000)
                turns = t;
        }

        if (!PointyLayout.VerifyInversionAllCells(1f, MapWidth, MapHeight))
        {
            Console.Error.WriteLine("HATA: heks ileri/geri tersleme (PointyLayout) — regresyon.");
            return 1;
        }
        Console.WriteLine("Nyrvexa SimRunner (headless) — " + (Environment.ProcessPath ?? "SimRunner") + " | heks tersleme: OK");
        const ulong worldSeed = 0xBADC0FFEE0UL;
        var rng = new DeterministicRng(worldSeed);
        var bus = new InMemoryEventBus();
        var state = new GameStateRoot
        {
            Header = { WorldSeed = worldSeed, TurnIndex = 0, RngState64 = rng.State, SchemaVersion = GameSchema.Current }
        };
        var defaultPath = UnitJsonNet.DefaultUnitsPath();
        if (UnitJsonNet.TryReadFile(defaultPath, out var unitsDto, out var jsonErr) && unitsDto != null)
        {
            if (UnitCatalogImporter.TryImport(unitsDto, state.UnitDefs, out var impErr))
                Console.WriteLine("  Birim kataloğu: " + defaultPath);
            else
                Console.WriteLine("  Uyarı: JSON okundu ama import edilemedi: " + impErr);
        }
        else
        {
            if (File.Exists(defaultPath)) Console.WriteLine("  Uyarı: " + jsonErr);
            else Console.WriteLine("  Uyarı: " + defaultPath + " yok, gömülü varsayılan archetype.");
        }
        state.World.Allocate(MapWidth, MapHeight, 0);
        new DefaultProceduralWorldGenerator().GenerateInto(state, ref rng, new WorldGenParams(MapWidth, MapHeight));
        state.Factions.Add(new FactionState { Index = 0, DebugName = "Kolektif_A", CultureRoot = new DefId("culture.k") });
        state.Factions.Add(new FactionState { Index = 1, DebugName = "Birlik_B", CultureRoot = new DefId("culture.b") });
        state.Diplomacy = new DiploMatrixState();
        state.Diplomacy.EnsureSize(2);
        state.Diplomacy.SetRelation(0, 1, 50, 10);
        state.Diplomacy.SetRelation(1, 0, 50, 10);
        var scoutId = state.Registry.Create();
        var scout = new UnitState
        {
            Id = scoutId,
            FactionIndex = 0,
            TileIndex = 23,
            Archetype = new DefId("unit.scout")
        };
        UnitRules.ApplyArchetypeStats(ref scout, state.UnitDefs);
        state.Units[scoutId] = scout;
        state.UnitOnTile[23] = scoutId;
        FogRevealService.RevealDisk(state.World, 23, 2, 0);
        var rivalId = state.Registry.Create();
        var rival = new UnitState
        {
            Id = rivalId,
            FactionIndex = 1,
            TileIndex = 18,
            Archetype = new DefId("unit.scout")
        };
        UnitRules.ApplyArchetypeStats(ref rival, state.UnitDefs);
        state.Units[rivalId] = rival;
        state.UnitOnTile[18] = rivalId;
        FogRevealService.RevealDisk(state.World, 18, 2, 1);
        var pipeline = TurnPipeline.CreateDefault(bus, state.CommandJournal);
        for (int i = 0; i < turns; i++)
        {
            if (i == 0)
            {
                state.CommandJournal.ClearBuffer();
                state.CommandJournal.Enqueue(new FoundCityCommand(0, 22, "Aldaris"));
                state.CommandJournal.Enqueue(new FoundCityCommand(1, 27, "Borath"));
                state.CommandJournal.Enqueue(new MoveUnitCommand(0, scoutId, 24));
            }
            pipeline.RunSingleTurn(state, rng);
            if (!TryVerifyWorldInvariants(state, out var invErr))
            {
                Console.Error.WriteLine("  HATA: simülasyon değişmezi: " + invErr);
                return 1;
            }
            LogStocks("  tur sonu, endeks=" + state.Header.TurnIndex, state);
        }
        {
            int rumors = 0;
            for (int hi = 0; hi < bus.History.Count; hi++)
            {
                if (bus.History[hi] is RumorRollEvent) rumors++;
            }
            Console.WriteLine("  Söylenti atışları (M1, ~%2/tur, " + turns + " tur): " + rumors);
        }
        state.CommandJournal.ClearBuffer();
        state.CommandJournal.Enqueue(new NoOpCommand(0));
        var ser = new PlainTextGameSerializer();
        var text = ser.SerializeGameState(state);
        if (text.IndexOf("journ=", StringComparison.Ordinal) < 0
            || text.IndexOf("N|0", StringComparison.Ordinal) < 0)
        {
            Console.Error.WriteLine("  HATA: journ= (NoOp) round-trip metinde yok.");
            return 1;
        }
        var back = ser.DeserializeGameState(text);
        var jbuf = back.CommandJournal.PeekBuffer();
        if (jbuf.Count != 1 || jbuf[0] is not NoOpCommand n || n.IssuerFactionIndex != 0)
        {
            Console.Error.WriteLine("  HATA: NoOp kuyruğu ayrıştırma (journ) regresyon.");
            return 1;
        }
        Console.WriteLine("  journ= (NoOp F0) serileştir/oku: OK");
        Console.WriteLine("  Kayıt yuvarlak: fraksiyon " + back.Factions.Count + " / şehir " + back.Cities.Count
            + "  feco eşit=" + (FactionEconomyEquals(state, back) ? "evet" : "hayır (regresyon)"));
        if (back.Factions.Count >= 2)
        {
            back.Diplomacy.GetPair(0, 1, out var tr, out var te);
            Console.WriteLine("  Diplo 0|1 = " + tr + "," + te + " (M1 diplo sürüklenmesi, " + turns + " tur sim)");
        }
        if (back.World.Tiles is { Length: > 23 })
            Console.WriteLine("  k23 ExploredMask (F0) = " + (back.World.Tiles[23].ExploredMask & 1));
        if (back.Header.DeclaredVictorFactionIndex >= 0)
            Console.WriteLine("  Kazanan (T≥8 ekonomi): F" + back.Header.DeclaredVictorFactionIndex);
        else
            Console.WriteLine("  Kazanan: henüz yok (T<8 veya " + turns + " tura yetişmedi).");
        Console.WriteLine("Bitti. Unity olmadan simülasyon OK.");
        return 0;
    }

    private static bool TryVerifyWorldInvariants(GameStateRoot s, out string err)
    {
        err = "";
        foreach (var kv in s.Units)
        {
            var u = kv.Value;
            if (!s.UnitOnTile.TryGetValue(u.TileIndex, out var on) || on != kv.Key)
            {
                err = "UnitOnTile birim/düz uyumsuz";
                return false;
            }
        }
        foreach (var kv in s.UnitOnTile)
        {
            if (!s.Units.TryGetValue(kv.Value, out var u) || u.TileIndex != kv.Key)
            {
                err = "UnitOnTile sözlük tutarsız";
                return false;
            }
        }
        for (int f = 0; f < s.Factions.Count; f++)
        {
            foreach (var rs in s.Factions[f].ResourceStock)
            {
                if (rs.Value < 0)
                {
                    err = "Negatif stok: " + rs.Key.Key;
                    return false;
                }
            }
        }
        return true;
    }

    private static bool FactionEconomyEquals(GameStateRoot a, GameStateRoot b)
    {
        if (a.Factions.Count != b.Factions.Count) return false;
        for (int i = 0; i < a.Factions.Count; i++)
        {
            var x = a.Factions[i];
            var y = b.Factions[i];
            x.ResourceStock.TryGetValue(new DefId("res.food"), out var xf);
            y.ResourceStock.TryGetValue(new DefId("res.food"), out var yf);
            x.ResourceStock.TryGetValue(new DefId("res.prod"), out var xp);
            y.ResourceStock.TryGetValue(new DefId("res.prod"), out var yp);
            if (xf != yf || xp != yp) return false;
            if (x.StoredResearchPoints != y.StoredResearchPoints) return false;
            if (x.ActiveResearchId != y.ActiveResearchId) return false;
            if (x.ActiveResearchProgress != y.ActiveResearchProgress) return false;
            if (x.UnlockedTechnologies.Count != y.UnlockedTechnologies.Count) return false;
            foreach (var u in x.UnlockedTechnologies)
            {
                if (!y.UnlockedTechnologies.Contains(u)) return false;
            }
        }
        return true;
    }

    private static void LogStocks(string label, GameStateRoot s)
    {
        for (int f = 0; f < s.Factions.Count; f++)
        {
            var fac = s.Factions[f];
            fac.ResourceStock.TryGetValue(new DefId("res.food"), out var food);
            fac.ResourceStock.TryGetValue(new DefId("res.prod"), out var pr);
            var u = fac.UnlockedTechnologies.Count;
            var p = (fac.ActiveResearchId.IsValid
                && ResearchM1Catalog.TryGetCost(fac.ActiveResearchId, out var tc))
                ? fac.ActiveResearchProgress + "/" + tc
                : "—";
            Console.WriteLine(label + " | " + fac.DebugName + " yiy=" + food + " ür=" + pr + " şehir=" + fac.CityIds.Count
                + " ABP=" + fac.StoredResearchPoints + " tech=" + u + "/" + ResearchM1Catalog.Ordered.Length
                + " ilerleme=" + p);
        }
    }
}
