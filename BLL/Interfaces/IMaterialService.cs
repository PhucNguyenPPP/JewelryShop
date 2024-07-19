using BOL;
using DTO;
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
        List<Material> SearchMaterial(string searchValue);
        Material GetMaterial(string materialId);
        bool AddMaterial(MaterialRequestDTO model);
        bool UpdateMaterial(MaterialRequestDTO model);
        bool DeleteMaterial(string id);
        bool CheckMaterialNameExist(string name);
        bool CheckAmountValidation(MaterialRequestDTO model);
    }
}
