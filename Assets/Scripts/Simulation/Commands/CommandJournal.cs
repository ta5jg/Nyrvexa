using System;
using System.Collections.Generic;
using Nyrvexa.Simulation.Loop;

namespace Nyrvexa.Simulation.Commands
{
    public sealed class CommandJournal
    {
        private readonly List<IGameCommand> _turnBuffer = new List<IGameCommand>();

        /// <summary> Bu tur sonunda uygulanacak, henüz Apply edilmemiş komut sayısı. </summary>
        public int PendingCount => _turnBuffer.Count;

        public IReadOnlyList<IGameCommand> PeekBuffer() => _turnBuffer;

        /// <summary> Kuyruktan koşul sağlayan girdileri sondan başa kaldırır. </summary>
        public int RemoveAllWhere(Predicate<IGameCommand> match)
        {
            if (match == null) throw new ArgumentNullException(nameof(match));
            int removed = 0;
            for (int i = _turnBuffer.Count - 1; i >= 0; i--)
            {
                if (match(_turnBuffer[i]))
                {
                    _turnBuffer.RemoveAt(i);
                    removed++;
                }
            }
            return removed;
        }

        public void Enqueue(IGameCommand cmd)
        {
            if (cmd != null) _turnBuffer.Add(cmd);
        }

        public void ClearBuffer() => _turnBuffer.Clear();

        public int ApplyAll(TurnContext ctx)
        {
            int n = 0;
            for (int i = 0; i < _turnBuffer.Count; i++)
            {
                _turnBuffer[i].Apply(ctx);
                n++;
            }
            _turnBuffer.Clear();
            return n;
        }
    }
}
