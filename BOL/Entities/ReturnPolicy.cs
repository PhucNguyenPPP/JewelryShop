using System;
using System.Collections.Generic;

namespace BOL.Entities;

public partial class ReturnPolicy
{
    public Guid PolicyId { get; set; }

    public string? PolicyName { get; set; }

    public string? PolicyDescription { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<SaleOrder> SaleOrders { get; set; } = new List<SaleOrder>();
}
