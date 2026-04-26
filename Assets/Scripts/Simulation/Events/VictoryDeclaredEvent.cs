/* =============================================================================
 * File:           Assets/Scripts/Simulation/Events/VictoryDeclaredEvent.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.9
 * 
 * Description:
 *   VictoryDeclaredEvent — Nyrvexa modülü (ayrıntı kaynakta).
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

namespace Nyrvexa.Simulation.Events
{
    public sealed class VictoryDeclaredEvent : IGameEvent
    {
        public string Channel => "Victory";
        public int VictorFactionIndex { get; }
        public long TurnIndex;
        public long VictorScore { get; }

        public VictoryDeclaredEvent(int victorFactionIndex, long atTurn, long score)
        {
            VictorFactionIndex = victorFactionIndex;
            TurnIndex = atTurn;
            VictorScore = score;
        }
    }
}
