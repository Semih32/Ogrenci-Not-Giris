using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Örnek öğrenci ve dersler oluşturma
        var ogrenci = new Ogrenci
        {
            Ad = "Ahmet",
            Dersler = new List<Ders>
            {
                new Ders { DersAdi = "Matematik", Not = 85, DersKatsayisi = 4 },
                new Ders { DersAdi = "Fizik", Not = 75, DersKatsayisi = 3 },
                new Ders { DersAdi = "Kimya", Not = 90, DersKatsayisi = 2 }
            }
        };

        // Ortalama hesaplama
        double ortalama = HesaplaOrtalama(ogrenci.Dersler);
        ogrenci.Ortalama = ortalama;

        // Sonuçları yazdırma
        Console.WriteLine($"Öğrenci: {ogrenci.Ad}");
        Console.WriteLine("Ders Notları:");
        foreach (var ders in ogrenci.Dersler)
        {
            Console.WriteLine($"{ders.DersAdi}: {ders.Not} (Katsayı: {ders.DersKatsayisi})");
        }
        Console.WriteLine($"Ağırlıklı Ortalama: {ortalama:F2}");
    }

    static double HesaplaOrtalama(List<Ders> dersler)
    {
        if (dersler == null || dersler.Count == 0)
            throw new ArgumentException("Ders listesi boş olamaz.");

        double toplamAgirlikliNot = 0;
        double toplamKatsayi = 0;

        foreach (var ders in dersler)
        {
            // Not kontrolü (0-100 arası)
            if (ders.Not < 0 || ders.Not > 100)
                throw new ArgumentException($"Geçersiz not değeri: {ders.Not}. Not 0-100 arasında olmalıdır.");

            // Katsayı kontrolü (pozitif olmalı)
            if (ders.DersKatsayisi <= 0)
                throw new ArgumentException($"Geçersiz katsayı değeri: {ders.DersKatsayisi}. Katsayı pozitif olmalıdır.");

            toplamAgirlikliNot += ders.Not * ders.DersKatsayisi;
            toplamKatsayi += ders.DersKatsayisi;
        }

        return toplamAgirlikliNot / toplamKatsayi;
    }
}

class Ogrenci
{
    public string Ad { get; set; }
    public List<Ders> Dersler { get; set; }
    public double Ortalama { get; set; }
}

class Ders
{
    public string DersAdi { get; set; }
    public double Not { get; set; }
    public double DersKatsayisi { get; set; }
} 