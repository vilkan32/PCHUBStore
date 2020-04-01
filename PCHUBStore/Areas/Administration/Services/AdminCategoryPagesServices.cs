using Microsoft.EntityFrameworkCore;
using PCHUBStore.Areas.Administration.Models.CategoryPagesViewModels;
using PCHUBStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Administration.Services
{
    public class AdminCategoryPagesServices : IAdminCategoryPagesServices
    {
        private readonly PCHUBDbContext context;

        public AdminCategoryPagesServices(PCHUBDbContext context)
        {
            this.context = context;
        }
        public async Task CreateCategoryPageAsync(CreateCategoryPageViewModel form)
        {
            await this.context.Pages.AddAsync(new Data.Models.Page
            {
                PageName = form.PageName
            });

            await this.context.SaveChangesAsync();
        }

        public async Task<bool> PageAlreadyExistsAsync(string pageName)
        {
            return await this.context.Pages.AnyAsync(x => x.PageName == pageName);
        }

    }
}
