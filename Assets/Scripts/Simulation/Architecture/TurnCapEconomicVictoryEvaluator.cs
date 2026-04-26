using Nyrvexa.Simulation.Core;
using Nyrvexa.Simulation.State;

namespace Nyrvexa.Simulation.Architecture
{
    /// <summary>
    /// Victory aşamasında: <c>turnIndex &gt;= endAtTurnIndex</c> iken
    /// skor = şehir * Wc + yiy*Wf + ür*Wp. Eşit toplam: daha çok şehir, sonra düşük fraksiyon indeksi.
    /// </summary>
    public sealed class TurnCapEconomicVictoryEvaluator : IVictoryConditionEvaluator
    {
        private static readonly DefId ResFood = new("res.food");
        private static readonly DefId ResProd = new("res.prod");

        private readonly long _endAtTurnIndex;
        private readonly int _weightCity;
        private readonly int _weightFood;
        private readonly int _weightProd;

        public TurnCapEconomicVictoryEvaluator(
            long endAtTurnIndex = 8,
            int weightCity = 1000,
            int weightFood = 1,
            int weightProd = 2)
        {
            _endAtTurnIndex = endAtTurnIndex;
            _weightCity = weightCity;
            _weightFood = weightFood;
            _weightProd = weightProd;
        }

        public int EvaluateVictor(GameStateRoot state, long turnIndex)
        {
            if (state == null) return -1;
            if (state.Header.DeclaredVictorFactionIndex >= 0) return state.Header.DeclaredVictorFactionIndex;
            if (turnIndex < _endAtTurnIndex) return -1;
            if (state.Factions.Count < 1) return -1;

            int bestF = 0;
            var bestTot = long.MinValue;
            var bestC = -1;
            for (int f = 0; f < state.Factions.Count; f++)
            {
                var c = state.Factions[f].CityIds.Count;
                state.Factions[f].ResourceStock.TryGetValue(ResFood, out var food);
                state.Factions[f].ResourceStock.TryGetValue(ResProd, out var pr);
                var tot = c * (long)_weightCity + food * _weightFood + pr * _weightProd;
                if (tot > bestTot)
                {
                    bestF = f;
                    bestTot = tot;
                    bestC = c;
                }
                else if (tot == bestTot)
                {
                    if (c > bestC)
                    {
                        bestF = f;
                        bestC = c;
                    }
                    else if (c == bestC && f < bestF) bestF = f;
                }
            }
            return bestF;
        }

        public long GetWeightedScore(GameStateRoot state, int factionIndex)
        {
            if (state == null || factionIndex < 0 || factionIndex >= state.Factions.Count) return 0;
            var c = state.Factions[factionIndex].CityIds.Count;
            state.Factions[factionIndex].ResourceStock.TryGetValue(ResFood, out var food);
            state.Factions[factionIndex].ResourceStock.TryGetValue(ResProd, out var pr);
            return c * (long)_weightCity + food * _weightFood + pr * _weightProd;
        }
    }
}
