using PCHUBStore.Data.Models;
using PCHUBStore.Services;
using PCHUBStore.Tests.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet;
using Constants;
using Newtonsoft.Json;
using PCHUBStore.Areas.Administration.Models.ProductViewModel;
using PCHUBStore.Filter.Models;
using PCHUBStore.Tests.AdminServicesTests.JSONModel;
using PCHUBStore.View.Models;
using PCHUBStore.View.Models.FilterViewModels;
using Xunit;
using String = System.String;

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
        [InlineData("Title1", "Title2", "title3", "Title4")]
        [InlineData("Title12", "Title22", "title32", "Title42")]
        [InlineData("Title13", "Title23", "title33", "Title43")]
        [InlineData("Title13", "Title23", "title33", "Title43")]
        public async Task TestIfOrderByDefaultOrdersAsExpected(string title1, string title2, string title3, string title4)
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


            await productService.OrderByAsync(ref model, "Default");

            Assert.True(model.Products.FirstOrDefault().Price == 300);

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


        [Theory]
        [InlineData("Laptops")]
        public async Task TestIfQueryProductsReturnsCorrectResults(string category)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);

            Account cloudinaryCredentials = new Account(
                CloudinaryAccountTests.CloudName,
                CloudinaryAccountTests.ApiKey,
                CloudinaryAccountTests.ApiSecret);

            var cloudinaryUtility = new Cloudinary(cloudinaryCredentials);
            var cloudinary = new CloudinaryServices(cloudinaryUtility);

            var adminProductServices = new Areas.Administration.Services.AdminProductsServices(context, cloudinary);

            var model = new InsertJsonProductViewModel();

            var laptopsJson = await
                File.ReadAllTextAsync(
                    @"C:\Users\velis\source\repos\PCHUBStore\PCHUBStore.Tests\AdminServicesTests\JSONLaptops\laptops.json");

            var laptops = JsonConvert.DeserializeObject<List<JsonProductModel>>(laptopsJson);

            await adminProductServices.CreateCategoryAsync(category);

            foreach (var laptop in laptops)
            {
                var jsonBasic = JsonConvert.SerializeObject(laptop.BasicChars);

                var jsonAdvanced = JsonConvert.SerializeObject(laptop.AdvancedChars);
                model.Category = category;
                model.BasicCharacteristics = jsonBasic;
                model.FullCharacteristics = jsonAdvanced;

                await adminProductServices.CreateLaptopFromJSONAsync(model);
            }

            var filters = new ProductFiltersUrlModel();
            var result = await productService.QueryProductsAsync(filters, category);

            Assert.Equal(2, result.Count());

            Assert.Contains(result, x => x.Title.Contains("Acer"));

            filters.Make = new string[]
            {
                "Acer",
            };

            filters.MinPrice = "700";
            filters.MaxPrice = "750";

            var secondResultTest = await productService.QueryProductsAsync(filters, category);

            Assert.Equal(1, secondResultTest.Count());

            Assert.Contains(secondResultTest, x => x.ArticleNumber.Contains("83366"));


            filters.Make = new string[]
            {
                "Dell",
            };
            filters.MinPrice = "800";
            filters.MaxPrice = "900";

            var thirdResultTest = await productService.QueryProductsAsync(filters, category);

            Assert.Empty(thirdResultTest);

        }

        [Theory]
        [InlineData("Laptops")]
        public async Task TestIfQueryLaptopsReturnsCorrectResults(string category)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);

            Account cloudinaryCredentials = new Account(
                CloudinaryAccountTests.CloudName,
                CloudinaryAccountTests.ApiKey,
                CloudinaryAccountTests.ApiSecret);

            var cloudinaryUtility = new Cloudinary(cloudinaryCredentials);
            var cloudinary = new CloudinaryServices(cloudinaryUtility);

            var adminProductServices = new Areas.Administration.Services.AdminProductsServices(context, cloudinary);

            var model = new InsertJsonProductViewModel();

            var laptopsJson = await
                File.ReadAllTextAsync(
                    @"C:\Users\velis\source\repos\PCHUBStore\PCHUBStore.Tests\AdminServicesTests\JSONLaptops\laptops.json");

            var laptops = JsonConvert.DeserializeObject<List<JsonProductModel>>(laptopsJson);

            await adminProductServices.CreateCategoryAsync(category);

            foreach (var laptop in laptops)
            {
                var jsonBasic = JsonConvert.SerializeObject(laptop.BasicChars);

                var jsonAdvanced = JsonConvert.SerializeObject(laptop.AdvancedChars);
                model.Category = category;
                model.BasicCharacteristics = jsonBasic;
                model.FullCharacteristics = jsonAdvanced;

                await adminProductServices.CreateLaptopFromJSONAsync(model);
            }

            var filters = new ProductFiltersUrlModel();

            if (filters.Make == null)
            {
                filters.Make = new string[] { "All" };
            }
            if (filters.Model == null)
            {
                filters.Model = new string[] { "All" };
            }
            if (filters.OrderBy == null)
            {
                filters.OrderBy = "Default";
            }
            if (filters.Processor == null)
            {
                filters.Processor = new string[] { "All" };
            }
            if (filters.VideoCard == null)
            {
                filters.VideoCard = new string[] { "All" };
            }
            if (filters.OS == null)
            {
                filters.OS = new string[] { "All" };
            }
            if (filters.RAM == null)
            {
                filters.RAM = new string[] { "All" };
            }
            if (filters.Resolution == null)
            {
                filters.Resolution = new string[] { "All" };
            }
            if (filters.FPS == null)
            {
                filters.FPS = new string[] { "All" };
            }
            if (filters.ReactionTime == null)
            {
                filters.ReactionTime = new string[] { "All" };
            }
            if (filters.MatrixType == null)
            {
                filters.MatrixType = new string[] { "All" };
            }
            if (filters.DisplaySize == null)
            {
                filters.DisplaySize = new string[] { "All" };
            }
            if (filters.Gaming == null)
            {
                filters.Gaming = new string[] { "All" };
            }
            if (filters.Interface == null)
            {
                filters.Interface = new string[] { "All" };
            }
            if (filters.Connectivity == null)
            {
                filters.Connectivity = new string[] { "All" };
            }
            if (filters.Type == null)
            {
                filters.Type = new string[] { "All" };
            }
            if (filters.Mechanical == null)
            {
                filters.Mechanical = new string[] { "All" };
            }

            var result = await productService.QueryLaptopsAsync(filters, 400, 1000);

           

            Assert.Equal(2, result.Count());

            Assert.Contains(result, x => x.Title.Contains("Acer"));

            filters.Make = new string[]
            {
                "Acer",
            };

            filters.MinPrice = "700";
            filters.MaxPrice = "750";

            var secondResultTest = await productService.QueryLaptopsAsync(filters, 700, 750);

            Assert.Equal(1, secondResultTest.Count());

            Assert.Contains(secondResultTest, x => x.ArticleNumber.Contains("83366"));


            filters.Make = new string[]
            {
                "Dell",
            };
            filters.MinPrice = "800";
            filters.MaxPrice = "900";

            var thirdResultTest = await productService.QueryLaptopsAsync(filters, 800,900);

            Assert.Empty(thirdResultTest);

        }

        [Theory]
        [InlineData("Monitors")]
        public async Task TestIfQueryMonitorsReturnsCorrectResults(string category)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);

            Account cloudinaryCredentials = new Account(
                CloudinaryAccountTests.CloudName,
                CloudinaryAccountTests.ApiKey,
                CloudinaryAccountTests.ApiSecret);

            var cloudinaryUtility = new Cloudinary(cloudinaryCredentials);
            var cloudinary = new CloudinaryServices(cloudinaryUtility);

            var adminProductServices = new Areas.Administration.Services.AdminProductsServices(context, cloudinary);

            var model = new InsertJsonProductViewModel();

            var monitorsJson = await
                File.ReadAllTextAsync(
                    @"C:\Users\velis\source\repos\PCHUBStore\PCHUBStore.Tests\AdminServicesTests\JSONMonitors\monitors.json");

            var monitors = JsonConvert.DeserializeObject<List<JsonProductModel>>(monitorsJson);

            await adminProductServices.CreateCategoryAsync(category);

            foreach (var monitor in monitors)
            {
                var jsonBasic = JsonConvert.SerializeObject(monitor.BasicChars);

                var jsonAdvanced = JsonConvert.SerializeObject(monitor.AdvancedChars);
                model.Category = category;
                model.BasicCharacteristics = jsonBasic;
                model.FullCharacteristics = jsonAdvanced;

                await adminProductServices.CreateMonitorFromJSONAsync(model);
            }

            var filters = new ProductFiltersUrlModel();

            if (filters.Make == null)
            {
                filters.Make = new string[] { "All" };
            }
            if (filters.Model == null)
            {
                filters.Model = new string[] { "All" };
            }
            if (filters.OrderBy == null)
            {
                filters.OrderBy = "Default";
            }
            if (filters.Processor == null)
            {
                filters.Processor = new string[] { "All" };
            }
            if (filters.VideoCard == null)
            {
                filters.VideoCard = new string[] { "All" };
            }
            if (filters.OS == null)
            {
                filters.OS = new string[] { "All" };
            }
            if (filters.RAM == null)
            {
                filters.RAM = new string[] { "All" };
            }
            if (filters.Resolution == null)
            {
                filters.Resolution = new string[] { "All" };
            }
            if (filters.FPS == null)
            {
                filters.FPS = new string[] { "All" };
            }
            if (filters.ReactionTime == null)
            {
                filters.ReactionTime = new string[] { "All" };
            }
            if (filters.MatrixType == null)
            {
                filters.MatrixType = new string[] { "All" };
            }
            if (filters.DisplaySize == null)
            {
                filters.DisplaySize = new string[] { "All" };
            }
            if (filters.Gaming == null)
            {
                filters.Gaming = new string[] { "All" };
            }
            if (filters.Interface == null)
            {
                filters.Interface = new string[] { "All" };
            }
            if (filters.Connectivity == null)
            {
                filters.Connectivity = new string[] { "All" };
            }
            if (filters.Type == null)
            {
                filters.Type = new string[] { "All" };
            }
            if (filters.Mechanical == null)
            {
                filters.Mechanical = new string[] { "All" };
            }

            var result = await productService.QueryMonitorsAsync(filters, 400, 1000);


            Assert.Equal(4, result.Count());

            Assert.Contains(result, x => x.Make.Contains("BenQ"));

            filters.Make = new string[]
            {
                "BenQ",
            };


            var secondResultTest = await productService.QueryMonitorsAsync(filters, 700, 800);

            Assert.Equal(1, secondResultTest.Count());

            Assert.Contains(secondResultTest, x => x.ArticleNumber.Contains("97773"));


            filters.Make = new string[]
            {
                "Dell",
            };
            filters.MinPrice = "800";
            filters.MaxPrice = "900";

            var thirdResultTest = await productService.QueryMonitorsAsync(filters, 800, 900);

            Assert.Empty(thirdResultTest);

        }


        [Theory]
        [InlineData("Computers")]
        public async Task TestIfQueryComputersReturnsCorrectResults(string category)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);

            Account cloudinaryCredentials = new Account(
                CloudinaryAccountTests.CloudName,
                CloudinaryAccountTests.ApiKey,
                CloudinaryAccountTests.ApiSecret);

            var cloudinaryUtility = new Cloudinary(cloudinaryCredentials);
            var cloudinary = new CloudinaryServices(cloudinaryUtility);

            var adminProductServices = new Areas.Administration.Services.AdminProductsServices(context, cloudinary);

            var model = new InsertJsonProductViewModel();

            var computersJson = await
                File.ReadAllTextAsync(
                    @"C:\Users\velis\source\repos\PCHUBStore\PCHUBStore.Tests\AdminServicesTests\JSONComputers\computers.json");

            var computers = JsonConvert.DeserializeObject<List<JsonProductModel>>(computersJson);

            await adminProductServices.CreateCategoryAsync(category);

            foreach (var computer in computers)
            {
                var jsonBasic = JsonConvert.SerializeObject(computer.BasicChars);

                var jsonAdvanced = JsonConvert.SerializeObject(computer.AdvancedChars);
                model.Category = category;
                model.BasicCharacteristics = jsonBasic;
                model.FullCharacteristics = jsonAdvanced;

                await adminProductServices.CreateComputerFromJSONAsync(model);
            }

            var filters = new ProductFiltersUrlModel();

            if (filters.Make == null)
            {
                filters.Make = new string[] { "All" };
            }
            if (filters.Model == null)
            {
                filters.Model = new string[] { "All" };
            }
            if (filters.OrderBy == null)
            {
                filters.OrderBy = "Default";
            }
            if (filters.Processor == null)
            {
                filters.Processor = new string[] { "All" };
            }
            if (filters.VideoCard == null)
            {
                filters.VideoCard = new string[] { "All" };
            }
            if (filters.OS == null)
            {
                filters.OS = new string[] { "All" };
            }
            if (filters.RAM == null)
            {
                filters.RAM = new string[] { "All" };
            }
            if (filters.Resolution == null)
            {
                filters.Resolution = new string[] { "All" };
            }
            if (filters.FPS == null)
            {
                filters.FPS = new string[] { "All" };
            }
            if (filters.ReactionTime == null)
            {
                filters.ReactionTime = new string[] { "All" };
            }
            if (filters.MatrixType == null)
            {
                filters.MatrixType = new string[] { "All" };
            }
            if (filters.DisplaySize == null)
            {
                filters.DisplaySize = new string[] { "All" };
            }
            if (filters.Gaming == null)
            {
                filters.Gaming = new string[] { "All" };
            }
            if (filters.Interface == null)
            {
                filters.Interface = new string[] { "All" };
            }
            if (filters.Connectivity == null)
            {
                filters.Connectivity = new string[] { "All" };
            }
            if (filters.Type == null)
            {
                filters.Type = new string[] { "All" };
            }
            if (filters.Mechanical == null)
            {
                filters.Mechanical = new string[] { "All" };
            }

            var result = await productService.QueryComputersAsync(filters, 1000, 2000);


            Assert.Equal(4, result.Count());

            Assert.Contains(result, x => x.Title.Contains("UPGRADED Компютър Fury (Ryzen 2700, 8GB, 1TB, 120 GB SSD, GTX 1660ti 6GB GDDR6, Win10)"));

            filters.Processor = new string[]
            {
                "AMD",
            };


            var secondResultTest = await productService.QueryComputersAsync(filters, 1800, 1820);

            Assert.Equal(3, secondResultTest.Count());

            Assert.Contains(secondResultTest, x => x.ArticleNumber.Contains("101279_101305"));


            filters.Make = new string[]
            {
                "Dell",
            };
            filters.MinPrice = "800";
            filters.MaxPrice = "900";

            var thirdResultTest = await productService.QueryComputersAsync(filters, 800, 900);

            Assert.Empty(thirdResultTest);

        }

        [Theory]
        [InlineData("Mice")]
        public async Task TestIfQueryMiceReturnsCorrectResults(string category)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);

            Account cloudinaryCredentials = new Account(
                CloudinaryAccountTests.CloudName,
                CloudinaryAccountTests.ApiKey,
                CloudinaryAccountTests.ApiSecret);

            var cloudinaryUtility = new Cloudinary(cloudinaryCredentials);
            var cloudinary = new CloudinaryServices(cloudinaryUtility);

            var adminProductServices = new Areas.Administration.Services.AdminProductsServices(context, cloudinary);

            var model = new InsertJsonProductViewModel();

            var miceJson = await
                File.ReadAllTextAsync(
                    @"C:\Users\velis\source\repos\PCHUBStore\PCHUBStore.Tests\AdminServicesTests\JSONMice\mice.json");

            var mice = JsonConvert.DeserializeObject<List<JsonProductModel>>(miceJson);

            await adminProductServices.CreateCategoryAsync(category);

            foreach (var mouse in mice)
            {
                var jsonBasic = JsonConvert.SerializeObject(mouse.BasicChars);

                var jsonAdvanced = JsonConvert.SerializeObject(mouse.AdvancedChars);
                model.Category = category;
                model.BasicCharacteristics = jsonBasic;
                model.FullCharacteristics = jsonAdvanced;

                await adminProductServices.CreateMouseFromJSONAsync(model);
            }

            var filters = new ProductFiltersUrlModel();

            if (filters.Make == null)
            {
                filters.Make = new string[] { "All" };
            }
            if (filters.Model == null)
            {
                filters.Model = new string[] { "All" };
            }
            if (filters.OrderBy == null)
            {
                filters.OrderBy = "Default";
            }
            if (filters.Processor == null)
            {
                filters.Processor = new string[] { "All" };
            }
            if (filters.VideoCard == null)
            {
                filters.VideoCard = new string[] { "All" };
            }
            if (filters.OS == null)
            {
                filters.OS = new string[] { "All" };
            }
            if (filters.RAM == null)
            {
                filters.RAM = new string[] { "All" };
            }
            if (filters.Resolution == null)
            {
                filters.Resolution = new string[] { "All" };
            }
            if (filters.FPS == null)
            {
                filters.FPS = new string[] { "All" };
            }
            if (filters.ReactionTime == null)
            {
                filters.ReactionTime = new string[] { "All" };
            }
            if (filters.MatrixType == null)
            {
                filters.MatrixType = new string[] { "All" };
            }
            if (filters.DisplaySize == null)
            {
                filters.DisplaySize = new string[] { "All" };
            }
            if (filters.Gaming == null)
            {
                filters.Gaming = new string[] { "All" };
            }
            if (filters.Interface == null)
            {
                filters.Interface = new string[] { "All" };
            }
            if (filters.Connectivity == null)
            {
                filters.Connectivity = new string[] { "All" };
            }
            if (filters.Type == null)
            {
                filters.Type = new string[] { "All" };
            }
            if (filters.Mechanical == null)
            {
                filters.Mechanical = new string[] { "All" };
            }

            var result = await productService.QueryMiceAsync(filters, 50, 100);


            Assert.Equal(2, result.Count());

            Assert.Contains(result, x => x.Title.Contains("Геймърска мишка Kingston HyperX Pulsefire Surge, RGB"));

            filters.Gaming = new string[]
            {
                "ДА",
            };


            var secondResultTest = await productService.QueryMiceAsync(filters, 50, 200);

            Assert.Equal(3, secondResultTest.Count());

            Assert.Contains(secondResultTest, x => x.ArticleNumber.Contains("110703"));


            filters.Make = new string[]
            {
                "Dell",
            };
            filters.MinPrice = "800";
            filters.MaxPrice = "900";

            var thirdResultTest = await productService.QueryMiceAsync(filters, 800, 900);

            Assert.Empty(thirdResultTest);

        }

        [Theory]
        [InlineData("Keyboards")]
        public async Task TestIfQueryKeyboardsReturnsCorrectResults(string category)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var productService = new ProductServices(context);

            Account cloudinaryCredentials = new Account(
                CloudinaryAccountTests.CloudName,
                CloudinaryAccountTests.ApiKey,
                CloudinaryAccountTests.ApiSecret);

            var cloudinaryUtility = new Cloudinary(cloudinaryCredentials);
            var cloudinary = new CloudinaryServices(cloudinaryUtility);

            var adminProductServices = new Areas.Administration.Services.AdminProductsServices(context, cloudinary);

            var model = new InsertJsonProductViewModel();

            var keyboardsJson = await
                File.ReadAllTextAsync(
                    @"C:\Users\velis\source\repos\PCHUBStore\PCHUBStore.Tests\AdminServicesTests\JSONKeyboards\keyboards.json");

            var keyboards = JsonConvert.DeserializeObject<List<JsonProductModel>>(keyboardsJson);

            await adminProductServices.CreateCategoryAsync(category);

            foreach (var keyboard in keyboards)
            {
                var jsonBasic = JsonConvert.SerializeObject(keyboard.BasicChars);

                var jsonAdvanced = JsonConvert.SerializeObject(keyboard.AdvancedChars);
                model.Category = category;
                model.BasicCharacteristics = jsonBasic;
                model.FullCharacteristics = jsonAdvanced;

                await adminProductServices.CreateKeyboardFromJSONAsync(model);
            }

            var filters = new ProductFiltersUrlModel();

            if (filters.Make == null)
            {
                filters.Make = new string[] { "All" };
            }
            if (filters.Model == null)
            {
                filters.Model = new string[] { "All" };
            }
            if (filters.OrderBy == null)
            {
                filters.OrderBy = "Default";
            }
            if (filters.Processor == null)
            {
                filters.Processor = new string[] { "All" };
            }
            if (filters.VideoCard == null)
            {
                filters.VideoCard = new string[] { "All" };
            }
            if (filters.OS == null)
            {
                filters.OS = new string[] { "All" };
            }
            if (filters.RAM == null)
            {
                filters.RAM = new string[] { "All" };
            }
            if (filters.Resolution == null)
            {
                filters.Resolution = new string[] { "All" };
            }
            if (filters.FPS == null)
            {
                filters.FPS = new string[] { "All" };
            }
            if (filters.ReactionTime == null)
            {
                filters.ReactionTime = new string[] { "All" };
            }
            if (filters.MatrixType == null)
            {
                filters.MatrixType = new string[] { "All" };
            }
            if (filters.DisplaySize == null)
            {
                filters.DisplaySize = new string[] { "All" };
            }
            if (filters.Gaming == null)
            {
                filters.Gaming = new string[] { "All" };
            }
            if (filters.Interface == null)
            {
                filters.Interface = new string[] { "All" };
            }
            if (filters.Connectivity == null)
            {
                filters.Connectivity = new string[] { "All" };
            }
            if (filters.Type == null)
            {
                filters.Type = new string[] { "All" };
            }
            if (filters.Mechanical == null)
            {
                filters.Mechanical = new string[] { "All" };
            }

            var result = await productService.QueryKeyboardsAsync(filters, 50, 100);


            Assert.Equal(3, result.Count());

            Assert.Contains(result, x => x.ArticleNumber.Contains("108829"));


            var secondResultTest = await productService.QueryKeyboardsAsync(filters, 50, 200);

            Assert.Equal(4, secondResultTest.Count());

            Assert.Contains(secondResultTest, x => x.ArticleNumber.Contains("108829"));


            filters.Make = new string[]
            {
                "Dell",
            };
            filters.MinPrice = "800";
            filters.MaxPrice = "900";

            var thirdResultTest = await productService.QueryKeyboardsAsync(filters, 800, 900);

            Assert.Empty(thirdResultTest);

        }


    }
}
