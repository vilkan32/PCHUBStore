using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;

using Xunit;

namespace PCHUBStore.Tests.WebTests
{
    public class WebTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        [Fact]
        public async Task HomeRouteTests()
        {
            // Arrange
            var factory = new WebApplicationFactory<Startup>();

            // Create an HttpClient which is setup for the test host
            var client = factory.CreateClient();

            // Act
            var response = await client.GetAsync("/Home");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        }

        [Fact]
        public async Task ErrorRouteTest()
        {
            // Arrange
            var factory = new WebApplicationFactory<Startup>();

            // Create an HttpClient which is setup for the test host
            var client = factory.CreateClient();

            // Act
            var response = await client.GetAsync("/Home/Error");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }

        [Fact]
        public async Task AccountRouteTest()
        {
            // Arrange
            var factory = new WebApplicationFactory<Startup>();

            // Create an HttpClient which is setup for the test host
            var client = factory.CreateClient();

            // Act
            var response = await client.GetAsync("/User/Profile");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        }
    }
}
