using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CustomerResquestDTO
    {
        public string? CustomerId { get; set; }
        [Required(ErrorMessage = "Please input Customer.")]
        [MaxLength(30, ErrorMessage = "Customer name can not exceed 30 characters.")]
        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Customer Name is invalid.")]
        public string? CustomerName { get; set; }

        [Required(ErrorMessage = "Please input PhoneNumber.")]
        [StringLength(10, ErrorMessage = "PhoneNumber number is invalid")]
        [RegularExpression("^0\\d{9}$", ErrorMessage = "PhoneNumber number is invalid.")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please input Email.")]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Email is invalid.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please input Address.")]
        [MaxLength(100, ErrorMessage = "Address can not exceed 100 characters.")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Please input Dob.")]
        public string? Dob { get; set; }
        public string? AvatarImg { get; set; }
        public string? EmployeeId { get; set;}
    }
}
