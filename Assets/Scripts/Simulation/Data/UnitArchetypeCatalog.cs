using System.Collections.Generic;
using Nyrvexa.Simulation.Core;

namespace Nyrvexa.Simulation.Data
{
    public sealed class UnitArchetypeCatalog
    {
        public readonly Dictionary<DefId, UnitArchetypeDef> Archetypes = new Dictionary<DefId, UnitArchetypeDef>();

        public bool TryGet(DefId id, out UnitArchetypeDef def) => Archetypes.TryGetValue(id, out def);

        public static UnitArchetypeCatalog CreateDefault()
        {
            var c = new UnitArchetypeCatalog();
            var scout = new UnitArchetypeDef
            {
                Id = new DefId("unit.scout"),
                VisionRange = 2,
                MovementPerTurn = 2,
                CombatPower = 1,
                Defense = 1
            };
            c.Archetypes[scout.Id] = scout;
            return c;
        }
    }
}
