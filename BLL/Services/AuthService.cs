using BLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System.Security.Cryptography;
using System.Text;
using DTO;
using BOL;

namespace BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IEmployeeRepository _employeeRepo;
        public AuthService(IEmployeeRepository employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        public LoginResponse CheckLogin(string username, string password)
        {
            var employeeList = _employeeRepo.GetAllEmployeesForLogin().ToList();
            var employee = employeeList.FirstOrDefault(c => c.UserName == username
            && c.Status == true);
            if (employee == null)
            {
                return new LoginResponse()
                {
                    IsSuccess = false,
                };
            }
            var passwordHash = HashPassword(password);
            var comparePassword = CompareByteArrays(passwordHash, employee.PasswordHash);
            if (comparePassword)
            {
				return new LoginResponse()
				{
                    EmployeeId = employee.EmployeeId,
                    UserName = employee.UserName,
                    RoleName = employee.Role.RoleName,
					IsSuccess = true,
				};
			}
			return new LoginResponse()
			{
				IsSuccess = false,
			};

		}

        public byte[] HashPassword(string password)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return hashedBytes;
        }

        public bool CompareByteArrays(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
            {
                return false;
            }
            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
