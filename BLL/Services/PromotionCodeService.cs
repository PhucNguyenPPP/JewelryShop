using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using BOL;
using DTO;
using Repositories.Interfaces;

namespace BLL.Services
{
    public class PromotionCodeService : IPromotionCodeService
    {
        private readonly IPromotionCodeRepository _promotionCodeRepository;
        public PromotionCodeService(IPromotionCodeRepository promotionCodeRepository)
        {
            _promotionCodeRepository = promotionCodeRepository;
        }
        public List<PromotionProgramCode> GetPromotionCodeList(PromotionProgramDTO promotionProgramDTO)
        {
            Guid.TryParse(promotionProgramDTO.PromotionProgramId, out Guid id );
            return _promotionCodeRepository.GetPromotionCodeList(id).ToList();
        }

        public List<PromotionProgramCode> SearchPromotionCode(string searchValue, PromotionProgramDTO promotionProgramDTO)
        {
            List<PromotionProgramCode> promotionProgramCodes = _promotionCodeRepository.GetPromotionCodeList(Guid.Parse(promotionProgramDTO.PromotionProgramId)).
                Where(c=> c.PromotionCodeName == searchValue).ToList();
            return promotionProgramCodes;
        }

        public bool DeletePromotionCode(string promotionCodeId, PromotionProgramDTO promotionProgramDTO)
        {
            PromotionProgramCode? code = _promotionCodeRepository.GetPromotionCodeList(Guid.Parse(promotionProgramDTO.PromotionProgramId))
                .Where(c => c.PromotionCodeId == Guid.Parse(promotionCodeId)).FirstOrDefault();
            if (code == null)
            {
                return false;
            }
            code.Status = false;
            _promotionCodeRepository.UpdatePromotionCode(code);
            bool result = _promotionCodeRepository.SaveChange();
            return result;
        }

        public bool AddPromotionCode(PromotionCodeDTO promotionCodeDTO, PromotionProgramDTO promotionProgramDTO)
        {
            Guid.TryParse(promotionCodeDTO.PromotionProgramId, out Guid promotionProgram);
            PromotionProgramCode promotionProgramCode = new PromotionProgramCode()
            {
                PromotionCodeId = Guid.NewGuid(),
                PromotionCodeName = promotionCodeDTO.PromotionCodeName,
                DiscountPercentage = promotionCodeDTO.DiscountPercentage,
                PromotionProgramId = promotionProgram,
                Status = true
            };
            _promotionCodeRepository.AddPromotionCode(promotionProgramCode);
            bool result = _promotionCodeRepository.SaveChange();
            return result;
        }
        
        public bool UpdatePromotionCode(PromotionCodeDTO promotionCodeDTO, PromotionProgramDTO promotionProgramDTO)
        {
            Guid.TryParse(promotionCodeDTO.PromotionCodeId, out Guid promotionCodeID);
            Guid.TryParse(promotionProgramDTO.PromotionProgramId, out Guid promotionProgramID);
            PromotionProgramCode promotionProgramCode = new PromotionProgramCode()
            {
                PromotionProgramId = promotionProgramID,
                PromotionCodeId = promotionCodeID,
                PromotionCodeName= promotionCodeDTO.PromotionCodeName,
                DiscountPercentage= promotionCodeDTO.DiscountPercentage,
            };
            _promotionCodeRepository.UpdatePromotionCode(promotionProgramCode);
            bool result = _promotionCodeRepository.SaveChange();
            return result;
        }
    }
}
