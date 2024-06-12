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
    public class CustomerRepository : ICustomerRepository
    {
        IGenericDAO<Customer> _customerDao;
        public CustomerRepository(IGenericDAO<Customer> customerDao)
        {
            _customerDao = customerDao;
        }
        public void AddCustomer(Customer customer)
        {
            _customerDao.Add(customer);
        }

        public List<Customer> GetAllCustomers()
        {
            return _customerDao.GetAll(c => c.Status == true).Include(c => c.Employee).ToList();
        }

        public void UpdateCustomer(Customer customer)
        {
            _customerDao.Update(customer);
        }

        public bool SaveChange()
        {
            return _customerDao.SaveChange();
        }

        public Customer GetById(Guid id)
        {
            return _customerDao.GetById(id);
        }
    }
}
