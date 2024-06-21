using BLL.Interfaces;
using BLL.Services;
using BOL;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace PRN221_JewelryShop.Pages.Manager.ProductScreen
{
    public class ProductDetailModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly IMaterialProductService _materialProductService;

        public LoginResponse LoginResponse { get; set; }
        public Product? Product { get; set; }

        [FromQuery(Name = "id")]
        public string ProductId { get; set; }

        public ProductDetailModel(IProductService productService, IMaterialProductService materialProductService)
        {
            _productService = productService;
            _materialProductService = materialProductService;

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
            var product = _productService.GetProductById(ProductId);
            if (product != null)
            {
                var materialProductList = _materialProductService.GetAllMaterialProductByProductId(ProductId);
                product.MaterialProducts = materialProductList;
                Product = product;
            }
            return Page();
        }
    }
}
