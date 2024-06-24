using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ISaleOrderRepository
    {
        List<SaleOrder> GetAllSaleOrders();
        void AddSaleOrder(SaleOrder saleOrder);
        bool SaveChange();
        SaleOrder GetSaleOrderById(Guid parseSaleOrderId);
		List<SaleOrder> GetAllSaleOrdersInMonth(int year, int month);
	}
}
