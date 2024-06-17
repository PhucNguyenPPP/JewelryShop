using BLL.Interfaces;
using BOL;
using Repositories.Interfaces;
using Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepository _materialRepo;
        public MaterialService(IMaterialRepository materialRepo)
        {
            _materialRepo = materialRepo;
        }
        public List<Material> GetAllMaterial()
        {
            return _materialRepo.GetAllMaterial();
        }

        public Material GetMaterial(string materialId)
        {
            Guid.TryParse(materialId, out Guid parseMaterialId);
            var material = _materialRepo.GetMaterial(parseMaterialId);
            return material;
        }
    }
}
