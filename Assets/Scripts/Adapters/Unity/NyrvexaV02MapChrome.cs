/* =============================================================================
 * File:           Assets/Scripts/Adapters/Unity/NyrvexaV02MapChrome.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.10
 * 
 * Description:
 *   v0.2: sağ üstte çerçeveli minimap (F0 FOW/owner), sağ altta kamera:
 *   haritaya dön / yer imi.
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using Nyrvexa.Simulation.World;
using UnityEngine;
using UnityEngine.UI;
using static Nyrvexa.Simulation.World.FogOfWarState;

namespace Nyrvexa.Adapters
{
    /// <summary> v0.2: sağ üstte çerçeveli minimap (F0 FOW/owner), sağ altta kamera: haritaya dön / yer imi. </summary>
    [DefaultExecutionOrder(35)]
    public sealed class NyrvexaV02MapChrome : MonoBehaviour
    {
        [SerializeField] private GameSessionHost _session;
        [SerializeField] private HexBoardCameraRig _cameraRig;
        [Tooltip("0 = F0 FOW, ana haritayla aynı.")]
        [SerializeField] [Range(0, 3)] private int _viewAsFaction;
        [SerializeField] private Color _unexplored = new(0.08f, 0.08f, 0.1f, 1f);
        [SerializeField] private Color _neutral = new(0.25f, 0.45f, 0.28f, 1f);
        [SerializeField] private Color _faction0 = new(0.3f, 0.5f, 0.9f, 1f);
        [SerializeField] private Color _faction1 = new(0.85f, 0.4f, 0.35f, 1f);
        [Header("Biyom (M1) — hex ile aynı mantık")]
        [Tooltip("Dolu: palet bu asset (hex’teki BiomeVisualConfig ile aynı olabilir). Boş: aşağıdaki 6 renk.")]
        [SerializeField] private BiomeVisualConfig _biomeConfig;
        [Tooltip("Açık: zemin biyom + fraksiyon karışımı. Kapalı: yalnız sahip renkleri.")]
        [SerializeField] private bool _tintByBiome = true;
        [SerializeField] [Range(0f, 1f)] private float _biomeFactionMix = 0.42f;
        [Tooltip("İndis 0..5 = biyom 1..6. Hex board’daki 6 rengi burada da aynen kullan.")]
        [SerializeField] private Color[] _biomeColors;
        [SerializeField] private Color _scoutMark = new(1f, 0.95f, 0.2f, 1f);
        [SerializeField] private Color _rivalScoutMark = new(1f, 0.45f, 0.2f, 1f);
        [SerializeField] private float _displayWidth = 200f;
        [SerializeField] private float _displayHeight = 120f;
        [SerializeField] private Color _frameColor = new(0.12f, 0.12f, 0.15f, 0.95f);
        [SerializeField] private Color _frameBevel = new(0.4f, 0.4f, 0.45f, 0.9f);

        private RawImage _mapRaw;
        private Text _minimapTitle;
        private Button _btnRestore;
        private Texture2D _tex;
        private int _lastW = -1;
        private int _lastH = -1;

        private void Awake()
        {
            if (_session == null) _session = GetComponent<GameSessionHost>();
            if (_cameraRig == null) _cameraRig = GetComponent<HexBoardCameraRig>();
        }

        private void OnValidate()
        {
            if (Application.isPlaying && _mapRaw != null) RedrawMinimap();
        }

        private void OnDestroy()
        {
            if (_session != null) _session.StateViewChanged -= OnView;
            if (_tex != null) Object.Destroy(_tex);
        }

        public int ViewAsFaction => _viewAsFaction;

        public void SetViewAsFaction(int factionIndex)
        {
            _viewAsFaction = Mathf.Clamp(factionIndex, 0, 3);
            UpdateMinimapTitleText();
            if (Application.isPlaying) RedrawMinimap();
        }

        private void UpdateMinimapTitleText()
        {
            if (_minimapTitle != null)
                _minimapTitle.text = " Minimap (F" + _viewAsFaction + ") — sis";
        }

        private void OnView() => RedrawMinimap();

        private void Start()
        {
            if (_session == null) _session = UnityEngine.Object.FindFirstObjectByType<GameSessionHost>();
            if (_cameraRig == null) _cameraRig = UnityEngine.Object.FindFirstObjectByType<HexBoardCameraRig>();
            if (_session == null) return;
            _session.EnsureBootstrapped();
            _session.StateViewChanged += OnView;
            BuildUi();
            RedrawMinimap();
        }

        private void BuildUi()
        {
            var canvas = GetComponentInChildren<Canvas>(true);
            if (canvas == null) return;
            if (canvas.transform.Find("NyrvexaV02_MapChrome") != null) return;
            var root = new GameObject("NyrvexaV02_MapChrome", typeof(RectTransform));
            root.transform.SetParent(canvas.transform, false);
            var rootRt = root.GetComponent<RectTransform>();
            rootRt.anchorMin = new Vector2(1, 1);
            rootRt.anchorMax = new Vector2(1, 1);
            rootRt.pivot = new Vector2(1, 1);
            rootRt.anchoredPosition = new Vector2(-12, -12);
            rootRt.sizeDelta = new Vector2(224, 248);

            var frameOuter = new GameObject("MapFrame", typeof(Image));
            frameOuter.transform.SetParent(root.transform, false);
            var foRt = frameOuter.GetComponent<RectTransform>();
            foRt.anchorMin = Vector2.zero;
            foRt.anchorMax = Vector2.one;
            foRt.sizeDelta = Vector2.zero;
            frameOuter.GetComponent<Image>().color = _frameBevel;
            _ = frameOuter;

            var frameMid = new GameObject("MapFrameFill", typeof(Image));
            frameMid.transform.SetParent(frameOuter.transform, false);
            var fmRt = frameMid.GetComponent<RectTransform>();
            fmRt.anchorMin = new Vector2(0, 0.3f);
            fmRt.anchorMax = new Vector2(1, 1);
            fmRt.offsetMin = new Vector2(1, 1);
            fmRt.offsetMax = new Vector2(-1, -1);
            frameMid.GetComponent<Image>().color = _frameColor;

            var mapHolder = new GameObject("MapTex", typeof(RawImage));
            mapHolder.transform.SetParent(frameMid.transform, false);
            var mRt = mapHolder.GetComponent<RectTransform>();
            mRt.anchorMin = new Vector2(0.5f, 0.5f);
            mRt.anchorMax = new Vector2(0.5f, 0.5f);
            mRt.pivot = new Vector2(0.5f, 0.5f);
            mRt.sizeDelta = new Vector2(_displayWidth, _displayHeight);
            mRt.anchoredPosition = Vector2.zero;
            _mapRaw = mapHolder.GetComponent<RawImage>();
            _mapRaw.uvRect = new Rect(0, 0, 1, 1);

            var cap = new GameObject("Title", typeof(Text));
            cap.transform.SetParent(root.transform, false);
            var cr = cap.GetComponent<RectTransform>();
            cr.anchorMin = new Vector2(0, 0.8f);
            cr.anchorMax = new Vector2(1, 1);
            cr.sizeDelta = Vector2.zero;
            cr.anchoredPosition = Vector2.zero;
            var ct = cap.GetComponent<Text>();
            ct.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf")
                      ?? Font.CreateDynamicFontFromOSFont("Arial", 14);
            ct.fontSize = 14;
            ct.color = Color.white;
            ct.alignment = TextAnchor.UpperLeft;
            _minimapTitle = ct;
            UpdateMinimapTitleText();

            var btnRoot = new GameObject("ViewButtons", typeof(Image));
            btnRoot.transform.SetParent(root.transform, false);
            var bRt = btnRoot.GetComponent<RectTransform>();
            bRt.anchorMin = new Vector2(0, 0);
            bRt.anchorMax = new Vector2(1, 0.32f);
            bRt.sizeDelta = Vector2.zero;
            bRt.anchoredPosition = Vector2.zero;
            btnRoot.GetComponent<Image>().color = new Color(0, 0, 0, 0.35f);

            var y = 4f;
            AddBtn(btnRoot.transform, y, "Haritaya dön", () =>
            {
                if (_cameraRig != null) _cameraRig.ResetViewToMap();
            });
            y += 32f;
            AddBtn(btnRoot.transform, y, "Gör. kaydet", OnSaveView);
            y += 32f;
            _btnRestore = AddBtn(btnRoot.transform, y, "Kayıt aç", OnRestore);
            if (_btnRestore != null) _btnRestore.interactable = _cameraRig != null && _cameraRig.HasViewBookmark;
        }

        private void OnSaveView()
        {
            if (_cameraRig == null) return;
            _cameraRig.SaveViewBookmark();
            if (_btnRestore != null) _btnRestore.interactable = _cameraRig.HasViewBookmark;
        }

        private void OnRestore()
        {
            if (_cameraRig == null) return;
            if (!_cameraRig.HasViewBookmark) return;
            _cameraRig.RestoreViewBookmark();
        }

        private static Button AddBtn(Transform parent, float y, string label, UnityEngine.Events.UnityAction onClick)
        {
            var go = new GameObject("Btn_" + label, typeof(Button), typeof(Image));
            go.transform.SetParent(parent, false);
            var rt = go.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(0, 0);
            rt.anchorMax = new Vector2(1, 0);
            rt.pivot = new Vector2(0.5f, 0);
            rt.anchoredPosition = new Vector2(0, y);
            rt.sizeDelta = new Vector2(-8, 28);
            go.GetComponent<Image>().color = new Color(0.2f, 0.3f, 0.4f, 0.9f);
            var tgo = new GameObject("L", typeof(Text));
            tgo.transform.SetParent(go.transform, false);
            var tr = tgo.GetComponent<RectTransform>();
            tr.anchorMin = Vector2.zero;
            tr.anchorMax = Vector2.one;
            tr.sizeDelta = Vector2.zero;
            var t = tgo.GetComponent<Text>();
            t.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf")
                    ?? Font.CreateDynamicFontFromOSFont("Arial", 12);
            t.fontSize = 12;
            t.color = Color.white;
            t.alignment = TextAnchor.MiddleCenter;
            t.text = label;
            t.raycastTarget = false;
            var b = go.GetComponent<Button>();
            b.onClick.AddListener(() => onClick?.Invoke());
            return b;
        }

        private void RedrawMinimap()
        {
            var pal = BiomeViewColors.ResolveBiomePalette(_biomeConfig, ref _biomeColors);
            if (_mapRaw == null || _session == null || _session.State == null) return;
            var s = _session.State;
            var w = s.World.Width;
            var h = s.World.Height;
            if (w < 1 || h < 1 || s.World.Tiles == null) return;
            if (_tex == null || w != _lastW || h != _lastH)
            {
                if (_tex != null) Object.Destroy(_tex);
                _tex = new Texture2D(w, h, TextureFormat.RGBA32, false) { name = "NyrvexaV02_Mini" };
                _tex.filterMode = FilterMode.Point;
                _tex.wrapMode = TextureWrapMode.Clamp;
                _lastW = w;
                _lastH = h;
            }
            var t = s.World.Tiles;
            for (int row = 0; row < h; row++)
            for (int col = 0; col < w; col++)
            {
                int i = row * w + col;
                _tex.SetPixel(col, h - 1 - row, CellColor(t[i], pal));
            }
            if (_session.TryGetScoutCurrentTileIndex(out var sf) && sf >= 0 && sf < t.Length)
                PaintScoutHalo(_tex, w, h, sc: sf, _scoutMark);
            if (_session.TryGetRivalScoutCurrentTileIndex(out var rf) && rf >= 0 && rf < t.Length)
                PaintScoutHalo(_tex, w, h, sc: rf, _rivalScoutMark);
            _tex.Apply(false, false);
            _mapRaw.texture = _tex;
        }

        private Color CellColor(in TileCell c, Color[] palette1To6)
        {
            if (!IsExploredBy(c, _viewAsFaction)) return _unexplored;
            return BiomeViewColors.ExploredCellSurface(
                c,
                _tintByBiome,
                _biomeFactionMix,
                _neutral,
                _faction0,
                _faction1,
                palette1To6);
        }

        private static void PaintScoutHalo(Texture2D tex, int w, int h, int sc, Color mark)
        {
            int sr = sc / w;
            int ccol = sc % w;
            for (int dy = -1; dy <= 1; dy++)
            for (int dx = -1; dx <= 1; dx++)
            {
                int pxc = ccol + dx;
                int pyr = sr + dy;
                if (pxc < 0 || pxc >= w || pyr < 0 || pyr >= h) continue;
                if (dy == 0 && dx == 0) { tex.SetPixel(pxc, h - 1 - pyr, mark); continue; }
                tex.SetPixel(pxc, h - 1 - pyr, Color.Lerp(
                    tex.GetPixel(pxc, h - 1 - pyr), mark, 0.5f));
            }
        }
    }
}
