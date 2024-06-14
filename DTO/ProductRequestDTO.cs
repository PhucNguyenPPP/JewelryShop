using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DTO
{
    public class ProductRequestDTO
    {
        public string? ProductId { get; set; }

        [Required(ErrorMessage ="Please input product name!")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage ="Product name can not include special character!")]
        public string? ProductName { get; set; }

        [Required(ErrorMessage ="Please input product price!")]
        [Range(1, (double)decimal.MaxValue, ErrorMessage ="Product price must be at greater than 1!")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage ="Please input product ammount!")]
        [Range(1, int.MaxValue, ErrorMessage ="Product ammount must be greater than 1!")]
        public int? AmountInStock { get; set; }


        public string? AvatarImg { get; set; }


        [Required(ErrorMessage ="Please choose counter!")]
        public string? CounterId { get; set; }


        public List<MaterialDTO>? MaterialDTOs {  get; set; }
    }
}
