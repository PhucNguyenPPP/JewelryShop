using BLL.Interfaces;
using BLL.Services;
using BOL;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace PRN221_JewelryShop.Pages.Manager.CounterScreen
{
    public class CounterManagementModel : PageModel
    {
        private readonly ICounterService _counterService;

        public List<Counter> CounterList { get; set; }
        public LoginResponse LoginResponse { get; set; }
        [BindProperty]
        public CounterDTO CounterDTO { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchValue { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 5;
        public CounterManagementModel(ICounterService counterService)
        {
            _counterService = counterService;
        }
        private void GetCounterList()
        {
            var listAll = _counterService.GetAllCounter();
            CounterList = PagingList(listAll);
        }

        private List<Counter> PagingList(List<Counter> list)
        {
            TotalPages = (int)Math.Ceiling(list.Count / (double)PageSize);
            return list.Skip((CurrentPage - 1) * PageSize)
            .Take(PageSize)
            .ToList();
        }


        public IActionResult OnGet()
        {
            GetCounterList();

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

        public IActionResult OnPostCreateCounter()
        {
            GetCounterList();

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

            if (!ModelState.IsValid)
            {
                TempData["CreateModelError"] = "Model State Invalid";
                return Page();
            }

            if (_counterService.CheckCounterExist(CounterDTO.CounterName))
            {
                TempData["CreateError"] = "Counter Name already exists";
                return Page();
            }

            var result = _counterService.AddCounter(CounterDTO);
            if (result)
            {
                TempData["CreateMsg"] = "Create Counter Successfully";
                GetCounterList();
                return Page();
            }
            else
            {
                TempData["CreateMsg"] = "Create Counter Unsuccessfully";
                return Page();
            }
        }


        public IActionResult OnPostUpdateCounter()
        {
            GetCounterList();

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

            if (!ModelState.IsValid)
            {
                TempData["UpdateModelError"] = "Model State Invalid";
                return Page();
            }

            var counter = _counterService.GetCounterById(CounterDTO.CounterId);

            if (_counterService.CheckCounterExist(CounterDTO.CounterName)
                && counter.CounterName != CounterDTO.CounterName)
            {
                TempData["UpdateError"] = "Counter Name already exists";
                return Page();
            }

            var result = _counterService.UpdateCounter(CounterDTO);
            if (result)
            {
                TempData["UpdateMsg"] = "Update Counter Successfully";
                GetCounterList();
                return Page();
            }
            else
            {
                TempData["UpdateMsg"] = "Update Counter Unsuccessfully";
                return Page();
            }
        }

        public IActionResult OnPostDeleteCounter()
        {
            GetCounterList();

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


            var delete = _counterService.DeleteCounter(CounterDTO.CounterId);
            if (delete)
            {
                TempData["DeleteMsg"] = "Delete Counter Successfully";
                GetCounterList();
                return Page();
            }
            else
            {
                TempData["DeleteMsg"] = "Delete Counter Unsuccessfully";
                return Page();
            }
        }

        public IActionResult OnGetPaging()
        {
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

            if (SearchValue.IsNullOrEmpty())
            {
                GetCounterList();
                return Page();
            }
            else
            {
                var searchList = _counterService.SearchCounter(SearchValue);
                var pagingList = PagingList(searchList);
                CounterList = pagingList;
                return Page();
            }
        }

        public IActionResult OnGetSearchCounter()
        {
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

            if (SearchValue.IsNullOrEmpty())
            {
                GetCounterList();
                return Page();
            }
            var searchList = _counterService.SearchCounter(SearchValue);
            if (searchList.Count == 0)
            {
                TempData["SearchMsg"] = "No result";
                CurrentPage = 0;
                return Page();
            }
            var pagingList = PagingList(searchList);
            CounterList = pagingList;
            return Page();
        }
    }
}
