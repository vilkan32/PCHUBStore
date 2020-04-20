using System;
using System.Collections.Generic;
using System.Linq;
using PCHUBStore.Tests.Common;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using PCHUBStore.Areas.Administration.Models.IndexPageViewModels;
using PCHUBStore.Data.Models;
using Xunit;
using AddBoxViewModel = PCHUBStore.Areas.Administration.Models.IndexPageViewModels.AddBoxViewModel;
using EditBoxViewModel = PCHUBStore.Areas.Administration.Models.IndexPageViewModels.EditBoxViewModel;


namespace PCHUBStore.Tests.AdminServicesTests.AdminIndexPageServices
{
    public class AdminIndexPageServicesTest
    {

        [Theory]
        [InlineData("Index")]
        [InlineData("Test")]
        [InlineData("Page")]
        [InlineData("TestPage")]
        public async Task TestIfPageExistsReturnsTrue(string pageName)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var adminPageService = new Areas.Administration.Services.AdminIndexPageServices(context);

            await context.Pages.AddAsync(new Page
            {
                PageName = pageName
            });

            await context.SaveChangesAsync();

            Assert.True(await adminPageService.PageExistsAsync(pageName));
        }

        [Theory]
        [InlineData("Index")]
        [InlineData("Test")]
        [InlineData("Page")]
        [InlineData("TestPage")]
        public async Task TestIfPageExistsReturnsFalse(string pageName)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var adminPageService = new Areas.Administration.Services.AdminIndexPageServices(context);

