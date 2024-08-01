using System;
using System.Collections.Generic;
using N4CoreLite.Records.Bases;

namespace DataAccess.Entities;

#nullable disable
public partial class OgrenciDers : Record
{
    public int OgrenciId { get; set; }

    public int DersId { get; set; }

    public decimal? Not1 { get; set; }

    public decimal? Not2 { get; set; }

    public decimal? Not3 { get; set; }

    public virtual Ders Ders { get; set; }

    public virtual Ogrenci Ogrenci { get; set; }
}
