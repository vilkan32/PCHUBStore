using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCHUBStore.Areas.Administration.Models.CategoryPagesViewModels;
using PCHUBStore.Areas.Administration.Services;
using PCHUBStore.Services;

namespace PCHUBStore.Areas.Administration.Controllers
{

    public class CategoriesController : AdministrationController
    {
        private readonly IAdminCategoryPagesServices service;
        private readonly ICloudinaryServices cloudinary;

        public CategoriesController(IAdminCategoryPagesServices service,
            ICloudinaryServices cloudinary)
        {
            this.service = service;
            this.cloudinary = cloudinary;
        }

        [HttpGet]
        public IActionResult CreateCategoryPage()
        {
            var model = new CreateCategoryPageViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoryPage(CreateCategoryPageViewModel form, List<IFormFile> files)
        {
            if (await this.service.PageAlreadyExistsAsync(form.PageName))
            {
                this.ModelState.AddModelError("Page Name", "Page Name already exists");
            }


            if (this.ModelState.IsValid)
            {
                var picture = await this.cloudinary.UploadPictureAsync(files[0], form.PageCategory + "CategoryPicture");

                form.PageCategory.Pictures.Add(picture);

                await this.service.CreateCategoryPageAsync(form);

                return this.RedirectToAction("Success", "Blacksmith", new { message = $"Category page with name: {form.PageName} has been created!" });
            }

            return View(form);
        }

        [HttpGet]
        public async Task<IActionResult> AddBox()
        {
            var model = new AddBoxViewModel();

            model.PageNames = await this.service.GetAllPageNamesAsync();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddBox(AddBoxViewModel form)
        {
            if (this.ModelState.IsValid && await this.service.PageAlreadyExistsAsync(form.PageName))
            {
                await this.service.AddBoxAsync(form);

                return this.RedirectToAction("Success", "Blacksmith", new { message = "Successfully Added Box" });
            }

            return this.View(form);

        }

        [HttpGet]
        public async Task<IActionResult> EditBoxes(string pageName)
        {
            var model = new EditBoxesViewModel();
            model.Pages = await this.service.GetAllPageNamesAsync();
            if (!string.IsNullOrEmpty(pageName) && await this.service.PageAlreadyExistsAsync(pageName))
            {
                model.PageName = pageName;
                var boxes = await this.service.GetAllBoxesForPageAsync(pageName);

                foreach (var box in boxes)
                {
                    model.Boxes.Add(new EditBoxViewModel
                    {
                        Color = box.Color,
                        Text = box.Text,
                        Href = box.Href,
                        IsDeleted = box.IsDeleted

                    });

                }
            }
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditBoxes(EditBoxesViewModel form)
        {

            if (this.ModelState.IsValid && await this.service.PageAlreadyExistsAsync(form.PageName))
            {
                    await this.service.EditBoxesAsync(form);

                return this.RedirectToAction("Success", "Blacksmith", new { message = "Successfully Edited Box" });
            }

            return this.View(form);

        }
    }
}