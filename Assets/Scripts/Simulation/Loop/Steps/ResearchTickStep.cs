// Copyright (c) 2026 Irfan Gedik. All rights reserved.
// This file is part of Nyrvexa. Licensing: see LICENSE in the repository root (TBD).
//


using Nyrvexa.Simulation.Core;
using Nyrvexa.Simulation.Loop;
using Nyrvexa.Simulation.Research;
using Nyrvexa.Simulation.State;

namespace Nyrvexa.Simulation.Loop.Steps
{
    /// <summary>
    /// ABP: üs + şehir bonusu, sonra kuyruktaki teknolojiye harcanır; bittiğinde sıradaki (M1 katalog) açılır.
    /// </summary>
    public sealed class ResearchTickStep : IPipelineStep
    {
        public string Name => "ResearchTick";

        public void Execute(TurnContext ctx)
        {
            var s = ctx.State;
            for (int i = 0; i < s.Factions.Count; i++)
            {
                var f = s.Factions[i];
                f.StoredResearchPoints += 1 + 2 * f.CityIds.Count;

                for (int guard = 0; guard < 64; guard++)
                {
                    if (!f.ActiveResearchId.IsValid
                        && ResearchM1Catalog.TryGetNextLockable(f.UnlockedTechnologies, out var pick, out _))
                    {
                        f.ActiveResearchId = pick;
                        f.ActiveResearchProgress = 0;
                    }

                    if (!f.ActiveResearchId.IsValid) break;
                    if (!ResearchM1Catalog.TryGetCost(f.ActiveResearchId, out var cost)) break;
                    var need = cost - f.ActiveResearchProgress;
                    if (need <= 0)
                    {
                        Complete(f, f.ActiveResearchId);
                        continue;
                    }
                    var spend = f.StoredResearchPoints < need ? f.StoredResearchPoints : need;
                    if (spend == 0) break;
                    f.StoredResearchPoints -= spend;
                    f.ActiveResearchProgress += spend;
                    if (f.ActiveResearchProgress < cost) break;
                    Complete(f, f.ActiveResearchId);
                }
            }
        }

        private static void Complete(FactionState f, DefId id)
        {
            f.UnlockedTechnologies.Add(id);
            f.ActiveResearchId = new DefId(string.Empty);
            f.ActiveResearchProgress = 0;
        }
    }
}
