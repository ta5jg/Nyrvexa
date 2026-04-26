# Nyrvexa — AAA 4X Teknik Direktörlük Runbook

**Statü:** Canlı; her iterasyonda `Assets/Scripts/Simulation/` ve bu belge hizalanır.  
**İlke:** `Nyrvexa.Simulation` = motor-bağımsız, deterministik, kayıt-tekrar üretilebilir; **Unity** = sunum + komut girişi.  
**Referans oyun sınıfı:** Civilization, Stellaris, Endless Legend, Dune: Spice Wars, Total War (stratejik katman), Paradox (diplo/anılaşma/ iç politika).

---

## 1) Bu aşamada alınan ana tasarım kararları

| # | Karar | Gerekçe |
|---|--------|---------|
| A1 | **Turn-based, tek iş parçacığı, deterministik** | MP ileride “aynı girdi = aynı state”; audit ve replay. |
| A2 | **Komut + olay ayrımı** | Ham tık değil: `IGameCommand` yürütülür, `IGameEvent` sadece gözlemlenebilir yan etkiler. |
| A3 | **Faz hattı = genişlebilir iskele** | `IPipelineStep` + `TurnPipeline` — Sistem 1–30 aşamalı “step” eklemleri. |
| A4 | **Data-first genişleme** | `DefId` + `StreamingAssets/Defs/*.json` (schema sürümlenir) — “kod dışı denge” mümkün. |
| A5 | **AI sınıfı: Utility (stratejik) + GOAP/HTN (taktik oturum)** | Ham ağaç taraması yok; bütçe, tehdit, fırsat ağırlıkları, sonra eylem zinciri. |
| A6 | **Ekonomi: stok + akış** | Bina/bölge/üretim hatları `Modifier` toplamı; Endless/ Stellaris benzeri genişleme. |
| A7 | **Diplomasi: skor + anlaşma** | `DiploMatrix` + ileride `Treaty` listesi (Paradox sözleşme modeli sadeleşmişi). |
| A8 | **FOW: tile maske; casus: ayrı “bilgi gürültüsü”** | Keşif = geometri; sızdırma = ayrı kanal. |
| A9 | **Kazanma: çok yollu, anti-snowball** | Bilim, hakimiyet, diplo birliği, puan, senaryo — skor tavanları. |
| A10 | **UI ve Unity hiçbir şeyi zorunlu tutmaz** | Simülasyon assembly’si `UnityEngine` içermez. |

---

## 2) Sistem mimarisi (katmanlar)

```
[ Sunum: Unity / ileride diğer istemciler ]
        │ komut kuyruğu
        ▼
┌───────────────────────────────────────────────┐
│ Command journal + sıra/ MP slot (ileride)     │
├───────────────────────────────────────────────┤
│ TurnPipeline (faz IPipelineStep zinciri)      │
│  → EventBus (IGameEventSink)                  │
│  → State mutasyonu (GameStateRoot, tek yazar)│
├───────────────────────────────────────────────┤
│ Modül yuvası:                                 │
│  World, Economy, Research, Military,         │
│  Diplo, Culture, Espionage, Trade, Ideology,  │
│  Crises, Victory, City, Pop, Gov              │
├───────────────────────────────────────────────┤
│ Veri: DefId, EntityId, DTO'lar, mod patch     │
├───────────────────────────────────────────────┤
│ IGameStateSerializer (kayıt)                │
│ DeterministicRng (seed’li)                    │
└───────────────────────────────────────────────┘
```

**Eşgüdüm kuralı:** Aynı tur içinde aynı domain’e eşzamanlı iki farklı mutasyon yok; ileride “salt okunur taktik alt tur” ayrı bütçe alır.

**30 sistemin eşleme (özet):**

| # | Sistem | Mevcut / hedef bağlantı (Nyrvexa) |
|---|--------|----------------------------------|
| 1 | Core loop | `TurnPipeline`, `RunSingleTurn` |
| 2 | World gen | Yeni: `IWorldGenerator` (iskelet) + seed |
| 3 | Hex | `OffsetOddRPointy`, `WorldMapState` |
| 4 | FOW | `FogOfWarState`, `SightAndExplorationStep` |
| 5 | Exploration | Keşif adımı, birim menzil |
| 6 | Expansion | `FoundCityCommand` |
| 7 | Economy | `EconomyStep`, stok, `DefId` |
| 8 | Population | Yeni: `FactionState` + ilave pop alanı (faz) |
| 9 | Production | Bina/tarif verisi + üretim adımı (faz) |
| 10 | Research | `ResearchGraphState` iskelet |
| 11 | Culture | Baskı/ sınır aşımı (faz) |
| 12 | Diplomacy | `DiploMatrixState` |
| 13 | Espionage | Ayrı olay/infiltrasyon (faz) |
| 14 | Askeri birim | `UnitState`, `UnitRules` |
| 15 | Combat | Taktik çözüm adımı (faz) |
| 16 | Stratejik AI | `IStrategicBrain` + Utility |
| 17 | Taktik AI | `ITacticalController` + planlayıcı |
| 18 | Faction traits | `Modifier` + `DefId` kültür |
| 19 | Leaders | `EntityId` lider, trait modları |
| 20 | Olay/ kriz | `CrisisEvent` şablonu + RNG |
| 21 | Ticaret | Rota/ akış, gümrük (faz) |
| 22 | İdeoloji | Ideology `DefId` (faz) |
| 23 | Hükümet | Gov `DefId` + yasa çarpanları (faz) |
| 24 | Yasalar | policy slot listesi (faz) |
| 25 | Kazanma | `IVictoryConditionEvaluator` (iskelet) |
| 26 | UI | `Adapters/Unity/*` ayrı assembly |
| 27 | Save/load | `IGameStateSerializer` |
| 28 | Mod | Def patch, manifest, whitelist (v2) |
| 29 | Dengeleme | Headless + hash + senaryo bankası (CI) |
| 30 | MP hazır mimari | Turn token + aynı komut sırası (ileride) |

