using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PCHUBStore.Areas.Administration.Models.CategoryPagesViewModels;
using PCHUBStore.Areas.Administration.Services;

namespace PCHUBStore.Areas.Administration.Controllers
{
   
    public class CategoriesController : AdministrationController
    {
        private readonly IAdminCategoryPagesServices service;

        public CategoriesController(IAdminCategoryPagesServices service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult CreateCategoryPage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoryPage(CreateCategoryPageViewModel form)
        {

            if(await this.service.PageAlreadyExistsAsync(form.PageName))
            {
                this.ModelState.AddModelError("Category", "Category already exists");

            }

            if (this.ModelState.IsValid)
            {
                return this.RedirectToAction("Success", "Blacksmith", new { message = $"Category page with name: {form.PageName} has been created!" });
            }

            return View(form);
        }
    }
}