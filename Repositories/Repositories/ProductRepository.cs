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
	public class ProductRepository : IProductRepository
	{
		IGenericDAO<Product> _productDAO;
		public ProductRepository(IGenericDAO<Product> productDAO)
		{
			_productDAO = productDAO;
		}

		public List<Product> GetProductList()
		{
			return _productDAO.GetAll(c => c.Status == true).Include(c => c.Counter).ToList();
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
			var productList = _productDAO.GetAll(c => true).Include(c => c.Counter).ToList();
			return productList?.FirstOrDefault(c => c.ProductId == id);
		}

		public List<Product> GetTopSellingProductInMonth(List<Guid> ordDetailList)
		{
			var productList = _productDAO.GetAll(s => ordDetailList.Contains((Guid)s.ProductId));
			return productList.ToList();
		}
	}
}
