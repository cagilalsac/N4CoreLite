using N4CoreLite.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class OgrenciCommandModel : Record
    {
        [Required(ErrorMessage = "{0} zorunludur!")]
        [DisplayName("Sınıf")]
        public int? SinifId { get; set; }

        [Required(ErrorMessage = "{0} zorunludur!")]
        [Length(2, 50, ErrorMessage = "{0} minimum {1} maksimum {2} karakter olmalıdır!")]
        [DisplayName("Adı")]
        public string? Adi { get; set; }

        [Required(ErrorMessage = "{0} zorunludur!")]
        [Length(2, 50, ErrorMessage = "{0} minimum {1} maksimum {2} karakter olmalıdır!")]
        [DisplayName("Soyadı")]
        public string? Soyadi { get; set; }

        [Required(ErrorMessage = "{0} zorunludur!")]
        [DisplayName("Doğum Tarihi")]
        public DateTime? DogumTarihi { get; set; }

        [DisplayName("Mezuniyet")]
        public bool MezunMu { get; set; }

        [Required(ErrorMessage = "{0} zorunludur!")]
        public int? Uyruk { get; set; }

        [Required(ErrorMessage = "{0} zorunludur!")]
        public int? Cinsiyeti { get; set; }

        [Length(11, 11, ErrorMessage = "{0} {1} karakter olmalıdır!")]
        [DisplayName("T.C. Kimlik No")]
        public string? TcKimlikNo { get; set; }

        [DisplayName("Dersler")]
        public List<int> DersIdleri { get; set; } = new List<int>();

        public string? KullaniciAdi { get; set; }
    }
}
