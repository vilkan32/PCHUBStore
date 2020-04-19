using PCHUBStore.Data.Models;
using PCHUBStore.Services;
using PCHUBStore.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PCHUBStore.Filter.Models;
using PCHUBStore.View.Models;
using PCHUBStore.View.Models.FilterViewModels;
using Xunit;

namespace PCHUBStore.Tests.ProductServicesTests
{
    public class ProductServicesTest
    {
        [Theory]
        [InlineData("Laptops")]
        [InlineData("Computers")]
        [InlineData("Monitors")]
        public async Task TestIfGetFiltersReturnsEmptyCollection(string category)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);

            var result = await productService.GetFiltersAsync(category);

            Assert.Empty(result);

        }

        [Theory]
        [InlineData("Laptops")]
        [InlineData("Computers")]
        [InlineData("Monitors")]
        public async Task TestIfGetFiltersReturnsCorrectResult(string category)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);


            await context.FilterCategories.AddAsync(new FilterCategory
            {
                CategoryName = category,
                IsDeleted = false,
                ModificationDate = DateTime.Now,

            });

            await context.SaveChangesAsync();

            var result = await productService.GetFiltersAsync(category);

            Assert.NotEmpty(result);

        }

        [Fact]
        public async Task TestIfApplyFiltersFromUrlAppliesFilters()
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);

            var filterCategory = new List<FilterCategoryViewModel>();

            var filtersPrice = new List<FilterViewModel>();


            filtersPrice.Add(new FilterViewModel
            {
                Name = "MinPrice",

            });

            filtersPrice.Add(new FilterViewModel
            {
                Name = "MaxPrice",

            });

            filterCategory.Add(new FilterCategoryViewModel
            {

                CategoryName = "Laptops",
                ViewSubCategoryName = "Price",
                Filters = filtersPrice
            });

            var filtersMake = new List<FilterViewModel>();

            filtersMake.Add(new FilterViewModel
            {
                Name = "Make",
                Value = "Acer"

            });

            filterCategory.Add(new FilterCategoryViewModel
            {

                CategoryName = "Laptops",
                ViewSubCategoryName = "Make",
                Filters = filtersMake
            });

            var filtersOrderBy = new List<FilterViewModel>();

            filtersOrderBy.Add(new FilterViewModel
            {
                Name = "Order By",
                Value = "Default"

            });

            filterCategory.Add(new FilterCategoryViewModel
            {

                CategoryName = "Laptops",
                ViewSubCategoryName = "OrderBy",
                Filters = filtersOrderBy
            });

            var urlFilters = new ProductFiltersUrlModel();

            urlFilters.Page = 1;
            urlFilters.MinPrice = "300";
            urlFilters.MaxPrice = "600";
            urlFilters.Make = new string[]
            {
                "Acer",
            };

            urlFilters.OrderBy = "Descending";


            await productService.ApplyFiltersFromUrlAsync(filterCategory, urlFilters);


            var result =
                filterCategory.FirstOrDefault(x => x.CategoryName == "Laptops" && x.ViewSubCategoryName == "Price");

            Assert.True(result.Filters.Any(x => x.Name == "MinPrice" && x.Value == "300"));

            Assert.True(result.Filters.Any(x => x.Name == "MaxPrice" && x.Value == "600"));

            var makeResult =
                filterCategory.FirstOrDefault(x => x.CategoryName == "Laptops" && x.ViewSubCategoryName == "Make");

            Assert.NotNull(makeResult);

            Assert.True(makeResult.Filters.Any(x => x.IsChecked == true));
        }

        [Fact]
        public async Task TestIfApplyFiltersFromUrlAppliesFiltersWithNull()
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);

            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await productService.ApplyFiltersFromUrlAsync(null, null);
            });
        }

        [Theory]
        [InlineData("productId1", "ProductId2", "ProductId3")]
        [InlineData("productId2", "ProductId3", "ProductId4")]
        [InlineData("productId6", "ProductId30", "ProductId304")]

        public async Task TestIfProductExistsAsync(string productId1, string productId2, string productId3)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);

            var products = new List<Product>();

            products.Add(new Product
            {
                Id = productId1,
                Price = 300,
                Title = "Test Product",
                IsDeleted = false,
                CreatedOn = DateTime.Now,

            });


            products.Add(new Product
            {
                Id = productId2,
                Price = 300,
                Title = "Test Product1",
                IsDeleted = false,
                CreatedOn = DateTime.Now,

            });

            products.Add(new Product
            {
                Id = productId3,
                Price = 300,
                Title = "Test Product2",
                IsDeleted = false,
                CreatedOn = DateTime.Now,

            });

            await context.Products.AddRangeAsync(products);

            await context.SaveChangesAsync();

            Assert.True(await productService.ProductExistsAsync(productId1));
            Assert.True(await productService.ProductExistsAsync(productId2));
            Assert.True(await productService.ProductExistsAsync(productId3));
            Assert.False(await productService.ProductExistsAsync("NonExisting"));
        }



        [Theory]
        [InlineData("Title1", "Title2", "title3", "Title4")]
        [InlineData("Title12", "Title22", "title32", "Title42")]
        [InlineData("Title13", "Title23", "title33", "Title43")]
        [InlineData("Title13", "Title23", "title33", "Title43")]
        public async Task TestIfOrderByOrdersAsExpected(string title1, string title2, string title3, string title4)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);

            var model = new ProductsViewModel();

            var products = new List<ProductViewModel>();

            products.Add(new ProductViewModel
            {
                Price = 300,
                Title = title1,
                
            });

            products.Add(new ProductViewModel
            {
                Price = 400,
                Title = title2,

            });

            products.Add(new ProductViewModel
            {
                Price = 500,
                Title = title3,

            });

            products.Add(new ProductViewModel
            {
                Price = 600,
                Title = title4,

            });

            model.Products = products;


            await productService.OrderByAsync(ref model, "PriceAsc");

            Assert.True(model.Products.FirstOrDefault().Price == 300);


            await productService.OrderByAsync(ref model, "PriceDesc");

            Assert.True(model.Products.FirstOrDefault().Price == 600);
        }

        [Theory]
        [InlineData("NonExisting")]
        [InlineData("NonExisting1")]
        [InlineData("NonExisting2")]
        public async Task GetProductReturnsNull(string productId)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);

           Assert.Null(await productService.GetProductAsync(productId));
        }


        [Theory]
        [InlineData("Product1")]
        [InlineData("Product2")]
        [InlineData("Product3")]
        public async Task GetProductReturnsCorrectValue(string productId)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);

            await context.Products.AddAsync(new Product
            {
                Price = 300,
                Id = productId,
                
            });

            await context.SaveChangesAsync();
            Assert.NotNull(await productService.GetProductAsync(productId));
        }
        [Fact]
        public async Task TestIfGetSimilarProductsThrowsErrorForInvalidCategory()
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);

            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await productService.GetSimilarProductsAsync(200, null);
            });
        }

        [Theory]
        [InlineData("Product1", "ProductOne", "ProductTwo")]
        [InlineData("Product12", "ProductOne12", "ProductTwo12")]
        [InlineData("Product123", "ProductOne123", "ProductTwo123")]
        public async Task TestIfGetSimilarProductsReturnCorrectResults(string productId1, 
            string productId2, 
            string productId3)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);

            var category = new Category
            {
                Name = "Laptops"
            };

            await context.Products.AddAsync(new Product
            {
                Price = 300,
                Id = productId1,
                Category = category
            });


            await context.Products.AddAsync(new Product
            {
                Price = 351,
                Id = productId2,
                Category = category
            });

            await context.Products.AddAsync(new Product
            {
                Price = 401,
                Id = productId3,
                Category = category

            });

            await context.SaveChangesAsync();

            var result = await productService.GetSimilarProductsAsync(350, "Laptops");

            Assert.Equal(3, result.Count());
        }

        [Theory]
        [InlineData("Laptops")]
        [InlineData("Computers")]
        [InlineData("Monitors")]
        public async Task TestIfCategoryExistsReturnsTrue(string category)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);


            await context.Categories.AddAsync(new Category
            {
                Name = category
            });

            await context.SaveChangesAsync();

            Assert.True(await productService.CategoryExistsAsync(category));
        }

        [Fact]
        public async Task TestIfCategoryExistsThrowsException()
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);


            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await productService.CategoryExistsAsync(null);
            });
        }


        [Fact]
        public async Task TestIfCategoryExistsReturnsFalse()
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);


           Assert.False(await productService.CategoryExistsAsync("NonExisting"));
        }

        [Theory]
        [InlineData("Product1", "ProductOne", "ProductTwo")]
        [InlineData("Product12", "ProductOne12", "ProductTwo12")]
        [InlineData("Product123", "ProductOne123", "ProductTwo123")]
        public async Task TestIfSearchForResultsWorksCorrectly(string productId1,
            string productId2,
            string productId3)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);

            var category = new Category
            {
                Name = "Laptops"
            };


            await context.Products.AddAsync(new Product
            {
                Price = 300,
                Id = productId1,
                Category = category,
                Title = productId1
                
            });


            await context.Products.AddAsync(new Product
            {
                Price = 351,
                Id = productId2,
                Category = category,
                Title = productId2
            });

            await context.Products.AddAsync(new Product
            {
                Price = 401,
                Id = productId3,
                Category = category,
                Title = productId3

            });

            await context.SaveChangesAsync();

            var searchResultOne = await productService.SearchForResultsAsync(productId1, "300", "400");

            Assert.NotEmpty(searchResultOne);

            Assert.Contains(searchResultOne, x => x.Title == productId1);

            var searchResultTwo = await productService.SearchForResultsAsync(productId2, "300", "400");

            Assert.NotEmpty(searchResultTwo);

            Assert.Contains(searchResultTwo, x => x.Title == productId2);
        }
        [Fact]
        public async Task TestIfSearchForResultsThrowsException()
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);

            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await productService.SearchForResultsAsync(null, null, null);
            });
            
        }

        [Fact]
        public async Task TestIfGetProductThrowsExceptionWhenCategoryNotExisting()
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);

            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await productService.GetProductAsync(null, null, false, null);
            });

        }

        [Theory]
        [InlineData("Laptops", "Product12", "ProductOne12", "ProductTwo12")]
        [InlineData("Computers", "Product1", "ProductOne", "ProductTwo")]
        [InlineData("Mice", "Product123", "ProductOne123", "ProductTwo123")]
        public async Task TestIfGetProductWorksProperlyWhenAuthenticated(string categoryName, string productId1,
            string productId2,
            string productId3)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);

            var category = new Category
            {
                Name = categoryName
            };


            await context.Products.AddAsync(new Product
            {
                Price = 300,
                Id = productId1,
                Category = category,
                Title = productId1

            });


            await context.Products.AddAsync(new Product
            {
                Price = 351,
                Id = productId2,
                Category = category,
                Title = productId2
            });

            await context.Products.AddAsync(new Product
            {
                Price = 401,
                Id = productId3,
                Category = category,
                Title = productId3

            });

            await context.Users.AddAsync(new User
            {
                Id = "userId",
                UserName = "Username",
                FirstName = "FirstName",

            });

            await context.SaveChangesAsync();
            

            var result = await productService.GetProductAsync(productId1, "Username", 
                true, categoryName);

            Assert.Equal(1,result.Views);

        }

        [Theory]
        [InlineData("Laptops", "Product12", "ProductOne12", "ProductTwo12")]
        [InlineData("Computers", "Product1", "ProductOne", "ProductTwo")]
        [InlineData("Mice", "Product123", "ProductOne123", "ProductTwo123")]
        public async Task TestIfGetProductWorksProperlyWhenNotAuthenticated(string categoryName, string productId1,
            string productId2,
            string productId3)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);

            var category = new Category
            {
                Name = categoryName
            };


            await context.Products.AddAsync(new Product
            {
                Price = 300,
                Id = productId1,
                Category = category,
                Title = productId1

            });


            await context.Products.AddAsync(new Product
            {
                Price = 351,
                Id = productId2,
                Category = category,
                Title = productId2
            });

            await context.Products.AddAsync(new Product
            {
                Price = 401,
                Id = productId3,
                Category = category,
                Title = productId3

            });

            await context.SaveChangesAsync();


            var result = await productService.GetProductAsync(productId1, "Username",
                false, categoryName);

            Assert.Equal(1, result.Views);

        }


    }
}
