using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PCHUBStore.Areas.Administration.Models.AccountViewModels;
using PCHUBStore.Areas.Administration.Services;

namespace PCHUBStore.Areas.Support.Controllers
{
    public class HomeController : SupportController
    {
        private readonly IAdminLayoutServices adminLayoutServices;

        public HomeController(IAdminLayoutServices adminLayoutServices)
        {
            this.adminLayoutServices = adminLayoutServices;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet("/api/AdminLayout")]
        public async Task<ActionResult<AdminLayoutViewModel>> GetUserInformationForLayout()
        {
            return await this.adminLayoutServices.GetAdminLayoutInformationAsync(this.User.Identity.Name);
        }
    }
}