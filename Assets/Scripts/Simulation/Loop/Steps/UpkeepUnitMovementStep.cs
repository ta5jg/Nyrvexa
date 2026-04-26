/* =============================================================================
 * File:           Assets/Scripts/Simulation/Loop/Steps/UpkeepUnitMovementStep.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.7
 * 
 * Description:
 *   UpkeepUnitMovementStep — Nyrvexa modülü (ayrıntı kaynakta).
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using System.Collections.Generic;
using Nyrvexa.Simulation.Data;
using Nyrvexa.Simulation.Loop;
using Nyrvexa.Simulation.Core;

namespace Nyrvexa.Simulation.Loop.Steps
{
    public sealed class UpkeepUnitMovementStep : IPipelineStep
    {
        public string Name => "Upkeep_RefreshMovement";

        public void Execute(TurnContext ctx)
        {
            var s = ctx.State;
            // Dictionary üzerinde foreach iken s.Units[k] = u ataması sürümü değiştirip InvalidOperationException üretebilir.
            var keys = new List<EntityId>(s.Units.Count);
            foreach (var k in s.Units.Keys) keys.Add(k);
            for (int i = 0; i < keys.Count; i++)
            {
                if (!s.Units.TryGetValue(keys[i], out var u)) continue;
                int cap = UnitRules.GetMovementCap(in u, s.UnitDefs);
                u.MaxMovement = cap;
                u.MovementPoints = cap;
                s.Units[keys[i]] = u;
            }
        }
    }
}
