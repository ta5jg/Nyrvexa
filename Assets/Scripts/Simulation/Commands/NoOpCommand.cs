using Nyrvexa.Simulation.Loop;

namespace Nyrvexa.Simulation.Commands
{
    public sealed class NoOpCommand : IGameCommand
    {
        public int IssuerFactionIndex { get; }
        public NoOpCommand(int issuerFactionIndex) => IssuerFactionIndex = issuerFactionIndex;
        public void Apply(TurnContext ctx) { }
    }
}
