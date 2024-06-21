using BOL;
using DAL.DAO;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class SaleOrderRepository : ISaleOrderRepository
    {
        private readonly IGenericDAO<SaleOrder> _saleOrderDao;
        public SaleOrderRepository(IGenericDAO<SaleOrder> saleOrderDao)
        {
            _saleOrderDao = saleOrderDao;
        }

        public void AddSaleOrder(SaleOrder saleOrder)
        {
            _saleOrderDao.Add(saleOrder);
        }

        public List<SaleOrder> GetAllSaleOrders()
        {
            return _saleOrderDao.GetAll(c => true)
                 .Include(c => c.Employee)
                 .Include(c => c.Customer)
                 .Include(c => c.Policy)
                 .Include(c => c.PromotionCode)
                 .Include(c => c.SaleOrderDetails)
                 .ThenInclude(i => i.Product)
                 .OrderByDescending(c => c.CreatedDate)
                 .ToList();
        }

        public SaleOrder GetSaleOrderById(Guid parseSaleOrderId)
        {
            var saleOrderList = _saleOrderDao.GetAll(c => true)
                .Include(c => c.Employee)
                .Include(c => c.Customer)
                .Include(c => c.Policy)
                .Include(c => c.PromotionCode)
                .Include(c => c.Policy)
                .Include(c => c.SaleOrderDetails)
                .ThenInclude(i => i.Product)
                .OrderByDescending(c => c.CreatedDate)
                .ToList();
            return saleOrderList?.FirstOrDefault(c => c.SaleOrderId == parseSaleOrderId);
        }

        public bool SaveChange()
        {
            return _saleOrderDao.SaveChange();
        }
    }
}
