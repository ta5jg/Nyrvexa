/* =============================================================================
 * File:           Assets/Scripts/Simulation/Loop/TurnPipeline.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.9
 * 
 * Description:
 *   TurnPipeline — Nyrvexa modülü (ayrıntı kaynakta).
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using System;
using System.Collections.Generic;
using Nyrvexa.Simulation.Commands;
using Nyrvexa.Simulation.Architecture;
using Nyrvexa.Simulation.AI;
using Nyrvexa.Simulation.Core;
using Nyrvexa.Simulation.Events;
using Nyrvexa.Simulation.Loop.Steps;
using Nyrvexa.Simulation.State;

namespace Nyrvexa.Simulation.Loop
{
    public sealed class TurnPipeline
    {
        private readonly IReadOnlyList<IPipelineStep> _steps;
        private readonly IGameEventSink _events;

        public TurnPipeline(IReadOnlyList<IPipelineStep> steps, IGameEventSink events)
        {
            _steps = steps ?? throw new ArgumentNullException(nameof(steps));
            _events = events ?? throw new ArgumentNullException(nameof(events));
        }

        public void RunSingleTurn(GameStateRoot state, DeterministicRng rng)
        {
            var ctx = new TurnContext(state, rng);
            ctx.SyncRngFromHeader();

            _events.Publish(new TurnPhaseEvent("TurnStart", state.Header.TurnIndex));

            for (int i = 0; i < _steps.Count; i++)
            {
                _steps[i].Execute(ctx);
            }

            state.Header.AdvanceTurn();
            ctx.PersistRngToHeader();
            _events.Publish(new TurnPhaseEvent("TurnEnd", state.Header.TurnIndex));
        }

        public static TurnPipeline CreateDefault(IGameEventSink events, CommandJournal journal)
        {
            var economicVictory = new TurnCapEconomicVictoryEvaluator(
                endAtTurnIndex: 8,
                weightCity: 1000,
                weightFood: 1,
                weightProd: 2);
            // Sıra: yürüyen MP → (isteğe olay) → rakip kuyruğa yazar → komutlar uygulanır
            // (kurma/hareket) → ekonomi/üretim/ABP → keşif → ... → zafer. Böylece ilk turda da şehir varken üretim işler.
            var steps = new IPipelineStep[]
            {
                new UpkeepUnitMovementStep(),
                new CrisisRumorStep(events),
                new OpponentStrategicStep(new SimpleRivalScoutBrain(), journal),
                new ApplyCommandsStep(journal),
                new EconomyStep(),
                new ProductionStep(),
                new CityPopulationGrowthStep(),
                new ResearchTickStep(),
                new SightAndExplorationStep(),
                new ScoutContactRumorStep(events),
                new UnitIndexReconcileStep(),
                new StubStep("Combat"),
                new StubStep("Espionage"),
                new StubStep("CulturePressure"),
                new DiplomacyDriftStep(),
                new VictoryCheckStep(economicVictory, events)
            };
            return new TurnPipeline(steps, events);
        }

        private sealed class StubStep : IPipelineStep
        {
            public string Name { get; }
            public StubStep(string name) => Name = name;
            public void Execute(TurnContext ctx) { }
        }

        private sealed class ApplyCommandsStep : IPipelineStep
        {
            private readonly CommandJournal _journal;
            public string Name => "ApplyCommands";
            public ApplyCommandsStep(CommandJournal journal) => _journal = journal;

            public void Execute(TurnContext ctx)
            {
                _journal.ApplyAll(ctx);
            }
        }
    }
}
