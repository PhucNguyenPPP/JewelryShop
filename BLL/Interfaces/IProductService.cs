using BOL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IProductService
    {
        List<Product> GetProductList();
        List<Product> SearchProduct(string searchValue);

        bool AddProduct(ProductDTO productDTO);

        bool UpdateProduct(ProductDTO productDTO);

        bool DeleteProduct(string productId);

    }
}
