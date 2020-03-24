using Microsoft.EntityFrameworkCore;
using PCHUBStore.Areas.Administration.Models.FilterViewModels;
using PCHUBStore.Data;
using PCHUBStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Administration.Services
{
    public class AdminFiltersServices : IAdminFiltersServices
    {
        private readonly PCHUBDbContext context;

        public AdminFiltersServices(PCHUBDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> BasicFiltersExistForCategoryAsync(string category)
        {
           return await this.context.FilterCategories.AnyAsync(x => x.CategoryName == category && (x.ViewSubCategoryName == "Order by" || x.ViewSubCategoryName == "Price"));
        }

        public async Task CreateBasicFiltersAsync(InsertBasicFiltersViewModel form)
        {
            await this.context.FilterCategories.AddAsync(new FilterCategory
            {
                CategoryName = form.Category,
                ViewSubCategoryName = "Order By",
                Filters = new List<PCHUBStore.Data.Models.Filter>
                {
                    new PCHUBStore.Data.Models.Filter
                    {
                        Name = "Price Ascending",
                        Value = "PriceAsc"
                    },
                    new PCHUBStore.Data.Models.Filter
                    {
                        Name = "Price Descending",
                        Value = "PriceDesc"
                    },
                    new PCHUBStore.Data.Models.Filter
                    {
                        Name = "Default",
                        Value = "Default",
                        
                    },
                }
            });

            await this.context.FilterCategories.AddAsync(new FilterCategory
            {
                CategoryName = form.Category,
                ViewSubCategoryName = "Price",
                Filters = new List<PCHUBStore.Data.Models.Filter>
                {
                    new PCHUBStore.Data.Models.Filter
                    {
                        Name = "MinPrice",
                        Value = "0"
                    },
                    new PCHUBStore.Data.Models.Filter
                    {
                        Name = "MaxPrice",
                        Value = "9999"
                    },
                }
            });

            await this.context.SaveChangesAsync();
        }

        public async Task CreateFilterCategoryAsync(InserFilterCategoryViewModel form)
        {
            await this.context.FilterCategories.AddAsync(new FilterCategory
            {

                CategoryName = form.Category,
                ViewSubCategoryName = form.CategoryViewSubName,

            });

            await this.context.SaveChangesAsync();
        }

        public Task<bool> FilterForCategoryExistsAsync(string category, string viewSub)
        {
            return this.context.FilterCategories.AnyAsync(x => x.CategoryName == category && x.ViewSubCategoryName == viewSub);
        }
    }
}
