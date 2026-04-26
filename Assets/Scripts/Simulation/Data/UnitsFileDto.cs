// Copyright (c) 2026 Irfan Gedik. All rights reserved.
// This file is part of Nyrvexa. Licensing: see LICENSE in the repository root (TBD).
//


using System;

namespace Nyrvexa.Simulation.Data
{
    /// <summary> Kök JSON: <c>Assets/StreamingAssets/Defs/*.json</c> (Unity JsonUtility alan adları ile uyumlu). </summary>
    [Serializable]
    public sealed class UnitsFileDto
    {
        public int schema;
        public UnitArchetypeRecord[] archetypes = Array.Empty<UnitArchetypeRecord>();
    }

    [Serializable]
    public sealed class UnitArchetypeRecord
    {
        public string id = "";
        public int visionRange;
        public int movementPerTurn;
        public int combatPower;
        public int defense;
    }
}
