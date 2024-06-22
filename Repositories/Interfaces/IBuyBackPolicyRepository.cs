using BOL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IBuyBackPolicyRepository
    {
        List<BuyBackPolicy> GetAllBuyBackPolicies();
        BuyBackPolicy GetById(Guid id);
        void AddBuyBackPolicy(BuyBackPolicy model);
        void UpdateBuyBackPolicy(BuyBackPolicy model);
        void DeleteBuyBackPolicy(BuyBackPolicy model);
        bool SaveChange();
    }
}
