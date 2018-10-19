using Armut.Iterable.Client.Contracts;
using Armut.Iterable.Client.Core.Responses;
using Armut.Iterable.Client.Models.ListModels;
using Armut.Iterable.Client.Models.UserModels;
using Armut.Iterable.Client.Tests.Base;
using Moq;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Armut.Iterable.Client.Tests.ClientTests.ListClientTests
{
    public class SubscribeAsyncTests : BaseTestClass
    {
        private readonly IListClient _listClient;

        public SubscribeAsyncTests()
        {
            _listClient = new ListClient(MockRestClient.Object);
        }

        [Fact]
        public async Task Should_Throw_Should_Throw_ArgumentException_If_Request_Is_Null()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _listClient.SubscribeAsync(null)).ConfigureAwait(false);
        }

        [Fact]
        public async Task Should_Subscribe()
        {
            const string path = "/api/lists/subscribe";
            const int successCount = 14;
            var request = new SubscribeRequest
            {
                ListId = 14,
                Subscribers = new UserModel[1]
                {
                    new UserModel
                    {
                        Email = "info@armut.com",
                        UserId = "info@armut.com"
                    }
                }
            };

            MockRestClient.Setup(m => m.PostAsync<SubscribeResponse>(It.Is<string>(a => a == path), It.IsAny<SubscribeRequest>())).ReturnsAsync(new ApiResponse<SubscribeResponse>
            {
                UrlPath = path,
                HttpStatusCode = HttpStatusCode.OK,
                Model = new SubscribeResponse
                {
                    SuccessCount = successCount
                }
            });

            ApiResponse<SubscribeResponse> response = await _listClient.SubscribeAsync(request).ConfigureAwait(false);

            Assert.NotNull(response);
            Assert.NotNull(response.Model);
            Assert.Equal(successCount, response.Model.SuccessCount);
            Assert.Equal(HttpStatusCode.OK, response.HttpStatusCode);
            Assert.Equal(path, response.UrlPath);

            MockRestClient.Verify(m => m.PostAsync<SubscribeResponse>(It.Is<string>(a => a == path), It.IsAny<SubscribeRequest>()), Times.Once);
        }
    }
}