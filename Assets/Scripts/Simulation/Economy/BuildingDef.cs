/* =============================================================================
 * File:           Assets/Scripts/Simulation/Economy/BuildingDef.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.9
 * 
 * Description:
 *   BuildingDef — Nyrvexa modülü (ayrıntı kaynakta).
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using System.Collections.Generic;
using Nyrvexa.Simulation.Core;

namespace Nyrvexa.Simulation.Economy
{
    public sealed class BuildingDef
    {
        public DefId Id;
        public readonly Dictionary<DefId, int> OutputPerTurn = new Dictionary<DefId, int>();
    }

    public sealed class EconomyDefCatalog
    {
        public readonly Dictionary<DefId, BuildingDef> Buildings = new Dictionary<DefId, BuildingDef>();

        public bool TryGetBuilding(DefId id, out BuildingDef b) => Buildings.TryGetValue(id, out b);

        public static EconomyDefCatalog CreateDefault()
        {
            var c = new EconomyDefCatalog();
            var palace = new BuildingDef
            {
                Id = new DefId("building.palace")
            };
            palace.OutputPerTurn[new DefId("res.food")] = 2;
            palace.OutputPerTurn[new DefId("res.prod")] = 1;
            c.Buildings[palace.Id] = palace;
            return c;
        }
    }
}
