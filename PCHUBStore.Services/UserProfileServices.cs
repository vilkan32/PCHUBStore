using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PCHUBStore.Data;
using PCHUBStore.Data.Models;
using PCHUBStore.View.Models.UserProfileViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCHUBStore.Services
{
    public class UserProfileServices : IUserProfileServices
    {
        private readonly PCHUBDbContext context;
        private readonly UserManager<User> userManager;
        private readonly IProductServices productService;

        public UserProfileServices(PCHUBDbContext context, 
            UserManager<User> userManager, IProductServices productService)
        {
            this.context = context;
            this.userManager = userManager;
            this.productService = productService;
        }


        public async Task<bool> AddToFavoritesAsync(string username, string id)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);


            var product = await this.productService.GetProductAsync(id);

            if (product != null && !user.FavoriteUserProducts.Select(x => x.Product).Any(x => x.Id == id))
            {
                user.FavoriteUserProducts.Add(new ProductUserFavorite
                {
                    User = user,
                    Product = product
                });
                await this.context.SaveChangesAsync();
                return true;
            }

            return false;
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

        public async Task<IEnumerable<ShipmentProduct>> GetAllShipmentsAsync(string username)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            var shipments = user.Shipments.SelectMany(x => x.ShipmentProducts);

            return shipments;
        }
    }
}
