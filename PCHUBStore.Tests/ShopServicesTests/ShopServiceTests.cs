using Microsoft.EntityFrameworkCore;
using PCHUBStore.Data.Models;
using PCHUBStore.Services;
using PCHUBStore.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PCHUBStore.Data.Models.Enums;
using PCHUBStore.View.Models.ShoppingCartViewModels;
using Xunit;

namespace PCHUBStore.Tests.ShopServicesTests
{
    public class ShopServiceTests
    {

        [Fact]
        public async Task TestIfUserShoppingCartHasChanged()
        {
            // Arrange
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);
            var shopService = new ShopServices(context, productService);

            var productsInsideUserCart = new List<Product>();

            productsInsideUserCart.Add(new Product
            {
                Id = "Product",
                Price = 300,
                Title = "Test Product",
                IsDeleted = false,
                CreatedOn = DateTime.Now,

            });


            productsInsideUserCart.Add(new Product
            {
                Id = "Product1",
                Price = 300,
                Title = "Test Product1",
                IsDeleted = false,
                CreatedOn = DateTime.Now,

            });

            productsInsideUserCart.Add(new Product
            {
                Id = "Product2",
                Price = 300,
                Title = "Test Product2",
                IsDeleted = false,
                CreatedOn = DateTime.Now,

            });

            await context.Products.AddRangeAsync(productsInsideUserCart);
            await context.Users.AddAsync(new User
            {

                UserName = "Test1",
                Email = "email@email.bg",
                LastLoginDate = DateTime.UtcNow,

            });

            await context.SaveChangesAsync();

            //Act
            await shopService.BuyProductAsync("Test1", "Product2");

            //Assert

            var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == "Test1");

            var cart = user.Cart;

            Assert.True(cart.ProductCarts.Count == 1);

            await shopService.BuyProductAsync("Test1", "Product");

            var user1 = await context.Users.FirstOrDefaultAsync(x => x.UserName == "Test1");

            var cart1 = user.Cart;

            Assert.True(cart1.ProductCarts.Count == 2);

            await shopService.BuyProductAsync("Test1", "Product1");

            var user2 = await context.Users.FirstOrDefaultAsync(x => x.UserName == "Test1");

            var cart2 = user.Cart;

            Assert.True(cart2.ProductCarts.Count == 3);

