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
    public class MaterialTypeRepository : IMaterialTypeRepository
    {
        private readonly IGenericDAO<MaterialType> _materialTypeDao;
        public MaterialTypeRepository(IGenericDAO<MaterialType> materialTypeDao)
        {
            _materialTypeDao = materialTypeDao;
        }
        public List<MaterialType> GetAllMaterialType()
        {
            return _materialTypeDao.GetAll(c => true).ToList();
        }

        public MaterialType GetMaterialType(Guid id)
        {
            return _materialTypeDao.GetAll(c => c.TypeId == id).FirstOrDefault();
        }
    }
}
