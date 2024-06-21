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
        private readonly IPromotionCodeRepository _promotionCodeRepository;
        public PromotionProgramService(IPromotionProgramRepository promotionProgramRepository,
            IPromotionCodeRepository promotionCodeRepository)
        {
            _promotionProgramRepository = promotionProgramRepository;
            _promotionCodeRepository = promotionCodeRepository;
        }
        public List<PromotionProgram> GetPromotionProgramList()
        {
            return _promotionProgramRepository.GetPromotionProgramList().ToList();
        }

        public List<PromotionProgram> SearchPromotionProgram(string searchValue)
        {
            List<PromotionProgram> promotionProgramsList = _promotionProgramRepository.GetPromotionProgramList().ToList();
            return promotionProgramsList.Where(c => c.PromotionProgramName.ToLower().Contains(searchValue.ToLower())).ToList();
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

            foreach(var i in promotionProgram.PromotionProgramCodes)
            {
                var promotionProgramCode = _promotionCodeRepository.GetById(i.PromotionCodeId);
                if(promotionProgramCode == null) 
                {
                    return false;
                }
                promotionProgramCode.Status = false;
                _promotionCodeRepository.UpdatePromotionCode(promotionProgramCode);
            }

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

            List<PromotionProgramCode> promotionProgramCodes = new List<PromotionProgramCode>();
            foreach (var i in promotionProgramDTO.PromotionCodeDTOs)
            {
                PromotionProgramCode promotionProgramCode = new PromotionProgramCode()
                {
                    PromotionCodeId = Guid.NewGuid(),
                    PromotionCodeName = i.PromotionCodeName,
                    DiscountPercentage = i.DiscountPercentage,
                    PromotionProgramId = promotionProgramID,
                    Status = true
                };
                promotionProgramCodes.Add(promotionProgramCode);
            }
            _promotionCodeRepository.AddRangePromotionCode(promotionProgramCodes);

            bool result = _promotionProgramRepository.SaveChange();
            return result;
        }

        public bool UpdatePromotionProgram(PromotionProgramDTO promotionProgramDTO)
        {
            Guid.TryParse(promotionProgramDTO.PromotionProgramId, out Guid promotionProgramID);
            DateTime.TryParse(promotionProgramDTO.ExpiredDate, out DateTime expiredDate);

            var promotionProgram = _promotionProgramRepository.GetById(promotionProgramID);
            promotionProgram.PromotionProgramName = promotionProgramDTO.PromotionProgramName;
            promotionProgram.ExpiredDate = expiredDate;      
            _promotionProgramRepository.UpdatePromotionProgram(promotionProgram);

            for(int i = 0; i < promotionProgramDTO.PromotionCodeDTOs.Count; i++)
            {
                var promotionProgramCode = _promotionCodeRepository
                    .GetById(Guid.Parse(promotionProgramDTO.PromotionCodeDTOs[i].PromotionCodeId));
                promotionProgramCode.PromotionCodeName = promotionProgramDTO.PromotionCodeDTOs[i].PromotionCodeName;
                promotionProgramCode.DiscountPercentage = promotionProgramDTO.PromotionCodeDTOs[i].DiscountPercentage;

                _promotionCodeRepository.UpdatePromotionCode(promotionProgramCode);
            }

            bool result = _promotionProgramRepository.SaveChange();
            return result;
        }

        public bool CheckPromotionProgramExist(string promotionProgramName)
        {
            var promotionProgramList = _promotionProgramRepository.GetPromotionProgramList().ToList();
            if(promotionProgramList.Any(c => c.PromotionProgramName == promotionProgramName))
            {
                return true;
            }
            return false;
        }

        public PromotionProgram GetPromotionProgram(string id)
        {
            Guid.TryParse (id, out Guid promotionProgramID);
            return _promotionProgramRepository.GetById(promotionProgramID);
        }
    }
}
