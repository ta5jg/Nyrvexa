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
