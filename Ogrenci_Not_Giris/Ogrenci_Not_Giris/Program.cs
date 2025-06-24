using System.Security.Cryptography.X509Certificates;

namespace Ogrenci_Not_Sistemi
{
    public class ogrenci
    {
        public int ogrenciNo { get; set; }
        public string ogrenciAd { get; set; }
        public string ogrenciSoyad { get; set; }
        public string sinif { get; set; }
        public List<ders> dersler { get; set; } = new List<ders>();
        public float ortalama { get; set; }
    }

    public class ders
    {
        public int dersId { get; set; }
        public string dersAd { get; set; }
        public int not { get; set; }
        public int dersKatsayısı { get; set; }
    }

    public class NotSistemi
    {
        public List<ogrenci> ogrenciler = new List<ogrenci>();

        public List<ders> _dersler = new List<ders>()
        {
            new ders { dersId = 1, dersAd = "Matematik", dersKatsayısı = 4 },
            new ders { dersId = 2, dersAd = "Fizik", dersKatsayısı = 3 },
            new ders { dersId = 3, dersAd = "Kimya", dersKatsayısı = 3 },
            new ders { dersId = 4, dersAd = "Biyoloji", dersKatsayısı = 2 }
        };

        
        
        public void ogrenciEkle()
        {
            Console.Write("Öğrenci No:");
            string ogrenciNo = Console.ReadLine();

            Console.Write("Adı:");
            string ad = Console.ReadLine();

            Console.Write("Soyadı:");
            string soyad = Console.ReadLine();

            Console.Write("Sınıfı:");
            string sinif = Console.ReadLine();

            if (string.IsNullOrEmpty(sinif) || string.IsNullOrWhiteSpace(ogrenciNo) || string.IsNullOrEmpty(ad) || string.IsNullOrEmpty(soyad)){
                Console.WriteLine("Bu kısımlar boş bırakılamaz");
                return;
            }
            
            if (!int.TryParse(ogrenciNo, out int ogrNo))
            {
                Console.WriteLine("Geçerli bir öğrenici Numarası giriniz!");
                return;
            }

            var ogrenci = new ogrenci()
            {
                ogrenciNo = ogrNo,
                ogrenciAd = ad,
                ogrenciSoyad = soyad,
                sinif = sinif,
                dersler = new List<ders>()
            };

            ogrenciler.Add(ogrenci);
            Console.WriteLine("Öğrenci başarıyla eklendi!");
        }

        public void notEkle()
        {
            var ogr = ogrenciBul();
            if (ogr == null) return;

            foreach (var ders in _dersler)
            {
                Console.WriteLine($"{ders.dersAd} - {ders.dersKatsayısı}");
                Console.Write("Aldığı Not:");
                string dersNot = Console.ReadLine();
                if (!int.TryParse(dersNot, out int dNot) || dNot <= 0 || dNot >= 100)
                {
                    Console.WriteLine("Geçerli bir not giriniz (0-100 arası)!");
                    return;
                }

                var yeniDers = new ders()
                {
                    dersId = ders.dersId,
                    dersAd = ders.dersAd,
                    not = dNot,
                    dersKatsayısı = ders.dersKatsayısı
                };
                ogr.dersler.Add(yeniDers);
            }

            float ortalama = ortalamaHesapla(ogr);
            Console.WriteLine("Öğrencinin ortalaması: " + ortalama);
        }

        public void notGuncelle()
        {
            var ogr = ogrenciBul();
            if (ogr == null) return;

            foreach (var ders in _dersler)
            {
                Console.Write($"Yeni {ders.dersAd} - {ders.dersKatsayısı} Notu: ");
                string güncelNot = Console.ReadLine();
                if (!int.TryParse(güncelNot, out int gNot) || gNot < 0 || gNot > 100)
                {
                    Console.WriteLine("Geçerli bir not giriniz (0-100 arası)!");
                    return;
                }

                var ogrenciDersi = ogr.dersler.Find(d => d.dersAd == ders.dersAd);
                if (ogrenciDersi != null)
                {
                    ogrenciDersi.not = gNot;
                    Console.WriteLine($"{ogrenciDersi.dersAd} dersi için not güncellendi: {gNot}");
                }
            }

            float ortalama = ortalamaHesapla(ogr);
            Console.WriteLine("Öğrencinin yeni ortalaması: " + ortalama);
        }
        public ogrenci ogrenciBul()
        {
            Console.Write("Öğrenci numarasını giriniz: ");
            string ogrNum = Console.ReadLine();

            if (!int.TryParse(ogrNum, out int ogrNo))
            {
                Console.WriteLine("Geçerli bir öğrenci numarası giriniz!");
                return null;
            }

            var ogr = ogrenciler.Find(o => o.ogrenciNo == ogrNo);

            if (ogr == null)
            {
                Console.WriteLine("Bu öğrenci numarasıyla kayıtlı bir öğrenci bulunmamaktadır!");
                return null;
            }

            return ogr;
        }

        public float ortalamaHesapla(ogrenci ogr)
        {
            float ortalama = 0;
            int katsayıToplamı = 0;
            int toplam = 0;
            foreach (var d in ogr.dersler)
            {
                toplam += d.not * d.dersKatsayısı;
                katsayıToplamı += d.dersKatsayısı;
            }
            ortalama = toplam / katsayıToplamı;
            ogr.ortalama = ortalama;
            return ortalama;
        }

        public void ogrenciListele()
        {
            if (ogrenciler.Count == 0)
            {
                Console.WriteLine("Henüz öğrenci eklenmemiş!");
                return;
            }
            Console.WriteLine("Öğrenci Listesi:");
            foreach (var ogr in ogrenciler)
            {
                Console.WriteLine($"Öğreci No:{ogr.ogrenciNo}\nAdı:{ogr.ogrenciAd}\nSoyadı:{ogr.ogrenciSoyad}\nSınıfı:{ogr.sinif}\nOrtalaması:{ogr.ortalama}");
                if (ogr.dersler.Count > 0)
                {
                    Console.WriteLine("Ders Notları:");
                    foreach (var d in ogr.dersler)
                    {
                        Console.WriteLine($"Ders Adı: {d.dersAd}, Not: {d.not}, Katsayı: {d.dersKatsayısı}");
                    }
                }
                Console.WriteLine("------------------------");
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            NotSistemi notSistemi = new NotSistemi();
            bool devam = true;

            while (devam)
            {
                Console.WriteLine("--Not Giriş Sistemi---");
                Console.WriteLine("1.Öğrenci Ekle");
                Console.WriteLine("2.Not Girişi Yap");
                Console.WriteLine("3.Not Güncelle");
                Console.WriteLine("4.Öğrencileri Listele");
                Console.WriteLine("5.Çıkış Yap");
                Console.Write("Seçim Yapınız(1-5):");
                string secim = Console.ReadLine();

                switch (secim)
                {
                    case "1":
                        notSistemi.ogrenciEkle();
                        break;

                    case "2":
                        notSistemi.notEkle();
                        break;

                    case "3":
                        notSistemi.notGuncelle();
                        break;

                    case "4":
                        notSistemi.ogrenciListele();
                        break;

                    case "5":
                        Console.WriteLine("Sistemden çıkılıyor....");
                        devam = false;
                        break;

                    default:
                        Console.WriteLine("Yanlış bir rakam tuşladınız!");
                        break;
                }
            }
        }
    }
}
