using BLL.Interfaces;
using BOL;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PRN221_JewelryShop.Pages.Staff.SaleOrderScreen
{
    public class SaleOrderDetailModel : PageModel
    {
        private readonly ISaleOrderService _saleOrderService;

        public LoginResponse LoginResponse { get; set; }
        public SaleOrder? SaleOrder { get; set; }

        [FromQuery(Name = "id")]
        public string SaleOrderId { get; set; }

        public SaleOrderDetailModel(ISaleOrderService saleOrderService)
        {
            _saleOrderService = saleOrderService;
        }
        public void OnGet()
        {
            SaleOrder = _saleOrderService.GetSaleOrderById(SaleOrderId);
        }
    }
}
