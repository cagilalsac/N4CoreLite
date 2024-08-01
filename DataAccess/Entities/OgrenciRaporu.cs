using System;
using System.Collections.Generic;
using N4CoreLite.Records.Bases;

namespace DataAccess.Entities;

#nullable disable
public partial class OgrenciRaporu : Record
{
    public int? OgrenciId { get; set; }

    public string OgrenciGuid { get; set; }

    public string SinifAdi { get; set; }

    public string OgrenciAdi { get; set; }

    public string OgrenciSoyadi { get; set; }

    public DateTime? OgrenciDogumTarihi { get; set; }

    public bool? OgrenciMezunMu { get; set; }

    public int? OgrenciUyruk { get; set; }

    public int? OgrenciCinsiyeti { get; set; }

    public string OgrenciTcKimlikNo { get; set; }

    public string OgrenciKullaniciAdi { get; set; }

    public string DersAdi { get; set; }

    public decimal? DersNotu1 { get; set; }

    public decimal? DersNotu2 { get; set; }

    public decimal? DersNotu3 { get; set; }

    public decimal? DersOrtalamasi { get; set; }

    public int SinifId { get; set; }

    public int? DersId { get; set; }
}
