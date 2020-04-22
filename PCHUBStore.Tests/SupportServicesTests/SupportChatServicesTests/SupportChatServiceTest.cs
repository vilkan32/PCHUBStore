using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PCHUBStore.Areas.Support.Services;
using PCHUBStore.Tests.Common;
using PCHUBStore.View.Models.HubModels;
using Xunit;


namespace PCHUBStore.Tests.SupportServicesTests.SupportChatServicesTests
{
    public class SupportChatServiceTest
    {
        [Fact]
        public async Task TestIfAddToQueueWorksAccordingly()
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var chatRequests = new RequestChatServices(context);

            var model = new ChatRequestModel();
            model.Email = "randomMail@mail.bg";
            model.ConnectionId = "connectionId";
            model.IsAuthenticated = true;
            model.Subject = "Help me select laptop";

            await chatRequests.AddToQueueAsync(model);

            var result = await context.ChatRequests.FirstOrDefaultAsync(x => x.Email == "randomMail@mail.bg");

            Assert.NotNull(result);

            Assert.Equal("connectionId", result.ConnetctionId);
            Assert.True(result.IsAuthenticated);
            Assert.Equal("Help me select laptop", result.Subject);
        }

        [Fact]
        public async Task TestIfGetAllChatRequestsReturnsEmptyCollection()
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var chatRequests = new RequestChatServices(context);

