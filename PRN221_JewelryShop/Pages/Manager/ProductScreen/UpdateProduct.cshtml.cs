using AutoMapper;
using BLL.Interfaces;
using BOL;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace PRN221_JewelryShop.Pages.Manager.ProductScreen
{
    public class UpdateProductModel : PageModel
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

        [FromQuery(Name = "id")]
        public string? ProductId { get; set; }

        public Product Product { get; set; }

        public List<MaterialProduct> AvailableMaterialList { get; set; }



        public UpdateProductModel(IProductService productService,
            IMaterialProductService materialProductService, ICounterService counterService,
            IMaterialService materialService, IImageService imageService,
            IMapper mapper)
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
            GetProduct(ProductId);
            GetAvailableMaterialList(ProductId);
            return Page();
        }

        private void GetAllInputField()
        {
            CounterList = _counterService.GetAllCounter();
            MaterialList = _materialService.GetAllMaterial();
        }

        private void GetProduct(string id)
        {
            var product = _productService.GetProductById(id);
            Product = product;
        }

        private void GetAvailableMaterialList(string id)
        {
            var materialList = _materialProductService.GetAllMaterialProductByProductId(id);
            AvailableMaterialList = materialList;
        }

        public IActionResult OnPostUpdateProduct()
        {
            GetAllInputField();
            GetProduct(ProductRequestDTO.ProductId);
            GetAvailableMaterialList(ProductRequestDTO.ProductId);

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

            var product = _productService.GetProductById(ProductRequestDTO.ProductId);
            var checkProductNameExists = _productService.CheckNameExisted(ProductRequestDTO.ProductName);
            if (checkProductNameExists && product.ProductName != ProductRequestDTO.ProductName)
            {
                TempData["NameExist"] = "Product Name already exists";
                return Page();
            }

            var checkValidMaterialList = _productService.CheckValidationMaterialDTOList(MaterialDTOList);
            if (!checkValidMaterialList.IsSuccess)
            {
                TempData["UpdateError"] = checkValidMaterialList.Message;
                return Page();
            }

            if(ProductAvatar != null)
            {
                ProductRequestDTO.AvatarImg = _imageService.ConvertToBase64(ProductAvatar);
            }

            ProductRequestDTO.MaterialDTOs = MaterialDTOList;

            var price = _productService.GetPriceProduct(MaterialDTOList, (decimal)ProductRequestDTO.Wage);
            ProductRequestDTO.Price = price;

            var checkValidAmountInStock = _productService.CheckMaterialAmountInStock(ProductRequestDTO);
            if (!checkValidAmountInStock.IsSuccess)
            {
                TempData["UpdateError"] = checkValidAmountInStock.Message;
                return Page();
            }
            var result = _productService.UpdateProduct(ProductRequestDTO);
            if (result)
            {
                TempData["UpdateMsg"] = "Update Successfully";
                GetProduct(ProductRequestDTO.ProductId);
                GetAvailableMaterialList(ProductRequestDTO.ProductId);
                return Page();
            }
            else
            {
                TempData["UpdateMsg"] = "Update Unsuccessfully";
                GetProduct(ProductRequestDTO.ProductId);
                GetAvailableMaterialList(ProductRequestDTO.ProductId);
                return Page();
            }
        }
    }
}
