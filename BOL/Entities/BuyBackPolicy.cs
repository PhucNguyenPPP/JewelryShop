using System;
using System.Collections.Generic;

namespace BOL.Entities;

public partial class BuyBackPolicy
{
    public Guid PolicyId { get; set; }

    public string? PolicyName { get; set; }

    public string? PolicyDescription { get; set; }

    public virtual ICollection<BuyBackOrderDetail> BuyBackOrderDetails { get; set; } = new List<BuyBackOrderDetail>();

    public virtual ICollection<BuyBackOrder> BuyBackOrders { get; set; } = new List<BuyBackOrder>();
}
