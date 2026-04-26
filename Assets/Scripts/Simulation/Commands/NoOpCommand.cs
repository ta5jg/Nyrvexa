/* =============================================================================
 * File:           Assets/Scripts/Simulation/Commands/NoOpCommand.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.9
 * 
 * Description:
 *   NoOpCommand — Nyrvexa modülü (ayrıntı kaynakta).
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using Nyrvexa.Simulation.Loop;

namespace Nyrvexa.Simulation.Commands
{
    public sealed class NoOpCommand : IGameCommand
    {
        public int IssuerFactionIndex { get; }
        public NoOpCommand(int issuerFactionIndex) => IssuerFactionIndex = issuerFactionIndex;
        public void Apply(TurnContext ctx) { }
    }
}
