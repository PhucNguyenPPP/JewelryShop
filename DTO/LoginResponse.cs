using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class LoginResponse
    {
        public Guid? EmployeeId { get; set; }
        public string? UserName { get; set; }
        public string? RoleName { get; set; }
        public bool IsSuccess { get; set; }
    }
}
