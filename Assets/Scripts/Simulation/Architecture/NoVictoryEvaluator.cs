/* =============================================================================
 * File:           Assets/Scripts/Simulation/Architecture/NoVictoryEvaluator.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.9
 * 
 * Description:
 *   Kazanma yok; ilerde bilim/diplo/alan şartları buraya asılır.
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using Nyrvexa.Simulation.State;

namespace Nyrvexa.Simulation.Architecture
{
    /// <summary> Kazanma yok; ilerde bilim/diplo/alan şartları buraya asılır. </summary>
    public sealed class NoVictoryEvaluator : IVictoryConditionEvaluator
    {
        public int EvaluateVictor(GameStateRoot state, long turnIndex) => -1;
    }
}
