using N4CoreLite.Records.Bases;
using System.ComponentModel;

namespace Business.Models
{
    public class SinifQueryModel : Record, IModifiedBy
    {
        [DisplayName("Sınıf Adı")]
        public string? Adi { get; set; }

        [DisplayName("Öğrenciler")]
        public List<OgrenciQueryModel>? Ogrenciler { get; set; }

        [DisplayName("Öğrenci Sayısı")]
        public int OgrenciSayisi { get; set; }

        [DisplayName("Oluşturulma Tarihi")]
        public DateTime? CreateDate { get; set; }

        [DisplayName("Oluşturan")]
        public string? CreatedBy { get; set; }

        [DisplayName("Güncellenme Tarihi")]
        public DateTime? UpdateDate { get; set; }

        [DisplayName("Güncelleyen")]
        public string? UpdatedBy { get; set; }
    }
}
