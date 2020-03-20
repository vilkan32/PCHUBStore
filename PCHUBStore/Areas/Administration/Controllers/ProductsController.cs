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

        public ProductsController(IAdminProductsServices service)
        {
            this.service = service;
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
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(InsertProductViewModel form)
        {

            var categories = await this.service.GetAllCategoryNamesAsync();
            form.Categories = categories.ToList();

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
        public IActionResult InsertJsonProduct()
        {
            return this.View();
        }


        [HttpPost]
        public IActionResult InsertJsonProduct(string text)
        {
            return this.View();
        }
    }
}