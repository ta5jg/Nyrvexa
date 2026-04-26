namespace Nyrvexa.Simulation.Events
{
    public interface IGameEventSink
    {
        void Publish(IGameEvent evt);
    }
}
