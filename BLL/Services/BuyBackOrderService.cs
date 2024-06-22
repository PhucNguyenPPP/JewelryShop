using BLL.Interfaces;
using BOL;
using DTO;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class BuyBackOrderService : IBuyBackOrderService
    {
        private readonly ISaleOrderRepository _saleOrderRepo;
        private readonly IGoldPriceService _goldPriceService;
        private readonly IBuyBackPolicyService _buyBackPolicyService;
        private readonly ISaleOrderDetailRepository _saleOrderDetailRepo;
        private readonly IBuyBackOrderRepository _buyBackOrderRepo;
        private readonly IBuyBackOrderDetailRepository _buyBackOrderDetailRepo;
        public BuyBackOrderService(ISaleOrderRepository saleOrderRepo, IGoldPriceService goldPriceService,
            IBuyBackPolicyService buyBackPolicyService,
            ISaleOrderDetailRepository saleOrderDetailRepo,
            IBuyBackOrderRepository buyBackOrderRepo,
            IBuyBackOrderDetailRepository buyBackOrderDetailRepo)
        {
            _saleOrderRepo = saleOrderRepo;
            _goldPriceService = goldPriceService;
            _buyBackPolicyService = buyBackPolicyService;
            _saleOrderDetailRepo = saleOrderDetailRepo;
            _buyBackOrderRepo = buyBackOrderRepo;
            _buyBackOrderDetailRepo = buyBackOrderDetailRepo;
        }

        public bool BuyBackSaleOrder(BuyBackRequestDTO model)
        {
            var bbPolicyList = _buyBackPolicyService.GetAllBuyBackPolicies();
            var saleOrder = _saleOrderRepo.GetSaleOrderById(Guid.Parse(model.SaleOrderId));
            var goldPriceList = _goldPriceService.GetGoldPrices();

            var bbOrderId = Guid.NewGuid();
            decimal? totalPrice = 0;

            List<BuyBackOrderDetail> buyBackOrderDetails = new List<BuyBackOrderDetail>();
            for (int i = 0; i < model.ProductIds?.Count; i++)
            {
                var bbPolicy = bbPolicyList.FirstOrDefault(c => c.PolicyId == Guid.Parse(model.PolicyIds[i]));

                //update sale order detail thành status đã buy back
                var saleOrderDetail = _saleOrderDetailRepo
                    .GetSaleOrderDetailByProductId(Guid.Parse(model.ProductIds[i]), Guid.Parse(model.SaleOrderId));
                saleOrderDetail.IsBuyBack = true;

                _saleOrderDetailRepo.UpdateSaleOrderDetail(saleOrderDetail);

                // nếu là vàng thì lấy giá trên thị trường
                if (bbPolicy.PolicyName == "Only Gold")
                {
                    decimal priceGold = 0;
                    if (saleOrderDetail.Product.ProductName == "SJC Gold Bar 100 fences")
                    {
                        priceGold = Convert.ToDecimal(goldPriceList.FirstOrDefault(c => c.Type == "SJC Gold Bar").BuyPrice);
                        totalPrice += priceGold;
                    }

                    if (saleOrderDetail.Product.ProductName == "SJC Gold Bar 50 fences")
                    {
                        priceGold = Convert.ToDecimal(goldPriceList.FirstOrDefault(c => c.Type == "SJC Gold Bar").BuyPrice * 0.5);
                        totalPrice += priceGold;
                    }

                    if (saleOrderDetail.Product.ProductName == "SJC Gold Bar 10 fences")
                    {
                        priceGold = Convert.ToDecimal(goldPriceList.FirstOrDefault(c => c.Type == "SJC Gold Bar").BuyPrice * 0.1);
                        totalPrice += priceGold;
                    }

                    if (saleOrderDetail.Product.ProductName == "24K Gold 50 fences")
                    {
                        priceGold = Convert.ToDecimal(goldPriceList.FirstOrDefault(c => c.Type == "24K Gold").BuyPrice * 0.5);
                        totalPrice += priceGold;
                    }

                    if (saleOrderDetail.Product.ProductName == "18K Gold 50 fences")
                    {
                        priceGold = Convert.ToDecimal(goldPriceList.FirstOrDefault(c => c.Type == "18K Gold").BuyPrice * 0.5);
                        totalPrice += priceGold;
                    }

                    if (saleOrderDetail.Product.ProductName == "14K Gold 50 fences")
                    {
                        priceGold = Convert.ToDecimal(goldPriceList.FirstOrDefault(c => c.Type == "14K Gold").BuyPrice * 0.5);
                        totalPrice += priceGold;
                    }

                    if (saleOrderDetail.Product.ProductName == "10K Gold 50 fences")
                    {
                        priceGold = Convert.ToDecimal(goldPriceList.FirstOrDefault(c => c.Type == "10K Gold").BuyPrice * 0.5);
                        totalPrice += priceGold;
                    }

                    BuyBackOrderDetail buyBackOrderDetail = new BuyBackOrderDetail()
                    {
                        BbodetailId = Guid.NewGuid(),
                        PolicyId = Guid.Parse(model.PolicyIds[i]),
                        Bboid = bbOrderId,
                        ProductId = Guid.Parse(model.ProductIds[i]),
                        Bbprice = priceGold,
                    };
                    buyBackOrderDetails.Add(buyBackOrderDetail);
                }
                // nếu không lấy phần trăm từ buy back policy
                else
                {
                    var policyValue = Convert.ToDecimal(bbPolicy.PolicyValue);
                    totalPrice += saleOrderDetail.FinalPrice * (policyValue);
                    BuyBackOrderDetail buyBackOrderDetail = new BuyBackOrderDetail()
                    {
                        BbodetailId = Guid.NewGuid(),
                        PolicyId = Guid.Parse(model.PolicyIds[i]),
                        Bboid = bbOrderId,
                        ProductId = Guid.Parse(model.ProductIds[i]),
                        Bbprice = saleOrderDetail.FinalPrice * (policyValue),
                    };
                    buyBackOrderDetails.Add(buyBackOrderDetail);
                }
            }

            _buyBackOrderDetailRepo.AddRangeBuyBackOrderDetail(buyBackOrderDetails);

            BuyBackOrder buyBackOrder = new BuyBackOrder()
            {
                Bboid = bbOrderId,
                TotalPrice = totalPrice,
                CreatedDate = DateTime.Now,
                CustomerId = saleOrder.CustomerId,
            };

            _buyBackOrderRepo.AddBuyBackOrder(buyBackOrder);
            return _buyBackOrderRepo.SaveChange();
        }
    }
}
