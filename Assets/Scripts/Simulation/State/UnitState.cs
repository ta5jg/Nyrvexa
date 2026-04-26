using Nyrvexa.Simulation.Core;

namespace Nyrvexa.Simulation.State
{
    public sealed class UnitState
    {
        public EntityId Id;
        public int FactionIndex;
        public int TileIndex;
        public DefId Archetype;
        public int MovementPoints;
        public int MaxMovement;
        public int VisionRadius = 1;
    }
}
