using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ReturnRequestDTO
    {
        public string SaleOrderId { get; set; }
        public List<string> ProductIds { get; set; }
        public List<string> Amount {  get; set; }
        public List<string> Reason { get; set; }
    }
}
