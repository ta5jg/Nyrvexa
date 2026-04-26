# Changelog

Tüm anlamlı sürümler [Semantic Versioning](https://semver.org/) ile numaralandırılır.

## [0.1.7] — 2026-04-26

### Changed

- C# dosya başları: Q-Verse / USDTgVerse tarzı `/* … */` blok (File, Author, Developer, tarihler, Version, Description, License — proprietary).
- `scripts/apply_nyrvexa_file_headers.py`: açıklama için önce `public|internal` + `class|struct|…` üstündeki `<summary>`; yoksa dosya adı yedeği; `static class` (SimRunner) desteklenir.
- `LICENSE`: USDTG GROUP TECHNOLOGY LLC + Irfan Gedik telif satırı.

## [0.1.6] — 2026-04-26

### Added

- `LICENSE`: telif (EN/TR) — geçici olarak tüm haklar saklı; ileride resmi lisansla değiştirilebilir.
- Tüm `Assets/**/*.cs` ve `tools/**/*.cs` dosyalarına tutarlı üst bilgi (copyright + Nyrvexa + LICENSE referansı).
- `scripts/apply_nyrvexa_file_headers.py`: yeni C# dosyalarına aynı üst bilgiyi idempotent eklemek için.

## [0.1.5] — 2026-04-26

### Added

- `RumorFlavorM1`: söylenti kodundan nötr, kısa lezzet cümleleri (HUD + ileri raporlama).
- `SimRunner`: 10 tur sonrası olay otobüsünde `RumorRollEvent` sayacı; `tools/SimRunner/bin` repoda izlenmiyor (yalnız `dotnet build`).

### Changed

- `InMemoryEventBus`: `TryGetLastOfType<T>`; `GameSessionHost.TryGetLastRumorRoll` tarama burada tekilleşti.

## [0.1.4] — 2026-04-25

### Added

- `PointyLayout` (Simulation): pointy heks yönünde ileri/geri tersleme, küp yuvarlama, `VerifyInversionAllCells`.
- `HexLayoutUnity.TryWorldToColRow` ve `ColRowToWorld` → `PointyLayout` ile aynı formül.
- `HexMapClickLog`: yatay düzlemde ışın + sol tık → col/row/flat; Host’tan harita senkronu; prototip menüsüne eklendi.
- `SimRunner`: açılışta `PointyLayout.VerifyInversionAllCells` (başarısız → exit 1).

## [0.1.3] — 2026-04-25

### Added

- `GameSessionHost`: Inspector’da harita genişlik / yükseklik; hazır senaryo için en az 28 hücre doğrulaması.
- `HexLayoutUnity.GetMapAxisAlignedBounds` — kamera çerçevesi için XZ kutu.
- `HexBoardCameraRig` — ortografik üstten hizalama (Play veya Context Menu); `PullMapFromSiblings` ile Host/gizmo ile aynı boyutlar.
- `HexGridGizmo`: Host’tan boyut senkronu; Scene’de düz index etiketleri (Editor `Handles`); `GridWidth` / `HexRadiusWorld` okuma.
- Prototip menüsü: otomatik `HexBoardCameraRig` + gizmo senkronu + rig alan çekimi.

## [0.1.2] — 2026-04-25

### Added

- `WorldMapDefaults` (10×6): `GameSessionHost`, `SimRunner` ve `HexGridGizmo` aynı SSOT boyutlarını kullanır.
- `HexLayoutUnity` (axial / col,row → XZ) + `HexGridGizmo` (Scene gizmo telleri, isteğe düz indeks vurgusu; varsayılan 23 = scout).
- Editor: **Prototip: Session + Heks gizmo**, **Heks ızgara gizmosu** menüleri.

## [0.1.1] — 2026-04-25

### Added

- Kök `Makefile` (`make sim` / `make sim-build`) — kısa yol, CI ile aynı `tools/SimRunner` akışı.
- `Nyrvexa.Editor` (yalnızca Editor): **GameObject → Nyrvexa → GameSessionHost ekle (Play için Run On Start)** menü ögesi; elle bileşen ekleme adımlarını kısaltır.

### Documentation

- README: hedef editör sürümü **2022.3.50f1** olarak sabitlendi (Hub eşleşmesi net).

## [0.1.0] — 2026-04-25

### Added

- Birim şablonları için `UnitsFileDto` + `UnitCatalogImporter`; `StreamingAssets/Defs/units.example.json` (schema 1) hem Unity `JsonUtility` hem SimRunner `System.Text.Json` ile yüklenebilir.
- `UnitCatalogFile` (Unity) ve `UnitJsonNet` (SimRunner); `GameSessionHost` üzerinde `Load From Streaming` ve dosya adı alanları.
- GitHub Actions: `nyrvexa-sim` — `tools/SimRunner` derleme ve headless koşu.

### Notes

- SimRunner’da `IncludeFields: true` gerekir (DTO alan tabanlı); Unity `JsonUtility` aynı sınıfları kullanır.
