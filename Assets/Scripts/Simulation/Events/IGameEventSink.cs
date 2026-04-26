/* =============================================================================
 * File:           Assets/Scripts/Simulation/Events/IGameEventSink.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.8
 * 
 * Description:
 *   IGameEventSink — Nyrvexa modülü (ayrıntı kaynakta).
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

namespace Nyrvexa.Simulation.Events
{
    public interface IGameEventSink
    {
        void Publish(IGameEvent evt);
    }
}
