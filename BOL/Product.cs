using System;
using System.Collections.Generic;

namespace BOL;

public partial class Product
{
    public Guid ProductId { get; set; }

    public string? ProductName { get; set; }

    public decimal? Price { get; set; }

    public int? AmountInStock { get; set; }

    public string? AvatarImg { get; set; }

    public Guid? CounterId { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<BuyBackOrderDetail> BuyBackOrderDetails { get; set; } = new List<BuyBackOrderDetail>();

    public virtual Counter? Counter { get; set; }

    public virtual ICollection<MaterialProduct> MaterialProducts { get; set; } = new List<MaterialProduct>();

    public virtual ICollection<ReturnOrderDetail> ReturnOrderDetails { get; set; } = new List<ReturnOrderDetail>();

    public virtual ICollection<SaleOrderDetail> SaleOrderDetails { get; set; } = new List<SaleOrderDetail>();
}
