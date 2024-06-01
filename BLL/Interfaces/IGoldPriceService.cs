﻿using BOL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IGoldPriceService
    {
        Task<List<GoldPriceDTO>> GetGoldPrices();

    }
}