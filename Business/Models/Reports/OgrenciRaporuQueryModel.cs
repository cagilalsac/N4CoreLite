using N4CoreLite.Records.Bases;
using System.ComponentModel;

namespace Business.Models.Reports
{
    public class OgrenciRaporuQueryModel : Record
    {
        public int? OgrenciId { get; set; }

        [DisplayName("Sınıf")]
        public string? Sinif { get; set; }

        [DisplayName("Adı Soyadı")]
        public string? OgrenciAdiSoyadi { get; set; }

        [DisplayName("Doğum Tarihi")]
        public string? OgrenciDogumTarihi { get; set; }

        [DisplayName("Mezuniyet")]
        public string? OgrenciMezunMu { get; set; }

        [DisplayName("Uyruk")]
        public string? OgrenciUyruk { get; set; }

        [DisplayName("Cinsiyet")]
        public string? OgrenciCinsiyeti { get; set; }

        [DisplayName("T.C Kimlik No")]
        public string? OgrenciTcKimlikNo { get; set; }

        [DisplayName("Ders")]
        public string? Ders { get; set; }

        [DisplayName("Ortalama")]
        public string? DersOrtalamasi { get; set; }
    }
}
