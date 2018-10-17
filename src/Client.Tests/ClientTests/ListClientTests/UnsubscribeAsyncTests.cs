using Armut.Iterable.Client.Contracts;
using Armut.Iterable.Client.Core.Responses;
using Armut.Iterable.Client.Models.ListModels;
using Armut.Iterable.Client.Tests.Base;
using Moq;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Armut.Iterable.Client.Tests.ClientTests.ListClientTests
{
    public class UnsubscribeAsyncTests : BaseTestClass
    {
        private readonly IListClient _listClient;

        public UnsubscribeAsyncTests()
        {
            _listClient = new ListClient(MockRestClient.Object);
        }

        [Fact]
        public async Task Should_Throw_Should_Throw_ArgumentException_If_Path_Is_Null_Or_Empty()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _listClient.UnsubscribeAsync(null)).ConfigureAwait(false);
        }

        [Fact]
        public async Task Should_Unsubscribe()
        {
            const string path = "/api/lists/unsubscribe";
            const int successCount = 14;
            var request = new UnsubscribeRequest
            {
                ListId = 14,
                Subscribers = new SubscriberModel[1]
                {
                    new SubscriberModel
                    {
                        Email = "info@armut.com",
                        UserId = "info@armut.com"
                    }
                }
            };

            MockRestClient.Setup(m => m.PostAsync<UnsubscribeResponse>(It.Is<string>(a => a == path), It.IsAny<UnsubscribeRequest>())).ReturnsAsync(new ApiResponse<UnsubscribeResponse>
            {
                UrlPath = path,
                HttpStatusCode = HttpStatusCode.OK,
                Model = new UnsubscribeResponse
                {
                    SuccessCount = successCount
                }
            });

            ApiResponse<UnsubscribeResponse> response = await _listClient.UnsubscribeAsync(request).ConfigureAwait(false);

            Assert.NotNull(response);
            Assert.NotNull(response.Model);
            Assert.Equal(successCount, response.Model.SuccessCount);
            Assert.Equal(HttpStatusCode.OK, response.HttpStatusCode);
            Assert.Equal(path, response.UrlPath);

            MockRestClient.Verify(m => m.PostAsync<UnsubscribeResponse>(It.Is<string>(a => a == path), It.IsAny<UnsubscribeRequest>()), Times.Once);
        }
    }
}