﻿using PCHUBStore.Areas.Support.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Support.Services
{
    public interface ISupportChartsService
    {
        Task<List<MostViewedProducts>> GetMostViewedProductsAsync();

        Task<List<MostExpensiveProducts>> GetMostExpensiveProductsAsync();
    }
}
