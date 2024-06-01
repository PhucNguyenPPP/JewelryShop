using System;
using System.Collections.Generic;

namespace BOL.Entities;

public partial class CounterEmployee
{
    public Guid CounterEmployeeId { get; set; }

    public Guid? CounterId { get; set; }

    public Guid? EmployeeId { get; set; }

    public DateTime? WorkingDate { get; set; }

    public bool? Status { get; set; }

    public virtual Counter? Counter { get; set; }

    public virtual Employee? Employee { get; set; }
}
