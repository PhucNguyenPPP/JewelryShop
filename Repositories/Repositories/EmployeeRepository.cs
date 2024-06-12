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
        public List<Employee> GetAllEmployees()
        {
            return _employeeDao.GetAll(c => c.Status == true).Include(c => c.Role).ToList();
        }
    }
}
