using BOL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IBuyBackOrderService
    {
        List<BuyBackOrder> GetAllBuyBackOrders();
        List<BuyBackOrder> SearchBuyBackOrders(string searchValue);
        bool BuyBackSaleOrder (BuyBackRequestDTO model, string employeeId);
        BuyBackOrder GetBuyBackOrderById (string id);
        bool CheckAmountBuyBackValid(BuyBackRequestDTO model);
        bool CheckBuyBackReasonValid(BuyBackRequestDTO model);
    }
}
