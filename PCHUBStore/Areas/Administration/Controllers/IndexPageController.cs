using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCHUBStore.Areas.Administration.Models.IndexPageViewModels;
using PCHUBStore.Areas.Administration.Services;
using PCHUBStore.Services;

namespace PCHUBStore.Areas.Administration.Controllers
{
    public class IndexPageController : AdministrationController
    {
        private readonly IAdminIndexPageServices service;
        private readonly ICloudinaryServices cloudinary;

        public IndexPageController(IAdminIndexPageServices service, ICloudinaryServices cloudinary)
        {
            this.service = service;
            this.cloudinary = cloudinary;
        }
        [HttpGet]
        public IActionResult CreateIndexPage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateIndexPage(CreateIndexPageViewModel form)
        {

            if(await this.service.PageExistsAsync(form.PageName))
            {
                this.ModelState.AddModelError("Index Page", "Already exists");
            }

            if (this.ModelState.IsValid)
            {
                await this.service.CreateIndexPageAsync(form.PageName);

                return this.RedirectToAction("Success", "Blacksmith", new { message = "Successfully Created Index Page" });
            }

            return View(form);
        }

        [HttpGet]
        public IActionResult AddPageCategory()
        {
            var model = new CreatePageCategoryViewModel();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddPageCategory(CreatePageCategoryViewModel form,
        List<IFormFile> files)
        {

            var pictureUrl = await this.cloudinary.UploadPictureAsync(files[0], form.CategoryName + "Picture");
            if (this.ModelState.IsValid)
            {
                await this.service.AddIndexPageCategoryAsync(form, pictureUrl, form.CategoryName + "Picture");
                return this.RedirectToAction("Success", "Blacksmith", new { message = "Successfully Created Category for Index Page" });
                
            }

            return this.View(form);
        }

        [HttpGet]
        public async Task<IActionResult> AddItemsCategory()
        {
            var model = new IndexItemsCategoryViewModel();
            model.ExistingCategories = await this.service.GetAllPageCategoryNamesAsync();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddItemsCategory(IndexItemsCategoryViewModel form)
        {

            form.ExistingCategories = await this.service.GetAllPageCategoryNamesAsync();

            if (this.ModelState.IsValid)
            {
                await this.service.AdditemsToCategoryAsync(form);
                return this.RedirectToAction("Success", "Blacksmith", new { message = "Successfully Added Items To Category" });
            }
            return this.View(form);
        }

        [HttpGet]
        public async Task<IActionResult> EditIndexCategory(string category)
        {
            var model = new EditIndexCategoryViewModel();
            model.Categories = await this.service.GetAllPageCategoryNamesAsync();

            if(category != null)
            {
               var categ = await this.service.GetIndexCategoryAsync(category);
                model.Category = category;
                var itemCategories = new List<EditItemCategoriesViewModel>();

                model.PageCategory = new List<EditCategoryViewModel>();
                foreach (var pc in categ)
                {
                    var pageCategory = new EditCategoryViewModel();
                    pageCategory.AllHref = pc.AllHref;
                    pageCategory.AllName = pc.AllName;
                    pageCategory.CategoryName = pc.CategoryName;
                    pageCategory.ItemCategories = new List<EditItemCategoriesViewModel>();
                    foreach (var ic in pc.ItemsCategories)
                    {
                        var itemCategory = new EditItemCategoriesViewModel { ItemCategory = ic.Category, };

                        itemCategory.Items = new List<IndexPageItemsViewModel>();

                        foreach (var item in ic.Items)
                        {
                            itemCategory.Items.Add(new IndexPageItemsViewModel { Text = item.Text, Href = item.Href });

                        }

                        pageCategory.ItemCategories.Add(itemCategory);
                    }

                    model.PageCategory.Add(pageCategory);
                }

                model.DisplayForm = true;
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditIndexCategory(EditIndexCategoryViewModel form)
        {

            if (this.ModelState.IsValid)
            {
                await this.service.EditIndexPageCategoryAsync(form, form.Category);
                return this.RedirectToAction("Success", "Blacksmith", new { message = "Successfully Edited Index Page Category" });
            }

            return this.View("EditIndexCategory", new { category = form.Category });
        }

        [HttpGet]
        public IActionResult AddBox()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBox(AddBoxViewModel form)
        {

            if (this.ModelState.IsValid)
            {

                await this.service.AddBoxAsync(form);

                return this.RedirectToAction("Success", "Blacksmith", new { message = "Successfully Added Box" });
            }

            return this.View(form);
        }

        [HttpGet]
        public async Task<IActionResult> EditBoxes()
        {

            var boxes = await this.service.GetAllBoxesAsync();

            var model = new EditBoxViewModel();
            model.BoxViewModel = new List<AddBoxViewModel>();

            foreach (var box in boxes)
            {
                model.BoxViewModel.Add(new AddBoxViewModel { Href = box.Href, Color = box.Color, Text = box.Text });
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditBoxes(EditBoxViewModel form)
        {
            if (this.ModelState.IsValid)
            {
                await this.service.EditBoxesAsync(form);

                return this.RedirectToAction("Success", "Blacksmith", new { message = "Successfully Edited Boxes" });
            }

            return this.View(form);
        }

        [HttpGet]
        public IActionResult UploadMainSliderPictures()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadMainSliderPictures(List<IFormFile> files)
        {
            var urls = new List<string>();

            foreach (var file in files)
            {
                urls.Add(await this.cloudinary.UploadPictureAsync(file, "MainSlider_" + file.FileName));
            }

            await this.service.UploadMainSliderPicturesAsync(urls);

            return this.View();
        }

        [HttpGet]
        public async Task<IActionResult> EditMainSliderPictures()
        {
            var model = new List<EditMainSliderPictures>();

            var pictures = await this.service.GetMainSliderPicturesAsync();

            foreach (var picture in pictures)
            {
                model.Add(new EditMainSliderPictures
                {
                    Id = picture.Id,
                    IsDeleted = picture.IsDeleted,
                    Name = picture.Name,
                    RedirectTo = picture.RedirectTo,
                    Url = picture.Url

                });
            }

                
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditMainSliderPictures(List<EditMainSliderPictures> form)
        {
            if (this.ModelState.IsValid)
            {
                await this.service.EditMainSliderPicturesAsync(form);
            }

            return this.RedirectToAction("EditMainSliderPictures");
        }
    }
}