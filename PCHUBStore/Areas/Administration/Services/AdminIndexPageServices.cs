using Microsoft.EntityFrameworkCore;
using PCHUBStore.Areas.Administration.Models.IndexPageViewModels;
using PCHUBStore.Data;
using PCHUBStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Administration.Services
{
    public class AdminIndexPageServices : IAdminIndexPageServices
    {
        private readonly PCHUBDbContext context;

        public AdminIndexPageServices(PCHUBDbContext context)
        {
            this.context = context;
        }


        public async Task<bool> PageExistsAsync(string pageName)
        {
            return await this.context.Pages.AnyAsync(x => x.PageName == pageName);
        }

        public async Task CreateIndexPageAsync(string pageName)
        {
            await this.context.Pages.AddAsync(new Data.Models.Page
            {
                PageName = pageName,
            });

            await this.context.SaveChangesAsync();
        }

        public async Task AddIndexPageCategoryAsync(CreatePageCategoryViewModel form, string pictureUrl, string pictureName)
        {
            var page = await this.context.Pages.FirstOrDefaultAsync(x => x.PageName == "Index");

            page.Categories.Add(new Data.Models.PageCategory
            {

                CategoryName = form.CategoryName,
                AllHref = form.AllHref,
                AllName = form.AllName,
                Pictures = new List<Picture> { new Picture {Name = pictureName, Url = pictureUrl } }
               
            });

            await this.context.SaveChangesAsync();

        }

        public async Task<List<string>> GetAllPageCategoryNamesAsync()
        {
            var page = await this.context.Pages.FirstOrDefaultAsync(x => x.PageName == "Index");

            return page.Categories.Select(x => x.CategoryName).ToList();
        }

        public async Task AdditemsToCategoryAsync(IndexItemsCategoryViewModel form)
        {
            var page = await this.context.Pages.FirstOrDefaultAsync(x => x.PageName == "Index");

            var category = page.Categories.FirstOrDefault(x => x.CategoryName == form.PageCategory);

            category.ItemsCategories.Add(new ItemsCategory
            {
                Category = form.ItemCategory,
            });

            foreach (var cat in form.Items.Where(x => x.Text != null && x.Href != null))
            {

                category.ItemsCategories.FirstOrDefault(x => x.Category == form.ItemCategory).Items.Add(new PageCategoryItems
                {

                    Href = cat.Href,
                    Text = cat.Text

                });

            }

            await this.context.SaveChangesAsync();
        }

        public async Task<List<PageCategory>> GetIndexCategoryAsync(string category)
        {
           var page = await this.context.Pages.FirstOrDefaultAsync(x => x.PageName == "Index");

            var categ = page.Categories.Where(x => x.CategoryName == category);

            return categ.ToList();
        }

        public async Task EditIndexPageCategoryAsync(EditIndexCategoryViewModel form, string previousCategoryName)
        {
            var page = await this.context.Pages.FirstOrDefaultAsync(x => x.PageName == "Index");

            foreach (var pc in form.PageCategory)
            {
                var pageCategory = page.Categories.FirstOrDefault(x => x.CategoryName == previousCategoryName);

                pageCategory.AllHref = pc.AllHref;
                pageCategory.AllName = pc.AllName;
                pageCategory.CategoryName = pc.CategoryName;

                for (int i = 0; i < pc.ItemCategories.Count; i++)
                {

                    var ic = pc.ItemCategories[i];
                    var itemCategory = pageCategory.ItemsCategories.ToList()[i];
                    itemCategory.Category = ic.ItemCategory;

                    for (int z = 0; z < itemCategory.Items.Count; z++)
                    {

                        var item = itemCategory.Items.ToList()[z];

                        item.Text = ic.Items[z].Text;

                        item.Href = ic.Items[z].Href;
                    }
                }
            }

            await this.context.SaveChangesAsync();
        }

        public async Task AddBoxAsync(AddBoxViewModel form)
        {
            var page = await this.context.Pages.FirstOrDefaultAsync(x => x.PageName == "Index");

            page.ColorfulBoxes.Add(new ColorfulBox { Color = form.Color, Href = form.Href, Text = form.Text, CreatedOn = DateTime.UtcNow, ModificationDate = DateTime.UtcNow });

            await this.context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ColorfulBox>> GetAllBoxesAsync()
        {
            var page = await this.context.Pages.FirstOrDefaultAsync(x => x.PageName == "Index");

            var boxes = page.ColorfulBoxes.Where(x => x.IsDeleted == false);

            return boxes;
        }


        public async Task EditBoxesAsync(EditBoxViewModel form)
        {
            var page = await this.context.Pages.FirstOrDefaultAsync(x => x.PageName == "Index");

            var boxes = page.ColorfulBoxes.Where(x => x.IsDeleted == false).ToList();

            for (int i = 0; i < form.BoxViewModel.Count; i++)
            {
                var formBox = form.BoxViewModel[i];
                var dbBox = boxes[i];

                dbBox.Href = formBox.Href;
                dbBox.Color = formBox.Color;
                dbBox.Text = formBox.Text;
                dbBox.IsDeleted = formBox.IsDeleted;
            }

            await this.context.SaveChangesAsync();
        }

        public async Task UploadMainSliderPicturesAsync(List<string> urls)
        {
            var mainSlider = await this.context.MainSliders.FirstOrDefaultAsync(x => x.Name == "MainSlider");

            if(mainSlider == null)
            {
                var mainSliderModel = new MainSlider { Name = "MainSlider" };
                this.context.MainSliders.Add(mainSliderModel);
                await this.context.SaveChangesAsync();

                mainSlider = await this.context.MainSliders.FirstOrDefaultAsync(x => x.Name == "MainSlider");
            }

            foreach (var url in urls)
            {
                mainSlider.MainSliderPictures.Add(new Picture { Name = "MainSlider", Url = url, });               
            }

            await this.context.SaveChangesAsync();
        }

        public async Task<List<Picture>> GetMainSliderPicturesAsync()
        {
            var mainSlider = await this.context.MainSliders.FirstOrDefaultAsync(x => x.Name == "MainSlider");

            var pictures = mainSlider.MainSliderPictures.ToList();

            return pictures;
        }

        public async Task EditMainSliderPicturesAsync(List<EditMainSliderPictures> form)
        {
            foreach (var picture in form)
            {
                var pic = await this.context.Pictures.FirstOrDefaultAsync(x => x.Id == picture.Id);

                pic.IsDeleted = picture.IsDeleted;
                pic.Name = picture.Name;
                pic.RedirectTo = picture.RedirectTo;
                pic.Url = picture.Url;

            }

            await this.context.SaveChangesAsync();
        }
    }
}
