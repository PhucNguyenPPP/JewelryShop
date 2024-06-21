using BLL.Interfaces;
using BOL;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace PRN221_JewelryShop.Pages.Manager.PromotionScreen
{
    public class UpdatePromotionModel : PageModel
    {
        private readonly IPromotionProgramService _promotionProgramService;
        private readonly IPromotionCodeService _promotionCodeService;
        public PromotionProgram PromotionProgram { get; set; }
        [FromQuery(Name = "id")]
        public string PromotionProgramId { get; set; }

        public LoginResponse LoginResponse { get; set; }
        [BindProperty]
        public PromotionProgramDTO PromotionProgramDTO { get; set; }
        public UpdatePromotionModel(IPromotionProgramService promotionProgramService, IPromotionCodeService promotionCodeService)
        {
            _promotionProgramService = promotionProgramService;
            _promotionCodeService = promotionCodeService;
        }
        public IActionResult OnGet()
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
            GetPromotionProgram();
            return Page();
        }
        private void GetPromotionProgram()
        {
            PromotionProgram = _promotionProgramService.GetPromotionProgram(PromotionProgramId);
        }

        public IActionResult OnPostUpdatePromotionProgram()
        {
            PromotionProgram = _promotionProgramService.GetPromotionProgram(PromotionProgramDTO.PromotionProgramId);
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

            var checkPromotionProgramNameExist = _promotionProgramService
                .CheckPromotionProgramExist(PromotionProgramDTO.PromotionProgramName);
            if (checkPromotionProgramNameExist
                && PromotionProgram.PromotionProgramName != PromotionProgramDTO.PromotionProgramName)
            {
                TempData["PromotionProgramNameExist"] = "Promotion Program Name already exists";
                return Page();
            }

            var checkDuplicatedPromotionCodeNameList = PromotionProgramDTO.PromotionCodeDTOs
            .GroupBy(p => p.PromotionCodeName)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();

            if(checkDuplicatedPromotionCodeNameList.Any()) 
            {
                    TempData["PromotionCodeNameExist"] = "Promotion Program Code Name in list can not be duplicated";
                    return Page();
            }

            for (int i = 0; i < PromotionProgramDTO.PromotionCodeDTOs.Count; i++)
            {
                var checkPromotionProgramCodeNameExist = _promotionCodeService
                .CheckPromotionCodeNameExists(PromotionProgramDTO.PromotionCodeDTOs[i].PromotionCodeName);
                if (checkPromotionProgramCodeNameExist
                    && PromotionProgram.PromotionProgramCodes.ToList()[i].PromotionCodeName != PromotionProgramDTO.PromotionCodeDTOs[i].PromotionCodeName)
                {
                    TempData["PromotionCodeNameExist"] = "Promotion Program Code Name " + PromotionProgramDTO.PromotionCodeDTOs[i].PromotionCodeName + " already exists";
                    return Page();
                }
            }
            var result = _promotionProgramService.UpdatePromotionProgram(PromotionProgramDTO);
            if (result)
            {
                TempData["UpdateMsg"] = "Update Promotion Program Successfully";
                return Page();
            }
            else
            {
                TempData["UpdateError"] = "Update Promotion Program Unsuccessfully";
                return Page();
            }

        }
    }
}
