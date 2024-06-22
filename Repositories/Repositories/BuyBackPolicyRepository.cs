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

        public void AddBuyBackPolicy(BuyBackPolicy model)
        {
            _buyBackPolicyDao.Add(model);
        }

        public void DeleteBuyBackPolicy(BuyBackPolicy model)
        {
           _buyBackPolicyDao.Update(model);
        }

        public List<BuyBackPolicy> GetAllBuyBackPolicies()
        {
            return _buyBackPolicyDao.GetAll(c => c.Status == true).ToList();
        }

        public BuyBackPolicy GetById(Guid id)
        {
            return _buyBackPolicyDao.GetById(id);
        }

        public bool SaveChange()
        {
            return _buyBackPolicyDao.SaveChange();
        }

        public void UpdateBuyBackPolicy(BuyBackPolicy model)
        {
            _buyBackPolicyDao.Update(model);
        }
    }
}
