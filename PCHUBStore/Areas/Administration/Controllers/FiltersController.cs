using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PCHUBStore.Areas.Administration.Models.FilterViewModels;
using PCHUBStore.Areas.Administration.Services;

namespace PCHUBStore.Areas.Administration.Controllers
{
    public class FiltersController : AdministrationController
    {
        private readonly IAdminFiltersServices service;
        private readonly IAdminProductsServices productsServices;

        public FiltersController(IAdminFiltersServices service, IAdminProductsServices productsServices)
        {
            this.service = service;
            this.productsServices = productsServices;
        }

        [HttpGet]
        public async Task<IActionResult> CreateBasicFilters()
        {
            var categories = await this.productsServices.GetAllCategoryNamesAsync();
            var model = new InsertBasicFiltersViewModel
            {
                Categories = categories.ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBasicFilters(InsertBasicFiltersViewModel filterCategory)
        {
            var categories = await this.productsServices.GetAllCategoryNamesAsync();
            filterCategory.Categories = categories.ToList();
            if(!categories.Any(x => x == filterCategory.Category))
            {
                this.ModelState.AddModelError("Product", "Category Product doesnt exist");
            }

            if(await this.service.BasicFiltersExistForCategoryAsync(filterCategory.Category))
            {
                this.ModelState.AddModelError("Filters", "Basic filters already created for this category");
            }

            if (ModelState.IsValid)
            {
                await this.service.CreateBasicFiltersAsync(filterCategory);
                
                return this.RedirectToAction("Success", "Blacksmith", new { message = $"Successfully created Basic filters for: {filterCategory.Category}" });
            }

            return View(filterCategory);
        }


        [HttpGet]

        public async Task<IActionResult> CreateFilterCategory()
        {
            var categories = await this.productsServices.GetAllCategoryNamesAsync();

            var model = new InserFilterCategoryViewModel
            {
                Categories = categories.ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFilterCategory(InserFilterCategoryViewModel form)
        {
            var categories = await this.productsServices.GetAllCategoryNamesAsync();

            form.Categories = form.Categories;

            if(!categories.Any(x => x == form.Category))
            {
                this.ModelState.AddModelError("Category", "Category does not exist");
            }

            if (await this.service.FilterForCategoryExistsAsync(form.Category, form.CategoryViewSubName))
            {
                this.ModelState.AddModelError("View Sub Name", "Already exists");
            }

            if (this.ModelState.IsValid)
            {
                await this.service.CreateFilterCategoryAsync(form);

                return this.RedirectToAction("Success", "Blacksmith", new { message = $"Successfully created Filter Category: {form.Category} View Sub Name: {form.CategoryViewSubName}" });
            }


            return this.View(form);
        }
       

        [HttpGet]
        public async Task<IActionResult> UpdateFilterCategory()
        {
            var categories = await this.productsServices.GetAllCategoryNamesAsync();

            var model = new UpdateFilterCategoryViewModel
            {
                Categories = categories.ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFilterCategory(UpdateFilterCategoryViewModel form)
        {
            var categories = await this.productsServices.GetAllCategoryNamesAsync();
            form.Categories = categories.ToList();

            if (!categories.Any(x => x == form.Category))
            {
                this.ModelState.AddModelError("Category", "Category does not exist");
            }

            if (this.ModelState.IsValid)
            {
                await this.service.UpdateCategoryAsync(form.Category);
            }

            return View(form);
        }

    }
}