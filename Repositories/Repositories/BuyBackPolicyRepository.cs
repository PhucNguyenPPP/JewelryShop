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
    public class BuyBackPolicyRepository : IBuyBackPolicyRepository
    {
        private readonly IGenericDAO<BuyBackPolicy> _buyBackPolicyDao;
        public BuyBackPolicyRepository(IGenericDAO<BuyBackPolicy> buyBackPolicyDao)
        {
            _buyBackPolicyDao = buyBackPolicyDao;
        }

        public List<BuyBackPolicy> GetAllBuyBackPolicies()
        {
            return _buyBackPolicyDao.GetAll(c => c.Status == true).ToList();
        }
    }
}
