using System;
using System.Collections.Generic;

namespace BOL.Entities;

public partial class SaleOrder
{
    public Guid SaleOrderId { get; set; }

    public double? TotalPrice { get; set; }

    public double? FinalPrice { get; set; }

    public DateTime? CreatedDate { get; set; }

    public Guid? PolicyId { get; set; }

    public Guid? PromotionCodeId { get; set; }

    public Guid? CustomerId { get; set; }

    public Guid? EmployeeId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ReturnPolicy? Policy { get; set; }

    public virtual PromotionProgramCode? PromotionCode { get; set; }

    public virtual ICollection<SaleOrderDetail> SaleOrderDetails { get; set; } = new List<SaleOrderDetail>();
}
