using BLL.Interfaces;
using BLL.Services;
using BOL;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace PRN221_JewelryShop.Pages.Staff.ReturnOrderScreen
{
    public class ReturnOrderManagementModel : PageModel
    {
        private readonly IReturnOrderService _returnOrderService;
        public List<ReturnOrder> ReturnOrderList { get; set; }
        public LoginResponse LoginResponse { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchValue { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 5;
        public ReturnOrderManagementModel(IReturnOrderService returnOrderService)
        {
            _returnOrderService = returnOrderService;
        }
        private void GetReturnOrderList()
        {
            var listAll = _returnOrderService.GetAllReturnOrders();
            ReturnOrderList = PagingList(listAll);
        }

        private List<ReturnOrder> PagingList(List<ReturnOrder> list)
        {
            TotalPages = (int)Math.Ceiling(list.Count / (double)PageSize);
            return list.Skip((CurrentPage - 1) * PageSize)
            .Take(PageSize)
            .ToList();
        }

        public IActionResult OnGet()
        {
            GetReturnOrderList();

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
                GetReturnOrderList();
                return Page();
            }
            else
            {
                var searchList = _returnOrderService.SearchReturnOrders(SearchValue);
                var pagingList = PagingList(searchList);
                ReturnOrderList = pagingList;
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
                GetReturnOrderList();
                return Page();
            }
            var searchList = _returnOrderService.SearchReturnOrders(SearchValue);
            if (searchList.Count == 0)
            {
                TempData["SearchMsg"] = "No result";
                CurrentPage = 0;
                return Page();
            }
            var pagingList = PagingList(searchList);
            ReturnOrderList = pagingList;
            return Page();
        }
    }
}
