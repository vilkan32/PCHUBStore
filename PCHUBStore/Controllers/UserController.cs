using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCHUBStore.Data;
using PCHUBStore.Data.Models;
using PCHUBStore.Services;
using PCHUBStore.View.Models.UserProfileViewModels;

namespace PCHUBStore.Controllers
{
    [Authorize(Roles = "StoreUser")]
    public class UserController : Controller
    {


        private readonly ICloudinaryServices cloudinaryService;
        private readonly IUserProfileServices userProfileServices;
        private readonly IMapper mapper;
        private readonly SignInManager<User> signInManager;

        public UserController(ICloudinaryServices cloudinaryService, 
            IUserProfileServices userProfileServices, 
            IMapper mapper,
            SignInManager<User> signInManager)
        {
            this.cloudinaryService = cloudinaryService;
            this.userProfileServices = userProfileServices;
            this.mapper = mapper;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {

            var user = await this.userProfileServices.GetUserProfileInformationAsync(this.User.Identity.Name);

            var userViewModel = this.mapper.Map<UserProfileViewModel>(user);
            userViewModel.History = this.mapper.Map<List<ProductHistoryViewModel>>(user.ProductUserReviews.Select(x => x.Product));
            userViewModel.Favorites = this.mapper.Map<List<ProductFavoriteViewModel>>(user.FavoriteUserProducts.Select(x => x.Product));


            userViewModel.ProfileActive = "active";

            if(userViewModel.OrderInformation == null)
            {
                userViewModel.OrderInformation = new EditDeliveryInformationForm();
                userViewModel.OrderInformation.Address = userViewModel.Address;
                userViewModel.OrderInformation.FirstName = userViewModel.FirstName;
                userViewModel.OrderInformation.LastName = userViewModel.LastName;
                userViewModel.OrderInformation.Phone = userViewModel.Phone;
                userViewModel.OrderInformation.City = userViewModel.City;
            }

            if(userViewModel.AccountSettings == null)
            {
                userViewModel.AccountSettings = new EditAccountSettingsForm();
                userViewModel.AccountSettings.Username = userViewModel.Username;
                userViewModel.AccountSettings.Email = userViewModel.Email;
            }

            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditDeliveryInformation([FromForm]UserProfileViewModel editForm)
        {

            if (ModelState.IsValid)
            {
                await this.userProfileServices.EditUserProfileOrderInformationAsync(this.User.Identity.Name, editForm.OrderInformation);

                return this.RedirectToAction("Profile", "User");

            }
            var user = await this.userProfileServices.GetUserProfileInformationAsync(this.User.Identity.Name);

            var userViewModel = this.mapper.Map<UserProfileViewModel>(user);
            userViewModel.History = this.mapper.Map<List<ProductHistoryViewModel>>(user.ProductUserReviews.Select(x => x.Product));
            userViewModel.Favorites = this.mapper.Map<List<ProductFavoriteViewModel>>(user.FavoriteUserProducts.Select(x => x.Product));


            userViewModel.OrderInformation = editForm.OrderInformation;
            userViewModel.DeliveryInformationActive = "active";
            return this.View("Profile", userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditAccountSettings([FromForm]UserProfileViewModel editForm)
        {

            if (ModelState.IsValid)
            {
                await this.userProfileServices.EditUserAccountSettingsAsync(this.User.Identity.Name, editForm.AccountSettings);

                return this.RedirectToAction("Profile", "User");

            }
            var user = await this.userProfileServices.GetUserProfileInformationAsync(this.User.Identity.Name);

            var userViewModel = this.mapper.Map<UserProfileViewModel>(user);
            userViewModel.History = this.mapper.Map<List<ProductHistoryViewModel>>(user.ProductUserReviews.Select(x => x.Product));
            userViewModel.Favorites = this.mapper.Map<List<ProductFavoriteViewModel>>(user.FavoriteUserProducts.Select(x => x.Product));


            userViewModel.AccountSettings = editForm.AccountSettings;
            userViewModel.AccountSettingsActive = "active";
            return this.View("Profile", userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UploadProfilePicture(List<IFormFile> files)
        {

            var imgUrl = await cloudinaryService.UploadPictureAsync(files[0], this.User.Identity.Name + "profilePicture");

            await this.userProfileServices.AddProfilePictureToUserAsync(imgUrl, this.User.Identity.Name);

            return this.RedirectToAction("Profile", "User");
        }

        [Authorize(Roles = "StoreUser")]
        [HttpGet("/api/Favorites")]
        public async Task<bool> AddToFavorites(string id)
        {
            return await this.userProfileServices.AddToFavoritesAsync(this.User.Identity.Name, id);
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await this.signInManager.SignOutAsync();

            return this.Redirect("/Home/Index");
        }

    }
}