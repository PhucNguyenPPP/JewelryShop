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
    public class BuyBackOrderRepository : IBuyBackOrderRepository
    {
        private readonly IGenericDAO<BuyBackOrder> _buyBackOrderDao;
        public BuyBackOrderRepository(IGenericDAO<BuyBackOrder> buyBackOrderDao)
        {
            _buyBackOrderDao = buyBackOrderDao;
        }
        public void AddBuyBackOrder(BuyBackOrder model)
        {
            _buyBackOrderDao.Add(model);
        }

        public bool SaveChange()
        {
            return _buyBackOrderDao.SaveChange();
        }
    }
}
