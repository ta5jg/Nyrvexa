/* =============================================================================
 * File:           Assets/Scripts/Simulation/Loop/Steps/EconomyStep.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.9
 * 
 * Description:
 *   EconomyStep — Nyrvexa modülü (ayrıntı kaynakta).
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

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
