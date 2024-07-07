using BLL.Interfaces;
using BOL;
using DTO;
using Microsoft.IdentityModel.Tokens;
using Repositories.Interfaces;

namespace BLL.Services
{
    public class SaleOrderService : ISaleOrderService
    {
        private readonly ISaleOrderRepository _saleOrderRepo;
        private readonly IProductService _productService;
        private readonly ISaleOrderDetailRepository _saleOrderDetailRepo;
        private readonly IProductRepository _productRepo;
        private readonly IPromotionCodeService _promotionCodeService;
        private readonly IReturnPolicyService _returnPolicyService;

        public SaleOrderService(ISaleOrderRepository saleOrderRepo, IProductService productService,
            ISaleOrderDetailRepository saleOrderDetailRepo,
            IProductRepository productRepo, IPromotionCodeService promotionCodeService,
            IReturnPolicyService returnPolicyService)
        {
            _saleOrderRepo = saleOrderRepo;
            _productService =  productService;
            _saleOrderDetailRepo = saleOrderDetailRepo;
            _productRepo = productRepo;
            _promotionCodeService = promotionCodeService;
            _returnPolicyService = returnPolicyService;
        }

        public bool AddSaleOrder(SaleOrderRequestDTO saleOrderDTO, Guid employeeId)
        {
            var saleOrderId = Guid.NewGuid();
            decimal? totalPriceAll = 0;
            List<SaleOrderDetail> saleOrderDetails = new List<SaleOrderDetail>();

            decimal? discountPercentage = 0;
            if (!saleOrderDTO.PromotionCode.IsNullOrEmpty())
            {
                var promotionCode = _promotionCodeService.GetPromotionCodeByPromotionCodeId(saleOrderDTO.PromotionCode);
                discountPercentage = promotionCode.DiscountPercentage;
            }

            foreach (var i in saleOrderDTO.SaleOrderDetails)
            {
                var product = _productService.GetProductById(i.ProductId);
                totalPriceAll += product.Price * i.Amount;

                //Get price of 1 product
                decimal? totalPriceProductDetail = 0;
                totalPriceProductDetail += product.Price * i.Amount;

                //set amount in stock
                product.AmountInStock -= i.Amount;
                _productRepo.UpdateProduct(product);


                SaleOrderDetail saleOrderDetail = new SaleOrderDetail()
                {
                    SaleOrderDetailId = Guid.NewGuid(),
                    SaleOrderId = saleOrderId,
                    ProductId = Guid.Parse(i.ProductId),
                    Amount = i.Amount,
                    TotalPrice = product.Price,
                    FinalPrice = totalPriceProductDetail - (product.Price * (discountPercentage/100)),
                    IsBuyBack = false,
                };
                saleOrderDetails.Add(saleOrderDetail);
            }


            SaleOrder saleOrder = new SaleOrder()
            {
                SaleOrderId = saleOrderId,
                TotalPrice = totalPriceAll,
                FinalPrice = totalPriceAll - (totalPriceAll * (discountPercentage / 100)),
                CreatedDate = DateTime.Now,
                PaymentMethod = saleOrderDTO.PaymentMethod,
                TransactionCode = saleOrderDTO.TransactionCode,
                CustomerId = Guid.Parse(saleOrderDTO.CustomerId),
                EmployeeId = employeeId,
                PromotionCodeId = Guid.TryParse(saleOrderDTO.PromotionCode, out Guid parsePromotionCodeId) ? parsePromotionCodeId : null
            };
            _saleOrderRepo.AddSaleOrder(saleOrder);
            _saleOrderDetailRepo.AddRangeSaleOrderDetail(saleOrderDetails);
            return _saleOrderRepo.SaveChange();

        }

        public List<SaleOrder> GetAllSaleOrders()
        {
            return _saleOrderRepo.GetAllSaleOrders();
        }

        public ResponseDTO CheckAmountInStock(SaleOrderRequestDTO model)
        {
            foreach (var i in model.SaleOrderDetails)
            {
                var product = _productRepo.GetById(Guid.Parse(i.ProductId));
                if (i.Amount > product.AmountInStock)
                {
                    return new ResponseDTO
                        (product.ProductName + " only has " + product.AmountInStock + " products left in stock", false);
                }
            }
            return new ResponseDTO("", true);
        }

        public List<SaleOrder> SearchSaleOrder(string searchValue)
        {
            var saleOrderList = _saleOrderRepo.GetAllSaleOrders().ToList();

            return saleOrderList.Where(c => c.Customer.CustomerName.ToLower()
            .Contains(searchValue.ToLower())).ToList();
        }

        public SaleOrder GetSaleOrderById(string id)
        {
            Guid.TryParse(id, out Guid parseSaleOrderId);
            var saleOrder = _saleOrderRepo.GetSaleOrderById(parseSaleOrderId);
            return saleOrder;
        }

        public bool ReturnSaleOrder(ReturnRequestDTO model)
        {
            var refundPercentage = _returnPolicyService.GetReturnPolicyRefundPercentage();
            decimal refundPercentageValue = Convert.ToDecimal(refundPercentage.PolicyValue) / 100m;
            foreach (var id in model.ProductIds)
            {
                var saleOrderDetail = _saleOrderDetailRepo.GetSaleOrderDetailByProductId(Guid.Parse(id), Guid.Parse(model.SaleOrderId));
                saleOrderDetail.ReturnDate = DateTime.Now;
                saleOrderDetail.ReturnPrice = saleOrderDetail.FinalPrice * refundPercentageValue;
                _saleOrderDetailRepo.UpdateSaleOrderDetail(saleOrderDetail);
            }
            return _saleOrderRepo.SaveChange();
        }
    }
}
