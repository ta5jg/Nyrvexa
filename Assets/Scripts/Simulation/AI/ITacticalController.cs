/* =============================================================================
 * File:           Assets/Scripts/Simulation/AI/ITacticalController.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.8
 * 
 * Description:
 *   Savaş / keşif alt oturumunda eylem seçimi (GOAP / yerel değerlendirme).
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using Nyrvexa.Simulation.Core;
using Nyrvexa.Simulation.Loop;
using Nyrvexa.Simulation.State;

namespace Nyrvexa.Simulation.AI
{
    /// <summary> Savaş / keşif alt oturumunda eylem seçimi (GOAP / yerel değerlendirme). </summary>
    public interface ITacticalController
    {
        void IssueOrders(EntityId groupId, GameStateRoot state, TurnContext ctx);
    }
}
