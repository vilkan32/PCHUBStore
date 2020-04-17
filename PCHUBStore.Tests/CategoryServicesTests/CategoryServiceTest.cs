using Microsoft.EntityFrameworkCore;
using PCHUBStore.Services;
using PCHUBStore.Tests.Common;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PCHUBStore.Tests.CategoryServicesTests
{
    public class CategoryServiceTest
    {

        [Theory]
        [InlineData("Computer")]
        [InlineData("Laptop")]
        [InlineData("Mice")]
        [InlineData("Monitor")]
        [InlineData("Index")]
        public async Task TestIfPageExists(string pageName)
        {
            // Arrange
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            ICategoryServices categoryService = new CategoryServices(context);
            var pages = context.Pages.ToListAsync();

            await context.Pages.AddAsync(new Data.Models.Page
            {
                PageName = pageName,

            });

            await context.SaveChangesAsync();
            // Act

            var result = await categoryService.PageAlreadyExistsAsync(pageName);

            // Assert
            Assert.True(result);
        }


        [Theory]
        [InlineData("Computer")]
        [InlineData("Laptop")]
        [InlineData("Mice")]
        [InlineData("Monitor")]
        [InlineData("Index")]
        public async Task TestIfReturnsCorrectPage(string pageName)
        {
            // Arrange
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            ICategoryServices categoryService = new CategoryServices(context);
            var pages = context.Pages.ToListAsync();

            await context.Pages.AddAsync(new Data.Models.Page
            {
                PageName = pageName,

            });

            await context.SaveChangesAsync();
            // Act

            var result = await categoryService.GetPageAsync(pageName);

            // Assert
            Assert.Equal(result.PageName, pageName);
        }

    }
}
