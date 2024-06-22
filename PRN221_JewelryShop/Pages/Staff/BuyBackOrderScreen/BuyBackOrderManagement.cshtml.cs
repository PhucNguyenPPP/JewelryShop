using BLL.Interfaces;
using BLL.Services;
using BOL;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace PRN221_JewelryShop.Pages.Staff.BuyBackOrderScreen
{
    public class BuyBackOrderManagementModel : PageModel
    {
        private readonly IBuyBackOrderService _buyBackOrderService;
        public List<BuyBackOrder> BuyBackOrderList { get; set; }
        public LoginResponse LoginResponse { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchValue { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 5;

        public BuyBackOrderManagementModel(IBuyBackOrderService buyBackOrderService)
        {
            _buyBackOrderService = buyBackOrderService;
        }
        private void GetBuyBackOderList()
        {
            var listAll = _buyBackOrderService.GetAllBuyBackOrders();
            BuyBackOrderList = PagingList(listAll);
        }

        private List<BuyBackOrder> PagingList(List<BuyBackOrder> list)
        {
            TotalPages = (int)Math.Ceiling(list.Count / (double)PageSize);
            return list.Skip((CurrentPage - 1) * PageSize)
            .Take(PageSize)
            .ToList();
        }

        public IActionResult OnGet()
        {
            GetBuyBackOderList();

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
                GetBuyBackOderList();
                return Page();
            }
            else
            {
                var searchList = _buyBackOrderService.SearchBuyBackOrders(SearchValue);
                var pagingList = PagingList(searchList);
                BuyBackOrderList = pagingList;
                return Page();
            }
        }

        public IActionResult OnGetSearchBuyBackOrder()
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
                GetBuyBackOderList();
                return Page();
            }
            var searchList = _buyBackOrderService.SearchBuyBackOrders(SearchValue);
            if (searchList.Count == 0)
            {
                TempData["SearchMsg"] = "No result";
                CurrentPage = 0;
                return Page();
            }
            var pagingList = PagingList(searchList);
            BuyBackOrderList = pagingList;
            return Page();
        }
    }
}
