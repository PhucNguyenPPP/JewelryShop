using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class MaterialRequestDTO
    {
        public string? MaterialId { get; set; }

        public string? MaterialName { get; set; }

        public string? MaterialTypeId { get; set; }

        public string? Price { get; set; }

        public string? AmountInStock { get; set; }
    }
}
