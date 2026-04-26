// Copyright (c) 2026 Irfan Gedik. All rights reserved.
// This file is part of Nyrvexa. Licensing: see LICENSE in the repository root (TBD).
//


namespace Nyrvexa.Simulation.Hex
{
    /// <summary> Pointy, odd-r offset → axial (redblobgames). </summary>
    public static class OffsetOddRPointyGrid
    {
        public static AxialCoord OffsetToAxial(int col, int row)
        {
            int q = col - (row - (row & 1)) / 2;
            return new AxialCoord(q, row);
        }

        public static void AxialToOffset(in AxialCoord a, out int col, out int row)
        {
            row = a.R;
            col = a.Q + (a.R - (a.R & 1)) / 2;
        }

        public static int FlatIndex(int col, int row, int width) => row * width + col;

        public static void Decompose(int index, int width, out int col, out int row)
        {
            col = index % width;
            row = index / width;
        }
    }
}
