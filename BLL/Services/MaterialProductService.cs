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
    public class MaterialProductService : IMaterialProductService
    {
        private readonly IMaterialProductRepository _materialProductRepository;
        public MaterialProductService(IMaterialProductRepository materialProductRepository)
        {
            _materialProductRepository = materialProductRepository;

        }
        public List<MaterialProduct> GetAllMaterialProductByProductId(string productId)
        {
            Guid.TryParse(productId, out var parseProductId);
            var materialProductList = _materialProductRepository.GetAllMaterialProductByProductId(parseProductId);
            return materialProductList;
        }
    }
}
