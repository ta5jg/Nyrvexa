/* =============================================================================
 * File:           Assets/Scripts/Simulation/Events/TurnPhaseEvent.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.8
 * 
 * Description:
 *   TurnPhaseEvent — Nyrvexa modülü (ayrıntı kaynakta).
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

namespace Nyrvexa.Simulation.Events
{
    public sealed class TurnPhaseEvent : IGameEvent
    {
        public string Channel => "Turn";
        public string Phase { get; }
        public long Turn { get; }

        public TurnPhaseEvent(string phase, long turn)
        {
            Phase = phase;
            Turn = turn;
        }
    }
}
