using BLL.Interfaces;
using BLL.Services;
using BOL;
using DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace PRN221_JewelryShop.Pages.Manager.PromotionScreen
{
    public class PromotionManagementModel : PageModel
    {
        private readonly IPromotionProgramService _promotionProgramService;
        private readonly IPromotionCodeService _promotionCodeService;
       
        public List<PromotionProgram> PromotionProgramList { get; set; }
        public LoginResponse LoginResponse { get; set; }

        [BindProperty]
        public CustomerResquestDTO CustomerResquestDTO { get; set; }
        [BindProperty]
        public PromotionProgramDTO PromotionProgramDTO { get; set; }

        public List<PromotionProgramCode> PromotionProgramCodes { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchValue { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 5;

        public PromotionManagementModel(IPromotionProgramService promotionProgramService, IPromotionCodeService promotionCodeService)
        {
            _promotionProgramService = promotionProgramService;
            _promotionCodeService = promotionCodeService;

        }
        private void GetPromotionProgramList()
        {
            var listAll = _promotionProgramService.GetPromotionProgramList();
            PromotionProgramList = PagingList(listAll);
        }

        private List<PromotionProgram> PagingList(List<PromotionProgram> list)
        {
            TotalPages = (int)Math.Ceiling(list.Count / (double)PageSize);
            return list.Skip((CurrentPage - 1) * PageSize)
            .Take(PageSize)
            .ToList();
        }

        public IActionResult OnGet()
        {
            GetPromotionProgramList();

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

        public IActionResult OnPostCreatePromotion([FromBody] PromotionProgramDTO promotionProgramDTO)
        {
            GetPromotionProgramList();
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

            var checkPromotionProgramExist = _promotionProgramService
                .CheckPromotionProgramExist(promotionProgramDTO.PromotionProgramName);
            if(checkPromotionProgramExist)
            {
                TempData["CreateError"] = "Promotion Program Name already exists";
                return StatusCode(400);
            }

            var checkDuplicatedPromotionCodeNameList = promotionProgramDTO.PromotionCodeDTOs
            .GroupBy(p => p.PromotionCodeName)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();

            if (checkDuplicatedPromotionCodeNameList.Any())
            {
                TempData["CreateError"] = "Promotion Program Code Name in list can not be duplicated";
                return StatusCode(400);
            }

            foreach (var i in promotionProgramDTO.PromotionCodeDTOs)
            {
                var checkPromotionProgramCodeExist = _promotionCodeService
                    .CheckPromotionCodeNameExists(i.PromotionCodeName);
                if (checkPromotionProgramCodeExist)
                {
                    TempData["CreateError"] = "Promotion Program Code Name (" + i.PromotionCodeName + ") already exists";
                    return StatusCode(400);
                }
            }

            var result = _promotionProgramService.AddPromotionProgram(promotionProgramDTO);
            if(result)
            {
                TempData["CreateMsg"] = "Create Promotion Program Successfully";
                return StatusCode(200);
            } else
            {
                TempData["CreateMsg"] = "Create Promotion Program Unsuccessfully";
                return StatusCode(200);
            }
        }

        public IActionResult OnPostDeletePromotionProgram()
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

            var result = _promotionProgramService
                .DeletePromotionProgram(PromotionProgramDTO.PromotionProgramId);
            if (result)
            {
                TempData["DeleteMsg"] = "Delete Promotion Program Successfully";
                GetPromotionProgramList();
                return Page();
            } else
            {
                TempData["DeleteMsg"] = "Delete Promotion Program Unsuccessfully";
                GetPromotionProgramList();
                return Page();
            }
        }

        public IActionResult OnGetSearchPromotion()
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
                GetPromotionProgramList();
                return Page();
            }
            var searchList = _promotionProgramService.SearchPromotionProgram(SearchValue);
            if (searchList.Count == 0)
            {
                TempData["SearchMsg"] = "No result";
                CurrentPage = 0;
                return Page();
            }
            var pagingList = PagingList(searchList);
            PromotionProgramList = pagingList;
            return Page();
        }

    }
}
