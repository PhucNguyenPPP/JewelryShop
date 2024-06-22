using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BuyBackPolicyRequestDTO
    {
        public string? PolicyId { get; set; }

        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage ="Policy Name can not have special characters or digits")]
        public string? PolicyName { get; set; }

        public string? PolicyDescription { get; set; }

        public int? PolicyValue { get; set; }
    }
}
