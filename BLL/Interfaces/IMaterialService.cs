using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IMaterialService
    {
        List<Material> GetAllMaterial();
        Material GetMaterial(string materialId);
    }
}
