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

		public List<SaleOrderDetail> GetAllSaleOrderDetailInMonth(List<SaleOrder> list)
		{
			var orderDetails = _saleOrderDetailDao.GetAll(s => true);
			list.Select(l => l.SaleOrderId).ToList();
			var ordList = new List<SaleOrderDetail>();
			foreach (var order in orderDetails)
			{
				foreach (var id in list)
				{

					if (order.SaleOrderId.Equals(id))
					{
						ordList.Add(order);
					}

				}
			}
			return ordList;
		}

		public void UpdateSaleOrderDetail(SaleOrderDetail model)
		{
			_saleOrderDetailDao.Update(model);
		}

		public List<Guid> GetTopSellingProductIDInMonth(List<Guid> saleOrdList)
		{
			var list = _saleOrderDetailDao.GetAll(s => saleOrdList.Contains((Guid)s.SaleOrderId)).ToList();
			var amountInMonth = list.GroupBy(p => p.ProductId).Select(group => new
			{
				ProductId = group.Key,
				Amount = group.Sum(p => p.Amount),
			})
			.ToList();
			var maxAmount = amountInMonth.Max(p => p.Amount);
			List<Guid> products = amountInMonth.Where(p => p.Amount == maxAmount)
				.Select(p => (Guid)p.ProductId)
				.ToList();
			return products;
		}
	}
}
