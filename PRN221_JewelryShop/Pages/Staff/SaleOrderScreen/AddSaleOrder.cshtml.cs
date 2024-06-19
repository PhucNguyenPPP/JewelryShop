using BLL.Interfaces;
using BOL;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace PRN221_JewelryShop.Pages.Staff.SaleOrderScreen
{
    public class AddSaleOrderModel : PageModel
    {
        private readonly ISaleOrderService _saleOrderService;
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;

        public AddSaleOrderModel(ISaleOrderService saleOrderService, IProductService productService,
            ICustomerService customerService)
        {
            _saleOrderService = saleOrderService;
            _productService = productService;
            _customerService = customerService;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchProductValue { get; set; }
        public List<Product> ProductResultList { get; set; }
        [FromQuery(Name = "id")]
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        [BindProperty(SupportsGet = true)]
        public string CustomerIdForm { get; set; }

        [BindProperty]
        public SaleOrderRequestDTO SaleOrderDTO { get; set; }

        public LoginResponse LoginResponse { get; set; }
        public void OnGet()
        {
            Customer = _customerService.GetCustomer(CustomerId);
            CustomerId = CustomerIdForm;
        }
        public void OnPostSearchProduct()
        {
            Customer = _customerService.GetCustomer(CustomerIdForm);
            CustomerId = CustomerIdForm;

            ProductResultList = new List<Product>();

            var loginResponseString = HttpContext.Session.GetString("LoginResponse");
            if (loginResponseString == null)
            {
                return;
            };

            LoginResponse = JsonSerializer.Deserialize<LoginResponse>(loginResponseString);
            if (LoginResponse.RoleName != "Staff")
            {
                return;
            }

            if (!SearchProductValue.IsNullOrEmpty())
            {
                ProductResultList = _productService.SearchProduct(SearchProductValue);
            }
        }

        public IActionResult OnPostAddSaleOrder([FromBody] SaleOrderRequestDTO saleOrderRequestDTO)
        {
            Customer = _customerService.GetCustomer(CustomerIdForm);

            var loginResponseString = HttpContext.Session.GetString("LoginResponse");
            if (loginResponseString == null)
            {
                return RedirectToPage("/Login");
            };

            LoginResponse = JsonSerializer.Deserialize<LoginResponse>(loginResponseString);
            if (LoginResponse.RoleName != "Staff")
            {
                return RedirectToPage("/Login");
            }
            SaleOrderDTO = saleOrderRequestDTO;
            var checkAmountInStock = _saleOrderService.CheckAmountInStock(saleOrderRequestDTO);
            if(!checkAmountInStock.IsSuccess)
            {
                TempData["ErrorSaleOrderMsg"] = checkAmountInStock.Message;
                return Page();
            }

            var result = _saleOrderService.AddSaleOrder(SaleOrderDTO, (Guid)LoginResponse.EmployeeId); 
            if(result)
            {
                TempData["CreateSaleOrderMsg"] = "Add Sale Order Successfully";
                ProductResultList = new List<Product>();
                return RedirectToPage("/Staff/SaleOrderScreen/SaleOrderManagement");
            }
            TempData["CreateSaleOrderMsg"] = "Add Sale Order Unsuccessfully";
            return Page();
        }

    }
}
