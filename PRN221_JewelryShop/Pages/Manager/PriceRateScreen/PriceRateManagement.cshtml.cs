using BLL.Interfaces;
using BLL.Services;
using BOL;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace PRN221_JewelryShop.Pages.Manager.PriceRateScreen
{
    public class PriceRateManagementModel : PageModel
    {
        private readonly IPriceRateService _priceRateService;
        public PriceRateManagementModel(IPriceRateService priceRateService)
        {
            _priceRateService = priceRateService;
        }
        public PriceRate PriceRate { get; set; }
        public LoginResponse LoginResponse { get; set; }

        [BindProperty]
        public PriceRateRequestDTO PriceRateRequestDTO { get; set; }
        public void GetPriceRate()
        {
            PriceRate = _priceRateService.GetPriceRate();
        }
        public IActionResult OnGet()
        {
            GetPriceRate();

            var loginResponseString = HttpContext.Session.GetString("LoginResponse");
            if (loginResponseString == null)
            {
                return RedirectToPage("/Login");
            };

            LoginResponse = JsonSerializer.Deserialize<LoginResponse>(loginResponseString);
            if (LoginResponse.RoleName != "Manager")
            {
                return RedirectToPage("/Login");
            }
            return Page();
        }

        public IActionResult OnPostUpdatePriceRate()
        {
            GetPriceRate();
            var loginResponseString = HttpContext.Session.GetString("LoginResponse");
            if (loginResponseString == null)
            {
                return RedirectToPage("/Login");
            };

            LoginResponse = JsonSerializer.Deserialize<LoginResponse>(loginResponseString);
            if (LoginResponse.RoleName != "Manager")
            {
                return RedirectToPage("/Login");
            }

            if(!ModelState.IsValid)
            {
                return Page();
            }

            var result = _priceRateService.UpdatePriceRate(PriceRateRequestDTO);
            if (result)
            {
                TempData["UpdateMsg"] = "Update Successfully";
                GetPriceRate();
                return Page();
            }
            else
            {
                TempData["UpdateMsg"] = "Update Unsuccessfully";
                GetPriceRate();
                return Page();
            }
        }
    }
}
