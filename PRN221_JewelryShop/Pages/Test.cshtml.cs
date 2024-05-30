using BLL.Interfaces;
using BLL.Services;
using BOL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PRN221_JewelryShop.Pages
{
    public class TestModel : PageModel
    {
        private readonly IProductService _productService;

        public TestModel(IProductService productService)
        {
            _productService = productService;
        }

        public List<Product> Products { get; private set; }

        public void OnGet()
        {
            Products = _productService.GetProductList();
            foreach (Product product in Products)
            {
                Console.WriteLine(product.ProductName);
            }
        }
    }
}
