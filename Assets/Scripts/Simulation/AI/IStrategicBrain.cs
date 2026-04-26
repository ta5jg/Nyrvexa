/* =============================================================================
 * File:           Assets/Scripts/Simulation/AI/IStrategicBrain.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.8
 * 
 * Description:
 *   Fraksiyon başına yüksek seviye hedef ve komut üretir (Utility + bütçe).
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using Nyrvexa.Simulation.Commands;
using Nyrvexa.Simulation.Loop;
using Nyrvexa.Simulation.State;

namespace Nyrvexa.Simulation.AI
{
    /// <summary> Fraksiyon başına yüksek seviye hedef ve komut üretir (Utility + bütçe). </summary>
    public interface IStrategicBrain
    {
        void QueueStrategicActions(int factionIndex, GameStateRoot state, CommandJournal journal, TurnContext ctx);
    }
}
