using Microsoft.EntityFrameworkCore;
using PCHUBStore.Data;
using PCHUBStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PCHUBStore.Services
{
    public class HomeService : IHomeService
    {
        private readonly PCHUBDbContext context;

        public HomeService(PCHUBDbContext context)
        {
            this.context = context;
        }


        public async Task<Page> LoadIndexPageComponentsAsync()
        {
            var indexModel = await this.context.Pages.FirstOrDefaultAsync(x => x.IsDeleted == false && x.PageName == "Index");

            return indexModel;
        }
    }
}
