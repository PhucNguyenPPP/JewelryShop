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
    public class MaterialTypeService : IMaterialTypeService
    {
        private readonly IMaterialTypeRepository _materialTypeRepo;
        public MaterialTypeService(IMaterialTypeRepository materialTypeRepo)
        {
            _materialTypeRepo = materialTypeRepo;
        }
        public List<MaterialType> GetAllMaterialType()
        {
            return _materialTypeRepo.GetAllMaterialType();
        }
    }
}
