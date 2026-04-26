/* =============================================================================
 * File:           Assets/Scripts/Simulation/Loop/Steps/ScoutContactRumorStep.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.9
 * 
 * Description:
 *   Keşifçiler bitişik / üst üste: ek söylenti (Crisis ile aynı olay türü).
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using Nyrvexa.Simulation.Events;
using Nyrvexa.Simulation.Loop;
using Nyrvexa.Simulation.State;

namespace Nyrvexa.Simulation.Loop.Steps
{
    /// <summary> Keşifçiler bitişik / üst üste: ek söylenti (Crisis ile aynı olay türü). </summary>
    public sealed class ScoutContactRumorStep : IPipelineStep
    {
        private static readonly string ScoutKey = "unit.scout";
        private readonly IGameEventSink _bus;

        public string Name => "Events_ScoutContact";

        public ScoutContactRumorStep(IGameEventSink bus) => _bus = bus;

        public void Execute(TurnContext ctx)
        {
            if (_bus == null) return;
            var s = ctx.State;
            if (s?.World == null) return;
            if (!TryGetScoutTile(s, 0, out var t0) || !TryGetScoutTile(s, 1, out var t1)) return;
            int d = s.World.AxialDistanceBetween(t0, t1);
            if (d > 1) return;
            if ((ctx.Rng.NextU64() % 4ul) != 0ul) return;
            int code = (int)(ctx.Rng.NextU64() & 0x7FFF_FFFF) ^ (t0 << 8) ^ t1;
            _bus.Publish(new RumorRollEvent(code, s.Header.TurnIndex));
        }

        private static bool TryGetScoutTile(GameStateRoot s, int fac, out int flat)
        {
            flat = -1;
            foreach (var kv in s.Units)
            {
                var u = kv.Value;
                if (u.FactionIndex != fac) continue;
                if (u.Archetype.Key != ScoutKey) continue;
                flat = u.TileIndex;
                return true;
            }
            return false;
        }
    }
}
