using PCHUBStore.Data.Models;
using PCHUBStore.Services;
using PCHUBStore.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PCHUBStore.Tests.HomeServicesTests
{
    public class HomeServiceTest
    {

        ///var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);

       //var userReviews = user.ProductUserReviews.Select(x => x.Product).TakeLast(10);

       // return userReviews;

        [Theory]
        [InlineData("TestProduct1", "TestProduct2", "TestProduct3", "TestUserOne")]
        [InlineData("TestProduct1", "TestProduct2", "TestProduct3", "TestUserTwo")]
        [InlineData("TestProduct1", "TestProduct2", "TestProduct3", "TestUserThree")]
        public async Task TestIfHomePageReturnsUserReviewedProducts(string productOne,string productTwo, string productThree, string username)
        {
            // Arrange
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var homeService = new HomeService(context);
            var productUserReview = new List<ProductUserReview>();
            productUserReview.Add(new ProductUserReview
            {

                Product = new Product
                {

                    Title = productOne,
                    Price = 300,

                }

            });

            productUserReview.Add(new ProductUserReview
            {

                Product = new Product
                {

                    Title = productTwo,
                    Price = 300,

                }

            });

            productUserReview.Add(new ProductUserReview
            {

                Product = new Product
                {

                    Title = productThree,
                    Price = 300,

                }

            });

            await context.Users.AddAsync(new Data.Models.User
            {
                FirstName = username,
                UserName = username,
                ProductUserReviews = productUserReview
            });

            await context.SaveChangesAsync();
            //Act
            var result = await homeService.GetUserReviewedProductsAsync(username);
            
            // Assert
            Assert.Equal(3, result.ToList().Count);
        }

        [Fact]

        public async Task TestIndexPageIsReturnedCorrectly()
        {
            // Arrange
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var homeService = new HomeService(context);

            await context.Pages.AddAsync(new Page
            {
                PageName = "Index",
                IsDeleted = false,


            });

            await context.SaveChangesAsync();

            var page = await homeService.LoadIndexPageComponentsAsync();

            Assert.NotNull(page);
            Assert.Equal("Index", page.PageName);
        }

        [Fact]

        public async Task TestIndexPageReturnsNullCases()
        {
            // Arrange
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var homeService = new HomeService(context);

            await context.Pages.AddAsync(new Page
            {
                PageName = "Index",
                IsDeleted = true,


            });

            await context.SaveChangesAsync();

            var page = await homeService.LoadIndexPageComponentsAsync();

            Assert.Null(page);
        }


        [Fact]

        public async Task TestMainSliderPicturesReturnCorrectResult()
        {
            // Arrange
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var picturesModel = new List<Picture>();

            for (int i = 0; i < 5; i++)
            {
                picturesModel.Add(new Picture
                {
                    Name = "TestPic" + i,
                    Url = "TestPic" + 1,
                });
            }
            var homeService = new HomeService(context);

            await context.MainSliders.AddAsync(new MainSlider
            {
             Name = "MainSlider",
             MainSliderPictures = picturesModel,

            });

            await context.SaveChangesAsync();

            var pictures = await homeService.GetMainSliderPicturesAsync();

            Assert.Equal(5, pictures.Count);

            Assert.True(pictures.Exists(x => x.Name.Contains("TestPic")));
        }


        [Fact]
        public async Task TestMainSliderPicturesThrowsNullReferenceWhenEmpty()
        {
            // Arrange
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var picturesModel = new List<Picture>();

            var homeService = new HomeService(context);


           await Assert.ThrowsAsync<NullReferenceException>(async () =>
                    await homeService.GetMainSliderPicturesAsync()
            );

        }

    }
}
