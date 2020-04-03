using Microsoft.EntityFrameworkCore;
using PCHUBStore.Areas.Administration.Models.CategoryPagesViewModels;
using PCHUBStore.Data;
using PCHUBStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Administration.Services
{
    public class AdminCategoryPagesServices : IAdminCategoryPagesServices
    {
        private readonly PCHUBDbContext context;

        public AdminCategoryPagesServices(PCHUBDbContext context)
        {
            this.context = context;
        }
        public async Task CreateCategoryPageAsync(CreateCategoryPageViewModel form)
        {

            var page = new Data.Models.Page
            {
                PageName = form.PageName
            };

            var pictures = new List<Picture>();

            foreach (var pictureUrl in form.PageCategory.Pictures)
            {
                pictures.Add(new Picture { Url = pictureUrl, Name = "Categories", CreatedOn = DateTime.UtcNow, ModificationDate = DateTime.UtcNow });
            }

            var pageCategory = new PageCategory
            {
                CategoryName = form.PageCategory.CategoryName,
                AllHref = form.PageCategory.AllHref,
                AllName = form.PageCategory.AllName,
                Pictures = pictures,
            };

            var itemCategories = new List<ItemsCategory>();

            foreach (var ic in form.PageCategory.ItemsCategories)
            {
                var pageCategoryItems = new List<PageCategoryItems>();
                if (string.IsNullOrEmpty(ic.Category))
                {
                    continue;
                }
                foreach (var item in ic.Items)
                {
                    if (string.IsNullOrEmpty(item.Text) || string.IsNullOrEmpty(item.Href))
                    {

                    }
                    else
                    {
                        pageCategoryItems.Add(new PageCategoryItems
                        {
                            Href = item.Href,
                            Text = item.Text,
                        });
                    }
                }

                itemCategories.Add(new ItemsCategory
                {

                    Category = ic.Category,
                    Items = pageCategoryItems
                });
            }

            pageCategory.ItemsCategories = itemCategories;
            page.Categories.Add(pageCategory);

            await this.context.Pages.AddAsync(page);

            await this.context.SaveChangesAsync();
        }

        public async Task<bool> PageAlreadyExistsAsync(string pageName)
        {
            return await this.context.Pages.Where(x => x.IsDeleted == false).AnyAsync(x => x.PageName == pageName);
        }

        public async Task AddBoxAsync(AddBoxViewModel form)
        {
            var page = await this.context.Pages.FirstOrDefaultAsync(x => x.PageName == form.PageName);

            page.ColorfulBoxes.Add(new ColorfulBox { Color = form.Color, Href = form.Href, Text = form.Text, CreatedOn = DateTime.UtcNow, ModificationDate = DateTime.UtcNow });

            await this.context.SaveChangesAsync();
        }

        public async Task<List<string>> GetAllPageNamesAsync()
        {

           var pages = await this.context.Pages.Where(x => x.PageName != "Index").ToListAsync();

            return pages.Select(x => x.PageName).ToList();
        }

        public async Task<List<ColorfulBox>> GetAllBoxesForPageAsync(string pageName)
        {

            var page = await this.context.Pages.FirstOrDefaultAsync(x => x.PageName == pageName);

            return page.ColorfulBoxes.ToList();
        }


        public async Task EditBoxesAsync(EditBoxesViewModel form)
        {
            var page = await this.context.Pages.FirstOrDefaultAsync(x => x.PageName == form.PageName);

            var boxes = page.ColorfulBoxes.Where(x => x.IsDeleted == false).ToList();

            for (int i = 0; i < form.Boxes.Count; i++)
            {
                var formBox = form.Boxes[i];

                var dbBox = boxes[i];

                dbBox.Href = formBox.Href;
                dbBox.Color = formBox.Color;
                dbBox.Text = formBox.Text;
                dbBox.IsDeleted = formBox.IsDeleted;
            }

            await this.context.SaveChangesAsync();


        }

        public async Task<Page> GetPageAsync(string pageName)
        {
            return await this.context.Pages.FirstOrDefaultAsync(x => x.PageName == pageName);
        }

        public async Task EditPageAsync(CreateCategoryPageViewModel form, bool filesIncluded)
        {
            var page = await this.context.Pages.FirstOrDefaultAsync(x => x.PageName == form.PreviousPageName);

            page.PageName = form.PageName;
            page.IsDeleted = form.IsDeleted;
            page.ModificationDate = DateTime.UtcNow;
            var pageCategory = page.Categories.FirstOrDefault();

            pageCategory.AllHref = form.PageCategory.AllHref;
            pageCategory.AllName = form.PageCategory.AllName;
            pageCategory.ModificationDate = DateTime.UtcNow;
            pageCategory.CategoryName = form.PageCategory.CategoryName;
            if (filesIncluded)
            {
                pageCategory.Pictures.FirstOrDefault().ModificationDate = DateTime.UtcNow;
                pageCategory.Pictures.FirstOrDefault().Url = form.PageCategory.Pictures[0];

            }
            for (int i = 0; i < pageCategory.ItemsCategories.Count; i++)
            {
                var cat = pageCategory.ItemsCategories.ToList()[i];
                var formCat = form.PageCategory.ItemsCategories[i];
                cat.Category = formCat.Category;
                cat.ModificationDate = DateTime.UtcNow;

                for (int z = 0; z < cat.Items.Count; z++)
                {
                    var item = cat.Items.ToList()[z];

                    var formItem = formCat.Items[z];

                    item.Href = formItem.Href;
                    item.Text = formItem.Text;
                    item.ModificationDate = DateTime.UtcNow;
                }
            }

            await this.context.SaveChangesAsync();
        }
    }
}
