using Microsoft.EntityFrameworkCore;
using PCHUBStore.Data.Models;
using PCHUBStore.Services;
using PCHUBStore.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using PCHUBStore.Data.Models.Enums;
using PCHUBStore.View.Models.UserProfileViewModels;
using Xunit;

namespace PCHUBStore.Tests.UserProfileServicesTests
{

    public class UserProfileServiceTests
    {


        [Theory]
        [InlineData("TestUser", "ProductOneId", "ProductTwoId", "ProductFourId")]
        [InlineData("TestUserOne", "ProductOneId1", "ProductTwoId1", "ProductFourId1")]
        [InlineData("TestUserTwo", "ProductOneId2", "ProductTwoId2", "ProductFourId2")]
        [InlineData("TestUserThree", "ProductOneId3", "ProductTwoId3", "ProductFourId3")]
        public async Task TestIfAddToFavoritesReturnTrueAndCorrectNumberOfProducts(string username, string product1, 
            string product2, string product3)
        {
            // Arrange
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);
            var userProfileService = new UserProfileServices(context,TestUserManager<User>(), productService);


            var products = new List<Product>();

            products.Add(new Product
            {
                Id = product1,
                Price = 300,
                Title = "Test Product",
                IsDeleted = false,
                CreatedOn = DateTime.Now,

            });


            products.Add(new Product
            {
                Id = product2,
                Price = 300,
                Title = "Test Product1",
                IsDeleted = false,
                CreatedOn = DateTime.Now,

            });

            products.Add(new Product
            {
                Id = product3,
                Price = 300,
                Title = "Test Product2",
                IsDeleted = false,
                CreatedOn = DateTime.Now,

            });

            await context.Products.AddRangeAsync(products);

            await context.Users.AddAsync(new User
            {

                UserName = username,
                Email = "email@email.bg",
                LastLoginDate = DateTime.UtcNow,

            });

            await context.SaveChangesAsync();

            await userProfileService.AddToFavoritesAsync(username, product1);

            var testUser = await context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            Assert.Equal(1, testUser.FavoriteUserProducts.Count);

            await userProfileService.AddToFavoritesAsync(username, product1);

            var testUserTwo = await context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            Assert.True(await userProfileService.AddToFavoritesAsync(username, product2));

            Assert.Equal(2, testUserTwo.FavoriteUserProducts.Count);

            var testUserThree = await context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            await userProfileService.AddToFavoritesAsync(username, product3);

            Assert.Equal(3, testUserThree.FavoriteUserProducts.Count);

            var testUserFour = await context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            await userProfileService.AddToFavoritesAsync(username, product3);

            Assert.Equal(3, testUserFour.FavoriteUserProducts.Count);
        }


