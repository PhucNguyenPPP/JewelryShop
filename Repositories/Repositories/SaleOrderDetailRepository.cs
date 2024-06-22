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
    public class SaleOrderDetailRepository : ISaleOrderDetailRepository
    {
        private readonly IGenericDAO<SaleOrderDetail> _saleOrderDetailDao;
        public SaleOrderDetailRepository(IGenericDAO<SaleOrderDetail> saleOrderDetailDao)
        {
            _saleOrderDetailDao = saleOrderDetailDao;
        }
        public void AddRangeSaleOrderDetail(List<SaleOrderDetail> list)
        {
            _saleOrderDetailDao.AddRange(list);
        }

        public SaleOrderDetail GetSaleOrderDetailByProductId(Guid productId, Guid saleOrderId)
        {
            return _saleOrderDetailDao.GetAll(c => c.ProductId == productId && c.SaleOrderId == saleOrderId)
                .Include(c => c.Product)
                .Include(c => c.SaleOrder)
                .FirstOrDefault();
        }

        public void UpdateSaleOrderDetail(SaleOrderDetail model)
        {
            _saleOrderDetailDao.Update(model);
        }
    }
}
