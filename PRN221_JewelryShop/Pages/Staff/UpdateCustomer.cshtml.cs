using BLL.Interfaces;
using BOL;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace PRN221_JewelryShop.Pages.Staff
{
    public class UpdateCustomerModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly IImageService _imageService;
        public UpdateCustomerModel(ICustomerService customerService, IImageService imageService)
        {
            _customerService = customerService;
            _imageService = imageService;
        }

        [BindProperty]
        public CustomerResquestDTO CustomerResquestDTO { get; set; }
        [BindProperty]
        public IFormFile CustomerAvatar { get; set; }

        public LoginResponse LoginResponse { get; set; }
        public string ErrorMsgUpdateCustomer { get; set; }
        public string NotificationMessage {  get; set; }

        public Customer Customer { get; set; }

        [FromQuery(Name = "id")]
        public string CustomerId {  get; set; }

        [BindProperty]
        public string CustomerIdForm { get; set; }
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
            var customer = _customerService.GetCustomer(CustomerId);
            if (customer != null)
            {
                Customer = customer;
            }
            return Page();
        }

        public IActionResult OnPostUpdateCustomer()
        {
            var customer = _customerService.GetCustomer(CustomerIdForm);
            if (customer == null)
            {
                return Page();
            }
            Customer = customer;
            CustomerResquestDTO.CustomerId = CustomerIdForm;


            if(CustomerAvatar != null)
            {
                CustomerResquestDTO.AvatarImg = _imageService.ConvertToBase64(CustomerAvatar);
            }


            var createCustomer = _customerService.UpdateCustomer(CustomerResquestDTO);
            if (createCustomer)
            {
                TempData["UpdateMessage"] = "Update Customer Successfully";
                return Page();
            }
            else
            {
                TempData["UpdateMessage"] = "Update Customer Unsuccessfully";
                return Page();
            }

        }
    }
}
