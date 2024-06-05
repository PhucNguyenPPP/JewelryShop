using BLL.Interfaces;
using BOL.DTOs;
using BOL.Entities;
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
        public CreateCustomerResquestDTO CreateCustomerResquestDTO { get; set; }
        [BindProperty]
        public IFormFile CustomerAvatar { get; set; }

        public LoginResponse LoginResponse { get; set; }

        public Customer Customer { get; set; }

        [FromQuery(Name = "id")]
        public Guid CustomerId {  get; set; }
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
    }
}
