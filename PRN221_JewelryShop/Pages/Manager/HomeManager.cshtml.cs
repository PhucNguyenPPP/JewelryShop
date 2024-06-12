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
			return Page();
		}

    }
}
