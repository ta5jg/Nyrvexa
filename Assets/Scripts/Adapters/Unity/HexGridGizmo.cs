/* =============================================================================
 * File:           Assets/Scripts/Adapters/Unity/HexGridGizmo.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.7
 * 
 * Description:
 *   Simülasyondaki düzlem ızgırasını gizmo telleriyle gösterir.
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using Nyrvexa.Simulation.World;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Nyrvexa.Adapters
{
    /// <summary> Simülasyondaki düzlem ızgırasını gizmo telleriyle gösterir. </summary>
    [ExecuteAlways]
    public sealed class HexGridGizmo : MonoBehaviour
    {
        [Header("Izgara")]
        [Min(1)] [SerializeField] private int _width = WorldMapDefaults.MapWidth;
        [Min(1)] [SerializeField] private int _height = WorldMapDefaults.MapHeight;
        [Tooltip("Açıkken: aynı objede (veya atanmış) GameSessionHost alan genişliği / yüksekliği kullanılır.")]
        [SerializeField] private bool _syncMapFromSessionHost;
        [SerializeField] private GameSessionHost _sessionHost;
        [Min(0.01f)] [SerializeField] private float _hexRadius = 1f;
        [SerializeField] private float _yOffset;
        [SerializeField] private Color _lineColor = new(0.35f, 0.6f, 0.95f, 0.45f);
        [SerializeField] private bool _useHighlightFlatIndex;
        [Tooltip("Play’de: keşifçi bulunduğu hücreyi _highlightFlatIndex üzerine yazar (gizmo sarı halka).")]
        [SerializeField] private bool _syncScoutTileHighlightInPlay = true;
        [SerializeField] [Min(0)] private int _highlightFlatIndex = 23;
        [SerializeField] private Color _highlightColor = new(1f, 0.75f, 0.2f, 0.85f);
        [Header("Geliştirme (Scene görünümü)")]
        [Tooltip("Oyun (player) build’de etkisi yok; Scene’de düz index görünür.")]
        [SerializeField] private bool _drawEditorTileIndexLabels;
        [SerializeField] private Color _indexLabelColor = new(0.2f, 0.2f, 0.2f, 0.85f);
        [SerializeField] private int _indexLabelFontSize = 10;

        public int GridWidth => ResolveWidth();
        public int GridHeight => ResolveHeight();
        public float HexRadiusWorld => _hexRadius;

        private int ResolveWidth()
        {
            if (_syncMapFromSessionHost)
            {
                var h = _sessionHost != null ? _sessionHost : GetComponent<GameSessionHost>();
                if (h != null) return h.MapWidth;
            }
            return _width;
        }

        private int ResolveHeight()
        {
            if (_syncMapFromSessionHost)
            {
                var h = _sessionHost != null ? _sessionHost : GetComponent<GameSessionHost>();
                if (h != null) return h.MapHeight;
            }
            return _height;
        }

        private void OnDrawGizmos() => DrawGrid();

        private void OnDrawGizmosSelected() => DrawGrid();

        private void DrawGrid()
        {
            int w = ResolveWidth();
            int h = ResolveHeight();
            if (w < 1 || h < 1) return;
            int hi = _highlightFlatIndex;
            var sh = _sessionHost != null ? _sessionHost : GetComponent<GameSessionHost>();
            var haveScoutPlay = false;
            if (Application.isPlaying && _syncScoutTileHighlightInPlay && sh != null
                && sh.TryGetScoutCurrentTileIndex(out var scFlat))
            {
                hi = scFlat;
                haveScoutPlay = true;
            }
            var showCellHi = _useHighlightFlatIndex || haveScoutPlay;
            var o = transform.position;
            for (int row = 0; row < h; row++)
            for (int col = 0; col < w; col++)
            {
                var i = row * w + col;
                var b = HexLayoutUnity.ColRowToWorldPointy(_hexRadius, col, row, 0f);
                var center = new Vector3(b.x + o.x, o.y + _yOffset, b.z + o.z);
                var c = showCellHi && i == hi ? _highlightColor : _lineColor;
                Gizmos.color = c;
                DrawPointyHexWires(center, _hexRadius);
#if UNITY_EDITOR
                if (_drawEditorTileIndexLabels)
                {
                    var s = new GUIStyle { fontSize = _indexLabelFontSize, normal = { textColor = _indexLabelColor } };
                    Handles.Label(center + new Vector3(0, 0.05f, 0), i.ToString(), s);
                }
#endif
            }
        }

        private static void DrawPointyHexWires(Vector3 center, float size)
        {
            for (int i = 0; i < 6; i++)
            {
                var a0 = (60f * i + 30f) * Mathf.Deg2Rad;
                var a1 = (60f * (i + 1) + 30f) * Mathf.Deg2Rad;
                var p0 = new Vector3(center.x + size * Mathf.Cos(a0), center.y, center.z + size * Mathf.Sin(a0));
                var p1 = new Vector3(center.x + size * Mathf.Cos(a1), center.y, center.z + size * Mathf.Sin(a1));
                Gizmos.DrawLine(p0, p1);
            }
        }
    }
}
