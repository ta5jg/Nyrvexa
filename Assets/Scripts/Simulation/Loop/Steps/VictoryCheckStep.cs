/* =============================================================================
 * File:           Assets/Scripts/Simulation/Loop/Steps/VictoryCheckStep.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.10
 * 
 * Description:
 *   Tur sonu zafer; kazanan ilan, olay, header.
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using Nyrvexa.Simulation.Architecture;
using Nyrvexa.Simulation.Events;
using Nyrvexa.Simulation.Loop;

namespace Nyrvexa.Simulation.Loop.Steps
{
    /// <summary> Tur sonu zafer; kazanan ilan, olay, header. </summary>
    public sealed class VictoryCheckStep : IPipelineStep
    {
        private readonly IVictoryConditionEvaluator _eval;
        private readonly IGameEventSink _events;

        public string Name => TurnPhasesCatalog.VictoryCheck;

        public VictoryCheckStep(IVictoryConditionEvaluator eval, IGameEventSink events)
        {
            _eval = eval;
            _events = events;
        }

        public void Execute(TurnContext ctx)
        {
            if (_eval == null) return;
            var s = ctx.State;
            if (s.Header.DeclaredVictorFactionIndex >= 0) return;
            var t = s.Header.TurnIndex;
            var v = _eval.EvaluateVictor(s, t);
            if (v < 0) return;
            s.Header.DeclaredVictorFactionIndex = v;
            long sc = 0;
            if (_eval is TurnCapEconomicVictoryEvaluator eco) sc = eco.GetWeightedScore(s, v);
            _events?.Publish(new VictoryDeclaredEvent(v, t, sc));
        }
    }
}
