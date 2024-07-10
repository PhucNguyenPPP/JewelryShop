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
    public class CounterService : ICounterService
    {
        private readonly ICounterRepository _counterRepo;
        public CounterService(ICounterRepository counterRepo)
        {
            _counterRepo = counterRepo;
        }

		public bool AddCounter(CounterDTO counterDTO)
		{
			throw new NotImplementedException();
		}

		public bool CheckCounterExist(string counterName)
		{
			throw new NotImplementedException();
		}

		public bool DeleteCounter(string counterId)
		{
			throw new NotImplementedException();
		}

		public List<Counter> GetAllCounter()
        {
           return _counterRepo.GetAllCounter();
        }

		public List<Counter> SearchCounter(string searchValue)
		{
			List<Counter> counter = _counterRepo.GetAllCounter().ToList();
			return counter.Where(c => c.CounterName.ToLower().Contains(searchValue.ToLower())).ToList();
		}

		public bool UpdateCounter(CounterDTO counterDTO)
		{
			throw new NotImplementedException();
		}
	}
}
