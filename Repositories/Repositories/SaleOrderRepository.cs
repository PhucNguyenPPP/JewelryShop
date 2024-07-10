using BOL;
using DAL.DAO;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

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

		public List<Guid> GetAllSaleOrderIDInMonth(int year, int month)
		{
			var list = GetAllSaleOrdersInMonth(year, month);        
            return list.Select(c => c.SaleOrderId).ToList();    
		}

		public List<SaleOrder> GetAllSaleOrders()
        {
            return _saleOrderDao.GetAll(c => true)
                 .Include(c => c.Employee)
                 .Include(c => c.Customer)
                 .Include(c => c.PromotionCode)
                 .Include(c => c.SaleOrderDetails)
                 .ThenInclude(i => i.Product)
                 .OrderByDescending(c => c.CreatedDate)
                 .ToList();
        }

		public List<SaleOrder> GetAllSaleOrdersInMonth(int year, int month)
		{
			return _saleOrderDao.GetAll(c => c.CreatedDate.Month==month && c.CreatedDate.Year==year)
				 .Include(c => c.Employee)
				 .Include(c => c.Customer)
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
                .Include(c => c.PromotionCode)
                .Include(c => c.SaleOrderDetails)
                .ThenInclude(i => i.Product)
                .OrderByDescending(c => c.CreatedDate)
                .ToList();
            return saleOrderList?.FirstOrDefault(c => c.SaleOrderId == parseSaleOrderId);
        }

		public decimal? GetTotalSalesAmountInRange(DateTime start, DateTime end)
		{
			List<SaleOrder> list = _saleOrderDao.GetAll(c => c.CreatedDate > start && c.CreatedDate < end)
				 .Include(c => c.Employee)
				 .Include(c => c.Customer)
				 .Include(c => c.PromotionCode)
				 .Include(c => c.SaleOrderDetails)
				 .ThenInclude(i => i.Product)
				 .OrderByDescending(c => c.CreatedDate)
				 .ToList();
			decimal? total = 0;
			foreach (SaleOrder saleOrder in list)
			{
				total = total + saleOrder.FinalPrice;
			}
			return total;	
		}

		public decimal? GetTotalSalesByEmployee(Guid employeeId,, int year, int month)
		{
			return _saleOrderDao.GetAll(s => s.EmployeeId == employeeId&&s.CreatedDate.Year==year&&s.CreatedDate.Month==month).Sum(s => s.TotalPrice);			
			
		}

		public decimal? GetTotalSalesInMonth(int year, int month)
		{
			List<SaleOrder> list = _saleOrderDao.GetAll(c => c.CreatedDate.Month == month && c.CreatedDate.Year == year)
				 .Include(c => c.Employee)
				 .Include(c => c.Customer)
				 .Include(c => c.PromotionCode)
				 .Include(c => c.SaleOrderDetails)
				 .ThenInclude(i => i.Product)
				 .OrderByDescending(c => c.CreatedDate)
				 .ToList();
            decimal? total = 0;  
            foreach (SaleOrder saleOrder in list)
            {
                total = total + saleOrder.FinalPrice;
            }
            return total;

		}

		public bool SaveChange()
        {
            return _saleOrderDao.SaveChange();
        }
    }
}
