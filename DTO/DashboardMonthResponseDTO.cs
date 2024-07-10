using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
	public class DashboardMonthResponseDTO
	{
		public decimal? TotalRevenueMonth {  get; set; }
		public decimal? TotalRevenueMonthBefore { get; set; }
		public List<SaleOrder>? SaleOrderListMonth { get; set; }
		public List<SaleOrder>? SaleOrderListMonthBefore { get; set; }
		public List<Product> TopProductMonth { get; set; }
		public decimal? RevenuePercentageChange
		{
			get
			{
				if (TotalRevenueMonthBefore == 0)
				{
					return 0;
				}
				return (((TotalRevenueMonth - TotalRevenueMonthBefore) / TotalRevenueMonthBefore) * 100);
			}
		}
		public int? SaleOrderAmountChange
		{
			get
			{
				if (SaleOrderListMonthBefore?.Count == 0)
				{
					return 0;
				}
				return ((SaleOrderListMonth.Count - SaleOrderListMonthBefore.Count) / SaleOrderListMonthBefore.Count) * 100;
			}
		}
	}
}
