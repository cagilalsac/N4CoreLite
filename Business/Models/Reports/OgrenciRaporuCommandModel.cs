using N4CoreLite.Records.Bases;
using System.ComponentModel;

namespace Business.Models.Reports
{
    public class OgrenciRaporuCommandModel : Record
    {
        [DisplayName("Adı Soyadı")]
        public string? OgrenciAdiSoyadi { get; set; }

        [DisplayName("Doğum Tarihi")]
        public DateTime? OgrenciDogumTarihiBaslangic { get; set; }

        public DateTime? OgrenciDogumTarihiBitis { get; set; }

        [DisplayName("Mezun")]
        public bool? OgrenciMezunMu { get; set; }

        [DisplayName("Uyruk")]
        public int? OgrenciUyruk { get; set; }

        [DisplayName("Sınıf")]
        public int? SinifId { get; set; }

        [DisplayName("Dersler")]
        public List<int>? DersIdleri { get; set; }
    }
}
