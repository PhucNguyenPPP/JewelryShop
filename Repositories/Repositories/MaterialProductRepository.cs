using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL.DAO;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories.Repositories
{
    public class MaterialProductRepository : IMaterialProductRepository
    {
        private readonly IGenericDAO<MaterialProduct> _materialProductDAO;
        public MaterialProductRepository( IGenericDAO<MaterialProduct> materialProductDAO)
        {
            _materialProductDAO = materialProductDAO;
        }

        public void AddRange(List<MaterialProduct> materialProducts)
        {
            _materialProductDAO.AddRange(materialProducts);
        }

        public void DeleteRange(List<MaterialProduct> materialProduct)
        {
            _materialProductDAO.DeleteRange(materialProduct);
        }

        public List<MaterialProduct> GetAllMaterialProductByProductId(Guid productId)
        {
            return _materialProductDAO.GetAll(c => c.ProductId == productId).Include(c => c.Material).ToList();
        }
    }
}
