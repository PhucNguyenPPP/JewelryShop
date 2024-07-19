using System;
using System.Collections.Generic;

namespace BOL;

public partial class MaterialType
{
    public Guid TypeId { get; set; }

    public string? TypeName { get; set; }

    public virtual ICollection<Material> Materials { get; set; } = new List<Material>();
}
