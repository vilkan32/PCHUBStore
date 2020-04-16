using Microsoft.EntityFrameworkCore;
using PCHUBStore.Areas.Support.Models;
using PCHUBStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Support.Services
{
    public class SupportChartsService : ISupportChartsService
    {
        private readonly PCHUBDbContext context;

        public SupportChartsService(PCHUBDbContext context)
        {
            this.context = context;
        }

        public async Task<List<MostViewedProducts>> GetMostViewedProductsAsync()
        {
            var mostViewedProducts = await this.context.Products.OrderByDescending(x => x.Views).Take(20).Select(x => new MostViewedProducts
            {
                Title = x.Title,
                Views = x.Views,

            }).ToListAsync();

            return mostViewedProducts;
        }

        public async Task<List<MostExpensiveProducts>> GetMostExpensiveProductsAsync()
        {
            return await this.context.Products.OrderByDescending(x => x.Price).Take(20).Select(x => new MostExpensiveProducts
            {
                Price = x.Price,
                Title = x.Title,

            }).ToListAsync();

        }
    }
}
