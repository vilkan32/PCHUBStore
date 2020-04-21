using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CloudinaryDotNet;
using Constants;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PCHUBStore.Areas.Administration.Models.CharacteristicsViewModels;
using PCHUBStore.Areas.Administration.Models.FilterViewModels;
using PCHUBStore.Areas.Administration.Models.ProductViewModel;
using PCHUBStore.Services;
using PCHUBStore.Tests.AdminServicesTests.JSONModel;
using PCHUBStore.Tests.Common;
using Xunit;

namespace PCHUBStore.Tests.AdminServicesTests.AdminFilterServices
{
    public class AdminFilterServiceTests
    {
        [Theory]
        [InlineData("Laptops")]
        [InlineData("Computers")]
        [InlineData("Mice")]
        [InlineData("Keyboards")]
        public async Task TestIfCreateBasicFiltersWorksAccordingly(string categoryName)
        {

            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var filtersService = new Areas.Administration.Services.AdminFiltersServices(context);

            var form = new InsertBasicFiltersViewModel();

            form.Category = categoryName;

            await filtersService.CreateBasicFiltersAsync(form);

            var result = await context.FilterCategories.FirstOrDefaultAsync(x => x.CategoryName == categoryName);

            Assert.NotNull(result);

            Assert.Contains(result.Filters, x => x.Value != null);

        }


        [Theory]
        [InlineData("Laptops")]
        [InlineData("Computers")]
        [InlineData("Mice")]
        [InlineData("Keyboards")]
        public async Task TestIfBasicFiltersExistForCategoryReturnsTrue(string categoryName)
        {

            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var filtersService = new Areas.Administration.Services.AdminFiltersServices(context);

            var form = new InsertBasicFiltersViewModel();

            form.Category = categoryName;

            await filtersService.CreateBasicFiltersAsync(form);

            Assert.True(await filtersService.BasicFiltersExistForCategoryAsync(categoryName));

        }

        [Theory]
        [InlineData(null)]
        [InlineData("Laptops")]
        [InlineData("Computers")]
        [InlineData("Mice")]
        [InlineData("Keyboards")]
        public async Task TestIfBasicFiltersExistForCategoryReturnsFalse(string categoryName)
        {

            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var filtersService = new Areas.Administration.Services.AdminFiltersServices(context);

            Assert.False(await filtersService.BasicFiltersExistForCategoryAsync(categoryName));

        }

        [Theory]
        [InlineData("TestCategory", "TestSubName")]
        [InlineData("Laptops", "Make")]
        [InlineData("Computers", "PCU")]
        [InlineData("Mice", "DPI")]
        public async Task TestIfCreateFilterCategoryWorksAccordingly(string category, string subName)
        {

            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var filtersService = new Areas.Administration.Services.AdminFiltersServices(context);

            var form = new InserFilterCategoryViewModel();

            form.Category = category;

            form.CategoryViewSubName = subName;
            await filtersService.CreateFilterCategoryAsync(form);

            var result = await context.FilterCategories.FirstOrDefaultAsync(x => x.CategoryName == category);

            Assert.Equal(subName, result.ViewSubCategoryName);

        }

        [Theory]
        [InlineData("TestCategory", "TestSubName")]
        [InlineData("Laptops", "Make")]
        [InlineData("Computers", "PCU")]
        [InlineData("Mice", "DPI")]
        public async Task TestIfFilterForCategoryExistsReturnsFalse(string category, string subName)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var filtersService = new Areas.Administration.Services.AdminFiltersServices(context);

            Assert.False(await filtersService.FilterForCategoryExistsAsync(category, subName));
        }

        [Theory]
        [InlineData("TestCategory", "TestSubName")]
        [InlineData("Laptops", "Make")]
        [InlineData("Computers", "PCU")]
        [InlineData("Mice", "DPI")]
        public async Task TestIfFilterForCategoryExistsReturnsTrue(string category, string subName)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();
            var filtersService = new Areas.Administration.Services.AdminFiltersServices(context);


            var form = new InserFilterCategoryViewModel();

            form.Category = category;

            form.CategoryViewSubName = subName;
            await filtersService.CreateFilterCategoryAsync(form);

