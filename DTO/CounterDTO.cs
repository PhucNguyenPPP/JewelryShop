using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
	public class CounterDTO
	{
		public string? CounterId { get; set; }

		[Required(ErrorMessage = "Please input counter name!")]
		[RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "Counter name can not include special character!")]
		public string? CounterName { get; set; }

		public List <EmployeeRequestDTO> Employees { get; set; }

		public List<ProductRequestDTO> Products { get; set; }
	}
}
