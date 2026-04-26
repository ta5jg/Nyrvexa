// Copyright (c) 2026 Irfan Gedik. All rights reserved.
// This file is part of Nyrvexa. Licensing: see LICENSE in the repository root (TBD).
//


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
