﻿using System;
using System.Collections.Generic;

namespace BOL.Entities;

public partial class BuyBackOrder
{
    public Guid Bboid { get; set; }

    public double? TotalPrice { get; set; }

    public DateTime? CreatedDate { get; set; }

    public Guid? CustomerId { get; set; }

    public Guid? BbpolicyId { get; set; }

    public virtual BuyBackPolicy? Bbpolicy { get; set; }

    public virtual ICollection<BuyBackOrderDetail> BuyBackOrderDetails { get; set; } = new List<BuyBackOrderDetail>();

    public virtual Customer? Customer { get; set; }
}
