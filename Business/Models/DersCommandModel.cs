using N4CoreLite.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class DersCommandModel : Record
    {
        [Required(ErrorMessage = "{0} zorunludur!")]
        [Length(3, 100, ErrorMessage = "{0} minimum {1} maksimum {2} karakter olmalıdır!")]
        [DisplayName("Ders Adı")]
        public string? Adi { get; set; }
    }
}
