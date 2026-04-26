/* =============================================================================
 * File:           Assets/Scripts/Simulation/Events/RumorRollEvent.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.10
 * 
 * Description:
 *   Deterministik "olay" aşamasından düşen kod (M1: ileri HUD/rapor; mekanik
 *   yok).
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

namespace Nyrvexa.Simulation.Events
{
    /// <summary> Deterministik "olay" aşamasından düşen kod (M1: ileri HUD/rapor; mekanik yok). </summary>
    public sealed class RumorRollEvent : IGameEvent
    {
        public string Channel => "Crisis";
        public int Code { get; }
        public long Turn { get; }

        public RumorRollEvent(int code, long turn)
        {
            Code = code;
            Turn = turn;
        }
    }
}
