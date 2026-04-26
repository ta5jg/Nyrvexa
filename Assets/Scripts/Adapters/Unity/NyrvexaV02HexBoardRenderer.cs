/* =============================================================================
 * File:           Assets/Scripts/Adapters/Unity/NyrvexaV02HexBoardRenderer.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.7
 * 
 * Description:
 *   v0.2: oyun durumundaki hücreleri (F0 FOW + sahip) renklendirir.
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using Nyrvexa.Simulation.Core;
using Nyrvexa.Simulation.State;
using Nyrvexa.Simulation.World;
using UnityEngine;
using static Nyrvexa.Simulation.World.FogOfWarState;

namespace Nyrvexa.Adapters
{
    /// <summary> v0.2: oyun durumundaki hücreleri (F0 FOW + sahip) renklendirir. </summary>
    [DefaultExecutionOrder(10)]
    public sealed class NyrvexaV02HexBoardRenderer : MonoBehaviour
    {
        [SerializeField] private GameSessionHost _session;
        [Tooltip("0 = oyuncu fraksiyonu (FOW).")]
        [SerializeField] [Range(0, 3)] private int _viewAsFaction;
        [SerializeField] private float _hexRadius = 1f;
        [SerializeField] private float _yPerCellJitter = 0.0004f;
        [SerializeField] private Color _unexplored = new(0.08f, 0.08f, 0.1f, 1f);
        [SerializeField] private Color _neutral = new(0.25f, 0.45f, 0.28f, 1f);
        [SerializeField] private Color _faction0 = new(0.3f, 0.5f, 0.9f, 1f);
        [SerializeField] private Color _faction1 = new(0.85f, 0.4f, 0.35f, 1f);
        [Header("Biyom (M1)")]
        [Tooltip("Dolu: palet bu asset’ten (hex+minimap’te aynı asset’i kullan). Boş: aşağıdaki dizi kullanılır.")]
        [SerializeField] private BiomeVisualConfig _biomeConfig;
        [Tooltip("Açık: hücre BiomeId (0=varsayılan, 1–6 prosedürel) arka plan tonu; sahip renkleri hafif karışır.")]
        [SerializeField] private bool _tintByBiome = true;
        [SerializeField] [Range(0f, 1f)] private float _biomeFactionMix = 0.42f;
        [Tooltip("Yalnız _biomeConfig boşken: indeks 0..5 = biyom 1..6. Uzunluk ≠ 6 olursa M1 varsayılanları doldurulur.")]
        [SerializeField] private Color[] _biomeColors;
        [SerializeField] private string _unlitColorShader = "Unlit/Color";
        [Header("v0.2 hareket ipucu (keşifçi)")]
        [Tooltip("Açık: keşifçinin 6 yönündeki, yürüyebileceği hücrelere hafif vurgu.")]
        [SerializeField] private bool _showScoutMoveHints = true;
        [SerializeField] private Color _validMoveHint = new(0.35f, 0.9f, 0.5f, 1f);
        [SerializeField] [Range(0f, 1f)] private float _exploredMoveHintBlend = 0.42f;
        [SerializeField] [Range(0f, 1f)] private float _fogMoveHintBlend = 0.2f;
        [Header("Rakip keşif (F1)")]
        [Tooltip("Açık: mevcut sis görünümünde (F0/F1) görülen hücrede rakip keşifçi varsa turuncu vurgu.")]
        [SerializeField] private bool _showRivalScoutOnTile = true;
        [SerializeField] private Color _rivalScoutTileTint = new(1f, 0.45f, 0.2f, 1f);
        [SerializeField] [Range(0f, 1f)] private float _rivalScoutTileBlend = 0.55f;

        private Transform _tilesRoot;
        private readonly System.Collections.Generic.List<MeshRenderer> _renderers = new();
        private Material _mat;
        private MaterialPropertyBlock _block;

        public int ViewAsFaction => _viewAsFaction;

        public void SetViewAsFaction(int factionIndex)
        {
            _viewAsFaction = Mathf.Clamp(factionIndex, 0, 3);
            if (Application.isPlaying) RefreshColors();
        }

        private void Awake() => _block = new MaterialPropertyBlock();

        private void OnState() => RefreshColors();

        private void Start()
        {
            if (_session == null) _session = GetComponent<GameSessionHost>();
            if (_session == null) _session = UnityEngine.Object.FindFirstObjectByType<GameSessionHost>();
            if (_session == null) return;
            if (TryGetComponent<HexGridGizmo>(out var giz)) _hexRadius = giz.HexRadiusWorld;
            _session.StateViewChanged += OnState;
            _session.EnsureBootstrapped();
            BuildIfNeeded();
            RefreshColors();
        }

        private void OnDestroy()
        {
            if (_session != null) _session.StateViewChanged -= OnState;
        }

        private void OnValidate()
        {
            BiomeViewColors.EnsureBiomePalette(ref _biomeColors);
            if (Application.isPlaying && _renderers.Count > 0) RefreshColors();
        }

        private void BuildIfNeeded()
        {
            if (_session == null || _session.State == null) return;
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                var ch = transform.GetChild(i);
                if (ch.name != "V02_Tiles") continue;
                if (Application.isPlaying) Object.Destroy(ch.gameObject);
                else Object.DestroyImmediate(ch.gameObject);
            }
            _renderers.Clear();
            var s = new GameObject("V02_Tiles");
            s.transform.SetParent(transform, false);
            _tilesRoot = s.transform;
            var w = _session.MapWidth;
            var h = _session.MapHeight;
            var mesh = NyrvexaV02HexMesh.GetOrCreatePointyUnit();
            var sh = Shader.Find(_unlitColorShader);
            if (sh == null) sh = Shader.Find("Sprites/Default");
            if (sh != null) _mat = new Material(sh);
            if (_mat == null) return;
            for (int row = 0; row < h; row++)
            for (int col = 0; col < w; col++)
            {
                var flat = row * w + col;
                var p = HexLayoutUnity.ColRowToWorldPointy(_hexRadius, col, row, 0f);
                var go = new GameObject("t" + flat);
                go.transform.SetParent(_tilesRoot, false);
                go.transform.localPosition = new Vector3(p.x, flat * _yPerCellJitter, p.z);
                go.transform.localScale = new Vector3(_hexRadius, _hexRadius, _hexRadius);
                var mf = go.AddComponent<MeshFilter>();
                mf.sharedMesh = mesh;
                var mr = go.AddComponent<MeshRenderer>();
                mr.sharedMaterial = _mat;
                _renderers.Add(mr);
            }
        }

        public void RefreshColors()
        {
            var palette1To6 = BiomeViewColors.ResolveBiomePalette(_biomeConfig, ref _biomeColors);
            if (_block == null) _block = new MaterialPropertyBlock();
            if (_session == null || _session.State == null) return;
            if (_renderers.Count == 0) BuildIfNeeded();
            if (_renderers.Count == 0) return;
            if (_mat == null) return;
            var g = _session.State;
            var w = g.World;
            if (w.Tiles == null) return;
            var nTiles = w.Tiles.Length;
            var neighbor = _showScoutMoveHints
                ? BuildScoutAdjacencyMask(g, w, _session.ScoutUnitId, nTiles)
                : null;
            for (int i = 0; i < _renderers.Count; i++)
            {
                if (i >= w.Tiles.Length) break;
                ref readonly var t = ref w.Tiles[i];
                Color c;
                if (!IsExploredBy(t, _viewAsFaction)) c = _unexplored;
                else
                {
                    c = BiomeViewColors.ExploredCellSurface(
                        t,
                        _tintByBiome,
                        _biomeFactionMix,
                        _neutral,
                        _faction0,
                        _faction1,
                        palette1To6);
                }
                if (neighbor != null && i < neighbor.Length && neighbor[i])
                {
                    if (!IsExploredBy(t, _viewAsFaction))
                        c = Color.Lerp(c, _validMoveHint, _fogMoveHintBlend);
                    else
                        c = Color.Lerp(c, _validMoveHint, _exploredMoveHintBlend);
                }
                if (_showRivalScoutOnTile
                    && IsExploredBy(t, _viewAsFaction)
                    && g.UnitOnTile.TryGetValue(i, out var onTile)
                    && onTile == _session.RivalScoutUnitId)
                {
                    c = Color.Lerp(c, _rivalScoutTileTint, _rivalScoutTileBlend);
                }
                _renderers[i].GetPropertyBlock(_block);
                if (_mat.HasProperty("_Color")) _block.SetColor("_Color", c);
                else if (_mat.HasProperty("_BaseColor")) _block.SetColor("_BaseColor", c);
                _renderers[i].SetPropertyBlock(_block);
            }
        }

        private static bool[] BuildScoutAdjacencyMask(GameStateRoot g, WorldMapState w, EntityId scoutId, int len)
        {
            var a = new bool[len];
            if (!g.Units.TryGetValue(scoutId, out var u)) return a;
            for (int d = 0; d < 6; d++)
            {
                int ni = w.GetNeighborIndex(u.TileIndex, d);
                if (ni >= 0 && ni < len) a[ni] = true;
            }
            return a;
        }
    }
}
