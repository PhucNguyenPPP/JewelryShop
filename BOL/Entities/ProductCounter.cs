using System;
using System.Collections.Generic;

namespace BOL.Entities;

public partial class ProductCounter
{
    public Guid ProductCounterId { get; set; }

    public Guid? ProductId { get; set; }

    public Guid? CounterId { get; set; }

    public bool? Status { get; set; }

    public virtual Counter? Counter { get; set; }

    public virtual Product? Product { get; set; }
}
