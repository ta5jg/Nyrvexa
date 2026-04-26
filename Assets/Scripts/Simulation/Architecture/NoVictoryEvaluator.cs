using Nyrvexa.Simulation.State;

namespace Nyrvexa.Simulation.Architecture
{
    /// <summary> Kazanma yok; ilerde bilim/diplo/alan şartları buraya asılır. </summary>
    public sealed class NoVictoryEvaluator : IVictoryConditionEvaluator
    {
        public int EvaluateVictor(GameStateRoot state, long turnIndex) => -1;
    }
}
