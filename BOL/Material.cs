using System;
using System.Collections.Generic;

namespace BOL;

public partial class Material
{
    public Guid MaterialId { get; set; }

    public string? MaterialName { get; set; }

    public Guid? MaterialTypeId { get; set; }

    public decimal? Price { get; set; }

    public decimal? AmountInStock { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<MaterialProduct> MaterialProducts { get; set; } = new List<MaterialProduct>();

    public virtual MaterialType? MaterialType { get; set; }
}
