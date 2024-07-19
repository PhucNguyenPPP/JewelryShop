using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PriceRateRequestDTO
    {
        [Required]
        [RegularExpression("^\\d+(\\.\\d+)?$", ErrorMessage ="Price rate value is invalid!")]
        public string? Value { get; set; }

    }
}
