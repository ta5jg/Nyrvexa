using Nyrvexa.Simulation.Core;
using Nyrvexa.Simulation.Data;
using Nyrvexa.Simulation.Loop;
using Nyrvexa.Simulation.Research;
using Nyrvexa.Simulation.World;

namespace Nyrvexa.Simulation.Loop.Steps
{
    /// <summary>
    /// Uygulanmış komutlardan sonra: birim ve şehir merkezine göre keşfî mask güncellemesi.
    /// </summary>
    public sealed class SightAndExplorationStep : IPipelineStep
    {
        public string Name => "SightExploration";

        public void Execute(TurnContext ctx)
        {
            var s = ctx.State;
            if (s.World.Tiles == null) return;
            var scout = new DefId("unit.scout");
            foreach (var kv in s.Units)
            {
                var u = kv.Value;
                int ex = 0;
                if (u.FactionIndex >= 0 && u.FactionIndex < s.Factions.Count
                    && s.Factions[u.FactionIndex].UnlockedTechnologies.Contains(ResearchM1Catalog.SurveyM1)
                    && u.Archetype == scout)
                    ex = 1;
                int r = UnitRules.GetEffectiveVision(in u, s.UnitDefs, ex);
                FogRevealService.RevealDisk(s.World, u.TileIndex, r, u.FactionIndex);
            }
            foreach (var c in s.Cities.Values)
            {
                if (c.CityVision < 0) continue;
                FogRevealService.RevealDisk(s.World, c.TileIndex, c.CityVision, c.FactionIndex);
            }
        }
    }
}
