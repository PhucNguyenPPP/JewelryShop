using System;
using System.Collections.Generic;

namespace BOL;

public partial class BuyBackPolicy
{
    public Guid PolicyId { get; set; }

    public string? PolicyName { get; set; }

    public string? PolicyDescription { get; set; }

    public int? PolicyValue { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<BuyBackOrderDetail> BuyBackOrderDetails { get; set; } = new List<BuyBackOrderDetail>();

    public virtual ICollection<ReturnOrderDetail> ReturnOrderDetails { get; set; } = new List<ReturnOrderDetail>();
}
