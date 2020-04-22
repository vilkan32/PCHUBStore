using PCHUBStore.Data.Models;
using System.Threading.Tasks;
using PCHUBStore.Areas.Support.Services;
using PCHUBStore.Tests.Common;
using Xunit;


namespace PCHUBStore.Tests.SupportServicesTests.SupportChartsServicesTests
{
    public class SupportChartsServiceTests
    {
        [Fact]
        public async Task TestIfGetMostViewedProductsReturnsEmpty()
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var chartsService = new SupportChartsService(context);

            Assert.Empty(await chartsService.GetMostViewedProductsAsync());

        }

        [Theory]
        [InlineData("ProductTitle", 20)]
        [InlineData("Product", 30)]
        [InlineData("RandomTestProduct", 1000)]
        [InlineData("Acer Asipre v19", 10000)]
        public async Task TestIfGetMostViewedProductsWorksAccordingly(string title, int views)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var chartsService = new SupportChartsService(context);

            await context.Products.AddRangeAsync(new Product
            {
                Title = title,
                Views = views,

            });

            await context.SaveChangesAsync();
            var result = await chartsService.GetMostViewedProductsAsync();

            Assert.NotEmpty(result);

            Assert.Contains(result, x => x.Views == views && x.Title == title);
        }

        [Fact]
        public async Task TestIfGetMostExpensiveProductsReturnEmptyCollection()
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var chartsService = new SupportChartsService(context);

            Assert.Empty(await chartsService.GetMostExpensiveProductsAsync());

        }

 
        [Theory]
        [InlineData("ProductTitle", 20)]
        [InlineData("Product", 30)]
        [InlineData("RandomTestProduct", 1000)]
        [InlineData("Acer Asipre v19", 10000)]
        public async Task TestIfGetMostExpensiveProductsWorksAccordingly(string title, decimal price)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var chartsService = new SupportChartsService(context);

            await context.Products.AddRangeAsync(new Product
            {
                Title = title,
                Price = price
            });

            await context.SaveChangesAsync();


            var result = await chartsService.GetMostExpensiveProductsAsync();

            Assert.NotEmpty(result);

            Assert.Contains(result, x => x.Title == title && x.Price == price);
        }


    }
}
