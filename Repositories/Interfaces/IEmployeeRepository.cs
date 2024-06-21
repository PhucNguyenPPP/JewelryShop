using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
		void AddEmployee(Employee employee);
		List<Employee> GetAllEmployees();
		List<Employee> GetAllEmployeesForLogin();
        Employee GetEmployee(Guid id);
		List<Employee> SearchEmployees(string search);
		bool SaveChange();
		void UpdateEmployee(Employee emp);
	}
}
