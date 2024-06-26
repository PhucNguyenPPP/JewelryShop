﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;

namespace Repositories.Interfaces
{
    public interface IMaterialProductRepository
    {
        void AddRange(List<MaterialProduct> materialProducts);

        public void DeleteRange(List<MaterialProduct> materialProduct);

        List<MaterialProduct> GetAllMaterialProductByProductId(Guid productId);
    }
}
