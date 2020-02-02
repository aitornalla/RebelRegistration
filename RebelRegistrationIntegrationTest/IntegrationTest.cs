using Microsoft.AspNetCore.Mvc.Testing;
using RebelRegistration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RebelRegistrationIntegrationTest
{
    public class IntegrationTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public IntegrationTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/api/rebelinfo")]
        public async Task GetHttpRequest(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();

            Assert.True(response.IsSuccessStatusCode);
        }
    }
}
