using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PCHUBStore.Areas.Administration.Models.CategoryViewModels;
using PCHUBStore.Areas.Administration.Models.ProductViewModel;
using PCHUBStore.Areas.Administration.Services;

namespace PCHUBStore.Areas.Administration.Controllers
{
    public class ProductsController : AdministrationController
    {
        private readonly IAdminProductsServices service;
        private readonly IAdminCharacteristicsServices adminCharacteristicsServices;

        public ProductsController(IAdminProductsServices service, IAdminCharacteristicsServices adminCharacteristicsServices)
        {
            this.service = service;
            this.adminCharacteristicsServices = adminCharacteristicsServices;
        }

        [HttpGet]
        public async Task<IActionResult> CreateCategory()
        {
            var categories = await this.service.GetAllCategoryNamesAsync();

            var model = new InsertCategoryModel { ExistingCategories = categories };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(InsertCategoryModel category)
        {
            var categories = await this.service.GetAllCategoryNamesAsync();
            if (categories.Contains(category.CategoryName))
            {
                this.ModelState.AddModelError("Category", "Category already exists");
            }

            if (!this.ModelState.IsValid)
            {
                category.ExistingCategories = categories;
                return this.View(category);
            }

            await this.service.CreateCategoryAsync(category.CategoryName);

            return this.RedirectToAction("Success", "Blacksmith", new { message = $"Successfully Created Category with name {category.CategoryName}" });
        }


        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var model = new InsertProductViewModel();
            var categories = await this.service.GetAllCategoryNamesAsync();
            model.Categories = categories.ToList();
            model.CategoryPattern = await this.adminCharacteristicsServices.GetAvailableCharacteristicsAsync();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(InsertProductViewModel form)
        {

            var categories = await this.service.GetAllCategoryNamesAsync();
            form.Categories = categories.ToList();
            form.CategoryPattern = await this.adminCharacteristicsServices.GetAvailableCharacteristicsAsync();

            if (!categories.Contains(form.Category))
            {
                this.ModelState.AddModelError("Category", "Invalid Category Name");
            }
            if (!this.ModelState.IsValid)
            {
                return this.View(form);
            }


            await this.service.CreateProductAsync(form);

            return this.RedirectToAction("Success", "Blacksmith", new { message = $"Successfully Created Product with id: {form.ArticleNumber} in category: {form.Category}" });


        }

        [HttpGet]
        public async Task<IActionResult> InsertJsonProduct()
        {
            var categories = await this.service.GetAllCategoryNamesAsync();
            

            var model = new InsertJsonProductViewModel { Categories = categories.ToList() };
            return this.View(model);
        }


        [HttpPost]
        public async Task<IActionResult> InsertJsonProduct(InsertJsonProductViewModel form)
        {

            var categories = await this.service.GetAllCategoryNamesAsync();
            form.Categories = categories.ToList();
            if (!categories.Any(x => x == form.Category))
            {
                this.ModelState.AddModelError("Category", "Category Doesnt Exist");
            }

           
            if (this.ModelState.IsValid)
            {
                if(form.Category == "Laptops")
                {
                    await this.service.CreateLaptopFromJSONAsync(form);

                }else if(form.Category == "Monitors")
                {
                    await this.service.CreateMonitorFromJSONAsync(form);
                }
                else if (form.Category == "Keyboards")
                {
                    await this.service.CreateKeyboardFromJSONAsync(form);

                }else if(form.Category == "Mice")
                {
                    await this.service.CreateMouseFromJSONAsync(form);

                }else if(form.Category == "Computers")
                {
                    await this.service.CreateComputerFromJSONAsync(form);
                }
                return this.RedirectToAction("Success", "Blacksmith", new { message = "Successfully added Product" });
            }
            return this.View(form);
        }
    }
}