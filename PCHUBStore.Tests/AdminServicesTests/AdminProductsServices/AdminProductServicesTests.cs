using System;
using CloudinaryDotNet;
using Constants;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PCHUBStore.Areas.Administration.Models.ProductViewModel;
using PCHUBStore.Services;
using PCHUBStore.Tests.AdminServicesTests.JSONModel;
using PCHUBStore.Tests.Common;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using PCHUBStore.Data.Models;
using Xunit;


namespace PCHUBStore.Tests.AdminServicesTests.AdminProductsServices
{
    public class AdminProductServicesTests
    {
        [Theory]
        [InlineData("Laptops")]
        [InlineData("Computers")]
        [InlineData("Mice")]
        [InlineData("Keyboards")]
        public async Task TestIfCreateCategoryWorksProperly(string category)
        {

            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            Account cloudinaryCredentials = new Account(
                CloudinaryAccountTests.CloudName,
                CloudinaryAccountTests.ApiKey,
                CloudinaryAccountTests.ApiSecret);

            var cloudinaryUtility = new Cloudinary(cloudinaryCredentials);
            var cloudinary = new CloudinaryServices(cloudinaryUtility);

            var adminProductServices = new Areas.Administration.Services.AdminProductsServices(context, cloudinary);

            await adminProductServices.CreateCategoryAsync(category);

            Assert.Contains(await context.Categories.ToListAsync(), x => x.Name == category);
        }

        [Theory]
        [InlineData("Laptops")]
        public async Task TestIfCreateJSONLaptopWorksAsExpected(string category)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            Account cloudinaryCredentials = new Account(
                CloudinaryAccountTests.CloudName,
                CloudinaryAccountTests.ApiKey,
                CloudinaryAccountTests.ApiSecret);

            var cloudinaryUtility = new Cloudinary(cloudinaryCredentials);
            var cloudinary = new CloudinaryServices(cloudinaryUtility);

            var adminProductServices = new Areas.Administration.Services.AdminProductsServices(context, cloudinary);

            var model = new InsertJsonProductViewModel();

            var laptopsJson =await
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

            var result = await context.Products.ToListAsync();

            Assert.Equal(2, result.Count);

            Assert.Contains(result, x => x.Make == "Acer");
            Assert.Contains(result, x => x.Price == 878);

        }


        [Theory]
        [InlineData("Keyboards", "97400")]
        [InlineData("Keyboards", "111420")]
        [InlineData("Keyboards", "61790")]
        [InlineData("Keyboards", "108829")]

        public async Task TestIfCreateJSONKeyboardWorksAsExpected(string category, string articleNumber)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
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

            var result = await context.Products.ToListAsync();

            Assert.Equal(4, result.Count);

