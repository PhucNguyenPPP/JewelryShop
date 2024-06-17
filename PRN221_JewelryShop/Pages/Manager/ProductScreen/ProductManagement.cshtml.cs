using BLL.Interfaces;
using BLL.Services;
using BOL;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace PRN221_JewelryShop.Pages.Manager.ProductScreen
{
    public class ProductManagementModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly IImageService _imageService;

        public List<Product> ProductList { get; set; }
        public LoginResponse LoginResponse { get; set; }

        [BindProperty]
        public ProductRequestDTO ProductRequestDTO { get; set; }

        [BindProperty]
        public IFormFile? CustomerAvatar { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchValue { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 5;
        public ProductManagementModel(IProductService productService, IImageService imageService)
        {
            _productService = productService;
            _imageService = imageService;
        }

        private void GetProductList()
        {
            var listAll = _productService.GetProductList();
            ProductList = PagingList(listAll);
        }

        private List<Product> PagingList(List<Product> list)
        {
            TotalPages = (int)Math.Ceiling(list.Count / (double)PageSize);
            return list.Skip((CurrentPage - 1) * PageSize)
            .Take(PageSize)
            .ToList();
        }

        public IActionResult OnGet()
        {
            GetProductList();

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

        public IActionResult OnGetPaging()
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
                GetProductList();
                return Page();
            }
            else
            {
                var searchList = _productService.SearchProduct(SearchValue);
                var pagingList = PagingList(searchList);
                ProductList = pagingList;
                return Page();
            }
        }

        public IActionResult OnGetSearchProduct()
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
                GetProductList();
                return Page();
            }
            var searchList = _productService.SearchProduct(SearchValue);
            if (searchList.Count == 0)
            {
                TempData["SearchMsg"] = "No result";
                CurrentPage = 0;
                return Page();
            }
            var pagingList = PagingList(searchList);
            ProductList = pagingList;
            return Page();
        }

        public IActionResult OnPostDeleteProduct()
        {
            GetProductList();
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


            var delete = _productService.DeleteProduct(ProductRequestDTO.ProductId);
            if (delete)
            {
                TempData["DeleteMsg"] = "Delete Successfully";
                GetProductList();
                return Page();
            }
            else
            {
                TempData["DeleteMsg"] = "Delete Unsuccessfully";
                return Page();
            }
        }
    }
}