            Assert.Empty(await chatRequests.GetAllChatRequestsAsync());
        }


        [Fact]
        public async Task TestIfGetAllChatRequestsWorksAccordingly()
        {

            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var chatRequests = new RequestChatServices(context);

            var modelOne = new ChatRequestModel();
            modelOne.Email = "randomMail@mail.bg";
            modelOne.ConnectionId = "connectionId";
            modelOne.IsAuthenticated = true;
            modelOne.Subject = "Help me select laptop";

            await chatRequests.AddToQueueAsync(modelOne);

            var modelTwo = new ChatRequestModel();
            modelOne.Email = "random@mail.bg";
            modelOne.ConnectionId = "connectionIdrandom";
            modelOne.IsAuthenticated = true;
            modelOne.Subject = "Help me select random laptop";

            await chatRequests.AddToQueueAsync(modelOne);


            var result = await chatRequests.GetAllChatRequestsAsync();

            Assert.NotEmpty(result);

            Assert.Equal(2, result.Count);

        }

        [Theory]
        [InlineData("ConnectionId1", "Subject", "asdasd@abv.bg", "TechnicianName")]
        [InlineData("ConnectionId2", "Subject2", "asdasd2@abv.bg", "TechnicianName2")]
        [InlineData("randomConnection", "randomSubject", "random@abv.bg", "randomTechnician")]
        public async Task TestIfEstablishConnectionWorksAccordingly(string connectionId, string subject, string email,
            string technicianName)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var chatRequests = new RequestChatServices(context);

            await chatRequests.EstablishConnectionAsync(connectionId, subject, email, technicianName);


            var result = await context.Chats.FirstOrDefaultAsync(x => x.ConnectionId == connectionId);

            Assert.NotNull(result);

            Assert.Equal(subject, result.Subject);
            Assert.Equal(technicianName, result.TechnicianName);

        }


        [Theory]
        [InlineData(null)]
        [InlineData("randomId")]
        [InlineData("connectionId")]
        public async Task TestIfConnectionIdExistsReturnsFalse(string connectionId)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var chatRequests = new RequestChatServices(context);

            Assert.False(await chatRequests.ConnectionIdExistsAsync(connectionId));
        }

        [Fact]
        public async Task TestIfConnectionIdExistsReturnsTrue()
        {

            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var chatRequests = new RequestChatServices(context);

            var modelOne = new ChatRequestModel();
            modelOne.Email = "randomMail@mail.bg";
            modelOne.ConnectionId = "connectionId";
            modelOne.IsAuthenticated = true;
            modelOne.Subject = "Help me select laptop";

            await chatRequests.AddToQueueAsync(modelOne);

            var modelTwo = new ChatRequestModel();
            modelOne.Email = "random@mail.bg";
            modelOne.ConnectionId = "connectionIdrandom";
            modelOne.IsAuthenticated = true;
            modelOne.Subject = "Help me select random laptop";

            await chatRequests.AddToQueueAsync(modelOne);


            Assert.True(await chatRequests.ConnectionIdExistsAsync("connectionId"));


            Assert.True(await chatRequests.ConnectionIdExistsAsync("connectionIdrandom"));
        }

        [Theory]
        [InlineData("ConnectionIdOne", "ConnectionIdTwo")]
        [InlineData("ConnectionIdTestOne", "ConnectionIdTestTwo")]
        [InlineData("ConnectionIdRandomTestOne", "ConnectionIdRandomTestTwo")]
        public async Task TestIfRemoveChatRequestWorksAccordingly(string connectionOne, string connectionTwo)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var chatRequests = new RequestChatServices(context);

            var modelOne = new ChatRequestModel();
            modelOne.Email = "randomMail@mail.bg";
            modelOne.ConnectionId = connectionOne;
            modelOne.IsAuthenticated = true;
            modelOne.Subject = "Help me select laptop";

            await chatRequests.AddToQueueAsync(modelOne);

            var modelTwo = new ChatRequestModel();
            modelOne.Email = "random@mail.bg";
            modelOne.ConnectionId = connectionTwo;
            modelOne.IsAuthenticated = true;
            modelOne.Subject = "Help me select random laptop";

            await chatRequests.AddToQueueAsync(modelOne);

            await chatRequests.RemoveChatRequestAsync(connectionOne);

            var resultOne = await chatRequests.GetAllChatRequestsAsync();

            Assert.NotEmpty(resultOne);

            Assert.Single(resultOne);

            await chatRequests.RemoveChatRequestAsync(connectionTwo);

            var resultTwo = await chatRequests.GetAllChatRequestsAsync();

            Assert.Empty(resultTwo);
        }

        [Theory]
        [InlineData("connectionId", "Subject", "asdas@dsdas.bg", "technicianName")]
        [InlineData("ConnectionId1", "Subject", "asdasd@abv.bg", "TechnicianName")]
        [InlineData("ConnectionId2", "Subject2", "asdasd2@abv.bg", "TechnicianName2")]
        [InlineData("randomConnection", "randomSubject", "random@abv.bg", "randomTechnician")]
        public async Task TestIfInsertChatWorksAccordingly(string connectionId, string subject, string email, string technicianName)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var chatRequests = new RequestChatServices(context);

            await chatRequests.EstablishConnectionAsync(connectionId, subject, email, technicianName);

            await chatRequests.InsertChatAsync(technicianName, connectionId, "User",
                "Please help me make the best choice!");


            var result = await context.Chats.FirstOrDefaultAsync(x => x.ConnectionId == connectionId);

            Assert.NotNull(result);

            Assert.NotNull(result.Messages);
        }

        [Theory]
        [InlineData("connectionId", "Subject", "asdas@dsdas.bg", "technicianName")]
        [InlineData("ConnectionId1", "Subject", "asdasd@abv.bg", "TechnicianName")]
        [InlineData("ConnectionId2", "Subject2", "asdasd2@abv.bg", "TechnicianName2")]
        [InlineData("randomConnection", "randomSubject", "random@abv.bg", "randomTechnician")]
        public async Task TestIfConnectionAlreadyEstablishedReturnsTrue(string connectionId, string subject, string email, string technicianName)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var chatRequests = new RequestChatServices(context);

            await chatRequests.EstablishConnectionAsync(connectionId, subject, email, technicianName);

            Assert.True(await chatRequests.ConnectionAlreadyEstablishedAsync(connectionId, "StoreUser"));
        }

        [Theory]
        [InlineData("connectionId", "Subject", "asdas@dsdas.bg", "technicianName")]
        [InlineData("ConnectionId1", "Subject", "asdasd@abv.bg", "TechnicianName")]
        [InlineData("ConnectionId2", "Subject2", "asdasd2@abv.bg", "TechnicianName2")]
        [InlineData("randomConnection", "randomSubject", "random@abv.bg", "randomTechnician")]
        public async Task TestIfConnectionAlreadyEstablishedReturnsFalse(string connectionId, string subject, string email, string technicianName)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var chatRequests = new RequestChatServices(context);

            Assert.False(await chatRequests.ConnectionAlreadyEstablishedAsync(connectionId, "StoreUser"));
        }

        [Fact]
        public async Task TestIfDisconnectFromChatThrowsError()
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var chatRequests = new RequestChatServices(context);

            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await chatRequests.DisconnectFromChat(null, null);
            });
        }


        [Theory]
        [InlineData("connectionId", "Subject", "asdas@dsdas.bg", "technicianName")]
        [InlineData("ConnectionId1", "Subject", "asdasd@abv.bg", "TechnicianName")]
        [InlineData("ConnectionId2", "Subject2", "asdasd2@abv.bg", "TechnicianName2")]
        [InlineData("randomConnection", "randomSubject", "random@abv.bg", "randomTechnician")]
        public async Task TestIfDisconnectFromChatWorksAccordingly(string connectionId, string subject, string email, string technicianName)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var chatRequests = new RequestChatServices(context);

            await chatRequests.EstablishConnectionAsync(connectionId, subject, email, technicianName);

            await chatRequests.DisconnectFromChat(technicianName, connectionId);

            var chat = await context.Chats.FirstOrDefaultAsync(x => x.TechnicianName == technicianName && x.ConnectionId == connectionId && x.ConnectionTerminated == true);

            Assert.NotNull(chat);

            Assert.True(chat.ConnectionTerminated);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("Invalid Connection Id")]
        [InlineData("Invalid Test")]
        public async Task TestIfEstablishedConnectionDetailsReturnsNull(string connectionId)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var chatRequests = new RequestChatServices(context);

            Assert.Null(await chatRequests.EstablishedConnectionDetailsAsync(connectionId, "StoreUser"));

            Assert.Null(await chatRequests.EstablishedConnectionDetailsAsync(connectionId, "Support"));

        }

        [Theory]
        [InlineData("connectionId", "Subject", "asdas@dsdas.bg", "technicianName")]
        [InlineData("ConnectionId1", "Subject", "asdasd@abv.bg", "TechnicianName")]
        [InlineData("ConnectionId2", "Subject2", "asdasd2@abv.bg", "TechnicianName2")]
        [InlineData("randomConnection", "randomSubject", "random@abv.bg", "randomTechnician")]
        public async Task TestIfEstablishedConnectionDetailsWorksAccordingly(string connectionId, string subject, 
            string email, 
            string technicianName)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var chatRequests = new RequestChatServices(context);

            await chatRequests.EstablishConnectionAsync(connectionId, subject, email, technicianName);
            
            Assert.NotNull(await chatRequests.EstablishedConnectionDetailsAsync(connectionId, "StoreUser"));

            Assert.NotNull(await chatRequests.EstablishedConnectionDetailsAsync(technicianName, "Admin"));

        }
    }
}
