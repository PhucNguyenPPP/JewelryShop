using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IReturnPolicyRepository
    {
        List<ReturnPolicy> GetAllReturnPolicies();
        ReturnPolicy GetById(Guid id);
        void UpdateReturnPolicy(ReturnPolicy model);
        bool SaveChange();
    }
}
