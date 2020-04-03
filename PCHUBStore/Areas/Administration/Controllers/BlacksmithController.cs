using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using PCHUBStore.Areas.Administration.Models.AccountViewModels;
using PCHUBStore.Areas.Administration.Models.CreateAdminViewModels;
using PCHUBStore.Areas.Administration.Services;
using PCHUBStore.Data.Models;
using PCHUBStore.Services.EmailSender;

namespace PCHUBStore.Areas.Administration.Controllers
{
    public class BlacksmithController : AdministrationController
    {
        private readonly IEmailSender emailSender;
        private readonly UserManager<User> userManager;
        private readonly IAdminLayoutServices adminLayoutServices;

        public BlacksmithController(IEmailSender emailSender,
                        UserManager<User> userManager,
                        IAdminLayoutServices adminLayoutServices)
        {
            this.emailSender = emailSender;
            this.userManager = userManager;
            this.adminLayoutServices = adminLayoutServices;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EditUserIndexPage()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult EditUserCategoryPage()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult CreateAdminAccount()
        {
            return this.View();
        }

        [HttpGet("/api/AdminLayout")]
        public async Task<ActionResult<AdminLayoutViewModel>> GetUserInformationForLayout()
        {
            return await this.adminLayoutServices.GetAdminLayoutInformationAsync(this.User.Identity.Name);
        }


        public async Task<IActionResult> CreateAdminAccount(RegisterNewAdminViewModel form)
        {

            if (ModelState.IsValid)
            {
                var user = new User { UserName = form.Username, Email = form.Email };
                var result = await userManager.CreateAsync(user, form.Password);

                if (result.Succeeded)
                {
                    await this.userManager.AddToRoleAsync(user, "Admin");

                    var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await emailSender.SendEmailAsync(form.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = form.Email });
                    }

                    return this.RedirectToAction("Success", "Blacksmith", new { message = "Successfully created new Admin account" });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return this.View(form);
        }

        [HttpGet]
        public IActionResult Success(string message)
        {
            this.ViewBag.message = message;
            return this.View();
        }
    }
}