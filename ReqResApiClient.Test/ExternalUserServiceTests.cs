using Microsoft.Extensions.Options;
using RaftLabs.ReqResApiClient.Configuration;
using RaftLabs.ReqResApiClient.Services.Implementation;
using System.Net;
using Moq;
using Moq.Protected;
using Xunit;

namespace RaftLabs.ReqResApiClient.Test
{
    public class ExternalUserServiceTests
    {
        [Fact]
        public async Task GetUserByIdAsync_ReturnsUser_WhenExists()
        {
            var userJson = """{"data": { "id": 1, "email": "test@reqres.in", "first_name": "Test", "last_name": "User", "avatar": "url" }}""";

            var handler = new Mock<HttpMessageHandler>();
            handler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(userJson)
                });

            var client = new HttpClient(handler.Object);
            var options = Options.Create(new ApiOptions { BaseUrl = "https://reqres.in/api" });
            var service = new ExternalUserService(client, options);

            var user = await service.GetUserByIdAsync(1);

            Assert.NotNull(user);
            Assert.Equal(1, user?.Id);
        }
    }
}
