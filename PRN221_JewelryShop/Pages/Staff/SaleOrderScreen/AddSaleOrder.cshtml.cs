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
        private readonly IPromotionCodeService _promotionCodeService;

        public AddSaleOrderModel(ISaleOrderService saleOrderService, IProductService productService,
            ICustomerService customerService, IPromotionCodeService promotionCodeService)
        {
            _saleOrderService = saleOrderService;
            _productService = productService;
            _customerService = customerService;
            _promotionCodeService = promotionCodeService;
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
        public List<PromotionProgramCode> PromotionCodeList { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchCustomer { get; set; }
        public IActionResult OnGet()
        {
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
            PromotionCodeList = _promotionCodeService.GetAllPromotionCodeNotExpiredList();
            return Page();
        }

        public IActionResult OnPostSearchProduct()
        {
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

            Customer = _customerService.GetCustomer(CustomerIdForm);
            CustomerId = CustomerIdForm;
            PromotionCodeList = _promotionCodeService.GetAllPromotionCodeNotExpiredList();

            ProductResultList = new List<Product>();

            if (!SearchProductValue.IsNullOrEmpty())
            {
                ProductResultList = _productService.SearchProduct(SearchProductValue);
            }
            return Page();
        }

        public IActionResult OnGetSearchCustomer()
        {
            PromotionCodeList = _promotionCodeService.GetAllPromotionCodeNotExpiredList();
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

            if (SearchCustomer.IsNullOrEmpty())
            {
                TempData["ErrorSearchCustomer"] = "No result";
                return Page();
            }

            var customer = _customerService.SearchCustomerByEmailOrPhone(SearchCustomer);
            if (customer != null)
            {
                Customer = customer;
                return Page();
            }
            else
            {
                TempData["ErrorSearchCustomer"] = "No result";
                return Page();
            }
        }


        public IActionResult OnPostAddSaleOrder([FromBody] SaleOrderRequestDTO saleOrderRequestDTO)
        {
            Customer = _customerService.GetCustomer(CustomerIdForm);
            PromotionCodeList = _promotionCodeService.GetAllPromotionCodeNotExpiredList();

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
            if (!checkAmountInStock.IsSuccess)
            {
                TempData["ErrorSaleOrderMsg"] = checkAmountInStock.Message;
                return StatusCode(400);
            }

            var result = _saleOrderService.AddSaleOrder(SaleOrderDTO, (Guid)LoginResponse.EmployeeId);
            if (result)
            {
                TempData["CreateSaleOrderMsg"] = "Add Sale Order Successfully";
                ProductResultList = new List<Product>();
                return StatusCode(200);
            }
            TempData["CreateSaleOrderMsg"] = "Add Sale Order Unsuccessfully";
            return StatusCode(400);
        }

    }
}
