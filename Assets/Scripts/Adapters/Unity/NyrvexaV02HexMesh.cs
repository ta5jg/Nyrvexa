/* =============================================================================
 * File:           Assets/Scripts/Adapters/Unity/NyrvexaV02HexMesh.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.9
 * 
 * Description:
 *   Birim circumradius (pointy) ile paylaşılan heks ağı (yatay XZ, Y=0).
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using UnityEngine;

namespace Nyrvexa.Adapters
{
    /// <summary> Birim circumradius (pointy) ile paylaşılan heks ağı (yatay XZ, Y=0). </summary>
    public static class NyrvexaV02HexMesh
    {
        private static Mesh _shared;

        public static Mesh GetOrCreatePointyUnit()
        {
            if (_shared != null) return _shared;
            const float r = 1f;
            const int n = 6;
            var verts = new Vector3[n + 1];
            verts[0] = Vector3.zero;
            for (int i = 0; i < n; i++)
            {
                var a = (60f * i + 30f) * Mathf.Deg2Rad;
                verts[i + 1] = new Vector3(r * Mathf.Cos(a), 0f, r * Mathf.Sin(a));
            }
            var tris = new int[n * 3];
            for (int i = 0; i < n; i++)
            {
                tris[i * 3] = 0;
                tris[i * 3 + 1] = 1 + i;
                tris[i * 3 + 2] = 1 + (i + 1) % n;
            }
            _shared = new Mesh { name = "NyrvexaV02PointyHex" };
            _shared.vertices = verts;
            _shared.triangles = tris;
            _shared.RecalculateNormals();
            _shared.RecalculateBounds();
            return _shared;
        }
    }
}
