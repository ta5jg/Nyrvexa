/* =============================================================================
 * File:           Assets/Scripts/Simulation/AI/SimpleRivalScoutBrain.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.8
 * 
 * Description:
 *   Fraksiyon 1: her tur bitişik rastgele (deterministik) yasal hücreye keşifçi
 *   sür.
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using System.Collections.Generic;
using Nyrvexa.Simulation.Commands;
using Nyrvexa.Simulation.Core;
using Nyrvexa.Simulation.Loop;
using Nyrvexa.Simulation.State;
using Nyrvexa.Simulation.World;

namespace Nyrvexa.Simulation.AI
{
    /// <summary> Fraksiyon 1: her tur bitişik rastgele (deterministik) yasal hücreye keşifçi sür. </summary>
    public sealed class SimpleRivalScoutBrain : IStrategicBrain
    {
        public void QueueStrategicActions(int factionIndex, GameStateRoot state, CommandJournal journal, TurnContext ctx)
        {
            if (journal == null || state?.World.Tiles == null) return;
            if (state.Factions.Count < 2 || factionIndex < 1) return;

            var ids = new List<EntityId>(4);
            foreach (var kv in state.Units)
            {
                if (kv.Value.FactionIndex == factionIndex) ids.Add(kv.Key);
            }
            if (ids.Count == 0) return;
            ids.Sort(CompareId);

            for (int i = 0; i < ids.Count; i++)
            {
                if (!state.Units.TryGetValue(ids[i], out var u)) continue;
                if (u.MovementPoints < 1) continue;
                if (u.TileIndex < 0 || u.TileIndex >= state.World.Tiles.Length) continue;
                if (!TryChooseNeighborIndex(state, in u, ref ctx.Rng, out var target)) continue;
                journal.Enqueue(new MoveUnitCommand(factionIndex, u.Id, target));
            }
        }

        private static int CompareId(EntityId a, EntityId b)
        {
            var c = a.Index.CompareTo(b.Index);
            return c != 0 ? c : a.Generation.CompareTo(b.Generation);
        }

        private static bool TryChooseNeighborIndex(GameStateRoot s, in UnitState u, ref DeterministicRng rng, out int target)
        {
            target = -1;
            var order = new int[6];
            for (int d = 0; d < 6; d++) order[d] = d;
            for (int n = 5; n > 0; n--)
            {
                var j = rng.NextInt(n + 1);
                (order[n], order[j]) = (order[j], order[n]);
            }
            for (int k = 0; k < 6; k++)
            {
                int d = order[k];
                int ti = s.World.GetNeighborIndex(u.TileIndex, d);
                if (ti < 0) continue;
                if (!IsLegalMoveTo(s, in u, ti)) continue;
                target = ti;
                return true;
            }
            return false;
        }

        private static bool IsLegalMoveTo(GameStateRoot s, in UnitState u, int targetTile)
        {
            if (targetTile < 0 || targetTile >= s.World.Tiles.Length) return false;
            if (s.World.AxialDistanceBetween(u.TileIndex, targetTile) != 1) return false;
            if (u.Archetype.Key == "unit.scout" && s.World.Tiles != null
                && TilePassage.IsBlockedScoutEnter(s.World.Tiles[targetTile].BiomeId))
                return false;
            if (u.MovementPoints < 1) return false;
            if (s.UnitOnTile.TryGetValue(targetTile, out var occ) && occ != u.Id)
            {
                if (s.Units[occ].FactionIndex == u.FactionIndex) return false;
                return false;
            }
            return true;
        }
    }
}
