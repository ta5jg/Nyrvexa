// Copyright (c) 2026 Irfan Gedik. All rights reserved.
// This file is part of Nyrvexa. Licensing: see LICENSE in the repository root (TBD).
//


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
