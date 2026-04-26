/* =============================================================================
 * File:           Assets/Scripts/Adapters/Unity/GameSessionHost.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.10
 * 
 * Description:
 *   GameSessionHost — Nyrvexa modülü (ayrıntı kaynakta).
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using System.IO;
using Nyrvexa.Simulation.Architecture;
using Nyrvexa.Simulation.Commands;
using Nyrvexa.Simulation.Core;
using Nyrvexa.Simulation.Data;
using Nyrvexa.Simulation.Diplomacy;
using Nyrvexa.Simulation.Events;
using Nyrvexa.Simulation.Loop;
using Nyrvexa.Simulation.Persistence;
using Nyrvexa.Simulation.State;
using Nyrvexa.Simulation.World;
using Nyrvexa.Simulation.World.Generation;
using Nyrvexa.Simulation.Research;
using UnityEngine;

namespace Nyrvexa.Adapters
{
    [DefaultExecutionOrder(-20)]
    public sealed class GameSessionHost : MonoBehaviour
    {
        [Header("Dünya")]
        [Min(1)] [SerializeField] private int _mapWidth = WorldMapDefaults.MapWidth;
        [Min(1)] [SerializeField] private int _mapHeight = WorldMapDefaults.MapHeight;

        [SerializeField] private ulong _worldSeed = 0xBADC0FFEE0UL;
        [Tooltip("Adım/Run modunda bu kadar `RunSingleTurn` (kazanma T≥8 için 12 önerilir).")]
        [SerializeField] private int _turnsToSimulate = 12;
        [SerializeField] private bool _logEachTurn = true;
        [SerializeField] private bool _runOnStart;
        [SerializeField] private bool _testSaveRoundTrip = true;
        [Tooltip("Assets/StreamingAssets/Defs/ altı; yoksa veya hatalıysa gömülü CreateDefault katalog kullanılır.")]
        [SerializeField] private string _unitDefsFileName = "units.example.json";
        [SerializeField] private bool _loadUnitDefsFromStreaming = true;
        [Header("v0.2 — oynanır dilim")]
        [Tooltip("Açık: Play’de yalnızca Bootstrap; turlar \"Sonraki tur\" ile. Kapalı: klasik _runOnStart senaryo.")]
        [SerializeField] private bool _v02StepByStepMode;
        [Tooltip("Kuyruk / tur sonu sonrası harita+HUD (isteğe).")]
        [SerializeField] private bool _v02PublishStateChanged = true;
        [Tooltip("Açık: aynı turda yeni tık, keşifçi için önceki bekleyen hareketi siler (tek hedef). Kapalı: üst üste hareket kuyruklanır (çoğu ikinci hareket tur sonunda yürünmez).")]
        [SerializeField] private bool _v02ReplaceScoutMoveInQueue = true;
        [Tooltip("Açık: adım modunda Space = sonraki tur.")]
        [SerializeField] private bool _v02SpaceToAdvanceTurn = true;
        [Tooltip("Yeni oyun: dünya tohumunu karıştır (deterministik; tekrar oynatılabilir fark).")]
        [SerializeField] private bool _v02NewGameRerollSeed = true;
        [Header("Biyom (M1)")]
        [Tooltip("Açık: Allocate sonrası seed’e bağlı deterministik biyom (1..6) boyar; kapat: tüm hücre defaultBiome (0) kalır.")]
        [SerializeField] private bool _paintProceduralBiomes = true;

        private GameStateRoot _state;
        private InMemoryEventBus _bus;
        private TurnPipeline _pipeline;
        private DeterministicRng _rng;
        private EntityId _scoutId;
        private EntityId _rivalScoutId;
        private int _v02ScenarioTurnsSimulated;

        public int MapWidth => _mapWidth;
        public int MapHeight => _mapHeight;
        public GameStateRoot State => _state;
        public EntityId ScoutUnitId => _scoutId;
        public EntityId RivalScoutUnitId => _rivalScoutId;
        public int TurnsToSimulateLimit => _turnsToSimulate;
        public bool V02StepByStep => _v02StepByStepMode;
        public int V02SimulatedCount => _v02ScenarioTurnsSimulated;

        /// <summary> Olay otobüsünde <see cref="RumorRollEvent"/> toplam adedi (Crisis + sınır fısıltı). </summary>
        public int CountRumorRollEventsInBus()
        {
            if (_bus == null) return 0;
            int n = 0;
            for (int i = 0; i < _bus.History.Count; i++)
            {
                if (_bus.History[i] is RumorRollEvent) n++;
            }
            return n;
        }

        /// <summary> Adım modunda bir sonraki turu oynatılabilir mi (kazanan yok, tur kotası dolmadı). </summary>
        public bool V02_CanStepMore()
        {
            if (!_v02StepByStepMode || _state == null) return false;
            if (_state.Header.DeclaredVictorFactionIndex >= 0) return false;
            if (_v02ScenarioTurnsSimulated >= _turnsToSimulate) return false;
            return true;
        }

        public event System.Action StateViewChanged;

        /// <summary> Keşifçinin o an bulunduğu düz hücre indeksi. </summary>
        public bool TryGetScoutCurrentTileIndex(out int flat)
        {
            flat = -1;
            if (_state == null) return false;
            if (!_state.Units.TryGetValue(_scoutId, out var u)) return false;
            flat = u.TileIndex;
            return true;
        }

        /// <summary> Kuyruktaki (bu tur) keşifçi hareketi; yoksa false. </summary>
        public bool TryGetPendingScoutMoveTarget(out int targetFlat) =>
            TryGetPendingMoveForUnit(_scoutId, out targetFlat);

        /// <summary> Olay otobüsünde son söylenti (M1: <see cref="RumorRollEvent"/>), yoksa false. </summary>
        public bool TryGetLastRumorRoll(out int code, out long turn)
        {
            code = 0;
            turn = 0;
            if (_bus == null) return false;
            if (!_bus.TryGetLastOfType<RumorRollEvent>(out var e)) return false;
            code = e.Code;
            turn = e.Turn;
            return true;
        }

        /// <summary> Rakip (F1) keşifçi düz indeks. </summary>
        public bool TryGetRivalScoutCurrentTileIndex(out int flat)
        {
            flat = -1;
            if (_state == null) return false;
            if (!_state.Units.TryGetValue(_rivalScoutId, out var u)) return false;
            flat = u.TileIndex;
            return true;
        }

        /// <summary> Bu tur kuyruğunda F1 keşif hedefi. </summary>
        public bool TryGetPendingRivalScoutMoveTarget(out int targetFlat) =>
            TryGetPendingMoveForUnit(_rivalScoutId, out targetFlat);

        private bool TryGetPendingMoveForUnit(EntityId unit, out int targetFlat)
        {
            targetFlat = -1;
            if (_state == null) return false;
            foreach (var c in _state.CommandJournal.PeekBuffer())
            {
                if (c is MoveUnitCommand m && m.Unit == unit)
                {
                    targetFlat = m.TargetTileIndex;
                    return true;
                }
            }
            return false;
        }

        private void Update()
        {
            if (!_v02StepByStepMode || !_v02SpaceToAdvanceTurn) return;
            if (_state == null) return;
            if (!Input.GetKeyDown(KeyCode.Space)) return;
            if (!V02_CanStepMore()) return;
            V02_AdvanceOneTurn();
        }

        private void Start()
        {
            if (_v02StepByStepMode)
            {
                EnsureBootstrapped();
                return;
            }
            if (_runOnStart) RunScenario();
        }

        public void EnsureBootstrapped()
        {
            if (_state != null) return;
            Bootstrap();
            if (_state != null) NotifyStateView();
        }

        /// <summary> v0.2: oyunu sıfırla (aynı harita boyu; isteğe tohum değişir) ve Bootstrap. Yalnız adım modunda. </summary>
        public void V02_StartNewSession()
        {
            if (!_v02StepByStepMode)
            {
                Debug.LogWarning("Nyrvexa: Yeni oyun yalnız v0.2 adım modunda. Inspector’da V02 Step By Step aç.");
                return;
            }
            if (_v02NewGameRerollSeed) _worldSeed = MixWorldSeed(_worldSeed);
            _state = null;
            _bus = null;
            _pipeline = null;
            _v02ScenarioTurnsSimulated = 0;
            EnsureBootstrapped();
            if (_logEachTurn) Debug.Log("Nyrvexa: yeni oturum. tohum=0x" + _worldSeed.ToString("X") + "  adım=0");
            NotifyStateView();
        }

        private static ulong MixWorldSeed(ulong s) => (s * 0x9E3779B97F4A7C15UL) ^ 0xBADC0D11DEADBEEFUL;

        [ContextMenu("Nyrvexa v0.2/Yeni oyun (oturum sıfırla)")]
        private void CtxV02StartNewSession() => V02_StartNewSession();

        /// <summary> NYRVEXA_V1 metnini persistentDataPath altına (nyrvexa_last_save.txt) yazar; paylaşım / yedek. </summary>
        [ContextMenu("Nyrvexa v0.2/Kaydı diske yaz (nyrvexa_last_save.txt)")]
        public void V02_ExportSaveToFile()
        {
            if (_state == null) { Debug.LogWarning("Nyrvexa: kayıt için oturum yok."); return; }
            try
            {
                var ser = new PlainTextGameSerializer();
                var text = ser.SerializeGameState(_state);
                var path = Path.Combine(Application.persistentDataPath, "nyrvexa_last_save.txt");
                File.WriteAllText(path, text);
                Debug.Log("Nyrvexa: kayıt yazıldı — " + path);
            }
            catch (System.Exception e)
            {
                Debug.LogError("Nyrvexa: dışa kayıt hatası: " + e.Message);
            }
        }

        [ContextMenu("Nyrvexa v0.2/Sonraki tur (adım modu)")]
        public void V02_AdvanceOneTurn()
        {
            if (!_v02StepByStepMode)
            {
                Debug.LogWarning("Nyrvexa: adım modu kapalı. Inspector’da v0.2 Step By Step aç veya bu menüyü yalnız adım modunda kullan.");
                return;
            }
            EnsureBootstrapped();
            if (_state == null) return;
            if (_state.Header.DeclaredVictorFactionIndex >= 0)
            {
                Debug.Log("Nyrvexa: oyun bitti; kazanan F" + _state.Header.DeclaredVictorFactionIndex);
                return;
            }
            if (_v02ScenarioTurnsSimulated >= _turnsToSimulate)
            {
                Debug.Log("Nyrvexa: senaryo tur sayısına ulaşıldı (" + _turnsToSimulate + ").");
                return;
            }
            if (_v02ScenarioTurnsSimulated == 0)
            {
                _state.CommandJournal.ClearBuffer();
                _state.CommandJournal.Enqueue(new FoundCityCommand(0, 22, "Aldaris"));
                _state.CommandJournal.Enqueue(new FoundCityCommand(1, 27, "Borath"));
                _state.CommandJournal.Enqueue(new MoveUnitCommand(0, _scoutId, 24));
            }
            _pipeline.RunSingleTurn(_state, _rng);
            _v02ScenarioTurnsSimulated++;
            if (_logEachTurn) LogStock($"T{_state.Header.TurnIndex} sonu", _state);
            if (_v02ScenarioTurnsSimulated >= _turnsToSimulate && _testSaveRoundTrip) TrySave();
            NotifyStateView();
        }

        public void V02_TryQueueScoutMoveTo(int targetFlat)
        {
            if (_state == null) { Debug.LogWarning("Nyrvexa: oturum yok."); return; }
            if (_state.World.Tiles == null || targetFlat < 0 || targetFlat >= _state.World.Tiles.Length)
            {
                Debug.LogWarning("Nyrvexa: hareket hedefi sınır dışı: düz " + targetFlat);
                return;
            }
            if (!_state.Units.TryGetValue(_scoutId, out var scout)) { Debug.LogWarning("Nyrvexa: keşifçi yok."); return; }
            var d = _state.World.AxialDistanceBetween(scout.TileIndex, targetFlat);
            if (d != 1)
            {
                Debug.LogWarning("Nyrvexa: sadece bitişik hekse tıkla (hedef düz " + targetFlat + ", mevcut " + scout.TileIndex + ", mesafe=" + d + ").");
                return;
            }
            if (_v02ReplaceScoutMoveInQueue)
            {
                var n = _state.CommandJournal.RemoveAllWhere(
                    c => c is MoveUnitCommand mu && mu.Unit == _scoutId);
                _state.CommandJournal.Enqueue(new MoveUnitCommand(0, _scoutId, targetFlat));
                Debug.Log("Nyrvexa: keşifçi → düz " + targetFlat
                    + (n > 0 ? " (önceki " + n + " hareket kaldırıldı)" : string.Empty)
                    + " | tur: Space / buton");
            }
            else
            {
                _state.CommandJournal.Enqueue(new MoveUnitCommand(0, _scoutId, targetFlat));
                Debug.Log("Nyrvexa: hareket kuyruğa: düz " + targetFlat + " (üst üste dikkat; aynı turda tek hedef için 'Replace' aç).");
            }
            NotifyStateView();
        }

        /// <summary> F0 (oyuncu) aktif M1 teknolojisini seçer; uygulama <see cref="V02_AdvanceOneTurn"/> ile. Tarım için önce hazine. </summary>
        public void V02_TryQueueSetResearch(DefId tech, int issuerFaction = 0)
        {
            if (_state == null) { Debug.LogWarning("Nyrvexa: oturum yok."); return; }
            if (!_v02StepByStepMode)
            {
                Debug.LogWarning("Nyrvexa: araştırma kuyruğu yalnızca adım modunda (v0.2) kullanılır.");
                return;
            }
            if (issuerFaction < 0 || issuerFaction >= _state.Factions.Count) return;
            if (!ResearchM1Catalog.IsValidResearchTarget(tech, _state.Factions[issuerFaction].UnlockedTechnologies))
            {
                Debug.LogWarning("Nyrvexa: bu teknoloji şu an seçilemez: " + tech.Key);
                return;
            }
            _state.CommandJournal.RemoveAllWhere(c => c is SetResearchCommand sr && sr.IssuerFactionIndex == issuerFaction);
            _state.CommandJournal.Enqueue(new SetResearchCommand(issuerFaction, tech));
            Debug.Log("Nyrvexa: araş → " + tech.Key + " (F" + issuerFaction + ") | Sonraki tur");
            NotifyStateView();
        }

        [ContextMenu("Nyrvexa v0.2/Kuyruğu temizle")]
        public void V02_ClearCommandQueue()
        {
            if (_state == null) { Debug.LogWarning("Nyrvexa: oturum yok."); return; }
            _state.CommandJournal.ClearBuffer();
            Debug.Log("Nyrvexa: komut kuyruğu temizlendi.");
            NotifyStateView();
        }

        private void NotifyStateView()
        {
            if (_v02PublishStateChanged) StateViewChanged?.Invoke();
        }

        [ContextMenu("Senaryo: 2 fraksiyon, şehir, ekonomi, kayıt testi")]
        public void RunScenario()
        {
            if (!ValidateScenarioMap())
                return;
            Bootstrap();
            if (_state == null) return;
            if (_logEachTurn) LogStock("T0 bitti (bootstrap, tur başlamadı)", _state);
            for (int i = 0; i < _turnsToSimulate; i++)
            {
                if (_state.Header.DeclaredVictorFactionIndex >= 0) break;
                if (i == 0)
                {
                    _state.CommandJournal.ClearBuffer();
                    _state.CommandJournal.Enqueue(new FoundCityCommand(0, 22, "Aldaris"));
                    _state.CommandJournal.Enqueue(new FoundCityCommand(1, 27, "Borath"));
                    _state.CommandJournal.Enqueue(new MoveUnitCommand(0, _scoutId, 24));
                }
                _pipeline.RunSingleTurn(_state, _rng);
                if (_logEachTurn)
                    LogStock($"T{_state.Header.TurnIndex} sonu", _state);
            }
            if (_testSaveRoundTrip) TrySave();
            NotifyStateView();
        }

        private void TrySave()
        {
            var ser = new PlainTextGameSerializer();
            string t = null;
            try
            {
                t = ser.SerializeGameState(_state);
                var back = ser.DeserializeGameState(t);
                if (back.Cities.Count != _state.Cities.Count || back.Factions.Count != _state.Factions.Count)
                    Debug.LogWarning("Nyrvexa: kayıt yüklemesi farklı şehir/fraksiyon sayısı.");
                else
                {
                    if (back.Factions.Count >= 2)
                    {
                        back.Diplomacy.GetPair(0, 1, out var tr, out var te);
                        if (tr < 0 || tr > 100 || te < 0 || te > 100)
                            Debug.LogWarning($"Nyrvexa: diplo yük (aralık dışı): 0|1 = {tr},{te}.");
                    }
                    if (back.World.Tiles != null && back.World.Tiles.Length > 23)
                    {
                        var m = back.World.Tiles[23].ExploredMask;
                        if ((m & 1) == 0) Debug.LogWarning("Nyrvexa: F0, k23 keşfî maske 0 (beklenti: açık).");
                    }
                    Debug.Log("Nyrvexa: NYRVEXA_V1 kayıt + FOW + diplo turu tamamlandı.");
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("Nyrvexa: kayıt hatası: " + e.Message);
            }
        }

        /// <summary>İlk hazır senaryo şehir/akış 22, 24, 27 düz indekslerini kullanır — toplam hücre &gt; 27 olmalı.</summary>
        private bool ValidateScenarioMap()
        {
            int n = _mapWidth * _mapHeight;
            if (n < 28)
            {
                Debug.LogError("Nyrvexa: bu hazır senaryo 28+ hücre ister (şehir/akış 27'ye kadar). Haritayı büyüt veya alanı 10×6 yap.");
                return false;
            }
            return true;
        }

        private void LogStock(string label, GameStateRoot s)
        {
            for (int f = 0; f < s.Factions.Count; f++)
            {
                s.Factions[f].ResourceStock.TryGetValue(new DefId("res.food"), out var food);
                s.Factions[f].ResourceStock.TryGetValue(new DefId("res.prod"), out var pr);
                Debug.Log($"Nyrvexa: {label} | {s.Factions[f].DebugName} yiyecek={food} üretim={pr} şehir={s.Factions[f].CityIds.Count}");
            }
        }

        private void Bootstrap()
        {
            if (!ValidateScenarioMap())
            {
                Debug.LogError("Nyrvexa: Bootstrap atlanamıyor. Hazır v0.2 senaryosu en az 28 hücre ister; Inspector’da Map Width × Height artır (ör. 10×6).");
                return;
            }
            _bus = new InMemoryEventBus();
            _rng = new DeterministicRng(_worldSeed);
            _state = new GameStateRoot
            {
                Header = { WorldSeed = _worldSeed, TurnIndex = 0, RngState64 = _rng.State, SchemaVersion = GameSchema.Current }
            };
            if (_loadUnitDefsFromStreaming && !string.IsNullOrWhiteSpace(_unitDefsFileName))
            {
                if (UnitCatalogFile.TryLoadFromStreamingData(_unitDefsFileName, out var unitsDto, out var loadErr))
                {
                    if (UnitCatalogImporter.TryImport(unitsDto, _state.UnitDefs, out var impErr))
                        Debug.Log("Nyrvexa: birim kataloğu " + _unitDefsFileName + " (StreamingAssets/Defs) yüklendi.");
                    else
                        Debug.LogWarning("Nyrvexa: birim JSON import hatası — gömülü katalog. " + impErr);
                }
                else
                    Debug.LogWarning("Nyrvexa: birim JSON okunamadı — gömülü katalog. " + loadErr);
            }
            _state.World.Allocate(_mapWidth, _mapHeight, 0);
            if (_paintProceduralBiomes)
            {
                var gen = new DefaultProceduralWorldGenerator();
                var genParams = new WorldGenParams(_mapWidth, _mapHeight);
                gen.GenerateInto(_state, ref _rng, genParams);
            }
            _state.Factions.Add(new FactionState { Index = 0, DebugName = "Kolektif_A", CultureRoot = new DefId("culture.k") });
            _state.Factions.Add(new FactionState { Index = 1, DebugName = "Birlik_B", CultureRoot = new DefId("culture.b") });
            _state.Diplomacy = new DiploMatrixState();
            _state.Diplomacy.EnsureSize(2);
            _state.Diplomacy.SetRelation(0, 1, 50, 10);
            _state.Diplomacy.SetRelation(1, 0, 50, 10);
            _scoutId = _state.Registry.Create();
            var scout = new UnitState
            {
                Id = _scoutId,
                FactionIndex = 0,
                TileIndex = 23,
                Archetype = new DefId("unit.scout")
            };
            UnitRules.ApplyArchetypeStats(ref scout, _state.UnitDefs);
            _state.Units[_scoutId] = scout;
            _state.UnitOnTile[23] = _scoutId;
            FogRevealService.RevealDisk(_state.World, 23, 2, 0);
            // Fraksiyon 1: rakip keşifçi (SimpleRivalScoutBrain). Düz 18 = 10×6 ızgarada (F0:23) ile çakışmayan açık hücre; min 28 hücre senaryolarda da geçerli.
            _rivalScoutId = _state.Registry.Create();
            var rival = new UnitState
            {
                Id = _rivalScoutId,
                FactionIndex = 1,
                TileIndex = 18,
                Archetype = new DefId("unit.scout")
            };
            UnitRules.ApplyArchetypeStats(ref rival, _state.UnitDefs);
            _state.Units[_rivalScoutId] = rival;
            _state.UnitOnTile[18] = _rivalScoutId;
            FogRevealService.RevealDisk(_state.World, 18, 2, 1);
            _pipeline = TurnPipeline.CreateDefault(_bus, _state.CommandJournal);
            _v02ScenarioTurnsSimulated = 0;
            NotifyStateView();
        }
    }
}
