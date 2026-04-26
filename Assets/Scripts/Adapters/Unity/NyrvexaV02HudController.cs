using System;
using Nyrvexa.Simulation.Commands;
using Nyrvexa.Simulation.Core;
using Nyrvexa.Simulation.Research;
using Nyrvexa.Simulation.State;
using Nyrvexa.Simulation.World;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Nyrvexa.Adapters
{
    /// <summary> v0.2: tur, kaynak metinleri, Sonraki tur. Canvas yoksa çalma anında gerekirse oluşturur. </summary>
    [DefaultExecutionOrder(20)]
    public sealed class NyrvexaV02HudController : MonoBehaviour
    {
        private static readonly DefId ResFood = new("res.food");
        private static readonly DefId ResProd = new("res.prod");

        [SerializeField] private GameSessionHost _session;
        [SerializeField] private bool _createEventSystemIfMissing = true;
        [SerializeField] private bool _buildCanvasIfNoChildCanvas = true;
        [SerializeField] private NyrvexaV02HexBoardRenderer _hex;
        [SerializeField] private NyrvexaV02MapChrome _mapChrome;

        private Text _turnText;
        private Text _queueText;
        private Text _scoutText;
        private Text _f0Text;
        private Text _f1Text;
        private Text _victoryText;
        private Text _rumorText;
        private Text _fowHintText;
        private Text _rivalText;
        private Button _btnNextTurn;
        private Button _btnClearQueue;
        private Button _btnResearchSurvey;
        private Button _btnResearchAgri;

        private void Awake()
        {
            if (_createEventSystemIfMissing && UnityEngine.Object.FindFirstObjectByType<EventSystem>() == null)
            {
                var es = new GameObject("EventSystem");
                es.AddComponent<EventSystem>();
                es.AddComponent<StandaloneInputModule>();
            }
            if (_buildCanvasIfNoChildCanvas && GetComponentInChildren<Canvas>(true) == null)
                BuildUi();
        }

        private void BuildUi()
        {
            var root = new GameObject("NyrvexaV02_HUD_Canvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            root.transform.SetParent(transform, false);
            var canvas = root.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            var scaler = root.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080);
            var panel = new GameObject("Panel", typeof(Image));
            panel.transform.SetParent(root.transform, false);
            var prt = panel.GetComponent<RectTransform>();
            prt.anchorMin = new Vector2(0, 1);
            prt.anchorMax = new Vector2(0, 1);
            prt.pivot = new Vector2(0, 1);
            prt.anchoredPosition = new Vector2(16, -12);
            prt.sizeDelta = new Vector2(560, 420);
            panel.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
            _turnText = NewText("Turn", new Vector2(8, -8), new Vector2(400, 26), 18, TextAnchor.UpperLeft, root.transform, prt);
            _queueText = NewText("Queue", new Vector2(8, -36), new Vector2(520, 22), 13, TextAnchor.UpperLeft, root.transform, prt);
            _scoutText = NewText("Scout", new Vector2(8, -58), new Vector2(400, 24), 14, TextAnchor.UpperLeft, root.transform, prt);
            _rivalText = NewText("Rival", new Vector2(8, -80), new Vector2(400, 22), 13, TextAnchor.UpperLeft, root.transform, prt);
            _f0Text = NewText("F0", new Vector2(8, -106), new Vector2(400, 26), 16, TextAnchor.UpperLeft, root.transform, prt);
            _f1Text = NewText("F1", new Vector2(8, -134), new Vector2(400, 26), 16, TextAnchor.UpperLeft, root.transform, prt);
            _victoryText = NewText("Victory", new Vector2(8, -162), new Vector2(400, 24), 14, TextAnchor.UpperLeft, root.transform, prt);
            _rumorText = NewText("Rumor", new Vector2(8, -188), new Vector2(520, 22), 12, TextAnchor.UpperLeft, root.transform, prt);
            _rumorText.color = new Color(0.95f, 0.85f, 0.7f, 1f);
            _fowHintText = NewText("FowHint", new Vector2(8, -214), new Vector2(400, 20), 12, TextAnchor.UpperLeft, root.transform, prt);
            _fowHintText.text = "Sis: F0=sen, F1=rakip (hex + minimap).";
            _fowHintText.color = new Color(0.85f, 0.9f, 1f, 1f);
            var clearGo = new GameObject("ClearQueue", typeof(Button), typeof(Image));
            clearGo.transform.SetParent(panel.transform, false);
            var crt = clearGo.GetComponent<RectTransform>();
            crt.anchorMin = new Vector2(0, 0);
            crt.anchorMax = new Vector2(0, 0);
            crt.pivot = new Vector2(0, 0);
            crt.anchoredPosition = new Vector2(8, 8);
            crt.sizeDelta = new Vector2(132, 32);
            clearGo.GetComponent<Image>().color = new Color(0.4f, 0.2f, 0.2f, 0.88f);
            var cbt = NewText("Kuyruğu boşat", Vector2.zero, crt.sizeDelta, 13, TextAnchor.MiddleCenter, clearGo.transform, crt);
            cbt.raycastTarget = false;
            _btnClearQueue = clearGo.GetComponent<Button>();
            _btnClearQueue.onClick.AddListener(() =>
            {
                if (_session == null) return;
                _session.V02_ClearCommandQueue();
            });
            var f0Go = new GameObject("FowF0", typeof(Button), typeof(Image));
            f0Go.transform.SetParent(panel.transform, false);
            var f0rt = f0Go.GetComponent<RectTransform>();
            f0rt.anchorMin = new Vector2(0, 0);
            f0rt.anchorMax = new Vector2(0, 0);
            f0rt.pivot = new Vector2(0, 0);
            f0rt.anchoredPosition = new Vector2(8, 44);
            f0rt.sizeDelta = new Vector2(64, 28);
            f0Go.GetComponent<Image>().color = new Color(0.25f, 0.35f, 0.55f, 0.92f);
            var f0l = NewText("F0", Vector2.zero, f0rt.sizeDelta, 13, TextAnchor.MiddleCenter, f0Go.transform, f0rt);
            f0l.raycastTarget = false;
            f0Go.GetComponent<Button>().onClick.AddListener(() => SetFowView(0));
            var f1Go = new GameObject("FowF1", typeof(Button), typeof(Image));
            f1Go.transform.SetParent(panel.transform, false);
            var f1rt = f1Go.GetComponent<RectTransform>();
            f1rt.anchorMin = new Vector2(0, 0);
            f1rt.anchorMax = new Vector2(0, 0);
            f1rt.pivot = new Vector2(0, 0);
            f1rt.anchoredPosition = new Vector2(80, 44);
            f1rt.sizeDelta = new Vector2(64, 28);
            f1Go.GetComponent<Image>().color = new Color(0.45f, 0.28f, 0.25f, 0.92f);
            var f1l = NewText("F1", Vector2.zero, f1rt.sizeDelta, 13, TextAnchor.MiddleCenter, f1Go.transform, f1rt);
            f1l.raycastTarget = false;
            f1Go.GetComponent<Button>().onClick.AddListener(() => SetFowView(1));
            _btnResearchSurvey = NewTinyButton("ResSurvey", "Araş: Hazine", new Vector2(8, 76), new Vector2(118, 28), panel.transform,
                new Color(0.2f, 0.3f, 0.4f, 0.9f), () => EnqueueRes(ResearchM1Catalog.SurveyM1));
            _btnResearchAgri = NewTinyButton("ResAgri", "Araş: Tarım", new Vector2(130, 76), new Vector2(118, 28), panel.transform,
                new Color(0.22f, 0.35f, 0.22f, 0.9f), () => EnqueueRes(ResearchM1Catalog.AgricultureM1));
            var btnGo = new GameObject("NextTurn", typeof(Button), typeof(Image));
            btnGo.transform.SetParent(panel.transform, false);
            var brt = btnGo.GetComponent<RectTransform>();
            brt.anchorMin = new Vector2(1, 0);
            brt.anchorMax = new Vector2(1, 0);
            brt.pivot = new Vector2(1, 0);
            brt.anchoredPosition = new Vector2(-8, 8);
            brt.sizeDelta = new Vector2(150, 36);
            btnGo.GetComponent<Image>().color = new Color(0.2f, 0.45f, 0.35f, 0.95f);
            var bt = NewText("Sonraki tur", Vector2.zero, brt.sizeDelta, 15, TextAnchor.MiddleCenter, btnGo.transform, brt);
            bt.raycastTarget = false;
            _btnNextTurn = btnGo.GetComponent<Button>();
            _btnNextTurn.onClick.AddListener(() =>
            {
                if (_session == null) return;
                _session.V02_AdvanceOneTurn();
            });
        }

        private void EnqueueRes(DefId tech)
        {
            if (_session == null) return;
            _session.V02_TryQueueSetResearch(tech, 0);
        }

        private static Button NewTinyButton(
            string goName, string label, Vector2 pos, Vector2 size, Transform panelParent, Color col, Action onClick)
        {
            var go = new GameObject(goName, typeof(Button), typeof(Image));
            go.transform.SetParent(panelParent, false);
            var rt = go.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(0, 0);
            rt.anchorMax = new Vector2(0, 0);
            rt.pivot = new Vector2(0, 0);
            rt.anchoredPosition = pos;
            rt.sizeDelta = size;
            go.GetComponent<Image>().color = col;
            var t = NewText("Txt", Vector2.zero, size, 12, TextAnchor.MiddleCenter, go.transform, rt);
            t.text = label;
            t.raycastTarget = false;
            var b = go.GetComponent<Button>();
            b.onClick.AddListener(() => onClick());
            return b;
        }

        private static Text NewText(
            string name,
            Vector2 anchored,
            Vector2 size,
            int font,
            TextAnchor align,
            Transform underCanvas,
            RectTransform localParent = null
        )
        {
            var go = new GameObject(name, typeof(Text));
            go.transform.SetParent(localParent != null ? localParent : underCanvas, false);
            var rt = go.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(0, 1);
            rt.anchorMax = new Vector2(0, 1);
            rt.pivot = new Vector2(0, 1);
            rt.anchoredPosition = anchored;
            rt.sizeDelta = size;
            var t = go.GetComponent<Text>();
            t.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            if (t.font == null) t.font = Font.CreateDynamicFontFromOSFont("Arial", 14);
            t.fontSize = font;
            t.color = Color.white;
            t.alignment = align;
            t.horizontalOverflow = HorizontalWrapMode.Overflow;
            t.verticalOverflow = VerticalWrapMode.Truncate;
            return t;
        }

        private void Start()
        {
            if (_session == null) _session = GetComponent<GameSessionHost>();
            if (_session == null) _session = UnityEngine.Object.FindFirstObjectByType<GameSessionHost>();
            if (_session == null) return;
            _session.EnsureBootstrapped();
            _session.StateViewChanged += OnView;
            Refresh();
        }

        private void OnDestroy()
        {
            if (_session != null) _session.StateViewChanged -= OnView;
        }

        private void OnView() => Refresh();

        private static string BuildQueueText(GameStateRoot s)
        {
            var buf = s.CommandJournal.PeekBuffer();
            if (buf.Count == 0) return "Kuyruk: —";
            var b = new System.Text.StringBuilder("Kuyruk: ");
            for (int i = 0; i < buf.Count; i++)
            {
                if (i > 0) b.Append(" · ");
                var c = buf[i];
                switch (c)
                {
                    case MoveUnitCommand m: b.Append("Hareket → ").Append(m.TargetTileIndex); break;
                    case FoundCityCommand f: b.Append("Kur @").Append(f.TileIndex); break;
                    case SetResearchCommand r: b.Append("Araş: ").Append(ShortTechKey(r.Tech)); break;
                    case NoOpCommand: b.Append("—"); break;
                    default: b.Append("?"); break;
                }
            }
            var t = b.ToString();
            return t.Length > 140 ? t.Substring(0, 137) + "…" : t;
        }

        private void SetFowView(int faction)
        {
            if (_hex == null) _hex = UnityEngine.Object.FindFirstObjectByType<NyrvexaV02HexBoardRenderer>();
            if (_mapChrome == null) _mapChrome = UnityEngine.Object.FindFirstObjectByType<NyrvexaV02MapChrome>();
            _hex?.SetViewAsFaction(faction);
            _mapChrome?.SetViewAsFaction(faction);
        }

        private void Refresh()
        {
            if (_turnText == null) return;
            if (_session == null || _session.State == null) return;
            var s = _session.State;
            if (_session != null)
            {
                var lim = _session.TurnsToSimulateLimit;
                var n = _session.V02SimulatedCount;
                _turnText.text = "Tur: T" + s.Header.TurnIndex + "  (adım " + n + "/" + lim + ")";
            }
            else
                _turnText.text = "Tur: T" + s.Header.TurnIndex;
            if (_queueText != null) _queueText.text = BuildQueueText(s);
            if (_scoutText != null)
            {
                if (_session != null && _session.TryGetScoutCurrentTileIndex(out var cur)
                    && s.World.Tiles != null && cur >= 0 && cur < s.World.Tiles.Length)
                {
                    var bio = BiomeM1Names.TryGet(s.World.Tiles[cur].BiomeId);
                    if (_session.TryGetPendingScoutMoveTarget(out var tgt))
                        _scoutText.text = "Keşif (F0): düz " + cur + " " + bio + "  →  " + tgt;
                    else
                        _scoutText.text = "Keşif (F0): düz " + cur + " " + bio;
                }
                else
                    _scoutText.text = "Keşif (F0): —";
            }
            if (_rivalText != null)
            {
                if (_session != null && _session.TryGetRivalScoutCurrentTileIndex(out var rcur)
                    && s.World.Tiles != null && rcur >= 0 && rcur < s.World.Tiles.Length)
                {
                    var rb = BiomeM1Names.TryGet(s.World.Tiles[rcur].BiomeId);
                    if (_session.TryGetPendingRivalScoutMoveTarget(out var rt))
                        _rivalText.text = "Rakip (F1): düz " + rcur + " " + rb + "  →  " + rt;
                    else
                        _rivalText.text = "Rakip (F1): düz " + rcur + " " + rb;
                }
                else
                    _rivalText.text = "Rakip (F1): —";
            }
            FmtFaction(_f0Text, 0, s);
            FmtFaction(_f1Text, 1, s);
            if (_victoryText != null)
            {
                var w = s.Header.DeclaredVictorFactionIndex;
                if (w < 0)
                    _victoryText.text = "Kazanan: — (T≥8 şehir+yiy+ür)";
                else
                    _victoryText.text = "Kazanan: F" + w + " — oyun bitti; Sonraki tur yok";
            }
            if (_rumorText != null)
            {
                if (_session != null && _session.TryGetLastRumorRoll(out var rCode, out var rTurn))
                    _rumorText.text = "Söylenti: T" + rTurn + "  #" + rCode.ToString("X8", System.Globalization.CultureInfo.InvariantCulture);
                else
                    _rumorText.text = "Söylenti: henüz yok (nadir, RNG)";
            }
            var notOver = s.Header.DeclaredVictorFactionIndex < 0;
            if (_btnNextTurn != null)
            {
                var step = _session != null && _session.V02StepByStep;
                _btnNextTurn.interactable = notOver && _session != null && (!step || _session.V02_CanStepMore());
            }
            if (_btnClearQueue != null) _btnClearQueue.interactable = notOver;
            if (_btnResearchSurvey != null && _btnResearchAgri != null && s.Factions.Count > 0)
            {
                var step = _session != null && _session.V02StepByStep;
                var f0 = s.Factions[0];
                void Upd(Button btn, DefId tech)
                {
                    var can = notOver && step && ResearchM1Catalog.IsValidResearchTarget(tech, f0.UnlockedTechnologies);
                    var isCur = f0.ActiveResearchId == tech;
                    btn.interactable = can && !isCur;
                }
                Upd(_btnResearchSurvey, ResearchM1Catalog.SurveyM1);
                Upd(_btnResearchAgri, ResearchM1Catalog.AgricultureM1);
            }
        }

        private static void FmtFaction(Text line, int fac, GameStateRoot g)
        {
            if (line == null) return;
            if (fac >= g.Factions.Count)
            {
                line.text = "—";
                return;
            }
            var f = g.Factions[fac];
            f.ResourceStock.TryGetValue(ResFood, out var food);
            f.ResourceStock.TryGetValue(ResProd, out var pr);
            var ar = FmtArLine(f);
            line.text = $"{f.DebugName}  yiy={food}  ür={pr}  şehir={f.CityIds.Count}  ABP={f.StoredResearchPoints}  {ar}";
        }

        private static string FmtArLine(FactionState f)
        {
            if (f.UnlockedTechnologies.Count >= ResearchM1Catalog.Ordered.Length) return "Araş: (M1 tamam)";
            if (!f.ActiveResearchId.IsValid) return "Araş: sırada";
            if (!ResearchM1Catalog.TryGetCost(f.ActiveResearchId, out var c)) return "Araş: ?";
            return "Araş: " + ShortTechKey(f.ActiveResearchId) + " " + f.ActiveResearchProgress + "/" + c;
        }

        private static string ShortTechKey(DefId id)
        {
            var k = id.Key ?? string.Empty;
            if (k.Length > 5 && k.StartsWith("tech.", StringComparison.Ordinal)) return k.Substring(5);
            return k.Length > 16 ? k.Substring(0, 16) : k;
        }
    }
}
