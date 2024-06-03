using BLL.Interfaces;
using BOL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PRN221_JewelryShop.Pages.Staff
{
    public class CustomerManagementModel : PageModel
    {
        private readonly ICustomerService _customerService;
        public CustomerManagementModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        public string msg{ get; set; }

        [BindProperty]
        public Customer customer { get; set; }
        public IActionResult GetOn()
        {
            msg = "123";
            return Page();
        }
    }
}
