using BLL.Interfaces;
using BOL.DTOs;
using BOL.Entities;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
        private readonly IGenericRepository<Customer> _customerRepo;

        public CustomerService(IGenericRepository<Customer> customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public bool AddCustomer(string customerName, string phoneNumber, string address, string email,
            string Dob, string avatarImg, string employeeId)
        {
            DateTime.TryParse(Dob, out DateTime parseDob);
            Customer newCustomer = new Customer()
            {
                CustomerId = Guid.NewGuid(),
                CustomerName = customerName,
                PhoneNumber = phoneNumber,
                Address = address,
                Email = email,
                Dob = parseDob,
                RegistrationDate = DateTime.Now,
                AvatarImg = avatarImg,
                Status = true,
                EmployeeId = Guid.Parse(employeeId)
            };
            _customerRepo.Add(newCustomer);
            var result = _customerRepo.SaveChange();
            return result;
        }

        public ResponseDTO CheckValidationCustomer(string customerName, string phoneNumber,
            string address, string email, string Dob)
        {
            var customerList = GetAllCustomers();
            if (customerName.IsNullOrEmpty())
            {
                return new ResponseDTO("Please input Customer Name", false);
            }

            Regex nameRegex = new Regex("^[A-Za-z]+$");
            if (nameRegex.IsMatch(customerName) || customerName.Length < 5)
            {
                return new ResponseDTO("Customer Name is not valid", false);
            }

            if (phoneNumber.IsNullOrEmpty())
            {
                return new ResponseDTO("Please input Phone Number", false);
            }

            Regex phoneRegex = new Regex(@"^0");
            if (!phoneRegex.IsMatch(phoneNumber) || phoneNumber.Length != 10)
            {
                return new ResponseDTO("Phone Number is not valid", false);
            }

            if (customerList.Any(c => c.PhoneNumber == phoneNumber))
            {
                return new ResponseDTO("Phone Number already exists", false);
            }

            if (address.IsNullOrEmpty())
            {
                return new ResponseDTO("Please input address", false);
            }

            if (address.Length < 10)
            {
                return new ResponseDTO("Address must have at least 10 characters", false);
            }

            if (email.IsNullOrEmpty())
            {
                return new ResponseDTO("Please input email", false);
            }

            Regex emailRegex = new Regex(@"^[a-zA-Z0-9_.+-]+@gmail\.com$");
            if (!emailRegex.IsMatch(email))
            {
                return new ResponseDTO("Email is not valid", false);
            }

            if (customerList.Any(c => c.Email == email))
            {
                return new ResponseDTO("Email already exists", false);
            }

            if (Dob.IsNullOrEmpty())
            {
                return new ResponseDTO("Please input date of birth", false);
            }
            return new ResponseDTO("Check Validation Successfully", true);
        }

        public ResponseDTO CheckValidationUpdateCustomer(string customerId, string customerName, string phoneNumber, string address, string email, string Dob)
        {
            var customerList = GetAllCustomers();
            var currentCustomer = _customerRepo.GetById(Guid.Parse(customerId));
            if (customerName.IsNullOrEmpty())
            {
                return new ResponseDTO("Please input Customer Name", false);
            }

            Regex nameRegex = new Regex("^[A-Za-z]+$");
            if (!nameRegex.IsMatch(customerName) || customerName.Length < 5)
            {
                return new ResponseDTO("Customer Name is not valid", false);
            }

            if (phoneNumber.IsNullOrEmpty())
            {
                return new ResponseDTO("Please input Phone Number", false);
            }

            Regex phoneRegex = new Regex(@"^0");
            if (!phoneRegex.IsMatch(phoneNumber) || phoneNumber.Length != 10)
            {
                return new ResponseDTO("Phone Number is not valid", false);
            }

            if (customerList.Any(c => c.PhoneNumber == phoneNumber))
            {
                if(currentCustomer.PhoneNumber != phoneNumber)
                {
                    return new ResponseDTO("Phone Number already exists", false);
                }
            }

            if (address.IsNullOrEmpty())
            {
                return new ResponseDTO("Please input address", false);
            }

            if (address.Length < 10)
            {
                return new ResponseDTO("Address must have at least 10 characters", false);
            }

            if (email.IsNullOrEmpty())
            {
                return new ResponseDTO("Please input email", false);
            }

            Regex emailRegex = new Regex(@"^[a-zA-Z0-9_.+-]+@gmail\.com$");
            if (!emailRegex.IsMatch(email))
            {
                return new ResponseDTO("Email is not valid", false);
            }

            if (customerList.Any(c => c.Email == email))
            {
                if(currentCustomer.Email != email)
                {
                    return new ResponseDTO("Email already exists", false);
                }
            }

            if (Dob.IsNullOrEmpty())
            {
                return new ResponseDTO("Please input date of birth", false);
            }
            return new ResponseDTO("Check Validation Successfully", true);
        }

        public bool DeleteCustomer(string customerId)
        {
            var customer = _customerRepo.GetById(Guid.Parse(customerId));
            if (customer == null)
            {
                return false;
            }
            customer.Status = false;

            _customerRepo.Delete(customer);
            var result = _customerRepo.SaveChange();
            return result;
        }

        public List<Customer> GetAllCustomers()
        {
            return _customerRepo.GetAll(c => c.Status == true).Include(c => c.Employee).ToList();
        }

        public Customer GetCustomer(Guid customerID)
        {
            var customer = _customerRepo.GetAll(c => c.Status == true && c.CustomerId == customerID).Include(c => c.Employee).FirstOrDefault();
            if (customer != null)
            {
                return customer;
            }
            return new Customer();
        }

        public List<Customer> SearchCustomers(string searchValue)
        {
            return _customerRepo.GetAll(c => (c.CustomerName.ToLower().Contains(searchValue.ToLower())
				|| c.Email.ToLower().Contains(searchValue.ToLower())
				|| c.PhoneNumber.Contains(searchValue)))
                .Include(c => c.Employee)
                .ToList();
        }

        public bool UpdateCustomer(string customerId, string customerName, string phoneNumber, string address, 
            string email, string Dob, string avatarImg)
        {
            var customer = _customerRepo.GetById(Guid.Parse(customerId));
            if(customer == null)
            {
                return false;
            }
            customer.CustomerName = customerName;
            customer.Email = email;
            customer.PhoneNumber = phoneNumber;
            customer.Address = address;
            customer.Email = email;
            customer.Dob = DateTime.Parse(Dob);
            customer.AvatarImg = avatarImg;

            _customerRepo.Update(customer);
            var result = _customerRepo.SaveChange();
            return result;
        }
    }
}

