using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PCHUBStore.Areas.Support.Models;
using PCHUBStore.Areas.Support.Services;
using PCHUBStore.Data.Models;
using PCHUBStore.Data.Models.Enums;
using PCHUBStore.Tests.Common;
using Xunit;


namespace PCHUBStore.Tests.SupportServicesTests.SupportShipmentManagerServicesTests
{
    public class ShipmentManagerServiceTest
    {
        [Theory]
        [InlineData(1, null)]
        [InlineData(2, "InvalidUsername")]
        [InlineData(3, "randomUsername")]
        public async Task TestIfAddActivityToShipmentReturnsNull(int shipmentId, string username)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var shipmentManager = new ShipmentManagerServices(context);

            var model = new ActivityViewModel();

            var result = await shipmentManager.AddActivityToShipmentAsync(shipmentId, model, username);

            Assert.Null(result);
        }


        [Theory]
        [InlineData(1, "ValidUsername")]
        [InlineData(2, "Username")]
        [InlineData(3, "randomUsername")]
        public async Task TestIfAddActivityToShipmentReturnsCorrectResult(int shipmentId, string username)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var shipmentManager = new ShipmentManagerServices(context);


            await context.Shipments.AddAsync(new Shipment
            {
                Id = shipmentId,
                Activities = new List<Activity>(),
            });

            await context.SaveChangesAsync();

            var form = new ActivityViewModel();
            form.OwnerName = username;
            form.Description = "Description";
            form.CreationDate = DateTime.UtcNow;
            form.ActivityType = ActivityType.Issue;

            var result = await shipmentManager.AddActivityToShipmentAsync(shipmentId, form, username);

