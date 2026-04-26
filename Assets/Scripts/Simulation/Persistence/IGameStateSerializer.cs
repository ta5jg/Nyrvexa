using Nyrvexa.Simulation.State;

namespace Nyrvexa.Simulation.Persistence
{
    public interface IGameStateSerializer
    {
        string SerializeGameState(GameStateRoot state);
        GameStateRoot DeserializeGameState(string json);
    }
}
