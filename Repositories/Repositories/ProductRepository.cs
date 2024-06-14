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
    public class ProductRepository : IProductRepository
    {
        IGenericDAO<Product> _productDAO;
        public ProductRepository(IGenericDAO<Product> productDAO)
        {
            _productDAO = productDAO;
        }

        public List<Product> GetProductList()
        {
            return _productDAO.GetAll(c => c.Status == true).ToList();
        }

        public bool SaveChange()
        {
            return _productDAO.SaveChange();
        }

        public void AddProduct(Product product)
        {
            _productDAO.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            _productDAO.Update(product);
        }

        public Product GetById(Guid id)
        {
            return _productDAO.GetById(id);
        }
    }
}
