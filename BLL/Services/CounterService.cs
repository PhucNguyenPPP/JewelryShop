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
			Counter? counter = _counterRepo.GetAllCounter().Where(c => c.CounterId.Equals(Guid.Parse(counterId))).FirstOrDefault();
			if (counter == null)
			{
				return false;
			}
			counter.Status = false;
			_counterRepo.UpdateCounter(counter);

			foreach (var i in counter.Employees)
			{
				var employee = _counterRepo.GetByEmployeeId(i.EmployeeId);
				if (employee == null)
				{
					return false;
				}
				employee.Status = false;
				_counterRepo.UpdateCounter(employee);
			}

			foreach (var i in counter.Products)
			{
				var product = _counterRepo.GetByProductId(i.ProductId);
				if(product == null)
				{
					return false;
				}
				product.Status = false;
				_counterRepo.UpdateCounter(product);
			}
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
