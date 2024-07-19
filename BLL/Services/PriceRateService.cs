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
    public class PriceRateService : IPriceRateService
    {
        private readonly IPriceRateRepository _priceRateRepository;
        public PriceRateService(IPriceRateRepository priceRateRepository)
        {
            _priceRateRepository = priceRateRepository;
        }
        public PriceRate GetPriceRate()
        {
            return _priceRateRepository.GetObjPriceRate();
        }

        public bool UpdatePriceRate(PriceRateRequestDTO model)
        {
            var priceRate = GetPriceRate();
            priceRate.PriceRate1 = double.Parse(model.Value);
            _priceRateRepository.UpdatePriceRate(priceRate);
            return _priceRateRepository.SaveChanges();
        }
    }
}
