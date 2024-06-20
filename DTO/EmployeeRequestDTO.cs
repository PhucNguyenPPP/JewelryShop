using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
	public class EmployeeRequestDTO
	{
		public string? EmployeeId { get; set; }
		[Required(ErrorMessage = "Please input employee name!")]
		[RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Employee name can not include digits and special characters!")]
		public string? EmployeeName { get; set; }
		[Required(ErrorMessage = "Please input username!")]
		[RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "User name can not include blank spaces and special characters!")]
		public string? UserName { get; set; }
		[Required(ErrorMessage = "Please input password!")]
		public byte[]? PasswordHash { get; set; }
		[Required(ErrorMessage = "Please input phone number!")]
		public string? PhoneNumber { get; set; }
		[Required(ErrorMessage = "Please input address!")]
		public string? Address { get; set; }
		[Required(ErrorMessage = "Please input email!")]
		[RegularExpression("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\\.[a-zA-Z0-9-]+)*$", ErrorMessage = "Please input the correct email!")]
		public string? Email { get; set; }
		[Required(ErrorMessage = "Please input Dob!")]
		[RegularExpression("^\\d{4}-(0[1 - 9]|1[0 - 2])-(0[1 - 9]|[12][0 - 9]|3[01])$", ErrorMessage = "Please input the correct format: yyyy-mm-dd!")]

		public DateTime? Dob { get; set; }
		public string? AvatarImg { get; set; }
		[Required(ErrorMessage = "Please input Role!")]
		public string? RoleId { get; set; }
		public bool? Status { get; set; }
	}
}
