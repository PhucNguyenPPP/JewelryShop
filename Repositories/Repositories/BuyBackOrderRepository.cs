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

        public List<BuyBackOrder> GetAllBuyBackOrders()
        {
            return _buyBackOrderDao.GetAll(c => true)
                .Include(c => c.Customer)
                .Include(c => c.BuyBackOrderDetails)
                .ThenInclude(c => c.Product)
                .Include(c => c.BuyBackOrderDetails)
                .ThenInclude(d => d.Policy)
                .OrderByDescending(c => c.CreatedDate)
                .ToList();
        }

        public BuyBackOrder GetBuyBackOrder(Guid id)
        {
            return _buyBackOrderDao.GetAll(c => c.Bboid == id)
                .Include(c => c.Customer)
                .Include(c => c.BuyBackOrderDetails)
                .ThenInclude(c => c.Product)
                .Include(c => c.BuyBackOrderDetails)
                .ThenInclude(d => d.Policy)
                .OrderByDescending(c => c.CreatedDate)
                .FirstOrDefault();
        }

        public bool SaveChange()
        {
            return _buyBackOrderDao.SaveChange();
        }
    }
}
