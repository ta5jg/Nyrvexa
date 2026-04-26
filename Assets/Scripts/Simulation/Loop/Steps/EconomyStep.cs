// Copyright (c) 2026 Irfan Gedik. All rights reserved.
// This file is part of Nyrvexa. Licensing: see LICENSE in the repository root (TBD).
//


using Nyrvexa.Simulation.Loop;

namespace Nyrvexa.Simulation.Loop.Steps
{
    public sealed class EconomyStep : IPipelineStep
    {
        public string Name => "Economy";

        public void Execute(TurnContext ctx)
        {
            var s = ctx.State;
            foreach (var pair in s.Cities)
            {
                var c = pair.Value;
                var fac = s.Factions[c.FactionIndex];
                for (int i = 0; i < c.BuildingIds.Count; i++)
                {
                    if (s.Defs.TryGetBuilding(c.BuildingIds[i], out var b))
                    {
                        foreach (var o in b.OutputPerTurn)
                            fac.AddResource(o.Key, o.Value);
                    }
                }
            }
        }
    }
}
