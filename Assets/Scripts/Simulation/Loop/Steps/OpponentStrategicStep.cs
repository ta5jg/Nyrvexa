/* =============================================================================
 * File:           Assets/Scripts/Simulation/Loop/Steps/OpponentStrategicStep.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.7
 * 
 * Description:
 *   İnsan (F0) dışındaki fraksiyonlara IStrategicBrain ile kuyruğa ekleme
 *   (ApplyCommands öncesi).
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using Nyrvexa.Simulation.AI;
using Nyrvexa.Simulation.Architecture;
using Nyrvexa.Simulation.Commands;
using Nyrvexa.Simulation.Loop;

namespace Nyrvexa.Simulation.Loop.Steps
{
    /// <summary> İnsan (F0) dışındaki fraksiyonlara IStrategicBrain ile kuyruğa ekleme (ApplyCommands öncesi). </summary>
    public sealed class OpponentStrategicStep : IPipelineStep
    {
        private readonly IStrategicBrain _brain;
        private readonly CommandJournal _journal;

        public string Name => TurnPhasesCatalog.OpponentStrategic;

        public OpponentStrategicStep(IStrategicBrain brain, CommandJournal journal)
        {
            _brain = brain;
            _journal = journal;
        }

        public void Execute(TurnContext ctx)
        {
            if (_brain == null || _journal == null) return;
            var s = ctx.State;
            for (int f = 1; f < s.Factions.Count; f++)
                _brain.QueueStrategicActions(f, s, _journal, ctx);
        }
    }
}
