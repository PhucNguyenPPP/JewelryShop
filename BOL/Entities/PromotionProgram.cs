using System;
using System.Collections.Generic;

namespace BOL.Entities;

public partial class PromotionProgram
{
    public Guid PromotionProgramId { get; set; }

    public string? PromotionProgramName { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ExpiredDate { get; set; }

    public virtual ICollection<PromotionProgramCode> PromotionProgramCodes { get; set; } = new List<PromotionProgramCode>();
}
