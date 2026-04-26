# Nyrvexa

**Nyrvexa**, harita merkezli keşif–genişleme–ekonomi–çatışma döngüsüne odaklanan bir **4X strateji** oyunudur. Bu depo, oyun istemcisinin ve ileride sunucu/araçlarının **kaynak ve sürüm** kaynağıdır; **Q-Verse** web uygulamalarının veya “tek sayfa oyun” kabuklarının yerine geçmez.

## Q-Verse Ekosistemi ile ilişki

- Nyrvexa, “Q‑Verse’ün bir özelliği” olarak konumlandırılmaz; **ayrı ürün**, **ayrı sürüm (SemVer)**, **ayrı mağaza / dağıtım** hikâyesi.
- Ekosistemle entegrasyon (hesap, cüzdan, listeler vb.) **isteğe bağlı** ve sözleşmeye dayalıdır; monolit veya zorunlu tekil backend yoktur.
- Resmi açıklamalar, imzalı sürümler veya “uyumluluk” notları Q‑Verse tarafında **referanslanabilir**; telif ve pazarlamanın merkezi bu repoda kalır.

## Teknoloji

| Alan | Değer |
|------|--------|
| Oyun motoru | **Unity** (LTS — sürüm: `_Unity_LTS_örn_6000.x_veya_2022.3LTS_`) |
| Hedef (MVP) | **PC** (önce tek platform; diğerleri ayrı karar) |
| Dil | C#; kök ad alanı önerisi: `Nyrvexa.*` |

> Unity proje kök adı: `Nyrvexa` veya `Nyrvexa.Client` (ekip tercihine bırakılır; repoda tek çözüm tutulur).

## Depo yerleşimi (taslak)

```
/                 — Unity proje kökü (ileride: Assets, Packages, ProjectSettings, …)
/docs             — İç notlar, yol haritası, tasarım özetleri (opsiyonel)
```

*(Unity projesi eklendikçe yukarıdaki ağaç netleşir.)*

## Geliştirme (Unity projesi hazır olduktan sonra)

1. **Gereksinimler:** Unity Editor (LTS, yukarıdaki sürümle hizalı), Git, LFS (büyük varlıklar için ayrı karar).
2. **Açma:** Depoyu klonla → Unity Hub ile bu klasörü proje olarak aç.
3. **Oynatma:** `_Play_` — sahne/startup sahnesi eklendikten sonra `README` bu bölüm güncellenir.

## Sürümleme

- **Sürüm:** [Semantic Versioning 2.0.0](https://semver.org/) (ör. `0.1.0` ilk oynanabilir prototip).
- **Changelog:** `CHANGELOG.md` (ilk anlamlı sürümde açılır).
- **Etiketler:** `vX.Y.Z` (Git etiketleri, imzalı sürümler ayrı politika).

## Güvenlik

Güvenlik açığı veya hassas bulgu raporları için: **`SECURITY.md`** (oluşturulduğunda) veya aşağıdaki iletişim alanını doldurun. Public issue’larda exploit detayı paylaşmayın.

- İletişim: `_güvenlik@alanadiniz_veya_form_`

## Lisans

**TBD** — telif metni eklendikten sonra bu bölüm `LICENSE` dosyası ile eşleşecek şekilde güncellenir.

## Marka notu (iç)

Aday ürün adı **Nyrvexa** (uydurma marka). Tescil, mağaza adı ve alan adı taramaları yayından önce tamamlanmalıdır. Ayrıntılı ürün tezi ve isim arşivi: Q‑Verse monoreposu içinde `docs/FOUR_X_STANDALONE_PRODUCT.md` (referans; bu repo bağımsız kalır).

---

*Bu README şablonudur: Unity sürümü, iletişim ve lisans alanlarını doldurun; ilk build sonrası “Geliştirme” bölümünü somut adımlarla güncelleyin.*
