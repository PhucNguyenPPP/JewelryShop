using BLL.Interfaces;
using BOL;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace PRN221_JewelryShop.Pages.Manager.ReturnPolicyScreen
{
    public class ReturnPolicyManagementModel : PageModel
    {
        private readonly IReturnPolicyService _returnPolicyService;

        public List<ReturnPolicy> ReturnPolicyList { get; set; }
        public LoginResponse LoginResponse { get; set; }

        [BindProperty]
        public List<ReturnPolicyRequestDTO> ReturnPolicyRequestDTOs { get; set; }

        public ReturnPolicyManagementModel(IReturnPolicyService returnPolicyService)
        {
            _returnPolicyService = returnPolicyService;
        }

        private void GetReturnPolicyList()
        {
            ReturnPolicyList = _returnPolicyService.GetAllReturnPolicies();
        }

        public IActionResult OnGet()
        {
            GetReturnPolicyList();

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

        public IActionResult OnPostUpdateReturnPolicy()
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

            var result = _returnPolicyService.UpdateReturnPolicy(ReturnPolicyRequestDTOs);
            if (result)
            {
                TempData["UpdateMsg"] = "Update Successfully";
                GetReturnPolicyList();
                return Page();
            } else
            {
                TempData["UpdateMsg"] = "Update Unsuccessfully";
                GetReturnPolicyList();
                return Page();
            }
        }
    }
}
