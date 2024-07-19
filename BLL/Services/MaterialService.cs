using BLL.Interfaces;
using BOL;
using DTO;
using Repositories.Interfaces;
using Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepository _materialRepo;
        private readonly IMaterialTypeRepository _materialTypeRepo;
        public MaterialService(IMaterialRepository materialRepo, IMaterialTypeRepository materialTypeRepo)
        {
            _materialRepo = materialRepo;
            _materialTypeRepo = materialTypeRepo;
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

        public bool AddMaterial(MaterialRequestDTO model)
        {
            Material newMaterial = new Material
            {
                MaterialId = Guid.NewGuid(),
                MaterialName = model.MaterialName,
                MaterialTypeId = Guid.Parse(model.MaterialTypeId),
                AmountInStock = decimal.Parse(model.AmountInStock),
                Price = decimal.Parse(model.Price),
                Status = true,
            };
            _materialRepo.AddMaterial(newMaterial);
            var result = _materialRepo.SaveChange();
            return result;
        }

        public bool UpdateMaterial(MaterialRequestDTO model)
        {
            Guid.TryParse(model.MaterialId, out Guid parseMaterialId);
            var material = _materialRepo.GetMaterial(parseMaterialId);
            if (material == null)
            {
                return false;
            }

            material.MaterialName = model.MaterialName;
            material.MaterialTypeId = Guid.Parse(model.MaterialTypeId);
            material.Price = decimal.Parse(model.Price);
            material.AmountInStock = decimal.Parse(model.AmountInStock);
            _materialRepo.UpdateMaterial(material);
            var result = _materialRepo.SaveChange();
            return result;
        }

        public bool DeleteMaterial(string id)
        {
            Guid.TryParse(id, out Guid parseMaterialId);
            var material = _materialRepo.GetMaterial(parseMaterialId);
            if (material == null)
            {
                return false;
            }
            material.Status = false;
            _materialRepo.DeleteMaterial(material);
            var result = _materialRepo.SaveChange();
            return result;
        }

        public bool CheckMaterialNameExist(string name)
        {
            return _materialRepo.GetAllMaterial().Any(c => c.MaterialName.ToLower() == name.ToLower());
        }

        public List<Material> SearchMaterial(string searchValue)
        {
            List<Material> material = _materialRepo.GetAllMaterial().ToList();
            return material.Where(c => c.MaterialName.ToLower().Contains(searchValue.ToLower())).ToList();
        }

        public bool CheckAmountValidation(MaterialRequestDTO model)
        {
            var type = _materialTypeRepo.GetMaterialType(Guid.Parse(model.MaterialTypeId));
            if(type == null)
            {
                return false;
            }
            Regex regexDiamond = new Regex(@"^\d+$");
            if (type.TypeName == "Diamond" && !regexDiamond.IsMatch(model.AmountInStock))
            {
                return false;
            }
            Regex regexGold = new Regex(@"^\d+(\.\d+)?$");
            if (type.TypeName == "Gold" && !regexGold.IsMatch(model.AmountInStock))
            {
                return false;
            }
            return true;
        }
    }
}
