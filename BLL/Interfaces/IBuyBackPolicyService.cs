using BOL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IBuyBackPolicyService
    {
        List<BuyBackPolicy> GetAllBuyBackPolicies();
    }
}
