using BLL.Interfaces;
using BOL.DTOs;
using BOL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace PRN221_JewelryShop.Pages.Staff
{
    public class CustomerManagementModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly IImageService _imageService;
        public CustomerManagementModel(ICustomerService customerService, IImageService imageService)
        {
            _customerService = customerService;
            _imageService = imageService;

            CustomerList = _customerService.GetAllCustomers();
        }
        public List<Customer> CustomerList {  get; set; }
        public string? ErrorMsgCreateCustomer { get; set; }
        public string? NotificationMessage {  get; set; }

        [BindProperty]
        public CreateCustomerResquestDTO CreateCustomerResquestDTO { get; set;}
        [BindProperty]
        public IFormFile CustomerAvatar { get; set; }

        public LoginResponse LoginResponse { get; set; }
        public IActionResult OnGet()
        {
            var loginResponseString = HttpContext.Session.GetString("LoginResponse");
            if (loginResponseString == null)
            {
                return RedirectToPage("/Login");
            };

            LoginResponse = JsonSerializer.Deserialize<LoginResponse>(loginResponseString);
            if (LoginResponse.RoleName != "Staff")
            {
                return RedirectToPage("/Login");
            }
            return Page();
        }

        private bool GetLoginResponse()
        {
            var loginResponseString = HttpContext.Session.GetString("LoginResponse");
            if (loginResponseString == null)
            {
                return false;
            }

            LoginResponse = JsonSerializer.Deserialize<LoginResponse>(loginResponseString);
            return true;
        }

        public void OnPostCreateCustomer()
        {
            if (!GetLoginResponse())
            {
                return;
            }
            var customerName = CreateCustomerResquestDTO.CustomerName;
            var address = CreateCustomerResquestDTO.Address;
            var phone = CreateCustomerResquestDTO.Phone;
            var email = CreateCustomerResquestDTO.Email;
            var birthdate = CreateCustomerResquestDTO.Birthdate.ToString("yyyy/MM/dd");
            var checkValidation = _customerService.CheckValidationCustomer(customerName, phone, address, email, birthdate);
            if (!checkValidation.IsSuccess)
            {
                ErrorMsgCreateCustomer = checkValidation.Message;
                return;
            }

            string avatarBase64 = _imageService.ConvertToBase64(CustomerAvatar);
            var createCustomer = _customerService.AddCustomer(customerName, phone, address, email, birthdate, avatarBase64, LoginResponse.EmployeeId.ToString());
            if(createCustomer){
                CustomerList = _customerService.GetAllCustomers();
                NotificationMessage = "Add Customer Successfully";
                return;
            } else
            {
                CustomerList = _customerService.GetAllCustomers();
                NotificationMessage = "Add Customer Successfully";
                return;
            }

        }

    }
}
