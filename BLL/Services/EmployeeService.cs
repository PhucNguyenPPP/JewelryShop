using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using BOL;
using DTO;
using Repositories.Interfaces;
using Repositories.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace BLL.Services
{
	public class EmployeeService : IEmployeeService
	{
		private readonly IEmployeeRepository _employeeRepo;
		private readonly IAuthService _authService;
		private readonly IRoleService _roleService;
		public EmployeeService(IEmployeeRepository employeeRepo, IAuthService authService,
			IRoleService roleService) { 
			_employeeRepo = employeeRepo;
			_authService = authService;
			_roleService = roleService;
		}

		private object CheckValidation(Employee employee)
		{
			throw new NotImplementedException();
		}

		public List<Employee> GetAllEmployee()
		{
			var list = _employeeRepo.GetAllEmployees();
			return list;
		}

        public Employee GetEmployee(string id)
		{
			Guid.TryParse(id, out var employeeId);
			var emp = _employeeRepo.GetEmployee(employeeId);
			return emp;
		}

		public List<Employee> SearchEmployees(string search) { 
			var list = _employeeRepo.SearchEmployees(search);
			return list;
		}

		public bool AddEmployee(EmployeeRequestDTO employeeDTO)
		{			
			var employeeId = Guid.NewGuid();
			var roleId = _roleService.GetStaffRole().RoleId;
			Employee employee = new Employee()
			{
				EmployeeId = employeeId,
                EmployeeName = employeeDTO.EmployeeName,
                Email = employeeDTO.Email,
				UserName = employeeDTO.UserName,
                AvatarImg = employeeDTO.AvatarImg,
                Address = employeeDTO.Address,
				Dob = employeeDTO.Dob,
				RoleId = roleId,
				PhoneNumber = employeeDTO.PhoneNumber,
				PasswordHash = _authService.HashPassword(employeeDTO.PasswordHash),
				CounterId = Guid.Parse(employeeDTO.CounterId),
                Status = true
			};
			_employeeRepo.AddEmployee(employee);
			bool result = _employeeRepo.SaveChange();
			return result;	
		}

		public bool UpdateEmployee(EmployeeRequestDTO employeeDTO)
		{
			Guid.TryParse(employeeDTO.EmployeeId, out Guid employeeId);
            Employee? emp = _employeeRepo.GetEmployee(employeeId);
			if (emp == null)
			{
				return false;
			}
			if (!employeeDTO.AvatarImg.IsNullOrEmpty())
			{
				emp.AvatarImg = employeeDTO.AvatarImg;
			}
			emp.EmployeeName = employeeDTO.EmployeeName;	
			emp.Email = employeeDTO.Email;	
			emp.Address = employeeDTO.Address;
			emp.PhoneNumber = employeeDTO.PhoneNumber;
			emp.Dob = employeeDTO.Dob;
			emp.CounterId = Guid.Parse(employeeDTO.CounterId);
			_employeeRepo.UpdateEmployee(emp);
			bool result = _employeeRepo.SaveChange();
			return result;
		}

		public bool DeleteEmployee(string id)
		{
			Guid.TryParse(id, out Guid empId);
			Employee? emp = _employeeRepo.GetEmployee(empId);
			if (emp == null)
			{
				return false;
			}
			emp.Status = false;
			_employeeRepo.UpdateEmployee(emp);
			bool result = _employeeRepo.SaveChange();
			return result;
		}

        public bool CheckUserNameExist(string userName)
        {
            var list = _employeeRepo.GetAllEmployees();
			if(list.Any(c=> c.UserName == userName))
			{
				return true;
			}
			return false;
        }

        public bool CheckEmailExist(string email)
        {
            var list = _employeeRepo.GetAllEmployees();
            if (list.Any(c => c.Email == email))
            {
                return true;
            }
            return false;
        }

        public bool CheckPhoneExist(string phone)
        {
            var list = _employeeRepo.GetAllEmployees();
            if (list.Any(c => c.PhoneNumber == phone))
            {
                return true;
            }
            return false;
        }
    }
}