            Assert.NotNull(result);

        }

        [Theory]
        [InlineData(null)]
        [InlineData("randomId")]
        [InlineData("InvalidId")]
        public async Task TestIfEditActivityThrowsError(string id)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var shipmentManager = new ShipmentManagerServices(context);

            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                var form = new ActivityViewModel();
                form.Id = id;
                await shipmentManager.EditActivityAsync(form);
            });
        }



        [Theory]
        [InlineData(1, "ValidUsername")]
        [InlineData(2, "Username")]
        [InlineData(3, "randomUsername")]
        public async Task TestIfEditActivityWorksAccordingly(int shipmentId, string username)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var shipmentManager = new ShipmentManagerServices(context);


            await context.Shipments.AddAsync(new Shipment
            {
                Id = shipmentId,
                Activities = new List<Activity>(),
                CreatedOn = DateTime.Now,
            });

            await context.SaveChangesAsync();

            var form = new ActivityViewModel();
            form.OwnerName = username;
            form.Description = "Description";
            form.CreationDate = DateTime.UtcNow;
            form.ActivityType = ActivityType.Issue;

            var activityId = await shipmentManager.AddActivityToShipmentAsync(shipmentId, form, username);

            var editActivityForm = new ActivityViewModel();

            editActivityForm.Id = activityId;
            editActivityForm.ActivityType = (ActivityType)form.ActivityType;
            editActivityForm.ActivityClosed = form.ActivityClosed;
            editActivityForm.Description = "randomDescription";
            editActivityForm.CreationDate = DateTime.UtcNow;

            await shipmentManager.EditActivityAsync(editActivityForm);

            var editedActivity = await context.Activities.FirstOrDefaultAsync(x => x.Id == activityId);

            Assert.Equal("randomDescription", editedActivity.Description);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(1200)]
        [InlineData(12000)]
        public async Task TestIfEditShipmentThrowsError(int id)
        {

            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var shipmentManager = new ShipmentManagerServices(context);


            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                var form = new ShipmentViewModel();

                await shipmentManager.EditShipmentAsync(id, form);
            });
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(1200)]
        [InlineData(12000)]
        public async Task TestIfEditShipmentWorksAccordingly(int id)
        {

            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var shipmentManager = new ShipmentManagerServices(context);

            await context.Shipments.AddAsync(new Shipment
            {
                Id = id,
                ReceivedOn = DateTime.Now,
                ShipmentCoveredBy = "Someone",
                ShipmentDetails = ShipmentDetails.Send,
                ShipmentImportancy = ShipmentImportancy.Urgent,
                ShipmentStatus = ShipmentStatus.Rejected,
                ShippingCompanyDetails = string.Empty,
                ConfirmationStatus = ConfirmationStatus.AwaitingResponse,
                ClientResponse = ClientResponse.AwaitingResponse,
                DeliveryConfirmationDate = DateTime.Now,
            });

            await context.SaveChangesAsync();

            var form = new ShipmentViewModel();

            form.ReceivedOn = DateTime.Now;
            form.ShipmentCoveredBy = "Us";
            form.ShipmentDetails = ShipmentDetails.Send;
            form.ShipmentImportancy = ShipmentImportancy.Normal;
            form.ShipmentStatus = ShipmentStatus.Confirmed;
            form.ShippingCompanyDetails = "";
            form.ConfirmationStatus = ConfirmationStatus.Confirmed;
            form.ClientResponse = ClientResponse.Confirmed;
            form.DeliveryConfirmationDate = DateTime.UtcNow;

            await shipmentManager.EditShipmentAsync(id, form);

            var result = await context.Shipments.FirstOrDefaultAsync(x => x.Id == id);

            Assert.NotNull(result);

            Assert.Equal(ShipmentStatus.Confirmed, result.ShipmentStatus);
            Assert.Equal("Us", result.ShipmentCoveredBy);
        }


        [Theory]
        [InlineData(1)]
        [InlineData(200)]
        [InlineData(3000)]
        [InlineData(1000000)]
        public async Task TestIfGetShipmentReturnsNull(int id)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var shipmentManager = new ShipmentManagerServices(context);

            var result = await shipmentManager.GetShipmentAsync(id);

            Assert.Null(result);

        }

        [Theory]
        [InlineData(1)]
        [InlineData(200)]
        [InlineData(3000)]
        [InlineData(1000000)]
        public async Task TestIfGetShipmentReturnsCorrectResult(int id)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var shipmentManager = new ShipmentManagerServices(context);


            await context.Shipments.AddAsync(new Shipment
            {
                Id = id,
            });

            await context.SaveChangesAsync();

            var result = await shipmentManager.GetShipmentAsync(id);

            Assert.NotNull(result);

        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task TestIfGetShipmentsReturnsEmptyCollection(int page)
        {

            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var shipmentManager = new ShipmentManagerServices(context);

            Assert.Empty(await shipmentManager.GetShipmentsAsync(page));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task TestIfGetShipmentsReturnsCorrectResults(int id)
        {

            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var shipmentManager = new ShipmentManagerServices(context);

            await context.Shipments.AddAsync(new Shipment
            {
                Id = id,
            });

            await context.SaveChangesAsync();

            Assert.NotEmpty(await shipmentManager.GetShipmentsAsync(1));
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(3, 4)]
        [InlineData(5, 6)]
        [InlineData(7, 8)]
        public async Task TestIfShipmentExists(int existingShipment, int nonExistingShipment)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var shipmentManager = new ShipmentManagerServices(context);

            await context.Shipments.AddAsync(new Shipment
            {
                Id = existingShipment,
            });

            await context.SaveChangesAsync();

            Assert.True(await shipmentManager.ShipmentExistsAsync(existingShipment));
            Assert.False(await shipmentManager.ShipmentExistsAsync(nonExistingShipment));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task TestIfTotalNumberOfShipmentsReturnsCorrectResult(int id)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var shipmentManager = new ShipmentManagerServices(context);

            Assert.Equal(0, shipmentManager.TotalNumberOfShipments());
            await context.Shipments.AddAsync(new Shipment
            {
                Id = id,
            });

            await context.SaveChangesAsync();

            Assert.Equal(1, shipmentManager.TotalNumberOfShipments());
        }

        [Fact]
        public async Task TestIfQueryShipmentsReturnsEmptyResult()
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var shipmentManager = new ShipmentManagerServices(context);

            var model = new ShipmentManagerIndexModel();

            var result = await shipmentManager.QueryShipmentsAsync(model);

            Assert.Empty(result);

        }

        [Fact]
        public async Task TestIfQueryShipmentsReturnsCorrectResultId()
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var shipmentManager = new ShipmentManagerServices(context);

            await context.Shipments.AddAsync(new Shipment
            {
                Id = 123,
            });

            await context.SaveChangesAsync();

            var model = new ShipmentManagerIndexModel();

            model.Id = 123;

            var result = await shipmentManager.QueryShipmentsAsync(model);

            Assert.NotEmpty(result);

            Assert.Contains(result, x => x.Id == 123);
        }

        [Fact]
        public async Task TestIfQueryShipmentsReturnsCorrectResultShippingCompany()
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var shipmentManager = new ShipmentManagerServices(context);

            await context.Shipments.AddAsync(new Shipment
            {
                Id = 123,
                ShippingCompany = ShippingCompany.Econt,
            });

            await context.SaveChangesAsync();

            var model = new ShipmentManagerIndexModel();

            model.ShippingCompany = ShippingCompany.Econt;

            var result = await shipmentManager.QueryShipmentsAsync(model);

            Assert.NotEmpty(result);

            Assert.Contains(result, x => x.ShippingCompany == ShippingCompany.Econt);
        }

        [Fact]
        public async Task TestIfQueryShipmentsReturnsCorrectResultConfirmationStatus()
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var shipmentManager = new ShipmentManagerServices(context);

            await context.Shipments.AddAsync(new Shipment
            {
                Id = 123,
                ConfirmationStatus = ConfirmationStatus.AwaitingResponse,
            });

            await context.SaveChangesAsync();

            var model = new ShipmentManagerIndexModel();

            model.ConfirmationStatus = ConfirmationStatus.AwaitingResponse;

            var result = await shipmentManager.QueryShipmentsAsync(model);

            Assert.NotEmpty(result);

            Assert.Contains(result, x => x.ConfirmationStatus == ConfirmationStatus.AwaitingResponse);
        }

        [Fact]
        public async Task TestIfQueryShipmentsReturnsCorrectResultFirstAndLastName()
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var shipmentManager = new ShipmentManagerServices(context);

            await context.Shipments.AddAsync(new Shipment
            {
                Id = 123,
                User = new User
                {
                    FirstName = "Spas",
                    LastName = "Spas",
                }
            });

            await context.SaveChangesAsync();

            var model = new ShipmentManagerIndexModel();

            model.FirstName = "Spas";
            model.LastName = "Spas";

            var result = await shipmentManager.QueryShipmentsAsync(model);

            Assert.NotEmpty(result);

            Assert.Contains(result, x => x.Id == 123);
        }

        [Fact]
        public async Task TestIfQueryShipmentsReturnsCorrectResultAddress()
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var shipmentManager = new ShipmentManagerServices(context);

            await context.Shipments.AddAsync(new Shipment
            {
                Id = 123,
                User = new User
                {
                    FirstName = "Spas",
                    LastName = "Spas",
                    Address = "random address"
                }
            });

            await context.SaveChangesAsync();

            var model = new ShipmentManagerIndexModel();

            model.Address = "random address";

            var result = await shipmentManager.QueryShipmentsAsync(model);

            Assert.NotEmpty(result);

            Assert.Contains(result, x => x.Id == 123);
        }
    }
}
