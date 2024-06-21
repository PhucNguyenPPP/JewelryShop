using BOL;
using DAL.DAO;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IGenericDAO<Employee> _employeeDao;
        public EmployeeRepository(IGenericDAO<Employee> employeeDao)
        {

            _employeeDao = employeeDao;

        }

		public void AddEmployee(Employee employee)
		{
			_employeeDao.Add(employee);
		}

		public List<Employee> GetAllEmployees()
        {
            return _employeeDao.GetAll(c => c.Status == true)
				.Include(c => c.Role)
                .Include(c => c.Counter)
                .Where(c => c.Role.RoleName == "Staff")
				.ToList();
        }

        public List<Employee> GetAllEmployeesForLogin()
        {
            return _employeeDao.GetAll(c => c.Status == true).Include(c => c.Role).Include(c => c.Counter).ToList();
        }

        public bool SaveChange()
		{
			return _employeeDao.SaveChange();
		}


		public Employee GetEmployee(Guid id)
		{
            return _employeeDao.GetById(id);
		}

		public List<Employee> SearchEmployees(string search)
		{
			return _employeeDao.GetAll(c => c.Status == true && c.EmployeeName.Contains(search))
				.Include(c => c.Role)
				.Include(c => c.Counter)
                .Where(c => c.Role.RoleName == "Staff")
                .ToList();
		}

		public void UpdateEmployee(Employee emp)
		{
			_employeeDao.Update(emp);
		}
	}
}
