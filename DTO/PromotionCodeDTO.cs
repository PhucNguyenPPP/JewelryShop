using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PromotionCodeDTO
    {
        public string? PromotionCodeId { get; set; }

        public string? PromotionCodeName { get; set; }

        public decimal? DiscountPercentage { get; set; }

        public string? PromotionProgramId { get; set; }
    }
}
