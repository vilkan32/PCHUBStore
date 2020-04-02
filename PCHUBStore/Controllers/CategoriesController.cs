using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PCHUBStore.Services;
using PCHUBStore.View.Models.CategoriesViewModels;

namespace PCHUBStore.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryServices service;

        public CategoriesController(ICategoryServices service)
        {
            this.service = service;
        }

        [HttpGet("Category/{pageName}")]
        public async Task<IActionResult> Category(string pageName)
        {

            if(await this.service.PageAlreadyExistsAsync(pageName))
            {
               var page = await this.service.GetPageAsync(pageName);

                var model = new CategoryPageViewModel();

                model.PageName = page.PageName;
                model.PageCategory = new PageCategoryViewModel();
                var pc = page.Categories.FirstOrDefault();
                model.PageCategory.AllHref = pc.AllHref;
                model.PageCategory.AllName = pc.AllName;
                model.PageCategory.CategoryName = pc.CategoryName;
                model.PageCategory.Picture = pc.Pictures.FirstOrDefault().Url;
                model.PageCategory.ItemsCategories = new List<CategoryPageItemsCategoryViewModel>();
                model.Boxes = new List<BoxViewModel>();
                foreach (var itemsCategory in pc.ItemsCategories)
                {
                    var itemCategory = new CategoryPageItemsCategoryViewModel
                    {
                        Category = itemsCategory.Category,
                        Items = new List<PageCategoryItemsViewModel>()
                    };

                    foreach (var item in itemsCategory.Items)
                    {
                        itemCategory.Items.Add(new PageCategoryItemsViewModel { Text = item.Text, Href = item.Href });
                    }


                    model.PageCategory.ItemsCategories.Add(itemCategory);
                }

                foreach (var item in page.ColorfulBoxes)
                {
                    model.Boxes.Add(new BoxViewModel
                    {

                        Text = item.Text,
                        Color = item.Color,
                        Href = item.Href
                    });
                }

                return this.View(model);
            }


            return this.RedirectToAction("Error", "Home");
        }
    }
}