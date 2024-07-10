using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using BOL;
using Repositories.Interfaces;

namespace BLL.Services
{
	public class DashboardService : IDashboardService
	{
		private readonly ISaleOrderRepository _saleOrderRepository;
		private readonly ISaleOrderDetailRepository _saleOrderDetailRepository;
		private readonly IEmployeeRepository _employeeRepository;
		private readonly IProductRepository _productRepository;
		public DashboardService(ISaleOrderRepository saleOrderRepository, ISaleOrderDetailRepository saleOrderDetailRepository
			, IProductRepository productRepository) {
			_saleOrderRepository = saleOrderRepository;
			_saleOrderDetailRepository = saleOrderDetailRepository;
			_productRepository = productRepository;
		}
		public List<SaleOrder> GetAllSaleOrdersInMonth(int year, int month)
		{
			return _saleOrderRepository.GetAllSaleOrdersInMonth(year, month);
		}

		public List<Product> GetTopSellingProductInMonth(int year, int month)
		{
			var saleOrdList = _saleOrderRepository.GetAllSaleOrderIDInMonth(year,month);
			var ordDetailList= _saleOrderDetailRepository.GetTopSellingProductIDInMonth(saleOrdList);
			var productList = _productRepository.GetTopSellingProductInMonth(ordDetailList);
			return productList; 
		}

		public decimal? GetTotalSalesAmountInRange(DateTime start, DateTime end)
		{
			return _saleOrderRepository.GetTotalSalesAmountInRange(start, end);
		}

		public decimal? GetTotalSalesInMonth(int year, int month)
		{
			return _saleOrderRepository.GetTotalSalesInMonth(year, month);
		}

		public List<SaleOrder> GetAllSaleOrdersInRange(DateTime start, DateTime end)
		{
			return _saleOrderRepository.GetAllSaleOrdersInRange(start, end);
		}
		public object GetTotalSalesOfEmployeeInMonth(Guid employeeId, int year, int month)
		{
			decimal? sale =  _saleOrderRepository.GetTotalSalesByEmployee(employeeId, year, month);
			return _employeeRepository.GetEmployeeSales(sale,employeeId);

		}
	}
}
