using BLL.Interfaces;
using BOL.DTOs;
using BOL.Entities;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IGenericRepository<Employee> _employeeRepo;
        public AuthService(IGenericRepository<Employee> employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        public LoginResponse CheckLogin(string username, string password)
        {
            var employee = _employeeRepo.GetAll(c => c.UserName == username)
                .Include(e => e.Role)
                .FirstOrDefault();
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
