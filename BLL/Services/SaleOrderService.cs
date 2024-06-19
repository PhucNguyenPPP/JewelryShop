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
    public class SaleOrderService : ISaleOrderService
    {
        private readonly ISaleOrderRepository _saleOrderRepo;
        private readonly IProductService _productService;
        private readonly ISaleOrderDetailRepository _saleOrderDetailRepo;
        private readonly IProductRepository _productRepo;
        public SaleOrderService(ISaleOrderRepository saleOrderRepo, IProductService productService,
            ISaleOrderDetailRepository saleOrderDetailRepo,
            IProductRepository productRepo)
        {
            _saleOrderRepo = saleOrderRepo;
            _productService =  productService;
            _saleOrderDetailRepo = saleOrderDetailRepo;
            _productRepo = productRepo;
        }

        public bool AddSaleOrder(SaleOrderRequestDTO saleOrderDTO, Guid employeeId)
        {
            var saleOrderId = Guid.NewGuid();
            decimal? totalPrice = 0;
            List<SaleOrderDetail> saleOrderDetails = new List<SaleOrderDetail>();

            foreach (var i in saleOrderDTO.SaleOrderDetails)
            {
                var product = _productService.GetProductById(i.ProductId);
                totalPrice += product.Price * i.Amount;

                product.AmountInStock -= i.Amount;
                _productRepo.UpdateProduct(product);

                SaleOrderDetail saleOrderDetail = new SaleOrderDetail()
                {
                    SaleOrderDetailId = Guid.NewGuid(),
                    SaleOrderId = saleOrderId,
                    ProductId = Guid.Parse(i.ProductId),
                    Amount = i.Amount,
                };
                saleOrderDetails.Add(saleOrderDetail);
            }

            SaleOrder saleOrder = new SaleOrder()
            {
                SaleOrderId = saleOrderId,
                TotalPrice = totalPrice,
                FinalPrice = totalPrice,
                CreatedDate = DateTime.Now,
                CustomerId = Guid.Parse(saleOrderDTO.CustomerId),
                EmployeeId = employeeId
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
