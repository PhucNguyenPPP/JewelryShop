using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SaleOrderRequestDTO
    {
        public string? CustomerId { get; set; }
        public List<SaleOrderDetailDTO>? SaleOrderDetails { get; set; }

        public string? PromotionCode {  get; set; } 
    }
}
