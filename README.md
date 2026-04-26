# Nyrvexa

**Nyrvexa**, harita merkezli keşif–genişleme–ekonomi–çatışma döngüsüne odaklanan bir **4X strateji** oyunudur. Bu depo, oyun istemcisinin ve ileride sunucu/araçlarının **kaynak ve sürüm** kaynağıdır; **Q-Verse** web uygulamalarının veya “tek sayfa oyun” kabuklarının yerine geçmez.

## Q-Verse Ekosistemi ile ilişki

- Nyrvexa, “Q‑Verse’ün bir özelliği” olarak konumlandırılmaz; **ayrı ürün**, **ayrı sürüm (SemVer)**, **ayrı mağaza / dağıtım** hikâyesi.
- Ekosistemle entegrasyon (hesap, cüzdan, listeler vb.) **isteğe bağlı** ve sözleşmeye dayalıdır; monolit veya zorunlu tekil backend yoktur.
- Resmi açıklamalar, imzalı sürümler veya “uyumluluk” notları Q‑Verse tarafında **referanslanabilir**; telif ve pazarlamanın merkezi bu repoda kalır.

## M1 sim (özet, güncel)

- Tur: ekonomi, biyom üretimi, şehir nüfus büyümesi, M1 araştırma + teknoloji etkileri, keşif, ünite harita indeks denetimi, zafer.
- Söylenti / kriz: düşük ihtimalli deterministik `RumorRollEvent` (kayıtla tekrar oynatılabilir).
- Kayıt: `plain text` (NYRVEXA_V1) + fraksiyon `feco`, isteğe **bekleyen komut kuyruğu** `journ=…`.
- Headless: `tools/SimRunner` ile aynı C# yığınını derle ve çalıştır.

## Teknoloji

| Alan | Değer |
|------|--------|
| Oyun motoru | **Unity 2022.3.50f1** LTS — `ProjectSettings/ProjectVersion.txt` ile aynı **patch**; Hub’da *Installs* → bu sürümü ekle, sonra proje aç. |
| Hedef (MVP) | **PC** (önce tek platform; diğerleri ayrı karar) |
| Dil | C#; kök ad alanı önerisi: `Nyrvexa.*` |

> Unity proje kök adı: `Nyrvexa` veya `Nyrvexa.Client` (ekip tercihine bırakılır; repoda tek çözüm tutulur).

## Depo yerleşimi

```
/docs/ARCHITECTURE_4X_MASTER.md   — Ana 4X vizyon, mimari, 30 sistem haritası
/Assets/Scripts/Simulation/       — Heks `World`, FOW, diplo kayıt; `Data/UnitArchetype*` + `UnitRules` (görüş/hareket şablonu)
/Assets/Scripts/Adapters/Unity/   — Unity giriş (`Nyrvexa.Unity`)
/Assets/Editor/                    — Sadece Editor: `GameSessionHost` / `HexGridGizmo` menü kısayolları (`Nyrvexa.Editor`)
/Assets/StreamingAssets/Defs/     — `units.example.json` (schema 1) birim kataloğu; `UnitCatalogImporter` + Unity/SimRunner yükleyici
/…                                — Unity proje ağacı (ProjectSettings, Packages) ayrıca
```

Bu depo, **Unity Hub ile doğrudan açılabilecek** minimal `ProjectSettings/ProjectVersion.txt` + `Packages/manifest.json` içerir. İlk açılışta `Library/` üretilir (git’e girmez). `Nyrvexa.Simulation` assembly’si `noEngineReferences: true` ile **saf C#** kalır; sunum sadece `Nyrvexa.Unity` içinde.

## macOS (M2) — Unity yokken deneme

1. **.NET 8 SDK** (Apple Silicon) kur: [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download) veya `brew install --cask dotnet-sdk`.
2. Depoda: `cd tools/SimRunner` → `dotnet run -c Release`  
   Aynı simülasyon (şehir, ekonomi, FOW, diplo, kayıt turu) terminalde çalışır; Unity gerekmez.

