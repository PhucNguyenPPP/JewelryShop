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
    public class CounterRepository : ICounterRepository
    {
        private readonly IGenericDAO<Counter> _counterDao;
        public CounterRepository(IGenericDAO<Counter> counterDao)
        {
            _counterDao = counterDao;
        }
        public List<Counter> GetAllCounter()
        {
            return _counterDao.GetAll(c => c.Status == true).ToList();
        }
    }
}
