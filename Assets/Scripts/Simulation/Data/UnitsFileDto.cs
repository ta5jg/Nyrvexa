/* =============================================================================
 * File:           Assets/Scripts/Simulation/Data/UnitsFileDto.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.8
 * 
 * Description:
 *   Kök JSON: Assets/StreamingAssets/Defs/*.json (Unity JsonUtility alan adları
 *   ile uyumlu).
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

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
