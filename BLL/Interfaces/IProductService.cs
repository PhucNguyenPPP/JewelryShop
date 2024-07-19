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

        bool AddProduct(ProductRequestDTO productDTO);

        bool UpdateProduct(ProductRequestDTO productDTO);

        bool DeleteProduct(string productId);

        bool CheckNameExisted(string productName);

        Product GetProductById (string productId);

        ResponseDTO CheckValidationMaterialDTOList(List<MaterialDTO> list);
        List<Product> SearchProductByStaff(string searchValue, string employeeId);
        decimal GetPriceProduct(List<MaterialDTO> materialDTOList, decimal wage);
        ResponseDTO CheckMaterialAmountInStock(ProductRequestDTO productDTO);
    }
}
