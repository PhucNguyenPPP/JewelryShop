using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
	public class DashboardRangeResponseDTO
	{
		public decimal? TotalRevenueRange { get; set; }
		public List<SaleOrder>? SaleOrderListRange { get; set; }
	}
}