            Assert.True(await filtersService.FilterForCategoryExistsAsync(category, subName));
        }

        [Theory]
        [InlineData("Laptops")]
        public async Task TestIfUpdateCategoryWorksAccordinglyForLaptops(string category)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var filtersService = new Areas.Administration.Services.AdminFiltersServices(context);
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

            var listOfSubNames = new List<string>
            {
                "Make", "Model", "OS", "Video Card", "Processor", "RAM",
            };

            for (int i = 0; i < listOfSubNames.Count; i++)
            {
                var formFilterCategory = new InserFilterCategoryViewModel();
                formFilterCategory.CategoryViewSubName = listOfSubNames[i];
                formFilterCategory.Category = category;
                await filtersService.CreateFilterCategoryAsync(formFilterCategory);
            }

            await filtersService.UpdateCategoryAsync(category);


            for (int i = 0; i < listOfSubNames.Count; i++)
            {
                var result = await context.FilterCategories.FirstOrDefaultAsync(x => x.CategoryName == category && x.ViewSubCategoryName == listOfSubNames[i]);
                
                Assert.NotNull(result);

                Assert.NotEmpty(result.Filters);
            }


        }

        [Theory]
        [InlineData("Mice")]
        public async Task TestIfUpdateCategoryWorksAccordinglyForMice(string category)
        {

            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var filtersService = new Areas.Administration.Services.AdminFiltersServices(context);

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


            var listOfSubNames = new List<string>
            {
                "Gaming", "Connectivity", "Interface", "Make"
            };

            for (int i = 0; i < listOfSubNames.Count; i++)
            {
                var formFilterCategory = new InserFilterCategoryViewModel();
                formFilterCategory.CategoryViewSubName = listOfSubNames[i];
                formFilterCategory.Category = category;
                await filtersService.CreateFilterCategoryAsync(formFilterCategory);
            }

            await filtersService.UpdateCategoryAsync(category);


            for (int i = 0; i < listOfSubNames.Count; i++)
            {
                var result = await context.FilterCategories.FirstOrDefaultAsync(x => x.CategoryName == category && x.ViewSubCategoryName == listOfSubNames[i]);

                Assert.NotNull(result);

                Assert.NotEmpty(result.Filters);
            }

        }


        [Theory]
        [InlineData("Monitors")]
        public async Task TestIfUpdateCategoryWorksAccordinglyForMonitors(string category)
        {

            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var filtersService = new Areas.Administration.Services.AdminFiltersServices(context);
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


            var listOfSubNames = new List<string>
            {
                 "Make", "Resolution", "FPS", "Reaction Time", "Matrix Type", "Display Size"
            };

            for (int i = 0; i < listOfSubNames.Count; i++)
            {
                var formFilterCategory = new InserFilterCategoryViewModel();
                formFilterCategory.CategoryViewSubName = listOfSubNames[i];
                formFilterCategory.Category = category;
                await filtersService.CreateFilterCategoryAsync(formFilterCategory);
            }

            await filtersService.UpdateCategoryAsync(category);


            for (int i = 0; i < listOfSubNames.Count; i++)
            {
                var result = await context.FilterCategories.FirstOrDefaultAsync(x => x.CategoryName == category && x.ViewSubCategoryName == listOfSubNames[i]);

                Assert.NotNull(result);

                Assert.NotEmpty(result.Filters);
            }

        }

        [Theory]
        [InlineData("Computers")]
        public async Task TestIfUpdateCategoryWorksAccordinglyForComputers(string category)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var filtersService = new Areas.Administration.Services.AdminFiltersServices(context);


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



            var listOfSubNames = new List<string>
            {
                "Video Card",  "RAM", "OS", "Processor"
            };

            for (int i = 0; i < listOfSubNames.Count; i++)
            {
                var formFilterCategory = new InserFilterCategoryViewModel();
                formFilterCategory.CategoryViewSubName = listOfSubNames[i];
                formFilterCategory.Category = category;
                await filtersService.CreateFilterCategoryAsync(formFilterCategory);
            }

            await filtersService.UpdateCategoryAsync(category);


            for (int i = 0; i < listOfSubNames.Count; i++)
            {
                var result = await context.FilterCategories.FirstOrDefaultAsync(x => x.CategoryName == category && x.ViewSubCategoryName == listOfSubNames[i]);

                Assert.NotNull(result);

                Assert.NotEmpty(result.Filters);
            }

        }

        [Theory]
        [InlineData("Keyboards")]
        public async Task TestIfUpdateCategoryWorksAccordinglyForKeyboards(string category)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var filtersService = new Areas.Administration.Services.AdminFiltersServices(context);

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

            var listOfSubNames = new List<string>
            {
                "Make",  "Type", "Interface", "Mechanical",
            };

            for (int i = 0; i < listOfSubNames.Count; i++)
            {
                var formFilterCategory = new InserFilterCategoryViewModel();
                formFilterCategory.CategoryViewSubName = listOfSubNames[i];
                formFilterCategory.Category = category;
                await filtersService.CreateFilterCategoryAsync(formFilterCategory);
            }

            await filtersService.UpdateCategoryAsync(category);


            for (int i = 0; i < listOfSubNames.Count; i++)
            {
                var result = await context.FilterCategories.FirstOrDefaultAsync(x => x.CategoryName == category && x.ViewSubCategoryName == listOfSubNames[i]);

                Assert.NotNull(result);

                Assert.NotEmpty(result.Filters);
            }

        }
    }
}
