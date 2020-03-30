using PCHUBStore.Data.Models;
using PCHUBStore.View.Models.UserProfileViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PCHUBStore.Services
{
    public interface IUserProfileServices
    {
        Task AddProfilePictureToUserAsync(string picUrl, string userName);

        Task<User> GetUserProfileInformationAsync(string userName);

        Task EditUserProfileOrderInformationAsync(string username, EditDeliveryInformationForm editForm);

        Task EditUserAccountSettingsAsync(string username, EditAccountSettingsForm editForm);

        Task<bool> AddToFavoritesAsync(string username, string id);
    }
}
