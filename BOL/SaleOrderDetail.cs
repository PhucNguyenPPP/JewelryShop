﻿using System;
using System.Collections.Generic;

namespace BOL;

public partial class SaleOrderDetail
{
    public Guid SaleOrderDetailId { get; set; }

    public Guid? SaleOrderId { get; set; }

    public Guid? ProductId { get; set; }

    public int? Amount { get; set; }

    public decimal? TotalPrice { get; set; }

    public decimal? FinalPrice { get; set; }

    public virtual Product? Product { get; set; }

    public virtual SaleOrder? SaleOrder { get; set; }
}
