using System;
using System.Collections.Generic;

namespace BOL.Entities;

public partial class Material
{
    public Guid MaterialId { get; set; }

    public string? MaterialName { get; set; }

    public virtual ICollection<MaterialProduct> MaterialProducts { get; set; } = new List<MaterialProduct>();
}
