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
		public EmployeeService(IEmployeeRepository employeeRepo) { 
			_employeeRepo = employeeRepo;

		}

		private object CheckValidation(Employee employee)
		{
			throw new NotImplementedException();
		}

		public List<Employee> GetAllEmPloyee()
		{
			var list = _employeeRepo.GetAllEmployees();
			return list;
		}

		public Employee GetEmployee(Guid id)
		{
			var emp = _employeeRepo.GetEmployee(id);
			return emp;
		}

		public List<Employee> SearchEmployees(string search) { 
			var list = _employeeRepo.SearchEmployees(search);
			return list;
		}

		public bool AddEmployee(EmployeeRequestDTO employeeDTO)
		{
			Guid.TryParse(employeeDTO.RoleId, out Guid roleId);
			var employeeId = Guid.NewGuid();
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
				PasswordHash = employeeDTO.PasswordHash,				
                Status = true
			};
			_employeeRepo.AddEmployee(employee);
			bool result = _employeeRepo.SaveChange();
			return result;	
		}

		public bool UpdateEmployee(EmployeeRequestDTO employeeDTO)
		{
			Guid.TryParse(employeeDTO.EmployeeId, out Guid employeeId);
            Guid.TryParse(employeeDTO.RoleId, out Guid roleId);
			Employee? emp = _employeeRepo.GetEmployee(employeeId);
			if (emp == null)
			{
				return false;
			}
			if (!employeeDTO.AvatarImg.IsNullOrEmpty())
			{
				emp.AvatarImg = employeeDTO.AvatarImg;
			}
			emp.UserName = employeeDTO.UserName;
			emp.EmployeeName = employeeDTO.EmployeeName;	
			emp.Email = employeeDTO.Email;	
			emp.Address = employeeDTO.Address;
			emp.PhoneNumber = employeeDTO.PhoneNumber;
			emp.Dob = employeeDTO.Dob;
			emp.RoleId = roleId;
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
	}
}
