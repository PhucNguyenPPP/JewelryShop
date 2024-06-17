using BOL;
using DAL.DAO;
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
        public List<Material> GetAllMaterial()
        {
           return _materialDao.GetAll(c => true).ToList();
        }

        public Material GetMaterial(Guid materialId)
        {
            return _materialDao.GetById(materialId);
        }
    }
}
