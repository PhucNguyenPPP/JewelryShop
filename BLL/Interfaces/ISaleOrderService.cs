using BOL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ISaleOrderService
    {
        bool AddSaleOrder(SaleOrderRequestDTO saleOrderDTO, Guid employeeId);
        List<SaleOrder> GetAllSaleOrders();
        ResponseDTO CheckAmountInStock(SaleOrderRequestDTO model);
        List<SaleOrder> SearchSaleOrder(string searchValue);
        SaleOrder GetSaleOrderById(string id);
    }
}
