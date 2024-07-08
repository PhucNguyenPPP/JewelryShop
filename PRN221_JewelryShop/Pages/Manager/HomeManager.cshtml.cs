using BLL.Interfaces;
using BOL;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace PRN221_JewelryShop.Pages.Manager
{
    public class HomeManagerModel : PageModel
    {
		private readonly IDashboardService _dashboardService;
        public HomeManagerModel(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }
        public List<SaleOrder> SaleOrdersInMonth { get; set; }
        public decimal? TotalSalesInMonth { get; set; }
        public List<Product> TopSellingProductInMonth { get; set; }
        public decimal? TotalSalesAmountInRange { get; set; }
        public IActionResult OnGet()
        {
            int year = 2024;
            int month = 6;

            var loginResponseString = HttpContext.Session.GetString("LoginResponse");
			if (loginResponseString == null)
			{
				return RedirectToPage("/Login");
			};

			var loginResponse = JsonSerializer.Deserialize<LoginResponse>(loginResponseString);
			if (loginResponse.RoleName != "Manager")
			{
					return RedirectToPage("/Login");
			}

            SaleOrdersInMonth = _dashboardService.GetAllSaleOrdersInMonth(year, month);
            TotalSalesInMonth = _dashboardService.GetTotalSalesInMonth(year, month);
            TopSellingProductInMonth = _dashboardService.GetTopSellingProductInMonth(year, month);
            // For range, you would typically handle user input to set startDate and endDate
            TotalSalesAmountInRange = _dashboardService.GetTotalSalesAmountInRange(DateTime.Parse("2000-01-01"), DateTime.Now);
            return Page();
		}

    }
}
