using Microsoft.EntityFrameworkCore;
using PCHUBStore.Areas.Administration.Models.AccountViewModels;
using PCHUBStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Administration.Services
{
    public class AdminLayoutServices : IAdminLayoutServices
    {
        private readonly PCHUBDbContext context;

        public AdminLayoutServices(PCHUBDbContext context)
        {
            this.context = context;
        }

        public async Task<AdminLayoutViewModel> GetAdminLayoutInformationAsync(string username)
        {
            var model = new AdminLayoutViewModel();

            var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            model.Username = user.Email;

            model.PictureUrl = user.ProfilePicture.Url;

            return model;
        }
    }
}
