using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DTO;

namespace BLL.Interfaces
{
	public interface IEmployeeService
	{
		List<Employee> GetAllEmployee();
        List<Employee> SearchEmployees(string search);
		Employee GetEmployee (string id);	
		bool AddEmployee (EmployeeRequestDTO employeeDTO);
		bool UpdateEmployee(EmployeeRequestDTO employeeDTO);
		bool DeleteEmployee (string id);
		bool CheckUserNameExist(string userName);
		bool CheckEmailExist(string email);
		bool CheckPhoneExist(string phone);
	}
}
