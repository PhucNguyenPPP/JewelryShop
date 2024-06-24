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
	}
}
