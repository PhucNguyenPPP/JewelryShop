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
    public class ReturnOrderRepository : IReturnOrderRepository
    {
        private readonly IGenericDAO<ReturnOrder> _returnDao;
        public ReturnOrderRepository(IGenericDAO<ReturnOrder> returnDao) 
        {
            _returnDao = returnDao;
        }

        public void AddReturnOrder(ReturnOrder model)
        {
            _returnDao.Add(model);
        }

        public List<ReturnOrder> GetAllReturnOrders()
        {
            return _returnDao.GetAll(c => true)
               .Include(c => c.Customer)
               .Include(c => c.ReturnOrderDetails)
               .ThenInclude(c => c.Product)
               .Include(c => c.ReturnOrderDetails)
               .OrderByDescending(c => c.CreatedDate)
               .ToList();
        }

        public ReturnOrder GetReturnOrder(Guid id)
        {
            return _returnDao.GetAll(c => c.ReturnOrderId == id)
                .Include(c => c.Customer)
               .Include(c => c.ReturnOrderDetails)
               .ThenInclude(c => c.Product)
               .Include(c => c.ReturnOrderDetails)
               .OrderByDescending(c => c.CreatedDate)
                .FirstOrDefault();
        }

        public bool SaveChange()
        {
            return _returnDao.SaveChange();
        }
    }
}
