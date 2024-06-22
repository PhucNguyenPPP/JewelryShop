using BLL.Interfaces;
using BLL.Services;
using BOL;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace PRN221_JewelryShop.Pages.Manager.BuyBackPolicyScreen
{
    public class BuyBackPolicyManagementModel : PageModel
    {
        private readonly IBuyBackPolicyService _buyBackPolicyService;

        public List<BuyBackPolicy> BuyBackPolicyList { get; set; }
        public LoginResponse LoginResponse { get; set; }
        [BindProperty]
        public BuyBackPolicyRequestDTO BuyBackPolicyRequestDTO { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchValue { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 5;

        public BuyBackPolicyManagementModel(IBuyBackPolicyService buyBackPolicyService)
        {
            _buyBackPolicyService = buyBackPolicyService;
        }

        private void GetBuyBackPolicyList()
        {
            var listAll = _buyBackPolicyService.GetAllBuyBackPolicies();
            BuyBackPolicyList = PagingList(listAll);
        }

        private List<BuyBackPolicy> PagingList(List<BuyBackPolicy> list)
        {
            TotalPages = (int)Math.Ceiling(list.Count / (double)PageSize);
            return list.Skip((CurrentPage - 1) * PageSize)
            .Take(PageSize)
            .ToList();
        }


        public IActionResult OnGet()
        {
            GetBuyBackPolicyList();

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

        public IActionResult OnPostCreateBuyBackPolicy()
        {
            GetBuyBackPolicyList();

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

            if (_buyBackPolicyService.CheckPolicyNameExist(BuyBackPolicyRequestDTO.PolicyName))
            {
                TempData["CreateError"] = "Policy Name already exists";
                return Page();
            }

            var result = _buyBackPolicyService.AddBuyBackPolicy(BuyBackPolicyRequestDTO);
            if (result)
            {
                TempData["CreateMsg"] = "Create Policy Successfully";
                GetBuyBackPolicyList();
                return Page();
            }
            else
            {
                TempData["CreateMsg"] = "Create Policy Unsuccessfully";
                return Page();
            }
        }


        public IActionResult OnPostUpdateBuyBackPolicy()
        {
            GetBuyBackPolicyList();

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

            var policy = _buyBackPolicyService.GetBuyBackPolicy(BuyBackPolicyRequestDTO.PolicyId);

            if (_buyBackPolicyService.CheckPolicyNameExist(BuyBackPolicyRequestDTO.PolicyName)
                && policy.PolicyName != BuyBackPolicyRequestDTO.PolicyName)
            {
                TempData["UpdateError"] = "Policy Name already exists";
                return Page();
            }

            var result = _buyBackPolicyService.UpdateBuyBackPolicy(BuyBackPolicyRequestDTO);
            if (result)
            {
                TempData["UpdateMsg"] = "Update Policy Successfully";
                GetBuyBackPolicyList();
                return Page();
            }
            else
            {
                TempData["UpdateMsg"] = "Update Policy Unsuccessfully";
                return Page();
            }
        }

        public IActionResult OnPostDeleteBuyBackPolicy()
        {
            GetBuyBackPolicyList();

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


            var delete = _buyBackPolicyService.DeleteBuyBackPolicy(BuyBackPolicyRequestDTO.PolicyId);
            if (delete)
            {
                TempData["DeleteMsg"] = "Delete Policy Successfully";
                GetBuyBackPolicyList();
                return Page();
            }
            else
            {
                TempData["DeleteMsg"] = "Delete Policy Unsuccessfully";
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
                GetBuyBackPolicyList();
                return Page();
            }
            else
            {
                var searchList = _buyBackPolicyService.SearchBuyBackPolicies(SearchValue);
                var pagingList = PagingList(searchList);
                BuyBackPolicyList = pagingList;
                return Page();
            }
        }

        public IActionResult OnGetSearchBuyBackPolicy()
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
                GetBuyBackPolicyList();
                return Page();
            }
            var searchList = _buyBackPolicyService.SearchBuyBackPolicies(SearchValue);
            if (searchList.Count == 0)
            {
                TempData["SearchMsg"] = "No result";
                CurrentPage = 0;
                return Page();
            }
            var pagingList = PagingList(searchList);
            BuyBackPolicyList = pagingList;
            return Page();
        }
    }
}
