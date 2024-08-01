using System;
using System.Collections.Generic;
using N4CoreLite.Records.Bases;

namespace DataAccess.Entities;

#nullable disable
public partial class Ogrenci : Record
{
    public int SinifId { get; set; }

    public string Adi { get; set; }

    public string Soyadi { get; set; }

    public DateTime DogumTarihi { get; set; }

    public bool MezunMu { get; set; }

    public int Uyruk { get; set; }

    public int Cinsiyeti { get; set; }

    public string TcKimlikNo { get; set; }

    public string KullaniciAdi { get; set; }

    public virtual ICollection<OgrenciDers> OgrenciDers { get; set; } = new List<OgrenciDers>();

    public virtual Sinif Sinif { get; set; }
}
