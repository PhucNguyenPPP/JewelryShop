using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IBuyBackOrderRepository
    {
        void AddBuyBackOrder(BuyBackOrder model);
        bool SaveChange();
    }
}
