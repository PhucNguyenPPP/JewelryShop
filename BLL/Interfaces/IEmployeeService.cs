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
		List<Employee> GetAllEmPloyee();
		List<Employee> SearchEmployees(string search);
		Employee GetEmployee (Guid id);	
		bool AddEmployee (EmployeeRequestDTO employeeDTO);
		bool UpdateEmployee(EmployeeRequestDTO employeeDTO);
		bool DeleteEmployee (string id);
	}
}
