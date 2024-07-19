using BLL.Interfaces;
using BOL;
using DTO;
using Repositories.Interfaces;
using Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ReturnOrderService : IReturnOrderService
    {
        private readonly IReturnOrderRepository _returnOrderRepo;
        private readonly IReturnOrderDetailRepository _returnOrderDetailRepo;
        private readonly IReturnPolicyService _returnPolicyService;
        private readonly ISaleOrderRepository _saleOrderRepo;
        private readonly ISaleOrderDetailRepository _saleOrderDetailRepo;
        private readonly IBuyBackOrderRepository _buyBackOrderRepo;
        private readonly IBuyBackOrderDetailRepository _buyBackOrderDetailRepo;
        private readonly IProductRepository _productRepository;
        public ReturnOrderService(IReturnOrderRepository returnOrderRepository, IReturnOrderDetailRepository returnOrderDetailRepository,
            IReturnPolicyService returnPolicyService, ISaleOrderRepository saleOrderRepository,
            ISaleOrderDetailRepository saleOrderDetailRepository, 
            IBuyBackOrderRepository buyBackOrderRepo, 
            IBuyBackOrderDetailRepository buyBackOrderDetailRepo,
            IProductRepository productRepository)
        {
            _returnOrderRepo = returnOrderRepository;
            _returnOrderDetailRepo = returnOrderDetailRepository;
            _returnPolicyService = returnPolicyService;
            _saleOrderRepo = saleOrderRepository;
            _saleOrderDetailRepo = saleOrderDetailRepository;
            _buyBackOrderRepo = buyBackOrderRepo;
            _buyBackOrderDetailRepo = buyBackOrderDetailRepo;
            _productRepository = productRepository;
        }

        public bool CheckAmountReturnValid(ReturnRequestDTO model)
        {
            var check = true;
            for (var i = 0; i < model.ProductIds.Count; i++)
            {
                var bbOrderList = _buyBackOrderRepo.GetAllBuyBackOrders().Where(c => c.SaleOrderId == Guid.Parse(model.SaleOrderId)).ToList();
                var returnOrderList = _returnOrderRepo.GetAllReturnOrders().Where(c => c.SaleOrderId == Guid.Parse(model.SaleOrderId)).ToList();
                int? returnAmount = 0;

                foreach (var b in bbOrderList)
                {
                    var bbOrderDetail = b.BuyBackOrderDetails.FirstOrDefault(c => c.Bboid == b.Bboid && c.ProductId == Guid.Parse(model.ProductIds[i]));
                    if (bbOrderDetail != null && bbOrderDetail.Amount != null)
                    {
                        returnAmount += bbOrderDetail.Amount;
                    }
                    else
                    {
                        returnAmount += 0;
                    }
                }

                foreach (var r in returnOrderList)
                {
                    var returnOrderDetail = r.ReturnOrderDetails.FirstOrDefault(c => c.ReturnOrderId == r.ReturnOrderId && c.ProductId == Guid.Parse(model.ProductIds[i]));
                    if (returnOrderDetail != null && returnOrderDetail.Amount != null)
                    {
                        returnAmount += returnOrderDetail.Amount;
                    }
                    else
                    {
                        returnAmount += 0;
                    }
                }

                var orderDetail = _saleOrderDetailRepo.GetSaleOrderDetailByProductId(Guid.Parse(model.ProductIds[i]), Guid.Parse(model.SaleOrderId));
                if (Int32.Parse(model.Amount[i]) < 0)
                {
                    return check = false;
                }
                if (Int32.Parse(model.Amount[i]) > (orderDetail.Amount - returnAmount))
                {
                    return check = false;
                }
            }
            return check;
        }

        public List<ReturnOrder> GetAllReturnOrders()
        {
            return _returnOrderRepo.GetAllReturnOrders();
        }

        public ReturnOrder GetReturnOrderById(string id)
        {
            Guid.TryParse(id, out Guid parseId);
            return _returnOrderRepo.GetReturnOrder(parseId);
        }

        public bool ReturnSaleOrder(ReturnRequestDTO model, string employeeId)
        {
            var returnPolicy = _returnPolicyService.GetReturnPolicyRefundPercentage();
            var saleOrder = _saleOrderRepo.GetSaleOrderById(Guid.Parse(model.SaleOrderId));

            var returnOrderId = Guid.NewGuid();
            decimal? totalPrice = 0;

            List<ReturnOrderDetail> returnOrderDetails = new List<ReturnOrderDetail>();
            for (int i = 0; i < model.ProductIds?.Count; i++)
            {

                var saleOrderDetail = _saleOrderDetailRepo
                    .GetSaleOrderDetailByProductId(Guid.Parse(model.ProductIds[i]), Guid.Parse(model.SaleOrderId));

                    var policyValue = Convert.ToDecimal(returnPolicy.PolicyValue);
                    totalPrice += ((saleOrderDetail.FinalPrice / saleOrderDetail.Amount) * (policyValue / 100)) * Int32.Parse(model.Amount[i]);
                    ReturnOrderDetail returnOrderDetail = new ReturnOrderDetail()
                    {
                        ReturnOrderDetailId = Guid.NewGuid(),
                        ReturnOrderId = returnOrderId,
                        ProductId = Guid.Parse(model.ProductIds[i]),
                        ReturnPrice = ((saleOrderDetail.FinalPrice / saleOrderDetail.Amount) * (policyValue / 100)) * Int32.Parse(model.Amount[i]),
                        Amount = Int32.Parse(model.Amount[i]),
                        Reason = model.Reason[i],

                    };
                    returnOrderDetails.Add(returnOrderDetail);

                var product = _productRepository.GetProductList().FirstOrDefault(c => c.ProductId == Guid.Parse(model.ProductIds[i]));
                product.AmountInStock += Int32.Parse(model.Amount[i]);
                _productRepository.UpdateProduct(product);

            }

            _returnOrderDetailRepo.AddRangeReturnOrderDetail(returnOrderDetails);

            ReturnOrder returnOrder = new ReturnOrder()
            {
                ReturnOrderId = returnOrderId,
                TotalPrice = totalPrice,
                CreatedDate = DateTime.Now,
                CustomerId = saleOrder.CustomerId,
                EmployeeId = Guid.Parse(employeeId),
                SaleOrderId = saleOrder.SaleOrderId,
            };

            _returnOrderRepo.AddReturnOrder(returnOrder);
            return _returnOrderRepo.SaveChange();
        }

        public List<ReturnOrder> SearchReturnOrders(string searchValue)
        {
            var list = GetAllReturnOrders();
            return list.Where(c => c.Customer.CustomerName.ToLower().Contains(searchValue.ToLower())).ToList();
        }
    }
}
