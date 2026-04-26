/* =============================================================================
 * File:           Assets/Scripts/Simulation/Research/ResearchGraphState.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.10
 * 
 * Description:
 *   Rezerv / MP-ileri senk. M1'de ağaç üzerinde.
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using System.Collections.Generic;
using Nyrvexa.Simulation.Core;

namespace Nyrvexa.Simulation.Research
{
    /// <summary> Rezerv / MP-ileri senk. M1'de ağaç <see cref="Nyrvexa.Simulation.State.FactionState.UnlockedTechnologies"/> üzerinde. </summary>
    public sealed class ResearchGraphState
    {
        public readonly HashSet<DefId> Unlocked = new HashSet<DefId>();
        public readonly Dictionary<DefId, int> InProgress = new Dictionary<DefId, int>();
    }
}
