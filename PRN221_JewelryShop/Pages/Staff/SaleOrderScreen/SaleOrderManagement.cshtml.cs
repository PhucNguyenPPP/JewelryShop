using BLL.Interfaces;
using BLL.Services;
using BOL;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace PRN221_JewelryShop.Pages.Staff.SaleOrderScreen
{
    public class SaleOrderManagementModel : PageModel
    {
        private readonly ISaleOrderService _saleOrderService;

        public List<SaleOrder> SaleOrderList { get; set; }
        public LoginResponse LoginResponse { get; set; }

        [BindProperty]
        public IFormFile? CustomerAvatar { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchValue { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 5;

        public SaleOrderManagementModel(ISaleOrderService saleOrderService)
        {
            _saleOrderService = saleOrderService;
        }
        private void GetSaleOrderList()
        {
            var listAll = _saleOrderService.GetAllSaleOrders();
            SaleOrderList = PagingList(listAll);
        }

        private List<SaleOrder> PagingList(List<SaleOrder> list)
        {
            TotalPages = (int)Math.Ceiling(list.Count / (double)PageSize);
            return list.Skip((CurrentPage - 1) * PageSize)
            .Take(PageSize)
            .ToList();
        }

        public IActionResult OnGet()
        {
            GetSaleOrderList();

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

        public IActionResult OnGetPaging()
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

            if (SearchValue.IsNullOrEmpty())
            {
                GetSaleOrderList();
                return Page();
            }
            else
            {
                var searchList = _saleOrderService.SearchSaleOrder(SearchValue);
                var pagingList = PagingList(searchList);
                SaleOrderList = pagingList;
                return Page();
            }
        }

        public IActionResult OnGetSearchSaleOrder()
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

            if (SearchValue.IsNullOrEmpty())
            {
                GetSaleOrderList();
                return Page();
            }
            var searchList = _saleOrderService.SearchSaleOrder(SearchValue);
            if (searchList.Count == 0)
            {
                TempData["SearchMsg"] = "No result";
                CurrentPage = 0;
                return Page();
            }
            var pagingList = PagingList(searchList);
            SaleOrderList = pagingList;
            return Page();
        }
    }
}
