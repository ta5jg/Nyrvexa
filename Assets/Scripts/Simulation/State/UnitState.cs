/* =============================================================================
 * File:           Assets/Scripts/Simulation/State/UnitState.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.8
 * 
 * Description:
 *   UnitState — Nyrvexa modülü (ayrıntı kaynakta).
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using Nyrvexa.Simulation.Core;

namespace Nyrvexa.Simulation.State
{
    public sealed class UnitState
    {
        public EntityId Id;
        public int FactionIndex;
        public int TileIndex;
        public DefId Archetype;
        public int MovementPoints;
        public int MaxMovement;
        public int VisionRadius = 1;
    }
}
