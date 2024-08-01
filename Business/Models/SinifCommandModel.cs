using N4CoreLite.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class SinifCommandModel : Record
    {
        [Required(ErrorMessage = "{0} zorunludur!")]
        [Length(5, 25, ErrorMessage = "{0} minimum {1} maksimum {2} karakter olmalıdır!")]
        [DisplayName("Sınıf Adı")]
        public string? Adi { get; set; }
    }
}
