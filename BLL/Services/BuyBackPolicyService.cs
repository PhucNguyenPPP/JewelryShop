using BLL.Interfaces;
using BOL;
using DTO;
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

        public bool AddBuyBackPolicy(BuyBackPolicyRequestDTO model)
        {
            BuyBackPolicy buyBackPolicy = new BuyBackPolicy()
            {
                PolicyId = Guid.NewGuid(),
                PolicyName = model.PolicyName,
                PolicyDescription = model.PolicyDescription,
                PolicyValue = model.PolicyValue,
                Status = true,
            };
            _buyBackPolicyRepo.AddBuyBackPolicy(buyBackPolicy);
            return _buyBackPolicyRepo.SaveChange();
        }

        public bool CheckPolicyNameExist(string policyName)
        {
            var list = GetAllBuyBackPolicies();
            if(list.Any(c => c.PolicyName == policyName))
            {
                return true;
            }
            return false;
        }

        public bool DeleteBuyBackPolicy(string policyId)
        {
            var bbPolicy = GetBuyBackPolicy(policyId);
            if (bbPolicy == null)
            {
                return false;
            }
            bbPolicy.Status = false;

            _buyBackPolicyRepo.DeleteBuyBackPolicy(bbPolicy);
            return _buyBackPolicyRepo.SaveChange();
        }

        public List<BuyBackPolicy> GetAllBuyBackPolicies()
        {
            return _buyBackPolicyRepo.GetAllBuyBackPolicies();
        }

        public BuyBackPolicy GetBuyBackPolicy(string policyId)
        {
            Guid.TryParse(policyId, out Guid parsePolicyId);
            return _buyBackPolicyRepo.GetById(parsePolicyId);
        }

        public List<BuyBackPolicy> SearchBuyBackPolicies(string searchValue)
        {
            var list = GetAllBuyBackPolicies();
            return list.Where(c => c.PolicyName.ToLower().Contains(searchValue.ToLower())).ToList();
        }

        public bool UpdateBuyBackPolicy(BuyBackPolicyRequestDTO model)
        {
            var bbPolicy = GetBuyBackPolicy(model.PolicyId);
            if(bbPolicy == null)
            {
                return false;
            }
            bbPolicy.PolicyName = model.PolicyName;
            bbPolicy.PolicyValue = model.PolicyValue;
            bbPolicy.PolicyDescription = model.PolicyDescription;

            _buyBackPolicyRepo.UpdateBuyBackPolicy(bbPolicy);
            return _buyBackPolicyRepo.SaveChange();
        }
    }
}
