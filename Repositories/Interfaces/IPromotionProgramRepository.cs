using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;

namespace Repositories.Interfaces
{
    public interface IPromotionProgramRepository
    {
        List<PromotionProgram> GetPromotionProgramList();

        void AddPromotionProgram(PromotionProgram promotionProgram);

        void UpdatePromotionProgram(PromotionProgram promotionProgram);

        bool SaveChange();

        PromotionProgram? GetById(Guid id);
    }
}
