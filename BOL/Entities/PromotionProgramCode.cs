using System;
using System.Collections.Generic;

namespace BOL.Entities;

public partial class PromotionProgramCode
{
    public Guid PromotionCodeId { get; set; }

    public string? PromotionCodeName { get; set; }

    public decimal? DiscountPercentage { get; set; }

    public bool? Status { get; set; }

    public Guid? PromotionProgramId { get; set; }

    public virtual PromotionProgram? PromotionProgram { get; set; }

    public virtual ICollection<SaleOrder> SaleOrders { get; set; } = new List<SaleOrder>();
}
