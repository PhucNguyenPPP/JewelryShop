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
    public class ReturnOrderService : IReturnOrderService
    {
        private readonly IReturnOrderRepository _returnOrderRepo;
        private readonly IReturnOrderDetailRepository _returnOrderDetailRepo;
        private readonly IReturnPolicyService _returnPolicyService;
        private readonly ISaleOrderRepository _saleOrderRepo;
        private readonly ISaleOrderDetailRepository _saleOrderDetailRepo;
        public ReturnOrderService(IReturnOrderRepository returnOrderRepository, IReturnOrderDetailRepository returnOrderDetailRepository,
            IReturnPolicyService returnPolicyService, ISaleOrderRepository saleOrderRepository,
            ISaleOrderDetailRepository saleOrderDetailRepository)
        {
            _returnOrderRepo = returnOrderRepository;
            _returnOrderDetailRepo = returnOrderDetailRepository;
            _returnPolicyService = returnPolicyService;
            _saleOrderRepo = saleOrderRepository;
            _saleOrderDetailRepo = saleOrderDetailRepository;

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

                    };
                    returnOrderDetails.Add(returnOrderDetail);
                
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
