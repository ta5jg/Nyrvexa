/* =============================================================================
 * File:           Assets/Scripts/Simulation/AI/UtilityStrategicStub.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.8
 * 
 * Description:
 *   UtilityStrategicStub — Nyrvexa modülü (ayrıntı kaynakta).
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using Nyrvexa.Simulation.Commands;
using Nyrvexa.Simulation.Loop;
using Nyrvexa.Simulation.State;

namespace Nyrvexa.Simulation.AI
{
    public sealed class UtilityStrategicStub : IStrategicBrain
    {
        public void QueueStrategicActions(int factionIndex, GameStateRoot state, CommandJournal journal, TurnContext ctx)
        {
        }
    }
}
