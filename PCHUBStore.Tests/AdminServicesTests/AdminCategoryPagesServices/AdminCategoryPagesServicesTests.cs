using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PCHUBStore.Areas.Administration.Models.CategoryPagesViewModels;
using PCHUBStore.Data.Models;
using PCHUBStore.Migrations;
using PCHUBStore.Tests.Common;
using Xunit;

namespace PCHUBStore.Tests.AdminServicesTests.AdminCategoryPagesServices
{
    public class AdminCategoryPagesServicesTests
    {
        [Fact]
        public async Task TestIfCreateCategoryPageWorksAccordingly()
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var categoryPagesService = new Areas.Administration.Services.AdminCategoryPagesServices(context);

            var model = new CreateCategoryPageViewModel();

            var items = new List<PageCategoryItemsViewModel>
            {
                new PageCategoryItemsViewModel
                {
                    Href = "Href",
                    Text = "Text",
                },
                new PageCategoryItemsViewModel
                {
                    Href = "HrefTest",
                    Text = "TextTest",
                },
                new PageCategoryItemsViewModel
                {
                    Href = "HrefTestNext",
                    Text = "TextTestNext",
                },
            };

            var itemCategories = new List<CategoryPageItemsCategoryViewModel>
            {

                new CategoryPageItemsCategoryViewModel
                {
                    Category = "Price",
                    Items = items,
                }
            };
            model.PageCategory = new PageCategoryViewModel
            {
                CategoryName = "Laptops",
                Pictures = new List<string>
                {
                    "Url1", "Ur2", "Url3",
                },
                AllHref = "AllHref",
                AllName = "AllName",
                ItemsCategories = itemCategories,
            };

            model.PageName = "Laptops";

            await categoryPagesService.CreateCategoryPageAsync(model);

            var pageResult = await context.Pages.FirstOrDefaultAsync(x => x.PageName == "Laptops");

            Assert.NotNull(pageResult);

            Assert.Contains(pageResult.Categories, (x) => x.CategoryName == "Laptops");

            var resultItemsCategories = pageResult.Categories.SelectMany(x => x.ItemsCategories);

            Assert.NotEmpty(resultItemsCategories);

            var resultItems = resultItemsCategories.SelectMany(x => x.Items);

            Assert.Contains(resultItems, x => x.Text == "Text" && x.Href == "Href");


        }

        [Theory]
        [InlineData(null)]
        [InlineData("NonExisting")]
        [InlineData("NonExistingCategory")]
        public async Task TestIfPageAlreadyExistsReturnsFalse(string category)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var categoryPagesService = new Areas.Administration.Services.AdminCategoryPagesServices(context);

