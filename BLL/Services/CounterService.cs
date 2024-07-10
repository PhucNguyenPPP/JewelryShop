using BLL.Interfaces;
using BOL;
using DTO;
using Repositories.Interfaces;
using Repositories.Repositories;
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
			Guid counterId = Guid.NewGuid();
			Counter counter = new Counter()
			{
				CounterId = counterId,
				CounterName = counterDTO.CounterName,
				
			};
		}

		public bool CheckCounterExist(string counterName)
		{
			List<Counter> counters = _counterRepo.GetAllCounter().ToList();
			if (counters.Any(c => c.CounterName == counterName))
			{
				return true;
			}
			return false;
		}

		public bool DeleteCounter(string counterId)
		{
			Counter? counter = _counterRepo.GetById(Guid.Parse(counterId));
			if (counter == null)
			{
				return false;
			}
			counter.Status = false;

			_counterRepo.UpdateCounter(counter);
			bool result = _counterRepo.SaveChange();

			return result;
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
