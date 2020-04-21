using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PCHUBStore.Areas.Administration.Models.CharacteristicsViewModels;
using PCHUBStore.Tests.Common;
using Xunit;

namespace PCHUBStore.Tests.AdminServicesTests.AdminCharacteristicsServices
{
    public class AdminCharacteristicsServiceTests
    {
        [Theory]
        [InlineData("CategoryNameOne")]
        [InlineData("CategoryNameTwo")]
        [InlineData("CategoryNameThree")]
        public async Task TestIfCreateCharacteristicsCategoryWorksAccordingly(string categoryName)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var characteristicsService = new Areas.Administration.Services.AdminCharacteristicsServices(context);

            var form = new InsertCharacteristicsCategoryViewModel();

            form.CategoryName = categoryName;

            await characteristicsService.CreateCharacteristicsCategoryAsync(form);

            var result =
                await context.AdminCharacteristicsCategories.FirstOrDefaultAsync(x => x.CategoryName == categoryName);


            Assert.NotNull(result);

        }

        [Fact]
        public async Task TestIfCreateCharacteristicsWorksAccordingly()
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var characteristicsService = new Areas.Administration.Services.AdminCharacteristicsServices(context);



            var categoryForm = new InsertCharacteristicsCategoryViewModel();

            categoryForm.CategoryName = "Laptops";

            await characteristicsService.CreateCharacteristicsCategoryAsync(categoryForm);

            var form = new InsertCharacteristicsViewModel();

            form.BasicCharacteristics.AddRange(new List<string>
            {
                "Acer",
                "Lenovo",
                "Dell",
                "Ombre",
            });

            form.Category = "Laptops";

            form.FullCharacteristics.AddRange(new List<string>
            {
                "Acer",
                "Lenovo",
                "Dell",
                "Ombre",
            });

            await characteristicsService.CreateCharacteristicsAsync(form);


            var result = await context.AdminCharacteristicsCategories.FirstOrDefaultAsync(x => x.CategoryName == form.Category);

            Assert.NotEmpty(result.FullCharacteristics);
            Assert.NotEmpty(result.BasicCharacteristics);

        }

        [Theory]
        [InlineData("Category")]
        [InlineData(null)]
        [InlineData("TestCategory")]
        public async Task TestIfCategoryExistsReturnsFalse(string categoryName)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var characteristicsService = new Areas.Administration.Services.AdminCharacteristicsServices(context);

            Assert.False(await characteristicsService.CategoryExistsAsync(categoryName));
        }

        [Theory]
        [InlineData("Category")]
        [InlineData("TestCategoryTwo")]
        [InlineData("TestCategory")]
        public async Task TestIfCategoryExistsReturnsTrue(string categoryName)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var characteristicsService = new Areas.Administration.Services.AdminCharacteristicsServices(context);

            var categoryForm = new InsertCharacteristicsCategoryViewModel();

            categoryForm.CategoryName = categoryName;

            await characteristicsService.CreateCharacteristicsCategoryAsync(categoryForm);
            Assert.True(await characteristicsService.CategoryExistsAsync(categoryName));
        }

        [Fact]
        public async Task TestIfCharacteristicsExistsWorksAccordingly()
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var characteristicsService = new Areas.Administration.Services.AdminCharacteristicsServices(context);



            var categoryForm = new InsertCharacteristicsCategoryViewModel();

            categoryForm.CategoryName = "Laptops";

            await characteristicsService.CreateCharacteristicsCategoryAsync(categoryForm);

            var form = new InsertCharacteristicsViewModel();

            form.BasicCharacteristics.AddRange(new List<string>
            {
                "Acer",
                "Lenovo",
                "Dell",
                "Ombre",
            });

            form.Category = "Laptops";

            form.FullCharacteristics.AddRange(new List<string>
            {
                "Acer",
                "Lenovo",
                "Dell",
                "Ombre",
            });

            await characteristicsService.CreateCharacteristicsAsync(form);


            Assert.True(await characteristicsService.CharacteristicsExistsAsync("Laptops"));

        }

        [Fact]
        public async Task TestIfCharacteristicsExistsReturnsFalse()
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var characteristicsService = new Areas.Administration.Services.AdminCharacteristicsServices(context);

            Assert.False(await characteristicsService.CharacteristicsExistsAsync("Laptops"));
        }

        [Fact]
        public async Task TestIfGetAvailableCharacteristicsReturnsEmptyCollection()
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var characteristicsService = new Areas.Administration.Services.AdminCharacteristicsServices(context);

            Assert.Empty(await characteristicsService.GetAvailableCharacteristicsAsync());
        }

        [Fact]
        public async Task TestIfGetAvailableCharacteristicsReturnsCorrectResult()
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var characteristicsService = new Areas.Administration.Services.AdminCharacteristicsServices(context);


            var categoryForm = new InsertCharacteristicsCategoryViewModel();

            categoryForm.CategoryName = "Laptops";

            await characteristicsService.CreateCharacteristicsCategoryAsync(categoryForm);

            Assert.NotEmpty(await characteristicsService.GetAvailableCharacteristicsAsync());
        }
    }
}
