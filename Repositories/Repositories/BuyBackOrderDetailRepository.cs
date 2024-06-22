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
    public class BuyBackOrderDetailRepository : IBuyBackOrderDetailRepository
    {
        private readonly IGenericDAO<BuyBackOrderDetail> _bbOrderDetailDao;
        public BuyBackOrderDetailRepository(IGenericDAO<BuyBackOrderDetail> bbOrderDetailDao)
        {
            _bbOrderDetailDao = bbOrderDetailDao;
        }
        public void AddRangeBuyBackOrderDetail(List<BuyBackOrderDetail> list)
        {
            _bbOrderDetailDao.AddRange(list);
        }
    }
}
