using System;
using System.Collections.Generic;
using N4CoreLite.Records.Bases;

namespace DataAccess.Entities;

#nullable disable
public partial class OgrenciRaporuView : Record
{
    public int? OgrenciId { get; set; }

    public string OgrenciGuid { get; set; }

    public string Sinif { get; set; }

    public string OgrenciAdiSoyadi { get; set; }

    public DateTime? OgrenciDogumTarihi { get; set; }

    public bool? OgrenciMezuniyet { get; set; }

    public int? OgrenciUyruk { get; set; }

    public int? OgrenciCinsiyet { get; set; }

    public string OgrenciTcKimlikNo { get; set; }

    public string OgrenciKullaniciAdi { get; set; }

    public string Ders { get; set; }

    public decimal? DersNotu1 { get; set; }

    public decimal? DersNotu2 { get; set; }

    public decimal? DersNotu3 { get; set; }

    public decimal? DersOrtalamasi { get; set; }

    public int SinifId { get; set; }

    public int? DersId { get; set; }
}
