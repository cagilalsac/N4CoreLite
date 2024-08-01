using Business.Enums;
using N4CoreLite.Records.Bases;
using System.ComponentModel;

namespace Business.Models
{
    public class OgrenciQueryModel : Record
    {
        [DisplayName("Sınıf")]
        public string? Sinif { get; set; }

        [DisplayName("Adı Soyadı")]
        public string? AdiSoyadi { get; set; }

        [DisplayName("Doğum Tarihi")]
        public string? DogumTarihi { get; set; }

        [DisplayName("Mezuniyet")]
        public string? MezunMu { get; set; }

        public Uyruklar Uyruk { get; set; }

        public Cinsiyetler Cinsiyeti { get; set; }

        [DisplayName("T.C. Kimlik No")]
        public string? TcKimlikNo { get; set; }

        [DisplayName("Dersler")]
        public string? Dersleri { get; set; }

        public string? KullaniciAdi { get; set; }

        [DisplayName("Not Ortalaması")]
        public string? NotOrtalamasi { get; set; }
    }
}
