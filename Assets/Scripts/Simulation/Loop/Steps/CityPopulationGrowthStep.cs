/* =============================================================================
 * File:           Assets/Scripts/Simulation/Loop/Steps/CityPopulationGrowthStep.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.10
 * 
 * Description:
 *   Gıda tüketerek şehir nüfus artışı; şehir/tur fazına göre kademe.
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using Nyrvexa.Simulation.Core;
using Nyrvexa.Simulation.Loop;
using Nyrvexa.Simulation.State;

namespace Nyrvexa.Simulation.Loop.Steps
{
    /// <summary> Gıda tüketerek şehir nüfus artışı; şehir/tur fazına göre kademe. </summary>
    public sealed class CityPopulationGrowthStep : IPipelineStep
    {
        private static readonly DefId ResFood = new("res.food");
        public string Name => "CityPopulation";

        public void Execute(TurnContext ctx)
        {
            var s = ctx.State;
            var T = s.Header.TurnIndex;
            if (T < 1) return;
            foreach (var kv in s.Cities)
            {
                var c = kv.Value;
                if (c == null) continue;
                if (c.Population >= 14) continue;
                if (c.FactionIndex < 0 || c.FactionIndex >= s.Factions.Count) continue;
                if ((T + c.TileIndex) % 3 != 0) continue;
                var fac = s.Factions[c.FactionIndex];
                if (!fac.ResourceStock.TryGetValue(ResFood, out var yiy) || yiy < 2) continue;
                if (c.Population >= 2 && c.Population * 2 > yiy) continue;
                fac.AddResource(ResFood, -2);
                c.Population++;
            }
        }
    }
}
