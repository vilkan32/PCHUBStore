using System;
using System.Collections.Generic;
using System.Linq;
using PCHUBStore.Tests.Common;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using PCHUBStore.Areas.Administration.Models.IndexPageViewModels;
using PCHUBStore.Data.Models;
using Xunit;

namespace PCHUBStore.Tests.AdminServicesTests.AdminLayoutServices
{
    public class AdminLayoutServiceTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("User1")]
        [InlineData("RandomUsername")]
        public async Task TestIfGetAdminLayoutInformationThrowsError(string username)
        {

            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var adminLayoutService = new Areas.Administration.Services.AdminLayoutServices(context);

            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await adminLayoutService.GetAdminLayoutInformationAsync(username);
            });


        }

        [Theory]
        [InlineData("TestUser")]
        [InlineData("Username")]
        public async Task TestIfGetAdminLayoutInformationWorksAccordingly(string username)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var adminLayoutService = new Areas.Administration.Services.AdminLayoutServices(context);

            await context.Users.AddAsync(new User
            {
                UserName = username,
                Email = username,
            });

            await context.SaveChangesAsync();

           var result = await adminLayoutService.GetAdminLayoutInformationAsync(username);
            
           Assert.Equal(username, result.Username);
           Assert.Equal("https://dndf.business/wp-content/uploads/2014/07/765-default-avatar-530x500.png", result.PictureUrl);

        }


    }
}
