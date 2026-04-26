// Copyright (c) 2026 Irfan Gedik. All rights reserved.
// This file is part of Nyrvexa. Licensing: see LICENSE in the repository root (TBD).
//


using Nyrvexa.Simulation.Commands;
using Nyrvexa.Simulation.Loop;
using Nyrvexa.Simulation.State;

namespace Nyrvexa.Simulation.AI
{
    public sealed class UtilityStrategicStub : IStrategicBrain
    {
        public void QueueStrategicActions(int factionIndex, GameStateRoot state, CommandJournal journal, TurnContext ctx)
        {
        }
    }
}
