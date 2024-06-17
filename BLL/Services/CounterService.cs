using BLL.Interfaces;
using BOL;
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
        public List<Counter> GetAllCounter()
        {
           return _counterRepo.GetAllCounter();
        }
    }
}
