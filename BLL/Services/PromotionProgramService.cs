using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using BOL;
using DTO;
using Microsoft.EntityFrameworkCore.Metadata;
using Repositories.Interfaces;

namespace BLL.Services
{
    public class PromotionProgramService : IPromotionProgramService
    {
        private readonly IPromotionProgramRepository _promotionProgramRepository;
        public PromotionProgramService(IPromotionProgramRepository promotionProgramRepository)
        {
            _promotionProgramRepository = promotionProgramRepository;
        }
        public List<PromotionProgram> GetPromotionProgramList()
        {
            return _promotionProgramRepository.GetPromotionProgramList().ToList();
        }

        public List<PromotionProgram> SearchPromotionProgram(string searchValue)
        {
            List<PromotionProgram> promotionProgramsList = _promotionProgramRepository.GetPromotionProgramList()
                .Where(c => c.PromotionProgramName.ToLower().Contains(searchValue)).ToList();
            return promotionProgramsList;
        }

        public bool DeletePromotionProgram(string promotionProgramId)
        {
            PromotionProgram? promotionProgram = _promotionProgramRepository.GetById(Guid.Parse(promotionProgramId));
            if (promotionProgram == null)
            {
                return false;
            }
            promotionProgram.Status = false;
            _promotionProgramRepository.UpdatePromotionProgram(promotionProgram);
            bool result = _promotionProgramRepository.SaveChange();
            return result;
        }

        public bool AddPromotionProgram(PromotionProgramDTO promotionProgramDTO)
        {
            DateTime.TryParse(promotionProgramDTO.ExpiredDate, out DateTime expiredDate);
            Guid promotionProgramID = Guid.NewGuid();
            PromotionProgram promotionProgram = new PromotionProgram()
            {
                PromotionProgramId = promotionProgramID,
                PromotionProgramName = promotionProgramDTO.PromotionProgramName,
                CreatedDate = DateTime.Now,
                ExpiredDate = expiredDate,
                Status = true
            };
            _promotionProgramRepository.AddPromotionProgram(promotionProgram);
            bool result = _promotionProgramRepository.SaveChange();
            return result;
        }

        public bool UpdatePromotionProgram(PromotionProgramDTO promotionProgramDTO)
        {
            Guid.TryParse(promotionProgramDTO.PromotionProgramId, out Guid promotionProgramID);
            DateTime.TryParse(promotionProgramDTO.ExpiredDate, out DateTime expiredDate);
            DateTime.TryParse(promotionProgramDTO.CreatedDate, out DateTime createdDate);

            PromotionProgram promotionProgram = new PromotionProgram()
            {
                PromotionProgramId = promotionProgramID,
                PromotionProgramName= promotionProgramDTO.PromotionProgramName,
                CreatedDate= createdDate,
                ExpiredDate= expiredDate,
            };
            _promotionProgramRepository.UpdatePromotionProgram(promotionProgram);
            bool result = _promotionProgramRepository.SaveChange();
            return result;
        }
    }
}
