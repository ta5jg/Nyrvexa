using System;
using System.Collections.Generic;

namespace Nyrvexa.Simulation.Events
{
    public sealed class InMemoryEventBus : IGameEventSink
    {
        private readonly List<IGameEvent> _history = new List<IGameEvent>();
        public IReadOnlyList<IGameEvent> History => _history;

        public void Publish(IGameEvent evt)
        {
            if (evt == null) return;
            _history.Add(evt);
        }

        public void Clear() => _history.Clear();
    }
}
