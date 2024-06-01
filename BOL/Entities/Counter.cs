using System;
using System.Collections.Generic;

namespace BOL.Entities;

public partial class Counter
{
    public Guid CounterId { get; set; }

    public string? CounterName { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<CounterEmployee> CounterEmployees { get; set; } = new List<CounterEmployee>();

    public virtual ICollection<ProductCounter> ProductCounters { get; set; } = new List<ProductCounter>();
}
