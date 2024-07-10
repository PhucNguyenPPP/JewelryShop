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
    public class ReturnOrderDetailRepository : IReturnOrderDetailRepository
    {
        private readonly IGenericDAO<ReturnOrderDetail> _returnOrderDetailDao;
        public ReturnOrderDetailRepository(IGenericDAO<ReturnOrderDetail> returnOrderDetailDao)
        {
            _returnOrderDetailDao = returnOrderDetailDao;
        }

        public void AddRangeReturnOrderDetail(List<ReturnOrderDetail> list)
        {
            _returnOrderDetailDao.AddRange(list);
        }
    }
}
