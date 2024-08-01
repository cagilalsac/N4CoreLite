using System;
using System.Collections.Generic;
using N4CoreLite.Records.Bases;

namespace DataAccess.Entities;

#nullable disable
public partial class Ders : Record
{
    public string Adi { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<OgrenciDers> OgrenciDers { get; set; } = new List<OgrenciDers>();
}
