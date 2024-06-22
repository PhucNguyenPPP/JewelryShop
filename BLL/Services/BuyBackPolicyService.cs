using BLL.Interfaces;
using BOL;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class BuyBackPolicyService : IBuyBackPolicyService
    {
        private readonly IBuyBackPolicyRepository _buyBackPolicyRepo;
        public BuyBackPolicyService(IBuyBackPolicyRepository buyBackPolicyRepo)
        {
            _buyBackPolicyRepo = buyBackPolicyRepo;
        }
        public List<BuyBackPolicy> GetAllBuyBackPolicies()
        {
            return _buyBackPolicyRepo.GetAllBuyBackPolicies();
        }
    }
}
