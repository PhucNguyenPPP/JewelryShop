using System;
using System.Collections.Generic;

namespace BOL;

public partial class MaterialProduct
{
    public Guid MaterialProductId { get; set; }

    public decimal? MaterialSize { get; set; }

    public Guid? MaterialId { get; set; }

    public Guid? ProductId { get; set; }

    public virtual Material? Material { get; set; }

    public virtual Product? Product { get; set; }
}
