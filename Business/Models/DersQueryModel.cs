using N4CoreLite.Records.Bases;
using System.ComponentModel;

namespace Business.Models
{
    public class DersQueryModel : Record
    {
        [DisplayName("Ders Adı")]
        public string? Adi { get; set; }

        [DisplayName("Öğrenci Sayısı")]
        public int OgrenciSayisi { get; set; }
    }
}
