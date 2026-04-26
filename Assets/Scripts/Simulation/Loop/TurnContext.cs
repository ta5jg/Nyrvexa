using Nyrvexa.Simulation.Core;
using Nyrvexa.Simulation.State;

namespace Nyrvexa.Simulation.Loop
{
    public sealed class TurnContext
    {
        public GameStateRoot State { get; }
        private DeterministicRng _rng;

        /// <summary> Tüm aşamalar aynı durumu paylaşır; deterministik oynat için struct kopya değil. </summary>
        public ref DeterministicRng Rng => ref _rng;

        public TurnContext(GameStateRoot state, DeterministicRng rng)
        {
            State = state;
            _rng = rng;
        }

        public void SyncRngFromHeader() => _rng.Reseed(State.Header.RngState64);

        public void PersistRngToHeader() => State.Header.RngState64 = _rng.State;
    }
}
