/* =============================================================================
 * File:           Assets/Scripts/Simulation/Events/IGameEvent.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.7
 * 
 * Description:
 *   IGameEvent — Nyrvexa modülü (ayrıntı kaynakta).
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

namespace Nyrvexa.Simulation.Events
{
    public interface IGameEvent
    {
        string Channel { get; }
    }
}
