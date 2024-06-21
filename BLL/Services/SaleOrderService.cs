using BLL.Interfaces;
using BOL;
using DTO;
using Microsoft.IdentityModel.Tokens;
using Repositories.Interfaces;
using Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class SaleOrderService : ISaleOrderService
    {
        private readonly ISaleOrderRepository _saleOrderRepo;
        private readonly IProductService _productService;
        private readonly ISaleOrderDetailRepository _saleOrderDetailRepo;
        private readonly IProductRepository _productRepo;
        private readonly IPromotionCodeService _promotionCodeService;
        public SaleOrderService(ISaleOrderRepository saleOrderRepo, IProductService productService,
            ISaleOrderDetailRepository saleOrderDetailRepo,
            IProductRepository productRepo, IPromotionCodeService promotionCodeService)
        {
            _saleOrderRepo = saleOrderRepo;
            _productService =  productService;
            _saleOrderDetailRepo = saleOrderDetailRepo;
            _productRepo = productRepo;
            _promotionCodeService = promotionCodeService;
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
                };
                saleOrderDetails.Add(saleOrderDetail);
            }


            SaleOrder saleOrder = new SaleOrder()
            {
                SaleOrderId = saleOrderId,
                TotalPrice = totalPriceAll,
                FinalPrice = totalPriceAll - (totalPriceAll * (discountPercentage / 100)),
                CreatedDate = DateTime.Now,
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
    }
}