---

## 3) Oyun mekanikleri (derinleşme yönü)

- **Büyük strateji:** imparatorluk ölçeği, çok cilt, uzun oyun.  
- **Dinamik diplo:** güven, gerginlik, savaş hedefi, ticaret, federasyon.  
- **Yapay zekâ:** fraksiyon profili, risk iştahı, “aşırı agresif / aşırı barış” kaçınma (hard/soft sınırlar).  
- **Taktik+stratejik savaş:** stratejik hareket/çatışma çözücü, isteğe alt oturum (taktik grid).  
- **İç politika:** mutluluk, isyankârlık, lider; krizler tetikleyici.  
- **Doğal krizler:** dış şok + hazırlık / sigorta.  
- **Dengeli kazanma:** geç aşamada “tek yol” baskısını tavan + maliyet eğrileri ile kes.

---

## 4) Veri modelleri (genişleme sözleşmesi)

| Varlık | Açıklama |
|--------|----------|
| `DefId` | Mod-uyumlu tanıtıcı; string havuzlu. |
| `EntityId` | Kalıcı varlık (nesil). |
| `GameStateHeader` | Şema, seed, `TurnIndex`, RNG snapshot. |
| `GameStateRoot` | Tek kök. |
| `*Def` (JSON) | Bina, teknoloji, birim, olay, vergi — `Data/` ve `Defs/`. |
| Olay/ kriz DTO’su | `CrisisDef`, tetik, seçenek, etki. |
| Ticaret rota DTO’su | Uç/ düğüm, kapasite, vergi. |

Sürümleme: `GameSchema` artışı, okuyucu eski sürüme geriye dönük.

---

## 5) C# / Unity iskeleti (konum)

- **Mimari sözleşmeler (yeni):** `Assets/Scripts/Simulation/Architecture/*.cs`  
- **Dönüşüm dizisi (yeni):** `TurnPhasesCatalog.cs`  
- **Oyun hattı (mevcut):** `Loop/TurnPipeline.cs`, `IPipelineStep.cs`  
- **Unity:** `Adapters/Unity/GameSessionHost` — sadece köprü; ağır mantık `Simulation` içinde.  

Detay: `SystemBoundaryInterfaces.cs` — `IWorldGenerator`, `IVictoryConditionEvaluator` motor-bağımsız arayüz.

---

## 6) Dengeleme notları (AAA sınıfı hedef)

- **Erken-oyun:** sınırlı inşaat / koloni fırsat bütçesi; açgözlü genişleme büyüme maliyeti.  
- **Orta-oyun:** savaş yorgunluğu, tedarik, tükenmez tek birim türü yok.  
- **Geç-oyun:** teknoloji/ kültür puan tavanı; diplo kazanma penceresi dar ama ulaşılır.  
- **Kriz:** savaş + borç + mutluluk kesişiminde ağırlık artan tetik.  
- **AI:** açık sömürü yok — minimum “insan gibi hata bütçesi”.

---

## 7) Test senaryoları (CI + headless)

| ID | Açıklama | Beklenti |
|----|-----------|----------|
| H0 | Aynı seed, 0 komut, N tur | Aynı `TurnIndex`, bilinen log imzaları. |
| H1 | Heks tersleme/ komşu | `PointyLayout` tutarlılık (mevcut). |
| H2 | Kayıt round-trip | `IGameStateSerializer` şehir/ diplo. |
| H3 | Komut sınırı | Geçersiz hücre, sessizce reddet, tur stabil. |
| H4 | (Faz) Zafer boş | `IVictoryConditionEvaluator` = null sonuç. |
| H5 | (Faz) Dünya üretim | Aynı seed, aynı biyom dağılımı. |

`tools/SimRunner` — hızlı regresyon; Unity Play — görsel/ input.

---

## 8) Sonraki otomatik geliştirme adımı (sırada ne var)

1. `IWorldGenerator` + tek harita doldurma (biyom int → `TileCell.BiomeId`).  
2. `IPipelineStep` altına `Economy` genişleme: `Modifier` toplamı, şehir stoku.  
3. `IStrategicBrain` somut: Utility eşiği + `CommandJournal` doldurma.  
4. `IVictoryConditionEvaluator` + tek “bilim kazanma” sınama.  
5. `Events`: `CrisisEvent` 1 taslak (JSON) + 1 test turu.  
6. CI: `nyrvexa-sim.yml` hash karşılaştırması (isteğe).

Bu runbook, `ARCHITECTURE_4X_MASTER.md` ile birlikte “tek hedef SSOT” seti oluşturur; çelişkide bu dosya (AAA runbook) **ürün tercihlerini** öne alır, eski belge detay eşleştirmesini tutar.
