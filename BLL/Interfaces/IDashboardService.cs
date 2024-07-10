using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;

namespace BLL.Interfaces
{
	public interface IDashboardService
	{
		List<SaleOrder> GetAllSaleOrdersInMonth(int year, int month);
		decimal? GetTotalSalesInMonth(int year, int month);
		List<Product> GetTopSellingProductInMonth (int year, int month);
		decimal? GetTotalSalesAmountInRange (DateTime start, DateTime end);
		List<SaleOrder> GetAllSaleOrdersInRange(DateTime start, DateTime end);
		object GetTotalSalesOfEmployeeInMonth(Guid employeeId,int year, int month);
	}
}
