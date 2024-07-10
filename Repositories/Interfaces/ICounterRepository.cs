using BOL;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ICounterRepository
    {
        List<Counter> GetAllCounter();

		void AddCounter(Counter counter);

		void UpdateCounter(Counter counter);

		bool SaveChange();

		Counter? GetByEmployeeId(Guid id);

		Counter? GetByProductId(Guid id);

		Counter? GetById(Guid id);
	}
}
