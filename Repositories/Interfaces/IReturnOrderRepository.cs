using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IReturnOrderRepository
    {
        void AddReturnOrder(ReturnOrder model);
        bool SaveChange();
        List<ReturnOrder> GetAllReturnOrders();
        ReturnOrder GetReturnOrder(Guid id);
    }
}
