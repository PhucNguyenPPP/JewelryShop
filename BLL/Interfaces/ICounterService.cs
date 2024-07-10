using BOL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICounterService
    {
        List<Counter> GetAllCounter();

		List<Counter> SearchCounter(string searchValue);

		bool AddCounter(CounterDTO counterDTO);

		bool UpdateCounter(CounterDTO counterDTO);

		bool DeleteCounter(string counterId);

		bool CheckCounterExist(string counterName);
		Counter GetCounterById (string counterId);
	}
}
