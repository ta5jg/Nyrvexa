/* =============================================================================
 * File:           Assets/Scripts/Simulation/Persistence/CommandJournalWire.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.7
 * 
 * Description:
 *   M1: bekleyen komut kuyruğunun satırı.
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using System;
using System.Globalization;
using System.Text;
using Nyrvexa.Simulation.Commands;
using Nyrvexa.Simulation.Core;
using Nyrvexa.Simulation.State;

namespace Nyrvexa.Simulation.Persistence
{
    /// <summary> M1: bekleyen komut kuyruğunun <see cref="PlainTextGameSerializer"/> satırı. </summary>
    public static class CommandJournalWire
    {
        public const string Key = "journ=";

        public static void Append(StringBuilder sb, GameStateRoot state)
        {
            if (state?.CommandJournal == null)
            {
                sb.Append(Key).Append("empty");
                return;
            }
            var buf = state.CommandJournal.PeekBuffer();
            if (buf.Count == 0)
            {
                sb.Append(Key).Append("empty");
                return;
            }
            sb.Append(Key);
            for (int i = 0; i < buf.Count; i++)
            {
                if (i > 0) sb.Append(';');
                sb.Append(EncodeOne(buf[i]));
            }
        }

        public static void ParseLineIntoState(string line, GameStateRoot s)
        {
            if (s?.CommandJournal == null || string.IsNullOrEmpty(line) || !line.StartsWith(Key, StringComparison.Ordinal)) return;
            var body = line.Substring(Key.Length);
            s.CommandJournal.ClearBuffer();
            if (string.IsNullOrEmpty(body) || body == "empty") return;
            var inv = CultureInfo.InvariantCulture;
            var parts = body.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < parts.Length; i++)
            {
                var one = parts[i].Trim();
                if (one.Length < 1) continue;
                var p = one.Split(new[] { '|' }, StringSplitOptions.None);
                if (p.Length < 2) continue;
                var kind = p[0].Trim();
                if (string.Equals(kind, "M", StringComparison.Ordinal) && p.Length == 5)
                {
                    int fac = int.Parse(p[1], inv);
                    var uid = new EntityId(uint.Parse(p[2], inv), uint.Parse(p[3], inv));
                    int tgt = int.Parse(p[4], inv);
                    s.CommandJournal.Enqueue(new MoveUnitCommand(fac, uid, tgt));
                }
                else if (string.Equals(kind, "R", StringComparison.Ordinal) && p.Length == 3)
                {
                    int fac = int.Parse(p[1], inv);
                    s.CommandJournal.Enqueue(new SetResearchCommand(fac, new DefId(Unescape(p[2]))));
                }
                else if (string.Equals(kind, "F", StringComparison.Ordinal) && p.Length == 4)
                {
                    int fac = int.Parse(p[1], inv);
                    int t = int.Parse(p[2], inv);
                    s.CommandJournal.Enqueue(new FoundCityCommand(fac, t, Unescape(p[3])));
                }
                else if (string.Equals(kind, "N", StringComparison.Ordinal) && p.Length == 2)
                {
                    int fac = int.Parse(p[1], inv);
                    s.CommandJournal.Enqueue(new NoOpCommand(fac));
                }
            }
        }

        private static string EncodeOne(IGameCommand c)
        {
            switch (c)
            {
                case MoveUnitCommand m:
                    return "M|"
                        + m.IssuerFactionIndex + "|" + m.Unit.Index + "|" + m.Unit.Generation + "|" + m.TargetTileIndex;
                case SetResearchCommand r:
                    return "R|" + r.IssuerFactionIndex + "|" + Escape(r.Tech.Key);
                case FoundCityCommand f:
                    return "F|" + f.IssuerFactionIndex + "|" + f.TileIndex + "|" + Escape(f.CityName ?? "X");
                case NoOpCommand n:
                    return "N|" + n.IssuerFactionIndex;
                default:
                    return "?|0";
            }
        }

        private static string Escape(string s)
        {
            if (string.IsNullOrEmpty(s)) return s ?? string.Empty;
            return s
                .Replace("%", "%25", StringComparison.Ordinal)
                .Replace("|", "%7C", StringComparison.Ordinal)
                .Replace(";", "%3B", StringComparison.Ordinal);
        }

        private static string Unescape(string s) =>
            s
                .Replace("%3B", ";", StringComparison.Ordinal)
                .Replace("%7C", "|", StringComparison.Ordinal)
                .Replace("%25", "%", StringComparison.Ordinal);
    }
}
