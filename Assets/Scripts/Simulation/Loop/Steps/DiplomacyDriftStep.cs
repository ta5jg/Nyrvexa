/* =============================================================================
 * File:           Assets/Scripts/Simulation/Loop/Steps/DiplomacyDriftStep.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.10
 * 
 * Description:
 *   M1: çift yönlü diplo ızgarasında güveni 50'ye, gerginliği 0'a yavaş çeker.
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using System;
using Nyrvexa.Simulation.Loop;

namespace Nyrvexa.Simulation.Loop.Steps
{
    /// <summary> M1: çift yönlü diplo ızgarasında güveni 50'ye, gerginliği 0'a yavaş çeker. </summary>
    public sealed class DiplomacyDriftStep : IPipelineStep
    {
        public string Name => "Diplomacy_Drift";

        public void Execute(TurnContext ctx)
        {
            var s = ctx.State;
            if (s?.Diplomacy == null) return;
            int n = s.Diplomacy.Size;
            if (n < 2) return;
            for (int a = 0; a < n; a++)
            {
                for (int b = 0; b < n; b++)
                {
                    if (a == b) continue;
                    s.Diplomacy.GetPair(a, b, out var tr, out var te);
                    tr = NudgeToward(tr, 50, 1, 0, 100);
                    te = NudgeToward(te, 0, 1, 0, 100);
                    s.Diplomacy.SetRelation(a, b, tr, te);
                }
            }
        }

        private static int NudgeToward(int v, int target, int step, int min, int max)
        {
            if (v < target) v = Math.Min(target, v + step);
            else if (v > target) v = Math.Max(target, v - step);
            return v < min ? min : (v > max ? max : v);
        }
    }
}
