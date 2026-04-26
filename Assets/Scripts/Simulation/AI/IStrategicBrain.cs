using Nyrvexa.Simulation.Commands;
using Nyrvexa.Simulation.Loop;
using Nyrvexa.Simulation.State;

namespace Nyrvexa.Simulation.AI
{
    /// <summary> Fraksiyon başına yüksek seviye hedef ve komut üretir (Utility + bütçe). </summary>
    public interface IStrategicBrain
    {
        void QueueStrategicActions(int factionIndex, GameStateRoot state, CommandJournal journal, TurnContext ctx);
    }
}
