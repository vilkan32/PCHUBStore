using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PCHUBStore.Areas.Administration.Models.CharacteristicsViewModels;
using PCHUBStore.Areas.Administration.Services;

namespace PCHUBStore.Areas.Administration.Controllers
{
    public class CharacteristicsController : AdministrationController
    {
        private readonly IAdminProductsServices productsServices;
        private readonly IAdminCharacteristicsServices characteristicsServices;

        public CharacteristicsController(IAdminProductsServices productsServices, IAdminCharacteristicsServices characteristicsServices)
        {
            this.productsServices = productsServices;
            this.characteristicsServices = characteristicsServices;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await this.productsServices.GetAllCategoryNamesAsync();

            var model = new InsertCharacteristicsViewModel { Categories = categories.ToList() };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(InsertCharacteristicsViewModel form)
        {
            
            var categories = await this.productsServices.GetAllCategoryNamesAsync();
            if (!categories.Contains(form.Category))
            {
                this.ModelState.AddModelError("Category", "Invalid Category Name");
            }
            if (await this.characteristicsServices.CharacteristicsExistsAsync(form.Category))
            {
                this.ModelState.AddModelError("Characteristics", "Characteristcs Already Exist Only One Characteristc per Category");
            }
            if (!this.ModelState.IsValid)
            {
                form.Categories = categories.ToList();
                return View(form);
            }

            await this.characteristicsServices.CreateCharacteristicsAsync(form);

            return this.RedirectToAction("Success", "Blacksmith", new { message = $"Successfully Create Characteristics for category {form.Category}" });
        }


        [HttpGet]
        public async Task<IActionResult> CreateCategory()
        {
            var categories = await this.productsServices.GetAllCategoryNamesAsync();

            var model = new InsertCharacteristicsCategoryViewModel { Categories = categories.ToList() };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(InsertCharacteristicsCategoryViewModel category)
        {
            var categories = await this.productsServices.GetAllCategoryNamesAsync();
            if(await this.characteristicsServices.CategoryExistsAsync(category.CategoryName))
            {
                this.ModelState.AddModelError("Category", "Category Already Exists");
            }
            if (!categories.Contains(category.CategoryName))
            {
                this.ModelState.AddModelError("Category", "Category Doesnt Exist");
            }

            if (!this.ModelState.IsValid)
            {
                category.Categories = categories.ToList();
                return this.View(category);
            }

            await this.characteristicsServices.CreateCharacteristicsCategoryAsync(category);

            return this.RedirectToAction("Success", "Blacksmith", new { message = $"Successfully Created Category with name {category.CategoryName}" });
        }
    }
}