## macOS — Unity Editor (projeyi çalıştırma)

1. [Unity Hub](https://unity.com/download) (Apple Silicon) + **Editör 2022.3.50f1** (LTS) — *Installs* ekranında sürüm numarası bire bir eşleşsin.
2. **Add** / **Aç** → `Nyrvexa` depo **kökü** (içinde `Assets`, `ProjectSettings`, `Packages` olan klasör). İlk açılışta `.meta` dosyaları ve `Library/` üretilebilir; normal.
3. Proje açılınca **File → New Scene** (kaydedebilirsin: `Assets/Scenes/Bootstrap.unity` önerilir) veya boş sahnede kal.
4. **GameObject → Nyrvexa → Prototip: Session + Heks gizmo** — `GameSessionHost` (Run On Start), `HexGridGizmo` (Host’tan boyut senkronu), `HexBoardCameraRig` (üstten kamera), `HexMapClickLog` (Play’de **sol tık** — konsola sütun/satır/düz indeks; kamera/ışın). Scene’de isteğe gizmo **Düz index etiketleri**. Haritayı `GameSessionHost` değiştirirsen hazır senaryo en az **28 hücre** ister; SimRunner açılışta `PointyLayout` tersleme (10×6) tutarlılık kontrolü yapar.
5. **Play** modunda Konsol’da simülasyon log’ları; **Context Menu** (komponente sağ tık) **Senaryo: 2 fraksiyon...** de çalışır.

> Henüz 3B sahne yok: bu **görsel yok** test kabuğudur. 16 GB RAM, erken prototip için yeterlidir. `Library/` büyür, `.gitignore` ile repoda tutulmaz.

## Geliştirme

1. **Gereksinimler:** Unity Editor (2022.3 LTS, `ProjectVersion.txt` ile hizalı), Git; LFS büyük varlıklar için ayrı karar.
2. **Headless sim (CI / terminal):** depo kökünde `make sim` (veya `cd tools/SimRunner` → `dotnet run -c Release`) — aynı `Nyrvexa.Simulation` derlemesi, `Assets/StreamingAssets/Defs/units.example.json` birim kataloğu ile. Tur sayısı: `dotnet run -c Release -- --turns=25` (varsayılan 10).
3. **Birim JSON:** Yeni tipler `Defs/*.json` içine (schema 1) eklenir; `GameSessionHost` üzerinde `Load Unit Defs From Streaming` açıkken `Unit Defs File Name` yolu `Assets/StreamingAssets/Defs/` altındaki dosya adıdır.
4. **CI:** `.github/workflows/nyrvexa-sim.yml` — Ubuntu’da `SimRunner` derlenir ve çalıştırılır (Unity lisansı gerekmez).
5. **Sahne:** `File → New Scene` → `Assets/Scenes/Bootstrap.unity` olarak kayıt; `GameSessionHost` ekle, `Run On Start` veya Context Menu senaryo.
6. **Rakip AI (M1+):** Fraksiyon 1, düz 18’de ikinci keşifçi bootstrap’ta; `OpponentStrategic` aşamasında `SimpleRivalScoutBrain` her tur bitişik (deterministik) yasal hücreye hareket kuyruklar, `ApplyCommands` ile oynatılır. İnsan hâlâ F0; `IStrategicBrain` ilerde değiştirilebilir.
7. **FOW görünümü (HUD):** Otomatik HUD’da **F0** / **F1** düğmeleri, `NyrvexaV02HexBoardRenderer` + `NyrvexaV02MapChrome` üzerinde aynı `SetViewAsFaction` ile o fraksiyonun sisi (Rakip yürüyüşünü görmek için F1). İstersen bu iki bileşene Inspector’dan sürükle; yoksa `FindObjectOfType` bulur.
8. **Kazanım (M1+):** `TurnCapEconomicVictoryEvaluator` — `Header.TurnIndex >= 8` iken (Victory aşamasında) skor = `1000*şehir + yiy + 2*ür`; kazanan `Header.DeclaredVictorFactionIndex`, `VictoryDeclaredEvent` otobüsü, kayıt satırı `victor=`. F0 F1 aynı skorda düşük indeks öne geçer. Adım modunda kazandıktan sonra yeni tur ilerlemez; `GameSessionHost` varsayılan **12** adım (Inspector’da `Turns To Simulate`) T≥8 ile uyumludur. HUD: rakip keşif satırı, minimap’te sarı = F0 / turuncu = F1 keşif; bitti veya adım doldu: Sonraki tur / kuyruk boşat kilitlenir.

## Biyom paleti (M1)

- Simülasyonda biyom **1–6** (prosedürel üretim veya ileride data); zemin rengi **görüntü** katmanındadır, `BiomeId` oyun state’inde tutulur.
- **Tek kaynak (önerilen):** `Create → Nyrvexa → Biome Visual Config` ile bir `BiomeVisualConfig` asset’i oluştur; **altı rengi** (indeks 0..5 = biyom 1..6) burada tanımla.
- Aynı asset’i hem **`NyrvexaV02HexBoardRenderer`** hem **`NyrvexaV02MapChrome`** üzerindeki `Biome Config` alanına at; hex ve sağ üst **minimap** aynı paleti kullanır.
- `Biome Config` **boş** bırakılırsa her bileşendeki kendi `Biome Colors` (6 eleman) dizisi kullanılır; dizi hatalı uzunluktaysa M1 **varsayılan** tonlara doldurulur.
- Fraksiyon rengi ile biyom, bileşenlerdeki **Tint By Biome** / **Biome Faction Mix** ile harmanlanır; `GameSessionHost` üzerinde prosedürel biyom boyaması açık olmalı (`Paint Procedural Biomes`).

## Sürümleme

- **Sürüm:** [Semantic Versioning 2.0.0](https://semver.org/); ayrıntı: `CHANGELOG.md` (güncel: `0.1.10` rakip takip eğilimi, dışa kayıt, SimRunner `--turns`).
- **Changelog:** `CHANGELOG.md`
- **Etiketler:** `vX.Y.Z` (Git etiketleri, imzalı sürümler ayrı politika).

## Güvenlik

Güvenlik açığı veya hassas bulgu raporları için: **`SECURITY.md`** (oluşturulduğunda) veya aşağıdaki iletişim alanını doldurun. Public issue’larda exploit detayı paylaşmayın.

- İletişim: `_güvenlik@alanadiniz_veya_form_`

## Lisans

Depo kökünde `LICENSE` — şu an **tüm haklar saklı** (proprietary, açık kaynak lisansı yayımlanana kadar). Açık kaynağa veya başka modele geçildiğinde bu bölüm ve `LICENSE` birlikte güncellenir.

**Dosya üst bilgisi (C#):** Q-Verse / USDTgVerse ile aynı blok yapı — `File`, `Author` (USDTG GROUP TECHNOLOGY LLC), `Developer` (Irfan Gedik), tarihler, `Version` (CHANGELOG’tan), `Description` (mümkünse sınıf `<summary>`), `License` (proprietary + `LICENSE` atfı). Toplu güncelleme: `python3 scripts/apply_nyrvexa_file_headers.py`.

## Marka notu (iç)

Aday ürün adı **Nyrvexa** (uydurma marka). Tescil, mağaza adı ve alan adı taramaları yayından önce tamamlanmalıdır. Ayrıntılı ürün tezi ve isim arşivi: Q‑Verse monoreposu içinde `docs/FOUR_X_STANDALONE_PRODUCT.md` (referans; bu repo bağımsız kalır).

---

*İletişim, lisans ve hedef platform satırlarını yayından önce doldurun.*
