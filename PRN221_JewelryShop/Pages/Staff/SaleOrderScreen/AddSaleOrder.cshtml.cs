using BLL.Interfaces;
using BLL.Services;
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
        private readonly IImageService _imageService;

        public AddSaleOrderModel(ISaleOrderService saleOrderService, IProductService productService,
            ICustomerService customerService, IPromotionCodeService promotionCodeService, IImageService imageService)
        {
            _saleOrderService = saleOrderService;
            _productService = productService;
            _customerService = customerService;
            _promotionCodeService = promotionCodeService;
            _imageService = imageService;
        }

        [BindProperty(SupportsGet = true)]
        public string? SearchProductValue { get; set; }
        public List<Product> ProductResultList { get; set; }
        public List<Customer> CustomerResultList { get; set; }
        [FromQuery(Name = "id")]
        public string? CustomerId { get; set; }
        [BindProperty]
        public Customer Customer { get; set; }
        [BindProperty]
        public CustomerResquestDTO? CustomerResquestDTO { get; set; }
        [BindProperty]
        public IFormFile? CustomerAvatar { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? CustomerIdForm { get; set; }

        [BindProperty]
        public SaleOrderRequestDTO SaleOrderDTO { get; set; }

        public LoginResponse LoginResponse { get; set; }
        public List<PromotionProgramCode> PromotionCodeList { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? SearchCustomer { get; set; }
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

            var customerList = _customerService.SearchCustomers(SearchCustomer);
            if (customerList != null)
            {
                CustomerResultList = customerList;
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

        public IActionResult OnPostSetCustomer()
        {
            PromotionCodeList = _promotionCodeService.GetAllPromotionCodeNotExpiredList();
            var loginResponseString = HttpContext.Session.GetString("LoginResponse");
            if (loginResponseString == null)
            {
                return RedirectToPage("/Login");
            }

            LoginResponse = JsonSerializer.Deserialize<LoginResponse>(loginResponseString);
            if (LoginResponse.RoleName != "Staff")
            {
                return RedirectToPage("/Login");
            }

            Customer = _customerService.GetCustomer(CustomerIdForm);

            return Page();
        }

        public IActionResult OnPostCreateCustomer()
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

            if (!ModelState.IsValid)
            {
                TempData["CreateModelError"] = "Model State Invalid";
                return Page();
            }

            if (_customerService.CheckEmailAlreadyExists(CustomerResquestDTO.Email))
            {
                TempData["CreateError"] = "Email already exists";
                return Page();
            }

            if (_customerService.CheckPhoneAlreadyExists(CustomerResquestDTO.PhoneNumber))
            {
                TempData["CreateError"] = "PhoneNumber already exists";
                return Page();
            }

            /*convert image file to base 64*/
            if (CustomerAvatar != null)
            {
                CustomerResquestDTO.AvatarImg = _imageService.ConvertToBase64(CustomerAvatar);
            }
            CustomerResquestDTO.EmployeeId = LoginResponse.EmployeeId.ToString();
            var customerId = Guid.NewGuid();
            var result = _customerService.AddCustomerForSaleOrder(CustomerResquestDTO, customerId);
            if (result)
            {
                Customer = _customerService.GetCustomer(customerId.ToString());
                return Page();
            }
            else
            {
                return Page();
            }
        }

    }
}
