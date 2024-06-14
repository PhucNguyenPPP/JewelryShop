using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL.DAO;
using DTO;

namespace Repositories.Interfaces
{
    public interface IProductRepository
    {
        
        List<Product> GetProductList();

        void AddProduct(Product product);

        void UpdateProduct(Product product);

        bool SaveChange();

        Product GetById(Guid id);
    }
}
