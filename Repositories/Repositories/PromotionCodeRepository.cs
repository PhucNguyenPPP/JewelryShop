using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL.DAO;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories.Repositories
{
    public class PromotionCodeRepository : IPromotionCodeRepository
    {
        private readonly IGenericDAO<PromotionProgramCode> _codeDAO;
        public PromotionCodeRepository(IGenericDAO<PromotionProgramCode> codeDAO)
        {
            _codeDAO = codeDAO;
        }
        public bool SaveChange()
        {
            return _codeDAO.SaveChange();
        }

        public PromotionProgramCode? GetById(Guid id)
        {
            var promotionList = _codeDAO.GetAll(c => true).ToList();
            return promotionList?.FirstOrDefault(c => c.PromotionCodeId == id);
        }

        public List<PromotionProgramCode> GetPromotionCodeList(Guid id)
        {
            return _codeDAO.GetAll(c=> c.Status == true && c.PromotionProgramId == id).
                Include(c=> c.PromotionProgram).ToList();
        }

        public void AddPromotionCode(PromotionProgramCode promotionCode)
        {
            _codeDAO.Add(promotionCode);
        }
        
        public void UpdatePromotionCode(PromotionProgramCode promotionCode)
        {
            _codeDAO.Update(promotionCode);
        }
    }
}
