using BOL;
using DAL.DAO;
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
    }
}
