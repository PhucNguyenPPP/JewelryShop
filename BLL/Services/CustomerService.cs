using AutoMapper;
using BLL.Interfaces;
using BOL;
using DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepo, IMapper mapper)
        {
            _customerRepo = customerRepo;
            _mapper = mapper;
        }

        public bool AddCustomer(CustomerResquestDTO dto)
        {
            DateTime.TryParse(dto.Dob, out DateTime parseDob);
            Customer newCustomer = new Customer()
            {
                CustomerId = Guid.NewGuid(),
                CustomerName = dto.CustomerName,
                PhoneNumber = dto.PhoneNumber,
                Address = dto.Address,
                Email = dto.Email,
                Dob = parseDob,
                RegistrationDate = DateTime.Now,
                AvatarImg = dto.AvatarImg,
                Status = true,
                EmployeeId = Guid.Parse(dto.EmployeeId)
            };
            _customerRepo.AddCustomer(newCustomer);
            var result = _customerRepo.SaveChange();
            return result;
        }

        public bool CheckEmailAlreadyExists(string email)
        {
            var customerList = _customerRepo.GetAllCustomers().ToList();
            var result = customerList.Any(x => x.Email == email);
            if(result)
            {
                return true;
            }
            return false;
        }

        public bool CheckPhoneAlreadyExists(string phoneNumber)
        {
            var customerList = _customerRepo.GetAllCustomers().ToList();
            var result = customerList.Any(x => x.PhoneNumber == phoneNumber);
            if (result)
            {
                return true;
            }
            return false;
        }

        public bool DeleteCustomer(string customerId)
        {
            var customerList = _customerRepo.GetAllCustomers().ToList();
            var customer = customerList.FirstOrDefault(c => c.CustomerId == Guid.Parse(customerId));
            if (customer == null)
            {
                return false;
            }
            customer.Status = false;

            _customerRepo.UpdateCustomer(customer);
            var result = _customerRepo.SaveChange();
            return result;
        }

        public List<Customer> GetAllCustomers()
        {
            return _customerRepo.GetAllCustomers().OrderByDescending(c => c.RegistrationDate).ToList();
        }

        public Customer GetCustomer(string customerID)
        {
            Guid.TryParse(customerID, out Guid parseCustomerId);
            var customer = _customerRepo.GetById(parseCustomerId);
            if (customer != null)
            {
                return customer;
            }
            return new Customer();
        }


        public List<Customer> SearchCustomers(string searchValue)
        {
            var customerList = _customerRepo.GetAllCustomers().ToList();

            return customerList.Where(c => (c.CustomerName.ToLower().Contains(searchValue.ToLower())
                || c.Email.ToLower().Contains(searchValue.ToLower())
                || c.PhoneNumber.Contains(searchValue))).ToList();
        }

        public bool UpdateCustomer(CustomerResquestDTO dto)
        {
            Guid.TryParse(dto.CustomerId, out Guid parseCustomerId);
            var customer = _customerRepo.GetById(parseCustomerId);
            if (customer == null)
            {
                return false;
            }
            if(!dto.AvatarImg.IsNullOrEmpty())
            {
                customer.AvatarImg = dto.AvatarImg;
            }
            customer.CustomerName = dto.CustomerName;
            customer.Email = dto.Email;
            customer.PhoneNumber =dto.PhoneNumber;
            customer.Address = dto.Address;
            customer.Dob = DateTime.Parse(dto.Dob);

            _customerRepo.UpdateCustomer(customer);
            var result = _customerRepo.SaveChange();
            return result;
        }
    }
}

