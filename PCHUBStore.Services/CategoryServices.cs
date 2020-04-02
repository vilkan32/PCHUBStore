using Microsoft.EntityFrameworkCore;
using PCHUBStore.Data;
using PCHUBStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCHUBStore.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly PCHUBDbContext context;

        public CategoryServices(PCHUBDbContext context)
        {
            this.context = context;
        }


        public async Task<Page> GetPageAsync(string pageName)
        {
            var page = await this.context.Pages.Where(x => x.IsDeleted == false).FirstOrDefaultAsync(x => x.PageName == pageName);
            return page;
        }

        public async Task<bool> PageAlreadyExistsAsync(string pageName)
        {
            return await this.context.Pages.Where(x => x.IsDeleted == false).AnyAsync(x => x.PageName == pageName);
        }
    }
}
