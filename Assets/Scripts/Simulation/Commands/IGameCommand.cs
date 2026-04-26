using Nyrvexa.Simulation.Loop;

namespace Nyrvexa.Simulation.Commands
{
    public interface IGameCommand
    {
        int IssuerFactionIndex { get; }
        void Apply(TurnContext ctx);
    }
}
