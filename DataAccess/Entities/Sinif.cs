using System;
using System.Collections.Generic;
using N4CoreLite.Records.Bases;

namespace DataAccess.Entities;

#nullable disable
public partial class Sinif : Record
{
    public string Adi { get; set; }

    public DateTime? CreateDate { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string UpdatedBy { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Ogrenci> Ogrenci { get; set; } = new List<Ogrenci>();
}
