// Copyright (c) 2026 Irfan Gedik. All rights reserved.
// This file is part of Nyrvexa. Licensing: see LICENSE in the repository root (TBD).
//


using System;
using Nyrvexa.Simulation.Loop;
using Nyrvexa.Simulation.Core;

namespace Nyrvexa.Simulation.Loop.Steps
{
    /// <summary>
    /// <see cref="State.GameStateRoot.UnitOnTile"/> sözlüğünü <see cref="State.GameStateRoot.Units"/> üzerinden yeniden yazar; çakışan iki birim tespit edilirse fırlatır.
    /// </summary>
    public sealed class UnitIndexReconcileStep : IPipelineStep
    {
        public string Name => "Movement_UnitMapIndex";

        public void Execute(TurnContext ctx)
        {
            var s = ctx.State;
            if (s.World.Tiles == null) return;
            s.UnitOnTile.Clear();
            foreach (var kv in s.Units)
            {
                var u = kv.Value;
                int t = u.TileIndex;
                if (t < 0 || t >= s.World.Tiles.Length)
                    throw new InvalidOperationException("Birim hücresi sınır dışı: Entity=" + u.Id + " düz=" + t);
                if (s.UnitOnTile.ContainsKey(t))
                {
                    var o = s.UnitOnTile[t];
                    throw new InvalidOperationException("Aynı hücrede iki birim: düz " + t + " → " + o + " ve " + kv.Key);
                }
                s.UnitOnTile[t] = kv.Key;
            }
            if (s.UnitOnTile.Count != s.Units.Count)
                throw new InvalidOperationException("Birim sayısı ile harita eşleşmesi hatalı: birim " + s.Units.Count + " eşleşen " + s.UnitOnTile.Count);
        }
    }
}
