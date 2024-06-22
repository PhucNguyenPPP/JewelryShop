using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IBuyBackOrderDetailRepository
    {
        void AddRangeBuyBackOrderDetail(List<BuyBackOrderDetail> list);
    }
}
