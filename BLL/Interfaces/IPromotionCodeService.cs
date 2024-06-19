using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DTO;

namespace BLL.Interfaces
{
    public interface IPromotionCodeService
    {
        List<PromotionProgramCode> GetPromotionCodeList(PromotionProgramDTO promotionProgramDTO);
        List<PromotionProgramCode> SearchPromotionCode(string searchValue, PromotionProgramDTO promotionProgramDTO);

        bool AddPromotionCode(PromotionCodeDTO promotionCodeDTO, PromotionProgramDTO promotionProgramDTO);

        bool UpdatePromotionCode(PromotionCodeDTO promotionCodeDTO, PromotionProgramDTO promotionProgramDTO);

        bool DeletePromotionCode(string promotionCodeId, PromotionProgramDTO promotionProgramDTO);
    }
}
