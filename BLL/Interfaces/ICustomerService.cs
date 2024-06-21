using BOL;
using DTO;

namespace BLL.Interfaces
{
    public interface ICustomerService
    {
        List<Customer> GetAllCustomers();
        List<Customer> SearchCustomers(string searchValue);
        bool AddCustomer(CustomerResquestDTO dto);
        bool UpdateCustomer(CustomerResquestDTO dto);
        bool DeleteCustomer(string customerId);
        Customer GetCustomer(string customerID);
        bool CheckPhoneAlreadyExists(string phoneNumber);
        bool CheckEmailAlreadyExists(string email);
        Customer SearchCustomerByEmailOrPhone(string searchValue);
    }
}
