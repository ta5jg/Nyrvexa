// Copyright (c) 2026 Irfan Gedik. All rights reserved.
// This file is part of Nyrvexa. Licensing: see LICENSE in the repository root (TBD).
//


namespace Nyrvexa.Simulation.Events
{
    /// <summary> Söylenti <see cref="RumorRollEvent.Code"/> → kısa, nötr metin (M1: görünüm; mekanik yok). </summary>
    public static class RumorFlavorM1
    {
        private static readonly string[] Lines =
        {
            "Pazar fısıltıları: stok eriyor, fiyat artacak deniyor.",
            "Lonca: yeni yol güvenli değil; konvoylar gecikir.",
            "Sınır: komşu şehirde silah tıkırtısı duyuldu (doğrulanmadı).",
            "Diplomatik not: gizli görüşme söylentisi, taraf yok.",
            "Kıtlık hikâyesi: depolar dolu, ama kimse satmıyor deniyor.",
            "Büyücü yok, ama rün taşları tüccarda aranıyor.",
            "Yerel önder: yeni vergi söylentisi, halk toplantı istiyor.",
            "Deniz: sis bastı, bir kayıp balıkçı teknesi aranıyor.",
            "Göçmen hattı: iz süren devriyeler, haber yayıldı.",
            "Erken uyarı: haşere kıyameti abartı, ama depolar açıldı.",
            "Madenci ayaklanması — aslında ücret pazarlığı, dedikodu büyüdü.",
            "Yabani hayvan: nehir boyunda izler; sürü değil, panik var.",
            "Kadın/erkek loncası: ortak tabela asıldı, espri-kavga karıştı.",
            "Tarih kütüphanesi: sayfa sayfa ateş, bir suçlu aranıyor (belirsiz).",
            "Kervan rotası: köprü çatlak, tamir ertelendi, mal bekliyor.",
            "Kehanet değil, hava durumu: kuraklık endişesi, yağmur gelir deniyor.",
        };

        /// <param name="code">Olaydaki imzasız 32 bit (ör. RNG çıktısı). </param>
        public static string GetLine(int code)
        {
            if (Lines.Length == 0) return "—";
            var u = unchecked((uint)code);
            return Lines[u % (uint)Lines.Length];
        }
    }
}
