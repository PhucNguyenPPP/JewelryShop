using BOL.DTOs;
using BOL.Entities;

namespace BLL.Interfaces
{
    public interface ICustomerService
    {
        List<Customer> GetAllCustomers();
        List<Customer> SearchCustomers(string searchValue);
        ResponseDTO CheckValidationCustomer(string customerName, string phoneNumber, string address,
            string email, string Dob);
        ResponseDTO CheckValidationUpdateCustomer(string customerId, string customerName, string phoneNumber, string address,
           string email, string Dob);
        bool AddCustomer(string customerName, string phoneNumber, string address,
            string email, string Dob, string avatarImg, string employeeId);
        bool UpdateCustomer(string customerId, string customerName, string phoneNumber, string address,
            string email, string Dob, string avatarImg);
        bool DeleteCustomer(string customerId);
        Customer GetCustomer(Guid customerID);
    }
}
