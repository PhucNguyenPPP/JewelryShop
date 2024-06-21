using System;
using System.Collections.Generic;

namespace BOL;

public partial class Counter
{
    public Guid CounterId { get; set; }

    public string? CounterName { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