        [Theory]
        [InlineData("TestUser", "ProductOneId", "ProductTwoId", "ProductFourId")]
        [InlineData("TestUserOne", "ProductOneId1", "ProductTwoId1", "ProductFourId1")]
        [InlineData("TestUserTwo", "ProductOneId2", "ProductTwoId2", "ProductFourId2")]
        [InlineData("TestUserThree", "ProductOneId3", "ProductTwoId3", "ProductFourId3")]
        public async Task TestIfAddToFavoritesThrowsErrorForInvalidUser(string username, string product1,
            string product2, string product3)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);
            var userProfileService = new UserProfileServices(context, TestUserManager<User>(), productService);

            var products = new List<Product>();

            products.Add(new Product
            {
                Id = product1,
                Price = 300,
                Title = "Test Product",
                IsDeleted = false,
                CreatedOn = DateTime.Now,

            });


            products.Add(new Product
            {
                Id = product2,
                Price = 300,
                Title = "Test Product1",
                IsDeleted = false,
                CreatedOn = DateTime.Now,

            });

            products.Add(new Product
            {
                Id = product3,
                Price = 300,
                Title = "Test Product2",
                IsDeleted = false,
                CreatedOn = DateTime.Now,

            });

            await context.Products.AddRangeAsync(products);

            await context.SaveChangesAsync();


            await Assert.ThrowsAsync<NullReferenceException>(async ()=>
            {
                await userProfileService.AddToFavoritesAsync(username, product1);
            });

            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await userProfileService.AddToFavoritesAsync(username, product2);
            });

            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await userProfileService.AddToFavoritesAsync(username, product3);
            });
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("TestUser", "testProductId")]
        [InlineData("TestUser1", "testProductId1")]
        [InlineData("TestUser2", "testProductId3")]
        public async Task TestWhenAddToFavoritesReturnsFalse(string username, string productId)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);
            var userProfileService = new UserProfileServices(context, TestUserManager<User>(), productService);

            Assert.False(await userProfileService.AddToFavoritesAsync(username, productId));
        }

        [Theory]
        [InlineData("UserOne")]
        [InlineData("UserTwo")]
        [InlineData("UserThree")]
        public async Task TestAddUserProfilePictureNotNull(string username)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);
            var userProfileService = new UserProfileServices(context, TestUserManager<User>(), productService);


            await context.Users.AddAsync(new User
            {

                UserName = username,
                Email = "email@email.bg",
                LastLoginDate = DateTime.UtcNow,

            });

            await context.SaveChangesAsync();

            await userProfileService.AddProfilePictureToUserAsync("randompictureurl", username);

            var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == username);


            Assert.NotNull(user.ProfilePicture);

        }

        [Theory]
        [InlineData("UserOne")]
        [InlineData("UserTwo")]
        [InlineData("UserThree")]
        public async Task TestAddUserProfilePictureThrowsWhenUserDoesNotExist(string username)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);
            var userProfileService = new UserProfileServices(context, TestUserManager<User>(), productService);


            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {

                await userProfileService.AddProfilePictureToUserAsync("randompictureurl", username);

            });
        }

        [Theory]
        [InlineData(null)]
        [InlineData("UserOne")]
        [InlineData("UserTwo")]
        [InlineData("UserThree")]
        public async Task TestIfEditUserProfileOrderInformationThrowsWhenUserInvalid(string username)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);
            var userProfileService = new UserProfileServices(context, TestUserManager<User>(), productService);

            var form = new EditDeliveryInformationForm();

            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await userProfileService.EditUserProfileOrderInformationAsync(username, form);
            });

        }

        [Theory]
        [InlineData(null)]
        [InlineData("UserOne")]
        [InlineData("UserTwo")]
        [InlineData("UserThree")]
        public async Task TestIfEditUserProfileOrderInformationWorksCorrectly(string username)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);
            var userProfileService = new UserProfileServices(context, TestUserManager<User>(), productService);

            var form = new EditDeliveryInformationForm();

            await context.Users.AddAsync(new User
            {

                UserName = username,
                Email = "email@email.bg",
                LastLoginDate = DateTime.UtcNow,

            });

            await context.SaveChangesAsync();

            var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == username);


            Assert.Null(user.FirstName);
            Assert.Null(user.LastName);
            Assert.Null(user.Address);
            Assert.Null(user.PhoneNumber);
            Assert.Null(user.City);

            form.LastName = "Stoqn";
            form.LastName = "Stoqnov";
            form.Address = "Stoqnovo 12";
            form.City = "Sofia";
            form.Phone = "0902232123";

            await userProfileService.EditUserProfileOrderInformationAsync(username, form);


            var changedInfoUser = await context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            Assert.Equal(changedInfoUser.FirstName, form.FirstName);
            Assert.Equal(changedInfoUser.LastName, form.LastName);
            Assert.Equal(changedInfoUser.Address, form.Address);
            Assert.Equal(changedInfoUser.City, form.City);
            Assert.Equal(changedInfoUser.PhoneNumber, form.Phone);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("UserOne")]
        [InlineData("UserTwo")]
        [InlineData("UserThree")]
        public async Task TestIfEditUserAccountSettingsThrowsError(string username)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);
            var userProfileService = new UserProfileServices(context, TestUserManager<User>(), productService);

            var accountSettingsForm = new EditAccountSettingsForm();

            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await userProfileService.EditUserAccountSettingsAsync(username, accountSettingsForm);
            });

        }

        [Theory]
        [InlineData(null)]
        [InlineData("UserOne")]
        [InlineData("UserTwo")]
        [InlineData("UserThree")]
        public async Task TestIfGetUserProfileInformationReturnsCorrectInformation(string username)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);
            var userProfileService = new UserProfileServices(context, TestUserManager<User>(), productService);

            await context.Users.AddAsync(new User
            {

                UserName = username,
                Email = "email@email.bg",
                LastLoginDate = DateTime.UtcNow,

            });

            await context.SaveChangesAsync();


            var accountSettingsForm = new EditAccountSettingsForm();

            var user = await userProfileService.GetUserProfileInformationAsync(username);

            Assert.Equal(username, user.UserName);

        }


        [Theory]
        [InlineData(null)]
        [InlineData("UserOne")]
        [InlineData("UserTwo")]
        [InlineData("UserThree")]
        public async Task TestIfGetUserProfileInformationReturnsNull(string username)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);
            var userProfileService = new UserProfileServices(context, TestUserManager<User>(), productService);

            var accountSettingsForm = new EditAccountSettingsForm();

            Assert.Null(await userProfileService.GetUserProfileInformationAsync(username));

        }

        [Theory]
        [InlineData(null)]
        [InlineData("UserOne")]
        [InlineData("UserTwo")]
        [InlineData("UserThree")]
        public async Task TestIfGetAllShipmentsThrowsError(string username)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);
            var userProfileService = new UserProfileServices(context, TestUserManager<User>(), productService);

            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await userProfileService.GetAllShipmentsAsync(username);
            });

        }

        [Theory]
        [InlineData(null)]
        [InlineData("UserOne")]
        [InlineData("UserTwo")]
        [InlineData("UserThree")]
        public async Task TestIfGetAllShipmentsReturnsCorrectResult(string username)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);
            var userProfileService = new UserProfileServices(context, TestUserManager<User>(), productService);
            var shopService = new ShopServices(context, productService);

            var productsInsideUserCart = new List<Product>();

            productsInsideUserCart.Add(new Product
            {
                Id = "TestProduct1",
                Price = 300,
                Title = "Test Product",
                IsDeleted = false,
                CreatedOn = DateTime.Now,

            });


            productsInsideUserCart.Add(new Product
            {
                Id = "TestProduct2",
                Price = 300,
                Title = "Test Product1",
                IsDeleted = false,
                CreatedOn = DateTime.Now,

            });

            productsInsideUserCart.Add(new Product
            {
                Id = "TestProduct3",
                Price = 300,
                Title = "Test Product2",
                IsDeleted = false,
                CreatedOn = DateTime.Now,

            });

            await context.Products.AddRangeAsync(productsInsideUserCart);


            await context.Users.AddAsync(new User
            {

                UserName = username,
                Email = "email@email.bg",
                LastLoginDate = DateTime.UtcNow,
                FirstName = "Sdasdsd",
                LastName = "Adsdfsdf",
                PhoneNumber = "231233423",
                Address = "dasdasdasdasdasd",
                City = "Sofia",

            });

            await context.SaveChangesAsync();

            await shopService.BuyProductAsync(username, "TestProduct1");
            await shopService.BuyProductAsync(username, "TestProduct2");
            await shopService.BuyProductAsync(username, "TestProduct3");


            var shippingCompany = ShippingCompany.Econt;

            Assert.True(await shopService.CheckoutSignedInUserAsync(username, shippingCompany));

            var result = await userProfileService.GetAllShipmentsAsync(username);

            Assert.Equal(3, result.Count());

        }

        public static UserManager<TUser> TestUserManager<TUser>(IUserStore<TUser> store = null) where TUser : class
        {
            store = store ?? new Mock<IUserStore<TUser>>().Object;
            var options = new Mock<IOptions<IdentityOptions>>();
            var idOptions = new IdentityOptions();
            idOptions.Lockout.AllowedForNewUsers = false;
            options.Setup(o => o.Value).Returns(idOptions);
            var userValidators = new List<IUserValidator<TUser>>();
            var validator = new Mock<IUserValidator<TUser>>();
            userValidators.Add(validator.Object);
            var pwdValidators = new List<PasswordValidator<TUser>>();
            pwdValidators.Add(new PasswordValidator<TUser>());
            var userManager = new UserManager<TUser>(store, options.Object, new PasswordHasher<TUser>(),
                userValidators, pwdValidators, new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(), null,
                new Mock<ILogger<UserManager<TUser>>>().Object);
            validator.Setup(v => v.ValidateAsync(userManager, It.IsAny<TUser>()))
                .Returns(Task.FromResult(IdentityResult.Success)).Verifiable();
            return userManager;
        }

    }
}
