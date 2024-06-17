using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IMaterialProductService
    {
        List<MaterialProduct> GetAllMaterialProductByProductId(string productId);


    }
}
