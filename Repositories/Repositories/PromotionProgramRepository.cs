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
    public class PromotionProgramRepository : IPromotionProgramRepository
    {
        IGenericDAO<PromotionProgram> _promotionDAO;

        public PromotionProgramRepository(IGenericDAO<PromotionProgram> promotionDAO)
        {
            _promotionDAO = promotionDAO;
        }
        public bool SaveChange()
        {
            return _promotionDAO.SaveChange();
        }

        public PromotionProgram? GetById(Guid id)
        {
            var promotionList = _promotionDAO.GetAll(c => true).ToList();
            return promotionList?.FirstOrDefault(c => c.PromotionProgramId == id);
        }

        public List<PromotionProgram> GetPromotionProgramList()
        {
            return _promotionDAO.GetAll(c => c.Status == true).ToList(); ;
        }

        public void AddPromotionProgram(PromotionProgram promotionProgram)
        {
            _promotionDAO.Add(promotionProgram);
        }
        
        public void UpdatePromotionProgram(PromotionProgram promotionProgram)
        {
            _promotionDAO.Update(promotionProgram);
        }
    }
}
