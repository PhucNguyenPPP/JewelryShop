using BLL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace PRN221_JewelryShop.Pages.Staff.GoldPriceScreen
{
    public class GoldPriceModel : PageModel
    {
        private readonly IGoldPriceService _goldPriceService;

        public LoginResponse LoginResponse { get; set; }
        public List<GoldPriceDTO> GoldPriceList { get; set; }
        public GoldPriceModel(IGoldPriceService goldPriceService)
        {
            _goldPriceService = goldPriceService;
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

            GoldPriceList = _goldPriceService.GetGoldPrices();

            return Page();
        }
    }
}
