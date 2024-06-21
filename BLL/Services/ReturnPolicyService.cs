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
    public class ReturnPolicyService : IReturnPolicyService
    {
        private readonly IReturnPolicyRepository _returnPolicyRepo;
        public ReturnPolicyService(IReturnPolicyRepository returnPolicyRepo)
        {
            _returnPolicyRepo = returnPolicyRepo;
        }
        public List<ReturnPolicy> GetAllReturnPolicies()
        {
            return _returnPolicyRepo.GetAllReturnPolicies();
        }

        public bool UpdateReturnPolicy(List<ReturnPolicyRequestDTO> returnPolicyRequestDTOs)
        {
            for(int i = 0; i < returnPolicyRequestDTOs.Count; i++)
            {
                var returnPolicy = _returnPolicyRepo.GetById(Guid.Parse(returnPolicyRequestDTOs[i].PolicyId));
                returnPolicy.PolicyDescription = returnPolicyRequestDTOs[i].PolicyDescription;
                returnPolicy.PolicyValue = Int32.Parse(returnPolicyRequestDTOs[i].PolicyValue);
                _returnPolicyRepo.UpdateReturnPolicy(returnPolicy);
            }
            return _returnPolicyRepo.SaveChange();
        }
    }
}
