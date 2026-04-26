/* =============================================================================
 * File:           Assets/Scripts/Simulation/State/FactionState.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.10
 * 
 * Description:
 *   FactionState — Nyrvexa modülü (ayrıntı kaynakta).
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using System.Collections.Generic;
using Nyrvexa.Simulation.Core;

namespace Nyrvexa.Simulation.State
{
    public sealed class FactionState
    {
        public int Index;
        public string DebugName;
        public DefId CultureRoot;
        public DefId Government;
        public DefId PrimaryIdeology;
        public int StoredResearchPoints;
        /// <summary> Boş <see cref="DefId"/> = yok. </summary>
        public DefId ActiveResearchId;
        public int ActiveResearchProgress;
        public readonly HashSet<DefId> UnlockedTechnologies = new HashSet<DefId>();
        public int AggressionBudget;
        public readonly Dictionary<DefId, int> Modifiers = new Dictionary<DefId, int>();
        public readonly List<EntityId> CityIds = new List<EntityId>();
        public readonly Dictionary<DefId, int> ResourceStock = new Dictionary<DefId, int>();

        public void AddResource(DefId r, int delta)
        {
            if (delta == 0) return;
            ResourceStock.TryGetValue(r, out var v);
            ResourceStock[r] = v + delta;
        }
    }
}
