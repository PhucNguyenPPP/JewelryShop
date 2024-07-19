using System;
using System.Collections.Generic;

namespace BOL;

public partial class BuyBackOrderDetail
{
    public Guid BbodetailId { get; set; }

    public Guid? PolicyId { get; set; }

    public Guid? Bboid { get; set; }

    public Guid? ProductId { get; set; }

    public decimal? Bbprice { get; set; }

    public int? Amount { get; set; }

    public string? Reason { get; set; }

    public virtual BuyBackOrder? Bbo { get; set; }

    public virtual BuyBackPolicy? Policy { get; set; }

    public virtual Product? Product { get; set; }
}
