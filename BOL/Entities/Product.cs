using System;
using System.Collections.Generic;

namespace BOL.Entities;

public partial class Product
{
    public Guid ProductId { get; set; }

    public string? ProductName { get; set; }

    public double? Price { get; set; }

    public int? AmountInStock { get; set; }

    public virtual ICollection<BuyBackOrderDetail> BuyBackOrderDetails { get; set; } = new List<BuyBackOrderDetail>();

    public virtual ICollection<MaterialProduct> MaterialProducts { get; set; } = new List<MaterialProduct>();

    public virtual ICollection<ProductCounter> ProductCounters { get; set; } = new List<ProductCounter>();

    public virtual ICollection<SaleOrderDetail> SaleOrderDetails { get; set; } = new List<SaleOrderDetail>();
}
