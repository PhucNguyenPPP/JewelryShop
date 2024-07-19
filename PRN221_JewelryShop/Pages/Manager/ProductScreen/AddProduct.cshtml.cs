using BLL.Interfaces;
using BOL;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace PRN221_JewelryShop.Pages.Manager.ProductScreen
{
    public class AddProductModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly IMaterialProductService _materialProductService;
        private readonly ICounterService _counterService;
        private readonly IMaterialService _materialService;
        private readonly IImageService _imageService;

        public LoginResponse LoginResponse { get; set; }
        [BindProperty]
        public ProductRequestDTO ProductRequestDTO { get; set; }
        public List<Counter> CounterList { get; set; }
        public List<Material> MaterialList { get; set; }

        [BindProperty]
        public List<MaterialDTO> MaterialDTOList { get; set; }

        [BindProperty]
        public IFormFile? ProductAvatar { get; set; }


        public AddProductModel(IProductService productService, 
            IMaterialProductService materialProductService, ICounterService counterService,
            IMaterialService materialService, IImageService imageService)
        {
            _productService = productService;
            _materialProductService = materialProductService;
            _counterService = counterService;
            _materialService = materialService;
            _imageService = imageService;
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
            GetAllInputField();
            return Page();
        }

        private void GetAllInputField()
        {
            CounterList = _counterService.GetAllCounter();
            MaterialList = _materialService.GetAllMaterial();
        }

        public IActionResult OnPostCreateProduct()
        {
            GetAllInputField();

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

                return Page();
            }

            var checkProductNameExists = _productService.CheckNameExisted(ProductRequestDTO.ProductName);
            if (checkProductNameExists)
            {
                TempData["NameExist"] = "Product Name already exists";
                return Page();
            }

            var checkValidMaterialList = _productService.CheckValidationMaterialDTOList(MaterialDTOList);
            if (!checkValidMaterialList.IsSuccess)
            {
                TempData["CreateError"] = checkValidMaterialList.Message;
                return Page();
            }

            ProductRequestDTO.AvatarImg = _imageService.ConvertToBase64(ProductAvatar);
            ProductRequestDTO.MaterialDTOs = MaterialDTOList;
            var price = _productService.GetPriceProduct(MaterialDTOList, (decimal)ProductRequestDTO.Wage);
            ProductRequestDTO.Price = price;

            var checkValidAmountInStock = _productService.CheckMaterialAmountInStock(ProductRequestDTO);
            if(!checkValidAmountInStock.IsSuccess)
            {
                TempData["CreateError"] = checkValidAmountInStock.Message;
                return Page();
            }
            var result = _productService.AddProduct(ProductRequestDTO);
            if (result)
            {
                TempData["CreateMsg"] = "Create Successfully";
                return RedirectToPage("/Manager/ProductScreen/ProductManagement");
            }
            else
            {
                TempData["CreateMsg"] = "Create Unsuccessfully";
                return Page();
            }
        }
        
    }
}
