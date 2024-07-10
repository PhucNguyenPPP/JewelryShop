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
		[BindProperty]
		public DateTime DashboardMonthDateTime { get; set; }
		[BindProperty]
		public DateTime StartDate { get; set; }
		[BindProperty]
		public DateTime EndDate { get; set; }
		public DashboardMonthResponseDTO DashboardMonthResponseDTO { get; set; }
		public DashboardRangeResponseDTO DashboardRangeResponseDTO { get; set; }
		public HomeManagerModel(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }
        public IActionResult OnGet()
        {
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

            GetDashBoardInfoByMonth(DateTime.Now.Month, DateTime.Now.Year);
            return Page();
		}

        public void GetDashBoardInfoByMonth(int month, int year)
        {
			int previousMonth = month == 1 ? 12 : month - 1;
			int previousYear = month == 1 ? year - 1 : year;

			DashboardMonthResponseDTO dashboardMonthResponseDTO = new DashboardMonthResponseDTO()
			{
				TotalRevenueMonth = _dashboardService.GetTotalSalesInMonth(year, month),
				TotalRevenueMonthBefore = _dashboardService.GetTotalSalesInMonth(previousYear, previousMonth),
				SaleOrderListMonth = _dashboardService.GetAllSaleOrdersInMonth(year, month),
				SaleOrderListMonthBefore = _dashboardService.GetAllSaleOrdersInMonth(previousYear, previousMonth),
				TopProductMonth = _dashboardService.GetTopSellingProductInMonth(year, month),
			};
			DashboardMonthResponseDTO = dashboardMonthResponseDTO;
		}

		public IActionResult OnPostSearchDashboardInfoByMonth()
		{
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
			GetDashBoardInfoByMonth(DashboardMonthDateTime.Month, DashboardMonthDateTime.Year);
			return Page();
		}

		public IActionResult OnPostSearchDashBoardInfoByRange()
		{
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
			DashboardRangeResponseDTO dashboardRangeResponseDTO = new DashboardRangeResponseDTO()
			{
				TotalRevenueRange = _dashboardService.GetTotalSalesAmountInRange(StartDate, EndDate),
				SaleOrderListRange = _dashboardService.GetAllSaleOrdersInRange(StartDate, EndDate)
			};
			GetDashBoardInfoByMonth(DateTime.Now.Month, DateTime.Now.Year);
			DashboardRangeResponseDTO = dashboardRangeResponseDTO;
			return Page();
		}

	}
}
