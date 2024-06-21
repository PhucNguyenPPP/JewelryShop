using BOL;
using DAL.DAO;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class ReturnPolicyRepository : IReturnPolicyRepository
    {
        private readonly IGenericDAO<ReturnPolicy> _returnPolicyDao;
        public ReturnPolicyRepository(IGenericDAO<ReturnPolicy> returnPolicyDao)
        {
            _returnPolicyDao = returnPolicyDao;
        }

        public List<ReturnPolicy> GetAllReturnPolicies()
        {
            return _returnPolicyDao.GetAll(c => c.Status == true).ToList();
        }

        public ReturnPolicy GetById(Guid id)
        {
            return _returnPolicyDao.GetById(id);
        }

        public bool SaveChange()
        {
            return _returnPolicyDao.SaveChange();
        }

        public void UpdateReturnPolicy(ReturnPolicy model)
        {
            _returnPolicyDao.Update(model);
        }
    }
}
