using BLL.Interfaces;
using BOL;
using DTO;
using Microsoft.IdentityModel.Tokens;
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
        private readonly IReturnOrderRepository _returnOrderRepository;
        private readonly IProductRepository _productRepository;
        public BuyBackOrderService(ISaleOrderRepository saleOrderRepo, IGoldPriceService goldPriceService,
            IBuyBackPolicyService buyBackPolicyService,
            ISaleOrderDetailRepository saleOrderDetailRepo,
            IBuyBackOrderRepository buyBackOrderRepo,
            IBuyBackOrderDetailRepository buyBackOrderDetailRepo,
            IReturnOrderRepository returnOrderRepository,
            IProductRepository productRepository)
        {
            _saleOrderRepo = saleOrderRepo;
            _goldPriceService = goldPriceService;
            _buyBackPolicyService = buyBackPolicyService;
            _saleOrderDetailRepo = saleOrderDetailRepo;
            _buyBackOrderRepo = buyBackOrderRepo;
            _buyBackOrderDetailRepo = buyBackOrderDetailRepo;
            _returnOrderRepository = returnOrderRepository;
            _returnOrderRepository = returnOrderRepository;
            _productRepository = productRepository;
        }

        public bool BuyBackSaleOrder(BuyBackRequestDTO model, string employeeId)
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

                var saleOrderDetail = _saleOrderDetailRepo
                    .GetSaleOrderDetailByProductId(Guid.Parse(model.ProductIds[i]), Guid.Parse(model.SaleOrderId));


                // nếu là vàng thì lấy giá trên thị trường
                if (bbPolicy.PolicyName == "Only Gold")
                {
                    decimal priceGold = 0;
                    if (saleOrderDetail.Product.ProductName == "SJC Gold Bar 100 fences")
                    {
                        priceGold = Convert.ToDecimal(goldPriceList.FirstOrDefault(c => c.Type == "SJC Gold Bar").BuyPrice);
                        totalPrice += priceGold * Int32.Parse(model.Amount[i]);
                    }

                    if (saleOrderDetail.Product.ProductName == "SJC Gold Bar 50 fences")
                    {
                        priceGold = Convert.ToDecimal(goldPriceList.FirstOrDefault(c => c.Type == "SJC Gold Bar").BuyPrice * 0.5);
                        totalPrice += priceGold * Int32.Parse(model.Amount[i]);
                    }

                    if (saleOrderDetail.Product.ProductName == "SJC Gold Bar 10 fences")
                    {
                        priceGold = Convert.ToDecimal(goldPriceList.FirstOrDefault(c => c.Type == "SJC Gold Bar").BuyPrice * 0.1);
                        totalPrice += priceGold * Int32.Parse(model.Amount[i]);
                    }

                    if (saleOrderDetail.Product.ProductName == "24K Gold 50 fences")
                    {
                        priceGold = Convert.ToDecimal(goldPriceList.FirstOrDefault(c => c.Type == "24K Gold").BuyPrice * 0.5);
                        totalPrice += priceGold * Int32.Parse(model.Amount[i]);
                    }

                    if (saleOrderDetail.Product.ProductName == "18K Gold 50 fences")
                    {
                        priceGold = Convert.ToDecimal(goldPriceList.FirstOrDefault(c => c.Type == "18K Gold").BuyPrice * 0.5);
                        totalPrice += priceGold * Int32.Parse(model.Amount[i]);
                    }

                    if (saleOrderDetail.Product.ProductName == "14K Gold 50 fences")
                    {
                        priceGold = Convert.ToDecimal(goldPriceList.FirstOrDefault(c => c.Type == "14K Gold").BuyPrice * 0.5);
                        totalPrice += priceGold * Int32.Parse(model.Amount[i]);
                    }

                    if (saleOrderDetail.Product.ProductName == "10K Gold 50 fences")
                    {
                        priceGold = Convert.ToDecimal(goldPriceList.FirstOrDefault(c => c.Type == "10K Gold").BuyPrice * 0.5);
                        totalPrice += priceGold * Int32.Parse(model.Amount[i]);
                    }

                    BuyBackOrderDetail buyBackOrderDetail = new BuyBackOrderDetail()
                    {
                        BbodetailId = Guid.NewGuid(),
                        PolicyId = Guid.Parse(model.PolicyIds[i]),
                        Bboid = bbOrderId,
                        ProductId = Guid.Parse(model.ProductIds[i]),
                        Bbprice = priceGold * Int32.Parse(model.Amount[i]),
                        Amount = Int32.Parse(model.Amount[i]),
                        Reason = model.Reason[i]
                    };
                    buyBackOrderDetails.Add(buyBackOrderDetail);

                    var product = _productRepository.GetProductList().FirstOrDefault(c => c.ProductId == Guid.Parse(model.ProductIds[i]));
                    product.AmountInStock += Int32.Parse(model.Amount[i]);
                    _productRepository.UpdateProduct(product);
                }
                // nếu không lấy phần trăm từ buy back policy
                else
                {
                    var policyValue = Convert.ToDecimal(bbPolicy.PolicyValue);
                    totalPrice += ((saleOrderDetail.FinalPrice / saleOrderDetail.Amount) * (policyValue / 100)) * Int32.Parse(model.Amount[i]);
                    BuyBackOrderDetail buyBackOrderDetail = new BuyBackOrderDetail()
                    {
                        BbodetailId = Guid.NewGuid(),
                        PolicyId = Guid.Parse(model.PolicyIds[i]),
                        Bboid = bbOrderId,
                        ProductId = Guid.Parse(model.ProductIds[i]),
                        Bbprice = ((saleOrderDetail.FinalPrice / saleOrderDetail.Amount) * (policyValue/100)) * Int32.Parse(model.Amount[i]),
                        Amount = Int32.Parse(model.Amount[i]),
                        Reason = model.Reason[i]

                    };
                    buyBackOrderDetails.Add(buyBackOrderDetail);

                    var product = _productRepository.GetProductList().FirstOrDefault(c => c.ProductId == Guid.Parse(model.ProductIds[i]));
                    product.AmountInStock += Int32.Parse(model.Amount[i]);
                    _productRepository.UpdateProduct(product);
                }
            }

            _buyBackOrderDetailRepo.AddRangeBuyBackOrderDetail(buyBackOrderDetails);

            BuyBackOrder buyBackOrder = new BuyBackOrder()
            {
                Bboid = bbOrderId,
                TotalPrice = totalPrice,
                CreatedDate = DateTime.Now,
                CustomerId = saleOrder.CustomerId,
                EmployeeId = Guid.Parse(employeeId),
                SaleOrderId = saleOrder.SaleOrderId,
            };

            _buyBackOrderRepo.AddBuyBackOrder(buyBackOrder);
            return _buyBackOrderRepo.SaveChange();
        }

        public bool CheckAmountBuyBackValid(BuyBackRequestDTO model)
        {
            var check = true;
            for (var i = 0; i < model.ProductIds.Count; i++)
            {
                var bbOrderList = _buyBackOrderRepo.GetAllBuyBackOrders().Where(c => c.SaleOrderId == Guid.Parse(model.SaleOrderId)).ToList();
                var returnOrderList = _returnOrderRepository.GetAllReturnOrders().Where(c => c.SaleOrderId == Guid.Parse(model.SaleOrderId)).ToList();
                int? buyBackAmount = 0;

                foreach (var b in bbOrderList)
                {
                    var bbOrderDetail = b.BuyBackOrderDetails.FirstOrDefault(c => c.Bboid == b.Bboid && c.ProductId == Guid.Parse(model.ProductIds[i]));
                    if (bbOrderDetail != null && bbOrderDetail.Amount != null)
                    {
                        buyBackAmount += bbOrderDetail.Amount;
                    }
                    else
                    {
                        buyBackAmount += 0;
                    }
                }

                foreach (var r in returnOrderList)
                {
                    var returnOrderDetail = r.ReturnOrderDetails.FirstOrDefault(c => c.ReturnOrderId == r.ReturnOrderId && c.ProductId == Guid.Parse(model.ProductIds[i]));
                    if (returnOrderDetail != null && returnOrderDetail.Amount != null)
                    {
                        buyBackAmount += returnOrderDetail.Amount;
                    }
                    else
                    {
                        buyBackAmount += 0;
                    }
                }

                var orderDetail = _saleOrderDetailRepo.GetSaleOrderDetailByProductId(Guid.Parse(model.ProductIds[i]), Guid.Parse(model.SaleOrderId));
                if(Int32.Parse(model.Amount[i]) < 0)
                {
                    return check = false;
                }
                if (Int32.Parse(model.Amount[i]) > (orderDetail.Amount - buyBackAmount))
                {
                    return check = false;
                }
            }
            return check;
        }

        public bool CheckBuyBackReasonValid(BuyBackRequestDTO model)
        {
            if(model.Reason.Any( c => c.IsNullOrEmpty()))
            {
                return false;
            }
            return true;
        }

        public List<BuyBackOrder> GetAllBuyBackOrders()
        {
            return _buyBackOrderRepo.GetAllBuyBackOrders();
        }

        public BuyBackOrder GetBuyBackOrderById(string id)
        {
            Guid.TryParse(id, out Guid parseId);
            return _buyBackOrderRepo.GetBuyBackOrder(parseId);
        }

        public List<BuyBackOrder> SearchBuyBackOrders(string searchValue)
        {
            var list = GetAllBuyBackOrders();
            return list.Where(c => c.Customer.CustomerName.ToLower().Contains(searchValue.ToLower())).ToList();
        }
    }
}
