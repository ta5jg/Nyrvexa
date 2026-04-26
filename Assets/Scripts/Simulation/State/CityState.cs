/* =============================================================================
 * File:           Assets/Scripts/Simulation/State/CityState.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.10
 * 
 * Description:
 *   CityState — Nyrvexa modülü (ayrıntı kaynakta).
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using System.Collections.Generic;
using Nyrvexa.Simulation.Core;

namespace Nyrvexa.Simulation.State
{
    public sealed class CityState
    {
        public string Name;
        public int FactionIndex;
        public int TileIndex;
        public int Population;
        public int CityVision = 1;
        public readonly List<DefId> BuildingIds = new List<DefId>();
    }
}
