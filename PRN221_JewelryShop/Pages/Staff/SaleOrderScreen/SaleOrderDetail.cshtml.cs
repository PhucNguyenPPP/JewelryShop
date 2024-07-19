using BLL.Interfaces;
using BOL;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace PRN221_JewelryShop.Pages.Staff.SaleOrderScreen
{
    public class SaleOrderDetailModel : PageModel
    {
        private readonly ISaleOrderService _saleOrderService;
        private readonly IReturnPolicyService _returnPolicyService;
        private readonly IBuyBackPolicyService _buyBackPolicyService;
        private readonly IGoldPriceService _goldPriceService;
        private readonly IBuyBackOrderService _buyBackOrderService;
        private readonly IReturnOrderService _returnOrderService;

        public LoginResponse LoginResponse { get; set; }
        public SaleOrder? SaleOrder { get; set; }

        [FromQuery(Name = "id")]
        public string SaleOrderId { get; set; }

        public ReturnPolicy? ReturnPolicyDateAllowReturn { get; set; }
        public ReturnPolicy? ReturnPolicyRefundPercentage { get; set; }
        public List<BuyBackPolicy>? BuyBackPolicyList { get; set; }
        public List<GoldPriceDTO>? GoldPriceDTOList { get; set; }
        public SaleOrderDetailModel(ISaleOrderService saleOrderService, IReturnPolicyService returnPolicyService,
            IBuyBackPolicyService buyBackPolicyService, IGoldPriceService goldPriceService,
            IBuyBackOrderService buyBackOrderService, IReturnOrderService returnOrderService)
        {
            _saleOrderService = saleOrderService;
            _returnPolicyService = returnPolicyService;
            _buyBackPolicyService  = buyBackPolicyService;
            _goldPriceService = goldPriceService;
            _buyBackOrderService = buyBackOrderService;
            _returnOrderService = returnOrderService;
        }
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
            GetSaleOrder();
            GetReturnPolicy();
            GetBuyBackPolicyList();
            GetGoldPriceDTOList();
            return Page();
        }

        private void GetSaleOrder()
        {
            SaleOrder = _saleOrderService.GetSaleOrderById(SaleOrderId);
        }

        private void GetReturnPolicy()
        {
            ReturnPolicyDateAllowReturn = _returnPolicyService.GetReturnPolicyDateAllowReturn();
            ReturnPolicyRefundPercentage = _returnPolicyService.GetReturnPolicyRefundPercentage();
        }

        private void GetBuyBackPolicyList()
        {
            BuyBackPolicyList = _buyBackPolicyService.GetAllBuyBackPolicies();
        }

        private void GetGoldPriceDTOList()
        {
            GoldPriceDTOList = _goldPriceService.GetGoldPrices();
        }

        public IActionResult OnPostReturnProduct([FromBody] ReturnRequestDTO model)
        {
            GetReturnPolicy();
            GetBuyBackPolicyList();
            GetGoldPriceDTOList();
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
            var checkAmountValid = _returnOrderService.CheckAmountReturnValid(model);
            if (!checkAmountValid)
            {
                TempData["ReturnAmountError"] = "Return Amount is invalid";
                return StatusCode(400);
            }
            var result = _returnOrderService.ReturnSaleOrder(model, LoginResponse.EmployeeId.ToString());
            if (result)
            {
                TempData["ReturnMsg"] = "Return Successfully";
                GetSaleOrder();
                return StatusCode(200);
            }
            else
            {
                TempData["ReturnMsg"] = "Return Unsuccessfully";
                GetSaleOrder();
                return StatusCode(400);
            }
        }

        public IActionResult OnPostBuyBackProduct([FromBody] BuyBackRequestDTO model)
        {
            GetReturnPolicy();
            GetBuyBackPolicyList();
            GetGoldPriceDTOList();
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
            var checkReason = _buyBackOrderService.CheckBuyBackReasonValid(model);
            if(!checkReason)
            {
                TempData["BuyBackReasonError"] = "Please input reason";
                return StatusCode(400);
            }
            var checkAmountValid = _buyBackOrderService.CheckAmountBuyBackValid(model);
            if(!checkAmountValid)
            {
                TempData["BuyBackAmountError"] = "Buy Back Amount is invalid";
                return StatusCode(400);
            }
            var result = _buyBackOrderService.BuyBackSaleOrder(model, LoginResponse.EmployeeId.ToString());
            if (result)
            {
                TempData["BuyBackMsg"] = "Buy back Successfully";
                GetSaleOrder();
                return StatusCode(200);
            }
            else
            {
                TempData["BuyBackMsg"] = "Buy back Unsuccessfully";
                GetSaleOrder();
                return StatusCode(400);
            }
        }
    }
}
