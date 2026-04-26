// Copyright (c) 2026 Irfan Gedik. All rights reserved.
// This file is part of Nyrvexa. Licensing: see LICENSE in the repository root (TBD).
//


using Nyrvexa.Simulation.State;

namespace Nyrvexa.Simulation.Data
{
    public static class UnitRules
    {
        public static int GetEffectiveVision(in UnitState u, UnitArchetypeCatalog cat, int extraRadius = 0)
        {
            int b;
            if (cat != null && cat.TryGet(u.Archetype, out var d)) b = d.VisionRange;
            else b = u.VisionRadius;
            b += extraRadius;
            return b < 0 ? 0 : b;
        }

        public static int GetMovementCap(in UnitState u, UnitArchetypeCatalog cat)
        {
            if (cat != null && cat.TryGet(u.Archetype, out var d)) return d.MovementPerTurn;
            return u.MaxMovement;
        }

        public static void ApplyArchetypeStats(ref UnitState u, UnitArchetypeCatalog cat)
        {
            if (cat == null || !cat.TryGet(u.Archetype, out var d)) return;
            u.VisionRadius = d.VisionRange;
            u.MaxMovement = d.MovementPerTurn;
            u.MovementPoints = d.MovementPerTurn;
        }
    }
}
