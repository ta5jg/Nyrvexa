/* =============================================================================
 * File:           Assets/Scripts/Simulation/Commands/IGameCommand.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.9
 * 
 * Description:
 *   IGameCommand — Nyrvexa modülü (ayrıntı kaynakta).
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using Nyrvexa.Simulation.Loop;

namespace Nyrvexa.Simulation.Commands
{
    public interface IGameCommand
    {
        int IssuerFactionIndex { get; }
        void Apply(TurnContext ctx);
    }
}
