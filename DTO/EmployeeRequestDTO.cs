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
		[RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "User name can not include blank spaces and special characters!")]
		public string? UserName { get; set; }
		[MinLength(6, ErrorMessage = "Password must have at least 6 characters")]
		public string? PasswordHash { get; set; }

		[Required(ErrorMessage = "Please input phone number!")]
        [RegularExpression("^0\\d{9}$", ErrorMessage = "PhoneNumber number is invalid.")]
        public string? PhoneNumber { get; set; }

		[Required(ErrorMessage = "Please input address!")]
		public string? Address { get; set; }
		[Required(ErrorMessage = "Please input email!")]
		[RegularExpression("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\\.[a-zA-Z0-9-]+)*$", ErrorMessage = "Please input the correct email!")]
		public string? Email { get; set; }
		[Required(ErrorMessage = "Please input Dob!")]
		public DateTime? Dob { get; set; }
		public string? AvatarImg { get; set; }
		public string? CounterId { get; set; }
		public bool? Status { get; set; }
	}
}
