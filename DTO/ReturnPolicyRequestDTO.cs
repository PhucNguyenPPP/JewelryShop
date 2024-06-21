using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ReturnPolicyRequestDTO
    {
        public string? PolicyId { get; set; }
        public string? PolicyDescription { get; set; }
        public string? PolicyValue   { get; set; }
    }
}
