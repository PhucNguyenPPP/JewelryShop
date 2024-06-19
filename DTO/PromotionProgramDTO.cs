﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PromotionProgramDTO
    {
        public string? PromotionProgramId { get; set; }

        [Required(ErrorMessage = "Please input promotion program name!")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Promotion program name can not include special character!")]
        public string? PromotionProgramName { get; set; }

        [Required(ErrorMessage = "Please input created date!")]
        public string? CreatedDate { get; set; }

        [Required(ErrorMessage = "Please input expiredDate!")]
        public string? ExpiredDate { get; set; }

        //public List<PromotionCodeDTO>? PromotionCodeDTOs { get; set; }

    }
}
