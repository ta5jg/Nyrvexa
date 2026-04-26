namespace Nyrvexa.Simulation.Loop
{
    public interface IPipelineStep
    {
        string Name { get; }
        void Execute(TurnContext ctx);
    }
}
