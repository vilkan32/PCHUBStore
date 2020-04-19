using System;
using System.Collections.Generic;
using System.IO;
using PCHUBStore.Tests.Common;
using System.Threading.Tasks;
using CloudinaryDotNet;
using Constants;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PCHUBStore.Areas.Administration.Models.ProductViewModel;
using PCHUBStore.Data.Models;
using PCHUBStore.Services;
using PCHUBStore.Tests.AdminServicesTests.JSONModel;
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
        public async Task TestIfCreateJSONModelWorksAsExpected(string category)
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

    }
}
