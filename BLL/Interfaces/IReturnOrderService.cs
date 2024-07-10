using BOL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IReturnOrderService
    {
        bool ReturnSaleOrder(ReturnRequestDTO model, string employeeId);
        List<ReturnOrder> GetAllReturnOrders();
        List<ReturnOrder> SearchReturnOrders(string searchValue);
        ReturnOrder GetReturnOrderById(string id);
    }
}
