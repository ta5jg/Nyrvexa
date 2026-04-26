/* =============================================================================
 * File:           Assets/Scripts/Simulation/Commands/SetResearchCommand.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.7
 * 
 * Description:
 *   Oyuncu (veya script) yeni aktif M1 teknolojisi seçer; farklı projeye
 *   geçerken mevcut bar ABP'ye iade.
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using Nyrvexa.Simulation.Core;
using Nyrvexa.Simulation.Loop;
using Nyrvexa.Simulation.Research;

namespace Nyrvexa.Simulation.Commands
{
    /// <summary> Oyuncu (veya script) yeni aktif M1 teknolojisi seçer; farklı projeye geçerken mevcut bar ABP'ye iade. </summary>
    public sealed class SetResearchCommand : IGameCommand
    {
        public int IssuerFactionIndex { get; }
        public DefId Tech { get; }

        public SetResearchCommand(int issuerFaction, DefId tech)
        {
            IssuerFactionIndex = issuerFaction;
            Tech = tech;
        }

        public void Apply(TurnContext ctx)
        {
            var s = ctx.State;
            if (IssuerFactionIndex < 0 || IssuerFactionIndex >= s.Factions.Count) return;
            var f = s.Factions[IssuerFactionIndex];
            if (!ResearchM1Catalog.IsValidResearchTarget(Tech, f.UnlockedTechnologies)) return;
            if (f.ActiveResearchId == Tech) return;
            if (f.ActiveResearchId.IsValid)
                f.StoredResearchPoints += f.ActiveResearchProgress;
            f.ActiveResearchId = Tech;
            f.ActiveResearchProgress = 0;
        }
    }
}
