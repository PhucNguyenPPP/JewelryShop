using System;
using System.Collections.Generic;

namespace BOL;

public partial class ReturnPolicy
{
    public Guid PolicyId { get; set; }

    public string? PolicyName { get; set; }

    public string? PolicyDescription { get; set; }

    public int? PolicyValue { get; set; }

    public bool? Status { get; set; }
}
