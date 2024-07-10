using BLL.Interfaces;
using BLL.Services;
using BOL;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace PRN221_JewelryShop.Pages.Staff.ReturnOrderScreen
{
    public class ReturnOrderDetailModel : PageModel
    {
        private readonly IReturnOrderService _returnOrderService;
        public LoginResponse LoginResponse { get; set; }
        public ReturnOrder ReturnOrder { get; set; }

        [FromQuery(Name = "id")]
        public string ReturnOrderId { get; set; }
        public ReturnOrderDetailModel(IReturnOrderService returnOrderService)
        {
            _returnOrderService = returnOrderService;
        }
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
            ReturnOrder = _returnOrderService.GetReturnOrderById(ReturnOrderId);
            return Page();
        }
    }
}
