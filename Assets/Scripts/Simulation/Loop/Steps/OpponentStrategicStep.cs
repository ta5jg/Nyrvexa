// Copyright (c) 2026 Irfan Gedik. All rights reserved.
// This file is part of Nyrvexa. Licensing: see LICENSE in the repository root (TBD).
//


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
