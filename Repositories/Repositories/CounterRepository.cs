using BOL;
using DAL.DAO;
using Microsoft.EntityFrameworkCore;
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

		public void AddCounter(Counter counter)
		{
			_counterDao.Add(counter);
		}

		public List<Counter> GetAllCounter()
        {
            return _counterDao.GetAll(c => c.Status == true).ToList();
        }

		public Counter? GetByEmployeeId(Guid id)
		{
			List<Counter> counters = _counterDao.GetAll(c => true).Include(c => c.Employees).ToList();
			return counters.FirstOrDefault(c => c.CounterId == id);
		}

		public Counter? GetById(Guid id)
		{
			List<Counter> counters = _counterDao.GetAll(C => true).ToList();
			return counters.FirstOrDefault(c=> c.CounterId == id);
		}

		public Counter? GetByProductId(Guid id)
		{
			List<Counter> counters = _counterDao.GetAll(c => true).Include(c=> c.Products).ToList();
			return counters.FirstOrDefault(c => c.CounterId == id);
		}

		public bool SaveChange()
		{
			return _counterDao.SaveChange();
		}

		public void UpdateCounter(Counter counter)
		{
			_counterDao.Update(counter);	
		}
	}
}
