using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IMaterialRepository
    {
        List<Material> GetAllMaterial();
        Material GetMaterial(Guid materialId);
    }
}
