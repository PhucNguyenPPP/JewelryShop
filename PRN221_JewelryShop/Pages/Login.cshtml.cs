using BLL.Interfaces;
using BLL.Services;
using BOL.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace PRN221_JewelryShop.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IAuthService _authService;

        public LoginModel(IAuthService authService)
        {
            _authService = authService;
        }

        [BindProperty]
        public LoginRequest LoginRequest { get; set; }
        public string ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            var loginResponse = _authService.CheckLogin(LoginRequest.Username, LoginRequest.Password);
            if(loginResponse.IsSuccess)
            {
                if(loginResponse.RoleName == "Manager")
                {
					HttpContext.Session.SetString("LoginResponse", JsonSerializer.Serialize(loginResponse));
					return RedirectToPage("/Manager/HomeManager");
				}

				if (loginResponse.RoleName == "Staff")
				{
					HttpContext.Session.SetString("LoginResponse", JsonSerializer.Serialize(loginResponse));
					return RedirectToPage("/Staff/HomeStaff");
				}
			}
            ErrorMessage = "Login failed. Please check your username and password.";
            return Page();
        }
    }
}
