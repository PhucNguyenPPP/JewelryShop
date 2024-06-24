using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using BOL;
using Repositories.Interfaces;

namespace BLL.Services
{
	public class DashboardService : IDashboardService
	{
		private readonly ISaleOrderRepository _saleOrderRepository;
		public DashboardService(ISaleOrderRepository saleOrderRepository) {
			_saleOrderRepository = saleOrderRepository;
		}

		public List<SaleOrder> GetAllSaleOrdersInMonth(int year, int month)
		{
			return _saleOrderRepository.GetAllSaleOrdersInMonth(year, month);
		}
	}
}
