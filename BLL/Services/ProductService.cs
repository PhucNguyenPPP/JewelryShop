using BLL.Interfaces;
using BOL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProductService : IProductService
    {
        IGenericRepository<Product> _productRepo;

        public ProductService(IGenericRepository<Product> productRepo)
        {
            _productRepo = productRepo;
        }

        public List<Product> GetProductList()
        {
            return _productRepo.GetAll(p => true).ToList();
        }
    }
}
