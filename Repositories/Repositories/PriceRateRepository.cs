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
    public class PriceRateRepository : IPriceRateRepository
    {
        private IGenericDAO<PriceRate> _priceRateDao;
        public PriceRateRepository(IGenericDAO<PriceRate> priceRateDao)
        {
            _priceRateDao = priceRateDao;
        }

        public PriceRate GetObjPriceRate()
        {
            return _priceRateDao.GetAll(c => true).FirstOrDefault();
        }

        public double GetPriceRate()
        {
            return (double)_priceRateDao.GetAll(c => true).FirstOrDefault().PriceRate1;
        }

        public bool SaveChanges()
        {
            return _priceRateDao.SaveChange();
        }

        public void UpdatePriceRate(PriceRate priceRate)
        {
            _priceRateDao.Update(priceRate);
        }


    }
}
