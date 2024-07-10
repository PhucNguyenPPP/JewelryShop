using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IReturnOrderDetailRepository
    {
        void AddRangeReturnOrderDetail(List<ReturnOrderDetail> list);
    }
}
