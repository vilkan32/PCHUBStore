using Microsoft.EntityFrameworkCore;
using PCHUBStore.Data.Models;
using PCHUBStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task TestIfGetMostViewedProductsWorksAccordingly(string title, int views)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var chartsService = new SupportChartsService(context);

            await context.Products.AddRangeAsync(new Product
            {
                Title = title,
                Views = views,

            });

            var result = 
        }

    }
}
