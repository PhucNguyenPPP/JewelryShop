using System;
using System.Collections.Generic;

namespace BOL;

public partial class ReturnOrder
{
    public Guid ReturnOrderId { get; set; }

    public decimal? TotalPrice { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? CustomerId { get; set; }

    public Guid? EmployeeId { get; set; }

    public Guid? SaleOrderId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual SaleOrder? SaleOrder { get; set; }
}
