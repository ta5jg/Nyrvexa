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