            Assert.True(cart2.ProductCarts.Any(x => x.Product.Id.Contains("Product")));
        }

        [Fact]
        public async Task TestIfWhenInvalidUserOrCart()
        {
            // Arrange
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);
            var shopService = new ShopServices(context, productService);

            var productsInsideUserCart = new List<Product>();

            productsInsideUserCart.Add(new Product
            {
                Id = "Product",
                Price = 300,
                Title = "Test Product",
                IsDeleted = false,
                CreatedOn = DateTime.Now,

            });


            productsInsideUserCart.Add(new Product
            {
                Id = "Product1",
                Price = 300,
                Title = "Test Product1",
                IsDeleted = false,
                CreatedOn = DateTime.Now,

            });

            productsInsideUserCart.Add(new Product
            {
                Id = "Product2",
                Price = 300,
                Title = "Test Product2",
                IsDeleted = false,
                CreatedOn = DateTime.Now,

            });

            await context.Products.AddRangeAsync(productsInsideUserCart);
            await context.SaveChangesAsync();
            await shopService.BuyProductAsync(null, null);

            await shopService.BuyProductAsync("asdasd", null);

            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await shopService.BuyProductAsync("asdasd", "Product1");
            });
        }

        [Theory]
        [InlineData("TestUser1")]
        [InlineData("TestUser2")]
        [InlineData("TestUser3")]
        public async Task TestIfReturnsSpecificProductsForUserCart(string username)
        {
            //Arrange
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);
            var shopService = new ShopServices(context, productService);

            var productsInsideUserCart = new List<Product>();

            productsInsideUserCart.Add(new Product
            {
                Id = "Product",
                Price = 300,
                Title = "Test Product",
                IsDeleted = false,
                CreatedOn = DateTime.Now,

            });


            productsInsideUserCart.Add(new Product
            {
                Id = "Product1",
                Price = 300,
                Title = "Test Product1",
                IsDeleted = false,
                CreatedOn = DateTime.Now,

            });

            productsInsideUserCart.Add(new Product
            {
                Id = "Product2",
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

            });

            await context.SaveChangesAsync();

            await shopService.BuyProductAsync(username, "Product2");
            await shopService.BuyProductAsync(username, "Product");
            await shopService.BuyProductAsync(username, "Product1");

            var result = await shopService.GetAllCartProductsAsync(username);

            Assert.Equal(3, result.Count);
            Assert.True(result.Any(x => x.Product.Title.Contains("Test Product")));
        }

        [Theory]
        [InlineData("TestUser1")]
        [InlineData("TestUser2")]
        [InlineData("TestUser3")]
        [InlineData(null)]
        public async Task TestIfThrowsErrorForInvalidUser(string username)
        {
            //Arrange
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);
            var shopService = new ShopServices(context, productService);

            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await shopService.GetAllCartProductsAsync(username);
            });
        }

        [Theory]
        [InlineData("TestUser1", 2)]
        [InlineData("TestUser2", 3)]
        [InlineData("TestUser3", 3)]
        [InlineData("TestUser4", 0)]
        public async Task TestIfProductQuantityIncreases(string username, int quantity)
        {
            //Arrange
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);
            var shopService = new ShopServices(context, productService);

            var productsInsideUserCart = new List<Product>();

            productsInsideUserCart.Add(new Product
            {
                Id = "Product",
                Price = 300,
                Title = "Test Product",
                IsDeleted = false,
                CreatedOn = DateTime.Now,

            });


            productsInsideUserCart.Add(new Product
            {
                Id = "Product1",
                Price = 300,
                Title = "Test Product1",
                IsDeleted = false,
                CreatedOn = DateTime.Now,

            });

            productsInsideUserCart.Add(new Product
            {
                Id = "Product2",
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

            });

            await context.SaveChangesAsync();

            await shopService.BuyProductAsync(username, "Product2");
            await shopService.BuyProductAsync(username, "Product");
            await shopService.BuyProductAsync(username, "Product1");

            await shopService.IncreaseProductQuantityAsync(username, "Product1", quantity);

            var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == username);
            var productCart = user.Cart.ProductCarts.FirstOrDefault(x => x.Product.Id == "Product1");

            Assert.Equal(productCart.Quantity, quantity);
        }

        [Fact]
        public async Task TestIfProductQuantityIncreasesThrowsErrorUserNull()
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);
            var shopService = new ShopServices(context, productService);

            var productsInsideUserCart = new List<Product>();

            productsInsideUserCart.Add(new Product
            {
                Id = "Product",
                Price = 300,
                Title = "Test Product",
                IsDeleted = false,
                CreatedOn = DateTime.Now,

            });


            productsInsideUserCart.Add(new Product
            {
                Id = "Product1",
                Price = 300,
                Title = "Test Product1",
                IsDeleted = false,
                CreatedOn = DateTime.Now,

            });

            productsInsideUserCart.Add(new Product
            {
                Id = "Product2",
                Price = 300,
                Title = "Test Product2",
                IsDeleted = false,
                CreatedOn = DateTime.Now,

            });

            await context.Products.AddRangeAsync(productsInsideUserCart);

            await context.SaveChangesAsync();

            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await shopService.IncreaseProductQuantityAsync(null, "Product1", 3);
            });
        }

        [Theory]
        [InlineData("TestUser1")]
        [InlineData("TestUser2")]
        [InlineData("TestUser3")]
        [InlineData("TestUser4")]
        public async Task TestIfIsCartEmptyOrNonExistingReturnsFalse(string username)
        {
            //Arrange
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);
            var shopService = new ShopServices(context, productService);

            var productsInsideUserCart = new List<Product>();

            productsInsideUserCart.Add(new Product
            {
                Id = "Product",
                Price = 300,
                Title = "Test Product",
                IsDeleted = false,
                CreatedOn = DateTime.Now,

            });


            productsInsideUserCart.Add(new Product
            {
                Id = "Product1",
                Price = 300,
                Title = "Test Product1",
                IsDeleted = false,
                CreatedOn = DateTime.Now,

            });

            productsInsideUserCart.Add(new Product
            {
                Id = "Product2",
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

            });

            await context.SaveChangesAsync();

            await shopService.BuyProductAsync(username, "Product2");
            await shopService.BuyProductAsync(username, "Product");
            await shopService.BuyProductAsync(username, "Product1");

            Assert.False(await shopService.IsCartEmptyOrNonExistingAsync(username));
        }

        [Theory]
        [InlineData("TestUser1")]
        [InlineData("TestUser2")]
        [InlineData("TestUser3")]
        [InlineData("TestUser4")]
        public async Task TestIfIsCartEmptyOrNonExistingReturnsTrue(string username)
        {
            //Arrange
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);
            var shopService = new ShopServices(context, productService);

            await context.Users.AddAsync(new User
            {

                UserName = username,
                Email = "email@email.bg",
                LastLoginDate = DateTime.UtcNow,

            });

            await context.SaveChangesAsync();

            Assert.True(await shopService.IsCartEmptyOrNonExistingAsync(username));
        }

        [Theory]
        [InlineData("TestUser1")]
        [InlineData("TestUser2")]
        [InlineData("TestUser3")]
        [InlineData("TestUser4")]
        public async Task TestIfReturnsCorrectNumberOfProducts(string username)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);
            var shopService = new ShopServices(context, productService);


            var productsInsideUserCart = new List<Product>();

            productsInsideUserCart.Add(new Product
            {
                Id = "Product",
                Price = 300,
                Title = "Test Product",
                IsDeleted = false,
                CreatedOn = DateTime.Now,

            });


            productsInsideUserCart.Add(new Product
            {
                Id = "Product1",
                Price = 300,
                Title = "Test Product1",
                IsDeleted = false,
                CreatedOn = DateTime.Now,

            });

            productsInsideUserCart.Add(new Product
            {
                Id = "Product2",
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

            });

            await context.SaveChangesAsync();

            await shopService.BuyProductAsync(username, "Product2");
            await shopService.BuyProductAsync(username, "Product");
            await shopService.BuyProductAsync(username, "Product1");


            var numberOfProducts = await shopService.GetNumberOfProductsAsync(username);

            Assert.Equal(3, numberOfProducts);
        }

        [Fact]
        public async Task TestIfReturnsZero()
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);
            var shopService = new ShopServices(context, productService);

            await context.Users.AddAsync(new User
            {

                UserName = "Test",
                Email = "email@email.bg",
                LastLoginDate = DateTime.UtcNow,

            });

            await context.SaveChangesAsync();

            var result = await shopService.GetNumberOfProductsAsync("Test");

            Assert.Equal(0, result);
        }

        [Theory]
        [InlineData("Test", "Product1", "Product2", "Product3")]
        [InlineData("Test1", "Product10", "Product20", "Product30")]
        [InlineData("Test2", "Product100", "Product200", "Product300")]
        public async Task TestIfRemoveProductFromCartAsyncRemovesProductsFromCart(string username, string product1,
            string product2, string product3)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);
            var shopService = new ShopServices(context, productService);


            var productsInsideUserCart = new List<Product>();

            productsInsideUserCart.Add(new Product
            {
                Id = product1,
                Price = 300,
                Title = "Test Product",
                IsDeleted = false,
                CreatedOn = DateTime.Now,

            });


            productsInsideUserCart.Add(new Product
            {
                Id = product2,
                Price = 300,
                Title = "Test Product1",
                IsDeleted = false,
                CreatedOn = DateTime.Now,

            });

            productsInsideUserCart.Add(new Product
            {
                Id = product3,
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

            });

            await context.SaveChangesAsync();

            await shopService.BuyProductAsync(username, product1);
            await shopService.BuyProductAsync(username, product2);
            await shopService.BuyProductAsync(username, product3);

            await shopService.RemoveProductFromCartAsync(username, product1);

            var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            var cartProductsCount = user.Cart.ProductCarts.Count();

            Assert.Equal(2, cartProductsCount);

        }

        [Theory]
        [InlineData("Test", "Product1", "Product2", "Product3")]
        [InlineData("Test1", "Product10", "Product20", "Product30")]
        [InlineData("Test2", "Product100", "Product200", "Product300")]
        public async Task TestIfRemoveProductFromCartThrowsException(string username, string product1,
            string product2, string product3)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);
            var shopService = new ShopServices(context, productService);


            var productsInsideUserCart = new List<Product>();

            productsInsideUserCart.Add(new Product
            {
                Id = product1,
                Price = 300,
                Title = "Test Product",
                IsDeleted = false,
                CreatedOn = DateTime.Now,

            });


            productsInsideUserCart.Add(new Product
            {
                Id = product2,
                Price = 300,
                Title = "Test Product1",
                IsDeleted = false,
                CreatedOn = DateTime.Now,

            });

            productsInsideUserCart.Add(new Product
            {
                Id = product3,
                Price = 300,
                Title = "Test Product2",
                IsDeleted = false,
                CreatedOn = DateTime.Now,

            });

            await context.Products.AddRangeAsync(productsInsideUserCart);

            await context.SaveChangesAsync();

            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await shopService.RemoveProductFromCartAsync(username, product2);
            });

        }

        [Theory]
        [InlineData("Test", "Product1", "Product2", "Product3")]
        [InlineData("Test1", "Product10", "Product20", "Product30")]
        [InlineData("Test2", "Product100", "Product200", "Product300")]
        public async Task TestIfCheckoutSignedInUserDetailsNotPresentReturnsFalse(string username, string product1,
            string product2, string product3)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);
            var shopService = new ShopServices(context, productService);


            var productsInsideUserCart = new List<Product>();

            productsInsideUserCart.Add(new Product
            {
                Id = product1,
                Price = 300,
                Title = "Test Product",
                IsDeleted = false,
                CreatedOn = DateTime.Now,

            });


            productsInsideUserCart.Add(new Product
            {
                Id = product2,
                Price = 300,
                Title = "Test Product1",
                IsDeleted = false,
                CreatedOn = DateTime.Now,

            });

            productsInsideUserCart.Add(new Product
            {
                Id = product3,
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

            });

            await context.SaveChangesAsync();

            await shopService.BuyProductAsync(username, product1);
            await shopService.BuyProductAsync(username, product2);
            await shopService.BuyProductAsync(username, product3);

            var shippingCompany = ShippingCompany.Econt;

            Assert.False(await shopService.CheckoutSignedInUserAsync(username, shippingCompany));

        }

        [Fact]
        public async Task TestIfCheckoutSignedInUserThrowsWhenInvalidUser()
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);
            var shopService = new ShopServices(context, productService);
            var shippingCompany = ShippingCompany.Econt;

            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await shopService.CheckoutSignedInUserAsync(null, shippingCompany);
            });
        }


        [Theory]
        [InlineData("Test", "Product1", "Product2", "Product3")]
        [InlineData("Test1", "Product10", "Product20", "Product30")]
        [InlineData("Test2", "Product100", "Product200", "Product300")]
        public async Task TestIfCheckoutSignedInUserReturnsTrue(string username, string product1,
            string product2, string product3)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);
            var shopService = new ShopServices(context, productService);


            var productsInsideUserCart = new List<Product>();

            productsInsideUserCart.Add(new Product
            {
                Id = product1,
                Price = 300,
                Title = "Test Product",
                IsDeleted = false,
                CreatedOn = DateTime.Now,

            });


            productsInsideUserCart.Add(new Product
            {
                Id = product2,
                Price = 300,
                Title = "Test Product1",
                IsDeleted = false,
                CreatedOn = DateTime.Now,

            });

            productsInsideUserCart.Add(new Product
            {
                Id = product3,
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

            await shopService.BuyProductAsync(username, product1);
            await shopService.BuyProductAsync(username, product2);
            await shopService.BuyProductAsync(username, product3);

            var shippingCompany = ShippingCompany.Econt;

            Assert.True(await shopService.CheckoutSignedInUserAsync(username, shippingCompany));
        }



        [Fact]
        public async Task CheckoutAnonumousUserWorksAsExected()
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);
            var shopService = new ShopServices(context, productService);
            var products = new List<PurchaseProductsAnonymousViewModel>();
            products.Add(new PurchaseProductsAnonymousViewModel
            {
                Id = "randomId",
                Quantity = 1,
                Title = "Laptop Acer 31",
                Price = 2000,

            });
            var anonymousCartViewModel = new AnonymousCartViewModel
            {
                FirstName = "Sensei",
                LastName = "Senseev",
                Address = "SenseiTv never purchase dot com",
                City = "Senseevo",
                ShippingCompany = View.Models.ShoppingCartViewModels.Enums.ShippingCompany.Econt,
                Email = "sensei@senssee.vf",
                PhoneNumber = "023231233",
                Products = products,
            };

            await shopService.ChechoutAnonymousAsync(anonymousCartViewModel);

            var user = await context.Users.FirstOrDefaultAsync(x => x.FirstName == "Sensei");
            Assert.NotNull(user);

            var userShipmentsExist = user.Shipments.Any();

            Assert.True(userShipmentsExist);
        }
        [Theory]
        [InlineData(null)]
        [InlineData("UserOne")]
        [InlineData("UserTwo")]
        public async Task TestIfGetLastCheckoutDetailsAsyncThrowsErrorForInvalidUser(string username)
        {

            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);
            var shopService = new ShopServices(context, productService);

            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await shopService.GetLastCheckoutDetailsAsync(username);
            });
        }
    }
}
