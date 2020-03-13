using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PCHUBStore.Data;
using PCHUBStore.Data.Models;
using PCHUBStore.View.Models.UserProfileViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PCHUBStore.Services
{
    public class UserProfileServices : IUserProfileServices
    {
        private readonly PCHUBDbContext context;
        private readonly UserManager<User> userManager;

        public UserProfileServices(PCHUBDbContext context, 
            UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task AddProfilePictureToUserAsync(string picUrl, string userName)
        {

            var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == userName);

            user.ProfilePicture = new Picture
            {

                Url = picUrl,
                Name = "ProfilePicture"

            };

            await context.SaveChangesAsync();
        }

        public async Task EditUserProfileOrderInformationAsync(string username, EditDeliveryInformationForm editForm)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            user.PhoneNumber = editForm.Phone;
            user.Address = editForm.Address;
            user.City = editForm.City;
            user.FirstName = editForm.FirstName;
            user.LastName = editForm.LastName;

            await context.SaveChangesAsync();
        }

        public async Task EditUserAccountSettingsAsync(string username, EditAccountSettingsForm editForm)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            user.UserName = editForm.Username;
            user.Email = editForm.Email;

            await userManager.ChangePasswordAsync(user, editForm.CurrentPassword, editForm.ConfirmPassword);
        }

        public async Task<User> GetUserProfileInformationAsync(string userName)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            return user;
        }


    }
}
