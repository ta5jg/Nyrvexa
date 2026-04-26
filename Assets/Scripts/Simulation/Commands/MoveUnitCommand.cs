/* =============================================================================
 * File:           Assets/Scripts/Simulation/Commands/MoveUnitCommand.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.9
 * 
 * Description:
 *   MoveUnitCommand — Nyrvexa modülü (ayrıntı kaynakta).
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using Nyrvexa.Simulation.Core;
using Nyrvexa.Simulation.Loop;
using Nyrvexa.Simulation.World;

namespace Nyrvexa.Simulation.Commands
{
    public sealed class MoveUnitCommand : IGameCommand
    {
        public int IssuerFactionIndex { get; }
        public EntityId Unit;
        public int TargetTileIndex;

        public MoveUnitCommand(int issuerFaction, EntityId unit, int targetTile)
        {
            IssuerFactionIndex = issuerFaction;
            Unit = unit;
            TargetTileIndex = targetTile;
        }

        public void Apply(TurnContext ctx)
        {
            var s = ctx.State;
            if (TargetTileIndex < 0 || TargetTileIndex >= s.World.Tiles.Length) return;
            if (!s.Units.TryGetValue(Unit, out var u)) return;
            if (u.FactionIndex != IssuerFactionIndex) return;
            if (s.World.AxialDistanceBetween(u.TileIndex, TargetTileIndex) != 1) return;
            if (u.Archetype.Key == "unit.scout" && s.World.Tiles != null
                && TilePassage.IsBlockedScoutEnter(s.World.Tiles[TargetTileIndex].BiomeId))
                return;
            if (u.MovementPoints < 1) return;
            if (s.UnitOnTile.TryGetValue(TargetTileIndex, out var occ) && occ != Unit)
            {
                if (s.Units[occ].FactionIndex == IssuerFactionIndex) return;
                return;
            }
            if (s.UnitOnTile.TryGetValue(u.TileIndex, out var onFrom) && onFrom == Unit)
                s.UnitOnTile.Remove(u.TileIndex);
            u.TileIndex = TargetTileIndex;
            u.MovementPoints -= 1;
            s.Units[Unit] = u;
            s.UnitOnTile[TargetTileIndex] = Unit;
        }
    }
}
