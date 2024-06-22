using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ISaleOrderDetailRepository
    {
        void AddRangeSaleOrderDetail(List<SaleOrderDetail> list);
        void UpdateSaleOrderDetail(SaleOrderDetail model);
        SaleOrderDetail GetSaleOrderDetailByProductId(Guid productId, Guid saleOrderId);
    }
}
