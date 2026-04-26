/* =============================================================================
 * File:           Assets/Scripts/Simulation/Data/UnitArchetypeDef.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.10
 * 
 * Description:
 *   Veri odaklı birim şablonu (mod/JSON sonraki adım).
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using Nyrvexa.Simulation.Core;

namespace Nyrvexa.Simulation.Data
{
    /// <summary> Veri odaklı birim şablonu (mod/JSON sonraki adım). </summary>
    public sealed class UnitArchetypeDef
    {
        public DefId Id;
        public int VisionRange = 1;
        public int MovementPerTurn = 2;
        public int CombatPower = 1;
        public int Defense = 1;
    }
}
