using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PCHUBStore.Services;

namespace PCHUBStore.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryServices service;

        public CategoriesController(ICategoryServices service)
        {
            this.service = service;
        }

        public async Task<IActionResult>  Category(string pageName)
        {

            if(await this.service.PageAlreadyExistsAsync(pageName))
            {
               var page = await this.service.GetPageAsync(pageName);

                return this.View();
            }


            return this.RedirectToAction("Error", "Home");
        }
    }
}