            Assert.False(await categoryPagesService.PageAlreadyExistsAsync(category));

        }

        [Theory]
        [InlineData(null)]
        [InlineData("NonExisting")]
        [InlineData("NonExistingCategory")]
        public async Task TestIfPageAlreadyExistsReturnsTrue(string category)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var categoryPagesService = new Areas.Administration.Services.AdminCategoryPagesServices(context);

            await context.Pages.AddAsync(new Page
            {
                PageName = category
            });

            await context.SaveChangesAsync();

            Assert.True(await categoryPagesService.PageAlreadyExistsAsync(category));

        }

        [Fact]
        public async Task TestIfAddBoxThrowsError()
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var categoryPagesService = new Areas.Administration.Services.AdminCategoryPagesServices(context);

            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                var model = new AddBoxViewModel();
                await categoryPagesService.AddBoxAsync(model);
            });

        }

        [Theory]
        [InlineData("Laptops", "Href", "Text", "Red")]
        [InlineData("Computers", "HrefTest", "TextTest", "Blue")]
        [InlineData("Mice", "RandomHref", "RandomText", "Yellow")]
        public async Task TestIfAddBoxWorksAccordingly(string pageName, string href, string text, string color)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var categoryPagesService = new Areas.Administration.Services.AdminCategoryPagesServices(context);

            await context.Pages.AddAsync(new Page
            {
                PageName = pageName,
                ColorfulBoxes = new List<ColorfulBox>(),
            });

            await context.SaveChangesAsync();

            var model = new AddBoxViewModel();

            model.Color = color;
            model.Href = href;
            model.Text = text;
            model.PageName = pageName;

            await categoryPagesService.AddBoxAsync(model);

            var result = await context.Pages.FirstOrDefaultAsync(x => x.PageName == pageName);

            Assert.NotNull(result);
            Assert.NotEmpty(result.ColorfulBoxes);
            Assert.Contains(result.ColorfulBoxes,
                boxes => boxes.Href == href && boxes.Text == text && boxes.Color == color);
        }

        [Fact]

        public async Task TestIfGetAllPageNamesReturnsEmptyCollection()
        {

            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var categoryPagesService = new Areas.Administration.Services.AdminCategoryPagesServices(context);

            var result = await categoryPagesService.GetAllPageNamesAsync();

            Assert.Empty(result);
        }

        [Theory]
        [InlineData("TestPageName")]
        [InlineData("RandomPageName")]
        [InlineData("ValidPageName")]
        public async Task TestIfGetAllPageNamesWorksAccordingly(string pageName)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var categoryPagesService = new Areas.Administration.Services.AdminCategoryPagesServices(context);

            await context.Pages.AddAsync(new Page
            {
                PageName = pageName,
            });

            await context.SaveChangesAsync();

            var result = await categoryPagesService.GetAllPageNamesAsync();

            Assert.NotEmpty(result);

            Assert.DoesNotContain(result, x => x == "Index");

            Assert.Contains(result, x => x == pageName);
        }

        [Fact]

        public async Task TestIfGetAllBoxesForPageThrowsError()
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var categoryPagesService = new Areas.Administration.Services.AdminCategoryPagesServices(context);

            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await categoryPagesService.GetAllBoxesForPageAsync(null);
            });

        }


        [Theory]
        [InlineData("Laptops", "Href", "Text", "Red")]
        [InlineData("Computers", "HrefTest", "TextTest", "Blue")]
        [InlineData("Mice", "RandomHref", "RandomText", "Yellow")]
        public async Task TestIfGetAllBoxesForPageWorksAccordingly(string pageName, string href, string text,
            string color)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var categoryPagesService = new Areas.Administration.Services.AdminCategoryPagesServices(context);

            await context.Pages.AddAsync(new Page
            {
                PageName = pageName,
                ColorfulBoxes = new List<ColorfulBox>(),
            });

            await context.SaveChangesAsync();

            var model = new AddBoxViewModel();

            model.Color = color;
            model.Href = href;
            model.Text = text;
            model.PageName = pageName;

            await categoryPagesService.AddBoxAsync(model);

            var result = await categoryPagesService.GetAllBoxesForPageAsync(pageName);

            Assert.NotEmpty(result);

            Assert.Contains(result, x => x.Href == href && x.Text == text && x.Color == color);
        }

        [Fact]
        public async Task TestIfEditBoxesThrowsError()
        {

            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var categoryPagesService = new Areas.Administration.Services.AdminCategoryPagesServices(context);

            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                var form = new EditBoxesViewModel();

                await categoryPagesService.EditBoxesAsync(form);
            });

        }

        [Theory]
        [InlineData("Laptops", "Href", "Text", "Red")]
        [InlineData("Computers", "HrefTest", "TextTest", "Blue")]
        [InlineData("Mice", "RandomHref", "RandomText", "Yellow")]
        public async Task TestIfEditBoxesWorksAccordingly(string pageName, string href, string text,
            string color)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var categoryPagesService = new Areas.Administration.Services.AdminCategoryPagesServices(context);

            await context.Pages.AddAsync(new Page
            {
                PageName = pageName,
                ColorfulBoxes = new List<ColorfulBox>(),
            });

            await context.SaveChangesAsync();

            var model = new AddBoxViewModel();

            model.Color = color;
            model.Href = href;
            model.Text = text;
            model.PageName = pageName;

            await categoryPagesService.AddBoxAsync(model);

            var boxModel = new EditBoxesViewModel();

            boxModel.PageName = pageName;
            boxModel.Boxes = new List<EditBoxViewModel>
            {
                new EditBoxViewModel
                {
                    Color = "edited",
                    Href = "edited",
                    Text = "edited",
                }
            };

            await categoryPagesService.EditBoxesAsync(boxModel);

            var result = await categoryPagesService.GetAllBoxesForPageAsync(pageName);

            Assert.NotEmpty(result);

            Assert.Contains(result, (x) => x.Href == "edited" && x.Color == "edited" && x.Text == "edited");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("PageName")]
        [InlineData("Laptops")]
        [InlineData("Computers")]
        public async Task TestIfGetPageReturnsNull(string pageName)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var categoryPagesService = new Areas.Administration.Services.AdminCategoryPagesServices(context);

            var result = await categoryPagesService.GetPageAsync(pageName);

            Assert.Null(result);
        }



        [Theory]
        [InlineData("PageName")]
        [InlineData("Laptops")]
        [InlineData("Computers")]
        public async Task TestIfGetPageReturnsCorrectResult(string pageName)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var categoryPagesService = new Areas.Administration.Services.AdminCategoryPagesServices(context);


            await context.Pages.AddAsync(new Page
            {
                PageName = pageName,
            });

            await context.SaveChangesAsync();
            
            var result = await categoryPagesService.GetPageAsync(pageName);

            Assert.NotNull(result);

            Assert.Equal(pageName, result.PageName);
        }

    }
}
