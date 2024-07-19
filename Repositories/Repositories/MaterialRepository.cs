using BOL;
using DAL.DAO;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly IGenericDAO<Material> _materialDao;
        public MaterialRepository(IGenericDAO<Material> materialDao)
        {
            _materialDao = materialDao;
        }

        public void AddMaterial(Material material)
        {
            _materialDao.Add(material);
        }

        public void DeleteMaterial(Material material)
        {
            _materialDao.Update(material);
        }

        public List<Material> GetAllMaterial()
        {
           return _materialDao.GetAll(c => c.Status == true)
                .Include(c => c.MaterialType)
                .ToList();
        }

        public Material GetMaterial(Guid materialId)
        {
            var materialList = _materialDao.GetAll(c => true)
                .Include(c => c.MaterialType)
                .ToList();
            return materialList?.FirstOrDefault(c => c.MaterialId == materialId);
        }

        public bool SaveChange()
        {
            return _materialDao.SaveChange();
        }

        public void UpdateMaterial(Material material)
        {
            _materialDao.Update(material);
        }
    }
}
