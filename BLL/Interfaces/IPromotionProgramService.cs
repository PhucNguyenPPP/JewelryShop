using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DTO;

namespace BLL.Interfaces
{
    public interface IPromotionProgramService
    {
        List<PromotionProgram> GetPromotionProgramList();
        List<PromotionProgram> SearchPromotionProgram(string searchValue);

        bool AddPromotionProgram(PromotionProgramDTO promotionProgramDTO);

        bool UpdatePromotionProgram(PromotionProgramDTO promotionProgramDTO);

        bool DeletePromotionProgram(string promotionProgramId);

    }
}
