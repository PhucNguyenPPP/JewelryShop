using BLL.Interfaces;
using BOL;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace PRN221_JewelryShop.Pages.Staff.BuyBackOrderScreen
{
    public class BuyBackOrderDetailModel : PageModel
    {
        private readonly IBuyBackOrderService _buyBackOrderService;
        public LoginResponse LoginResponse { get; set; }
        public BuyBackOrder BuyBackOrder { get; set; }

        [FromQuery(Name = "id")]
        public string BuyBackOrderId { get; set; }

        public BuyBackOrderDetailModel(IBuyBackOrderService buyBackOrderService)
        {
            _buyBackOrderService = buyBackOrderService;
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
            BuyBackOrder = _buyBackOrderService.GetBuyBackOrderById(BuyBackOrderId);
            return Page();
        }
    }
}
