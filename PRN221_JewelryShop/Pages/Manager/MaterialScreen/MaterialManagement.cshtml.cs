using BLL.Interfaces;
using BLL.Services;
using BOL;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace PRN221_JewelryShop.Pages.Manager.MaterialScreen
{
    public class MaterialManagementModel : PageModel
    {
        private readonly IMaterialService _materialService;
        private readonly IMaterialTypeService _materialTypeService;
        public MaterialManagementModel(IMaterialTypeService materialTypeService, IMaterialService materialService)
        {
            _materialTypeService = materialTypeService;
            _materialService = materialService;
        }
        public List<Material> MaterialList { get; set; }
        public List<MaterialType> MaterialTypeList { get; set; }    
        public LoginResponse LoginResponse { get; set; }
        [BindProperty]
        public MaterialRequestDTO MaterialRequestDTO { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchValue { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 5;
        public void GetAllMaterialType()
        {
            MaterialTypeList = _materialTypeService.GetAllMaterialType();
        }
        private void GetMaterialList()
        {
            var listAll = _materialService.GetAllMaterial();
            MaterialList = PagingList(listAll);
        }

        private List<Material> PagingList(List<Material> list)
        {
            TotalPages = (int)Math.Ceiling(list.Count / (double)PageSize);
            return list.Skip((CurrentPage - 1) * PageSize)
            .Take(PageSize)
            .ToList();
        }


        public IActionResult OnGet()
        {
            GetAllMaterialType();
            GetMaterialList();

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

        public IActionResult OnPostCreateMaterial()
        {
            GetAllMaterialType();
            GetMaterialList();

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

            if (_materialService.CheckMaterialNameExist(MaterialRequestDTO.MaterialName))
            {
                TempData["CreateError"] = "Material Name already exists";
                return Page();
            }

            if(!_materialService.CheckAmountValidation(MaterialRequestDTO))
            {
                TempData["CreateError"] = "Amount is invalid";
                return Page();
            }

            var result = _materialService.AddMaterial(MaterialRequestDTO);
            if (result)
            {
                TempData["CreateMsg"] = "Create Material Successfully";
                GetMaterialList();
                return Page();
            }
            else
            {
                TempData["CreateMsg"] = "Create Material Unsuccessfully";
                return Page();
            }
        }


        public IActionResult OnPostUpdateMaterial()
        {
            GetAllMaterialType();
            GetMaterialList();

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

            var material = _materialService.GetMaterial(MaterialRequestDTO.MaterialId);

            if (_materialService.CheckMaterialNameExist(MaterialRequestDTO.MaterialName)
                && material.MaterialName != MaterialRequestDTO.MaterialName)
            {
                TempData["UpdateError"] = "Material Name already exists";
                return Page();
            }

            if (!_materialService.CheckAmountValidation(MaterialRequestDTO))
            {
                TempData["UpdateError"] = "Amount is invalid";
                return Page();
            }

            var result = _materialService.UpdateMaterial(MaterialRequestDTO);
            if (result)
            {
                TempData["UpdateMsg"] = "Update Material Successfully";
                GetMaterialList();
                return Page();
            }
            else
            {
                TempData["UpdateMsg"] = "Update Material Unsuccessfully";
                GetMaterialList();
                return Page();
            }
        }

        public IActionResult OnPostDeleteMaterial()
        {
            GetAllMaterialType();
            GetMaterialList();

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


            var delete = _materialService.DeleteMaterial(MaterialRequestDTO.MaterialId);
            if (delete)
            {
                TempData["DeleteMsg"] = "Delete Material Successfully";
                GetMaterialList();
                return Page();
            }
            else
            {
                TempData["DeleteMsg"] = "Delete Material Unsuccessfully";
                return Page();
            }
        }

        public IActionResult OnGetPaging()
        {
            GetAllMaterialType();
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
                GetMaterialList();
                return Page();
            }
            else
            {
                var searchList = _materialService.SearchMaterial(SearchValue);
                var pagingList = PagingList(searchList);
                MaterialList = pagingList;
                return Page();
            }
        }

        public IActionResult OnGetSearchMaterial()
        {
            GetAllMaterialType();
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
                GetMaterialList();
                return Page();
            }
            var searchList = _materialService.SearchMaterial(SearchValue);
            if (searchList.Count == 0)
            {
                TempData["SearchMsg"] = "No result";
                CurrentPage = 0;
                return Page();
            }
            var pagingList = PagingList(searchList);
            MaterialList = pagingList;
            return Page();
        }

}
}
