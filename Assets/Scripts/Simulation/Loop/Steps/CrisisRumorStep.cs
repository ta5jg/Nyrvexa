// Copyright (c) 2026 Irfan Gedik. All rights reserved.
// This file is part of Nyrvexa. Licensing: see LICENSE in the repository root (TBD).
//


using Nyrvexa.Simulation.Loop;
using Nyrvexa.Simulation.Events;

namespace Nyrvexa.Simulation.Loop.Steps
{
    /// <summary> M1: düşük oranlı söylenti olayı — RNG ileri; kayıtla tekrarlanabilir. </summary>
    public sealed class CrisisRumorStep : IPipelineStep
    {
        private readonly IGameEventSink _bus;

        public string Name => "EventsAndCrises";

        public CrisisRumorStep(IGameEventSink bus) => _bus = bus;

        public void Execute(TurnContext ctx)
        {
            if (_bus == null) return;
            if ((ctx.Rng.NextU64() % 100ul) < 2ul)
            {
                int code = ctx.Rng.NextInt(1 << 8);
                _bus.Publish(new RumorRollEvent(code, ctx.State.Header.TurnIndex));
            }
        }
    }
}
