/* =============================================================================
 * File:           Assets/Scripts/Simulation/Loop/Steps/ProductionStep.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.7
 * 
 * Description:
 *   Bina dışı "işlenen arazi" getirisi: şehir hücresinin biyomuna göre yiy/ür.
 *   binaları işler; bu adım aynı turda biyom verimini ekler.
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using Nyrvexa.Simulation.Core;
using Nyrvexa.Simulation.Loop;
using Nyrvexa.Simulation.Research;

namespace Nyrvexa.Simulation.Loop.Steps
{
    /// <summary>
    /// Bina dışı "işlenen arazi" getirisi: şehir hücresinin biyomuna göre yiy/ür.
    /// <see cref="EconomyStep"/> binaları işler; bu adım aynı turda biyom verimini ekler.
    /// </summary>
    public sealed class ProductionStep : IPipelineStep
    {
        private static readonly DefId ResFood = new("res.food");
        private static readonly DefId ResProd = new("res.prod");

        public string Name => "Production";

        public void Execute(TurnContext ctx)
        {
            var s = ctx.State;
            var tiles = s.World.Tiles;
            if (tiles == null) return;
            foreach (var pair in s.Cities)
            {
                var c = pair.Value;
                if (c.FactionIndex < 0 || c.FactionIndex >= s.Factions.Count) continue;
                if (c.TileIndex < 0 || c.TileIndex >= tiles.Length) continue;
                var biome = tiles[c.TileIndex].BiomeId;
                var (fd, pd) = YieldFromBiome(biome);
                var fac = s.Factions[c.FactionIndex];
                if (fd != 0) fac.AddResource(ResFood, fd);
                if (pd != 0) fac.AddResource(ResProd, pd);
                if (fac.UnlockedTechnologies.Contains(ResearchM1Catalog.AgricultureM1))
                    fac.AddResource(ResFood, 1);
            }
        }

        /// <summary> Biyom 1..6 (prosedür); 0 veya dış aralık → düz çayırlık varsayımı. </summary>
        private static (int food, int prod) YieldFromBiome(ushort biomeId)
        {
            int idx = biomeId == 0 ? 0 : (int)((biomeId - 1) % 6);
            return idx switch
            {
                0 => (1, 0),
                1 => (2, 0),
                2 => (0, 1),
                3 => (1, 1),
                4 => (0, 1),
                _ => (1, 0)
            };
        }
    }
}
