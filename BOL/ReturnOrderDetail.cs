using System;
using System.Collections.Generic;

namespace BOL;

public partial class ReturnOrderDetail
{
    public Guid ReturnOrderDetailId { get; set; }

    public Guid? ReturnOrderId { get; set; }

    public Guid? ProductId { get; set; }

    public decimal? ReturnPrice { get; set; }

    public int? Amount { get; set; }

    public string? Reason { get; set; }

    public virtual Product? Product { get; set; }

    public virtual ReturnOrder? ReturnOrder { get; set; }
}
