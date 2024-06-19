using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;

namespace Repositories.Interfaces
{
    public interface IPromotionCodeRepository
    {
        List<PromotionProgramCode> GetPromotionCodeList(Guid id);

        void AddPromotionCode(PromotionProgramCode promotionCode);

        void UpdatePromotionCode(PromotionProgramCode promotionCode);

        bool SaveChange();

        PromotionProgramCode? GetById(Guid id);
    }
}
