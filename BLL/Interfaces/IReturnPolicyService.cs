using BOL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IReturnPolicyService
    {
        List<ReturnPolicy> GetAllReturnPolicies();
        bool UpdateReturnPolicy(List<ReturnPolicyRequestDTO> returnPolicyRequestDTOs);
        ReturnPolicy GetReturnPolicyDateAllowReturn();
        ReturnPolicy GetReturnPolicyRefundPercentage();
    }
}
