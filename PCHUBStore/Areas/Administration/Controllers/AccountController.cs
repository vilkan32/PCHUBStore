using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PCHUBStore.Areas.Administration.Models.AccountViewModels;
using PCHUBStore.Areas.Administration.Services;
using PCHUBStore.Data.Models;
using PCHUBStore.Services;

namespace PCHUBStore.Areas.Administration.Controllers
{
    public class AccountController : AdministrationController
    {
        
        private readonly IMapper mapper;
        private readonly ICloudinaryServices cloudinary;
        private readonly IUserProfileServices userProfileServices;
        private readonly SignInManager<User> signInManager;

        public AccountController(
            IMapper mapper,
            ICloudinaryServices cloudinary,
            IUserProfileServices userProfileServices,
            SignInManager<User> signInManager)
        {

            this.mapper = mapper;
            this.cloudinary = cloudinary;
            this.userProfileServices = userProfileServices;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var model = this.mapper.Map<AccountProfileViewModel>(await this.userProfileServices.GetUserProfileInformationAsync(this.User.Identity.Name));         
            return View(model);
        }
     

        [HttpPost]
        public async Task<IActionResult> UploadProfilePicture(List<IFormFile> files)
        {
            var imgUrl = await this.cloudinary.UploadPictureAsync(files[0], this.User.Identity.Name + "profilePicture");

            await this.userProfileServices.AddProfilePictureToUserAsync(imgUrl, this.User.Identity.Name);

            return this.RedirectToAction("Profile", "Account");
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await this.signInManager.SignOutAsync();

            return this.Redirect("/Home/Index");
        }


    }
}