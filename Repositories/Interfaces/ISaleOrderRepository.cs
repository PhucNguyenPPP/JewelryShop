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
		decimal? GetTotalSalesInMonth(int year, int month);
		List<Guid> GetAllSaleOrderIDInMonth(int year, int month);
		decimal? GetTotalSalesAmountInRange(DateTime start, DateTime end);
	}
}
