// Copyright (c) 2026 Irfan Gedik. All rights reserved.
// This file is part of Nyrvexa. Licensing: see LICENSE in the repository root (TBD).
//


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

        /// <summary> Geçmişte bu türden son olay (yoksa false). </summary>
        public bool TryGetLastOfType<T>(out T evt) where T : class, IGameEvent
        {
            for (int i = _history.Count - 1; i >= 0; i--)
            {
                if (_history[i] is T t)
                {
                    evt = t;
                    return true;
                }
            }
            evt = default!;
            return false;
        }
    }
}
