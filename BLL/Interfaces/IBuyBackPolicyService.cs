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
        List<BuyBackPolicy> SearchBuyBackPolicies(string searchValue);
        bool AddBuyBackPolicy(BuyBackPolicyRequestDTO model);
        bool UpdateBuyBackPolicy(BuyBackPolicyRequestDTO model);
        bool DeleteBuyBackPolicy(string policyId);
        BuyBackPolicy GetBuyBackPolicy(string policyId);
        bool CheckPolicyNameExist (string policyName);

    }
}