            Assert.False(await adminPageService.PageExistsAsync(pageName));
        }

        [Theory]
        [InlineData("Index")]
        [InlineData("Test")]
        [InlineData("Page")]
        [InlineData("TestPage")]
        public async Task TestIfCreatePageWorksAccordingly(string pageName)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var adminPageService = new Areas.Administration.Services.AdminIndexPageServices(context);

            await adminPageService.CreateIndexPageAsync(pageName);

            var result = await context.Pages.FirstOrDefaultAsync(x => x.PageName == pageName);

            Assert.Equal(pageName, result.PageName);

        }

        [Theory]
        [InlineData("Test")]
        [InlineData("Page")]
        [InlineData("TestPage")]
        public async Task TestIfAddIndexPageCategoryThrowsError(string pagename)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var adminPageService = new Areas.Administration.Services.AdminIndexPageServices(context);

            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                var form = new CreatePageCategoryViewModel();

                await adminPageService.AddIndexPageCategoryAsync(form, "pictureUrl", "PictureName");
            });
        }

        [Fact]
        public async Task TestIfAddIndexPageCategoryWorksProperly()
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var adminPageService = new Areas.Administration.Services.AdminIndexPageServices(context);


            await adminPageService.CreateIndexPageAsync("Index");


            var form = new CreatePageCategoryViewModel();

            form.AllHref = "AllHref";
            form.AllName = "AllLaptops";
            form.CategoryName = "Laptops";

            await adminPageService.AddIndexPageCategoryAsync(form, "pictureUrl", "PictureName");

            var result = await context.Pages.FirstOrDefaultAsync(x => x.PageName == "Index");

            Assert.Equal("Index",result.PageName);

            Assert.Contains(result.Categories, (pageCategory) => pageCategory.CategoryName == "Laptops");

            Assert.Contains(result.Categories, (pageCategory) => pageCategory.AllHref == "AllHref");

            Assert.Contains(result.Categories, (pageCategory) => pageCategory.AllName == "AllLaptops");
        }

        [Fact]
        public async Task TestIfGetAllPageCategoryNamesThrowsError()
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var adminPageService = new Areas.Administration.Services.AdminIndexPageServices(context);

            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await adminPageService.GetAllPageCategoryNamesAsync();
            });

        }

        [Theory]
        [InlineData("Index", "IndexCategory1")]
        [InlineData("Index", "IndexCategory2")]
        [InlineData("Index", "IndexCategory3")]
        [InlineData("Index", "IndexCategory4")]
        public async Task TestIfGetAllPageCategoryNamesWorksAccordingly(string pagename, string categoryName)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var adminPageService = new Areas.Administration.Services.AdminIndexPageServices(context);


            await adminPageService.CreateIndexPageAsync(pagename);


            var form = new CreatePageCategoryViewModel();

            form.AllHref = "AllHref";
            form.AllName = "AllLaptops";
            form.CategoryName = categoryName;

            await adminPageService.AddIndexPageCategoryAsync(form, "pictureUrl", "PictureName");

            var result = await adminPageService.GetAllPageCategoryNamesAsync();

            Assert.Contains(result, (str) => str == categoryName);
        }

        [Fact]
        public async Task TestIfAdditemsToCategoryThrowsError()
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var adminPageService = new Areas.Administration.Services.AdminIndexPageServices(context);

            var form = new IndexItemsCategoryViewModel();


            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await adminPageService.AdditemsToCategoryAsync(form);
            });
        }


        [Theory]
        [InlineData("TestText", "TestHref")]
        [InlineData("TestText1", "TestHref1")]
        [InlineData("TestText2", "TestHref2")]
        [InlineData("TestText3", "TestHref3")]
        public async Task TestIfAdditemsToCategoryWorksAccordingly(string text, string href)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var adminPageService = new Areas.Administration.Services.AdminIndexPageServices(context);

            await adminPageService.CreateIndexPageAsync("Index");


            var formCategory = new CreatePageCategoryViewModel();

            formCategory.AllHref = "AllHref";
            formCategory.AllName = "AllLaptops";
            formCategory.CategoryName = "Category";

            await adminPageService.AddIndexPageCategoryAsync(formCategory, "pictureUrl", "PictureName");

            var form = new IndexItemsCategoryViewModel();

            form.PageCategory = "Category";

            form.ItemCategory = "Price";

            form.Items = new List<IndexPageItemsViewModel>();

            form.Items.Add(new IndexPageItemsViewModel
            {
                Text = text,
                Href =  href,
            });

            await adminPageService.AdditemsToCategoryAsync(form);

            var result = await context.PageCategories.FirstOrDefaultAsync(x => x.CategoryName == "Category");

            Assert.Contains(result.ItemsCategories, (ic) => ic.Category == "Price");
            Assert.Contains(result.ItemsCategories, (ic) => EnumerableExtensions.Any(ic.Items));



            foreach (var ic in result.ItemsCategories)
            {
                foreach (var item in ic.Items)
                {
                    Assert.Equal(text, item.Text);
                    Assert.Equal(href, item.Href);
                }
            }
        }

        [Theory]
        [InlineData("TestCategory")]
        [InlineData("TestCategory1")]
        [InlineData("TestCategory2")]
        [InlineData("TestCategory3")]
        public async Task TesIfGetIndexCategoryThrowsError(string category)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var adminPageService = new Areas.Administration.Services.AdminIndexPageServices(context);

            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await adminPageService.GetIndexCategoryAsync(category);
            });
        }

        [Theory]
        [InlineData("Index", "IndexCategory1")]
        [InlineData("Index", "IndexCategory2")]
        [InlineData("Index", "IndexCategory3")]
        [InlineData("Index", "IndexCategory4")]
        public async Task TestIfGetIndexCategoryReturnsCorrectResult(string pagename, string categoryName)
        {

            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var adminPageService = new Areas.Administration.Services.AdminIndexPageServices(context);

            await adminPageService.CreateIndexPageAsync(pagename);


            var form = new CreatePageCategoryViewModel();

            form.AllHref = "AllHref";
            form.AllName = "AllLaptops";
            form.CategoryName = categoryName;

            await adminPageService.AddIndexPageCategoryAsync(form, "pictureUrl", "PictureName");


            var result = await adminPageService.GetIndexCategoryAsync(categoryName);

            Assert.NotEmpty(result);

            Assert.Contains(result, (pc) => pc.CategoryName == categoryName);
        }

        [Fact]
        public async Task TestIfEditIndexPageCategoryThrowsError()
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var adminPageService = new Areas.Administration.Services.AdminIndexPageServices(context);

            var form = new EditIndexCategoryViewModel();

            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await adminPageService.EditIndexPageCategoryAsync(form, "RandomCategory");
            });
        }


        [Theory]
        [InlineData("TestText", "TestHref")]
        [InlineData("TestText1", "TestHref1")]
        [InlineData("TestText3", "TestHref3")]
        public async Task TestIfEditIndexPageCategoryWorksAccordingly(string text, string href)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var adminPageService = new Areas.Administration.Services.AdminIndexPageServices(context);

            await adminPageService.CreateIndexPageAsync("Index");


            var formCategory = new CreatePageCategoryViewModel();

            formCategory.AllHref = "AllHref";
            formCategory.AllName = "AllLaptops";
            formCategory.CategoryName = "Category";

            await adminPageService.AddIndexPageCategoryAsync(formCategory, "pictureUrl", "PictureName");

            var form = new IndexItemsCategoryViewModel();

            form.PageCategory = "Category";

            form.ItemCategory = "Price";

            form.Items = new List<IndexPageItemsViewModel>();

            form.Items.Add(new IndexPageItemsViewModel
            {
                Text = text,
                Href = href,
            });

            await adminPageService.AdditemsToCategoryAsync(form);

            var model = new EditIndexCategoryViewModel();
            model.Category = "Category";
            model.PageCategory = new List<EditCategoryViewModel>();

            var itemCategories = new List<EditItemCategoriesViewModel>();
            var items = new List<IndexPageItemsViewModel>();

            items.Add(new IndexPageItemsViewModel
            {
                Text = "TextRand",
                Href = "HrefRand",
            });
            itemCategories.Add(new EditItemCategoriesViewModel
            {
                ItemCategory = "Price",
                Items = items
            });
            model.PageCategory.Add(new EditCategoryViewModel
            {
                CategoryName = "NewCategory",
                AllHref = "NewAllHref",
                AllName = "AllName",
                ItemCategories = itemCategories
            });


            await adminPageService.EditIndexPageCategoryAsync(model, "Category");

            var result = await context.Pages.FirstOrDefaultAsync(x => x.PageName == "Index");

            Assert.NotNull(result);

            Assert.Contains(result.Categories, (pc) => pc.CategoryName == "NewCategory");
            Assert.Contains(result.Categories, (pc) => pc.AllHref == "NewAllHref");
            Assert.Contains(result.Categories, (pc) => pc.AllName == "AllName");
        }

        [Fact]
        public async Task TestIfAddBoxThrowsError()
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var adminPageService = new Areas.Administration.Services.AdminIndexPageServices(context);

            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                var model = new AddBoxViewModel();

                await adminPageService.AddBoxAsync(model);
            });
        }

        [Theory]
        [InlineData("Red", "TestHref", "TestText")]
        [InlineData("Blue", "TestHref1", "TestText1")]
        [InlineData("Purple", "TestHref2", "TestText2")]
        [InlineData("Yellow", "TestHref3", "TestText3")]
        public async Task TestIfAddBoxWorksAccordingly(string color, string href, string text)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var adminPageService = new Areas.Administration.Services.AdminIndexPageServices(context);

            var model = new AddBoxViewModel();

            await adminPageService.CreateIndexPageAsync("Index");

            model.Color = color;
            model.Href = href;
            model.Text = text;

            await adminPageService.AddBoxAsync(model);

            var result = await context.Pages.FirstOrDefaultAsync(x => x.PageName == "Index");

            Assert.NotEmpty(result.ColorfulBoxes);

            Assert.Contains(result.ColorfulBoxes, (cb) => cb.Text == text && cb.Href == href && cb.Color == color);


        }

        [Fact]

        public async Task TestIfGetAllBoxesThrowsError()
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var adminPageService = new Areas.Administration.Services.AdminIndexPageServices(context);

            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await adminPageService.GetAllBoxesAsync();
            });

        }


        [Theory]
        [InlineData("Red", "TestHref", "TestText")]

        public async Task TestIfGetAllBoxesWorksAccordingly(string color, string href, string text)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var adminPageService = new Areas.Administration.Services.AdminIndexPageServices(context);

            var model = new AddBoxViewModel();

            await adminPageService.CreateIndexPageAsync("Index");

            model.Color = color;
            model.Href = href;
            model.Text = text;

            await adminPageService.AddBoxAsync(model);

            var result = await adminPageService.GetAllBoxesAsync();

            Assert.NotEmpty(result);
            Assert.Equal(1, result.Count());


        }

        [Fact]
        public async Task TestIfUploadMainSliderPicturesWorksAccordingly()
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var testUrls = new List<string>
            {
                "TestUrl1",
                "TestUrl2",
                "RandomTestUrl",
            };

            var adminPageService = new Areas.Administration.Services.AdminIndexPageServices(context);


            await adminPageService.UploadMainSliderPicturesAsync(testUrls);

            var result = await context.MainSliders.FirstOrDefaultAsync(x => x.Name == "MainSlider");

            Assert.Equal("MainSlider", result.Name);

            Assert.NotEmpty(result.MainSliderPictures);
        }

        [Fact]
        public async Task TestIfGetMainSliderPicturesThrowsError()
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();


            var adminPageService = new Areas.Administration.Services.AdminIndexPageServices(context);

            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await adminPageService.GetMainSliderPicturesAsync();
            });

        }

        [Fact]
        public async Task TestIfGetMainSliderPicturesWorksAccordingly()
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var testUrls = new List<string>
            {
                "TestUrl1",
                "TestUrl2",
                "RandomTestUrl",
            };

            var adminPageService = new Areas.Administration.Services.AdminIndexPageServices(context);


            await adminPageService.UploadMainSliderPicturesAsync(testUrls);

            var result = await adminPageService.GetMainSliderPicturesAsync();

            Assert.NotEmpty(result);

            Assert.Contains(result, pic => pic.Url == "TestUrl1");
        }

        [Fact]

        public async Task TestIfEditMainSliderPicturesThrowsError()
        {

            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var adminPageService = new Areas.Administration.Services.AdminIndexPageServices(context);
            var form = new List<EditMainSliderPictures>();

            form.Add(new EditMainSliderPictures
            {
                Url = "Url",
                Name = "Name",
                IsDeleted = false,
            });
            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
               await adminPageService.EditMainSliderPicturesAsync(form);
            });

        }

        [Fact]

        public async Task TestIfEditMainSliderPicturesWorksAccordingly()
        {

            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var adminPageService = new Areas.Administration.Services.AdminIndexPageServices(context);

            var testUrls = new List<string>
            {
                "TestUrl1",
                "TestUrl2",
                "RandomTestUrl",
            };

            await adminPageService.UploadMainSliderPicturesAsync(testUrls);

            var mainSlider = await context.MainSliders.FirstOrDefaultAsync(x => x.Name == "MainSlider");

            var pictureIds = mainSlider.MainSliderPictures.Select(x => x.Id);

            var form = new List<EditMainSliderPictures>();

            foreach (var id in pictureIds)
            {
                form.Add(new EditMainSliderPictures
                {
                    Url = "Url",
                    Name = "Name",
                    IsDeleted = false,
                    Id = id
                });
            }

            await adminPageService.EditMainSliderPicturesAsync(form);

            var result = await adminPageService.GetMainSliderPicturesAsync();

            Assert.NotEmpty(result);

            foreach (var resultPic in result)
            {
                Assert.Equal("Url", resultPic.Url);
                Assert.Equal("Name", resultPic.Name);
            }

        }

        [Fact]
        public async Task TestIfEditBoxesThrowsError()
        {

            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var adminPageService = new Areas.Administration.Services.AdminIndexPageServices(context);

            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                var form = new EditBoxViewModel();

                await adminPageService.EditBoxesAsync(form);
            });

        }

        [Fact]
        public async Task TestIfEditBoxesWorksAccordingly()
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var adminPageService = new Areas.Administration.Services.AdminIndexPageServices(context);

            var model = new AddBoxViewModel();

            await adminPageService.CreateIndexPageAsync("Index");

            model.Color = "Red";
            model.Href = "TestModelHref";
            model.Text = "TestModelText";

            await adminPageService.AddBoxAsync(model);

            var form = new EditBoxViewModel();

            form.BoxViewModel = new List<AddBoxViewModel>();

            form.BoxViewModel.Add(new AddBoxViewModel
            {
                Text = "Tested",
                Color = "Tested",
                Href = "Tested",
                IsDeleted = true,
            });

            await adminPageService.EditBoxesAsync(form);

            var page = await context.Pages.FirstOrDefaultAsync(x => x.PageName == "Index");

            var boxes = page.ColorfulBoxes.Where(x => x.IsDeleted == false).ToList();

            Assert.Empty(boxes);

            var deletedBoxes = page.ColorfulBoxes.Where(x => x.IsDeleted == true).ToList();

            foreach (var box in deletedBoxes)
            {
                Assert.Equal(box.Color, "Tested");
                Assert.Equal(box.Href, "Tested");
                Assert.Equal(box.Text, "Tested");
                Assert.True(box.IsDeleted);
            }
        }


    }
}