            Assert.Contains(result, x => x.Title == "Геймърска механична клавиатура Cooler Master CK530 TKL RGB Brown swithces (CM-KEY-CK-530-GKGM1-US)");
            Assert.Contains(result, x => x.ArticleNumber.Contains(articleNumber));

        }

        [Theory]
        [InlineData("Mice", "110703")]
        [InlineData("Mice", "109115")]
        [InlineData("Mice", "111619")]
        [InlineData("Mice", "56736")]

        public async Task TestIfCreateJSONMouseWorksAsExpected(string category, string articleNumber)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
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

            var result = await context.Products.ToListAsync();

            Assert.Equal(4, result.Count);

            Assert.Contains(result, x => x.ArticleNumber.Contains(articleNumber));

        }

        [Theory]
        [InlineData("Monitor", "114476")]
        [InlineData("Monitor", "123569")]
        [InlineData("Monitor", "97773")]

        public async Task TestIfCreateJSONMonitorWorksAsExpected(string category, string articleNumber)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
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

            var result = await context.Products.ToListAsync();

            Assert.Equal(4, result.Count);

            Assert.Contains(result, x => x.ArticleNumber.Contains(articleNumber));

        }

        [Theory]
        [InlineData("Computers", "101279_101305")]
        [InlineData("Computers", "118887_118895")]
        [InlineData("Computers", "92738_125632")]
        public async Task TestIfCreateJSONComputerWorksAsExpected(string category, string articleNumber)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
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

            var result = await context.Products.ToListAsync();

            Assert.Equal(4, result.Count);

            Assert.Contains(result, x => x.ArticleNumber.Contains(articleNumber));

        }

        [Theory]
        [InlineData("Computers", "Laptops", "Monitors", "Keyboards")]
        [InlineData("Computers1", "Laptops1", "Monitors1", "Keyboards1")]
        [InlineData("Computers2", "Laptops2", "Monitors2", "Keyboards2")]
        [InlineData("Computers3", "Laptops3", "Monitors3", "Keyboards3")]
        public async Task TestIfGetAllCategoryNamesReturnsAllNamesCorrectly(string category, string category1, string category2, string category3)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            Account cloudinaryCredentials = new Account(
                CloudinaryAccountTests.CloudName,
                CloudinaryAccountTests.ApiKey,
                CloudinaryAccountTests.ApiSecret);

            var cloudinaryUtility = new Cloudinary(cloudinaryCredentials);
            var cloudinary = new CloudinaryServices(cloudinaryUtility);

            var adminProductServices = new Areas.Administration.Services.AdminProductsServices(context, cloudinary);

            await adminProductServices.CreateCategoryAsync(category);
            await adminProductServices.CreateCategoryAsync(category1);
            await adminProductServices.CreateCategoryAsync(category2);
            await adminProductServices.CreateCategoryAsync(category3);


            var result = await adminProductServices.GetAllCategoryNamesAsync();

            Assert.NotEmpty(result);
            Assert.Contains(result, x => x == category);
            Assert.Contains(result, x => x == category1);
            Assert.Contains(result, x => x == category2);
            Assert.Contains(result, x => x == category3);
        }


        [Theory]
        [InlineData(null)]
        [InlineData("Invalid")]
        [InlineData("Invalid1")]
        [InlineData("Invalid2")]
        [InlineData("Invalid3")]
        public async Task TestIfGetProductReturnsNull(string productId)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            Account cloudinaryCredentials = new Account(
                CloudinaryAccountTests.CloudName,
                CloudinaryAccountTests.ApiKey,
                CloudinaryAccountTests.ApiSecret);

            var cloudinaryUtility = new Cloudinary(cloudinaryCredentials);
            var cloudinary = new CloudinaryServices(cloudinaryUtility);

            var adminProductServices = new Areas.Administration.Services.AdminProductsServices(context, cloudinary);

            var result = await adminProductServices.GetProductAsync(productId);

            Assert.Null(result);
        }


        [Theory]
        [InlineData("ProductId1")]
        [InlineData("ProductId2")]
        [InlineData("ProductId3")]
        [InlineData("ProductId4")]
        public async Task TestIfGetProductReturnsCorrectResult(string productId)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            Account cloudinaryCredentials = new Account(
                CloudinaryAccountTests.CloudName,
                CloudinaryAccountTests.ApiKey,
                CloudinaryAccountTests.ApiSecret);

            var cloudinaryUtility = new Cloudinary(cloudinaryCredentials);
            var cloudinary = new CloudinaryServices(cloudinaryUtility);

            var adminProductServices = new Areas.Administration.Services.AdminProductsServices(context, cloudinary);

            await context.Products.AddAsync(new Product
            {
                Id = productId,
            });

            await context.SaveChangesAsync();

            var result = await adminProductServices.GetProductAsync(productId);

            Assert.NotNull(result);

            Assert.Equal(result.Id, productId);
        }

        [Fact]
        public async Task TestIfUpdateHtmlDescriptionThrowsError()
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            Account cloudinaryCredentials = new Account(
                CloudinaryAccountTests.CloudName,
                CloudinaryAccountTests.ApiKey,
                CloudinaryAccountTests.ApiSecret);

            var cloudinaryUtility = new Cloudinary(cloudinaryCredentials);
            var cloudinary = new CloudinaryServices(cloudinaryUtility);

            var adminProductServices = new Areas.Administration.Services.AdminProductsServices(context, cloudinary);

            var form = new InserHtmlInProductViewModel();

            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await adminProductServices.UpdateHtmlDescriptionAsync(form);
            });
        }

        [Theory]
        [InlineData("ProductId1")]
        [InlineData("ProductId2")]
        [InlineData("ProductId3")]
        [InlineData("ProductId4")]
        public async Task TestIfUpdateHtmlDescriptionWorksProperly(string productId)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            Account cloudinaryCredentials = new Account(
                CloudinaryAccountTests.CloudName,
                CloudinaryAccountTests.ApiKey,
                CloudinaryAccountTests.ApiSecret);

            var cloudinaryUtility = new Cloudinary(cloudinaryCredentials);
            var cloudinary = new CloudinaryServices(cloudinaryUtility);

            var adminProductServices = new Areas.Administration.Services.AdminProductsServices(context, cloudinary);

            var form = new InserHtmlInProductViewModel();

            await context.Products.AddAsync(new Product
            {
                Id = productId,
            });

            await context.SaveChangesAsync();

            form.ProductId = productId;

            form.HtmlContent = "<p>Hello World</p>";

            await adminProductServices.UpdateHtmlDescriptionAsync(form);

            var result = await context.Products.FirstOrDefaultAsync(x => x.Id == productId);

            Assert.NotNull(result.HtmlDescription);

            Assert.Equal("<p>Hello World</p>", result.HtmlDescription);

        }

    }
}
