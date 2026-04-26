// Copyright (c) 2026 Irfan Gedik. All rights reserved.
// This file is part of Nyrvexa. Licensing: see LICENSE in the repository root (TBD).
//


using System;
using System.Collections.Generic;

namespace Nyrvexa.Simulation.Hex
{
    /// <summary>
    /// Pointy-topped hex, eksenel koordinat (cube ile uyumlu: S = -Q - R).
    /// </summary>
    public readonly struct AxialCoord : IEquatable<AxialCoord>
    {
        public readonly int Q;
        public readonly int R;

        public AxialCoord(int q, int r)
        {
            Q = q;
            R = r;
        }

        public int S => -Q - R;

        public int DistanceTo(in AxialCoord b) =>
            (Math.Abs(Q - b.Q) + Math.Abs(S - b.S) + Math.Abs(R - b.R)) / 2;

        public void GetNeighbors(Span<AxialCoord> buffer6)
        {
            if (buffer6.Length < 6) throw new ArgumentException("En az 6 yuva gerekir.");
            buffer6[0] = new AxialCoord(Q + 1, R);
            buffer6[1] = new AxialCoord(Q + 1, R - 1);
            buffer6[2] = new AxialCoord(Q, R - 1);
            buffer6[3] = new AxialCoord(Q - 1, R);
            buffer6[4] = new AxialCoord(Q - 1, R + 1);
            buffer6[5] = new AxialCoord(Q, R + 1);
        }

        public static IReadOnlyList<AxialCoord> NeighborList(in AxialCoord a)
        {
            var b = new AxialCoord[6];
            a.GetNeighbors(b);
            return b;
        }

        public static bool operator ==(AxialCoord a, AxialCoord b) => a.Q == b.Q && a.R == b.R;
        public static bool operator !=(AxialCoord a, AxialCoord b) => !(a == b);
        public bool Equals(AxialCoord other) => Q == other.Q && R == other.R;
        public override bool Equals(object obj) => obj is AxialCoord c && Equals(c);
        public override int GetHashCode() => (Q * 397) ^ R;
    }
}
