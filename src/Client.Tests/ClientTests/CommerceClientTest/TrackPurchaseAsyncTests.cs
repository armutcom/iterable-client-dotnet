using Armut.Iterable.Client.Contracts;
using Armut.Iterable.Client.Core.Responses;
using Armut.Iterable.Client.Tests.Base;
using Moq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Threading.Tasks;
using Armut.Iterable.Client.Models.CommerceModels;
using Armut.Iterable.Client.Models.UserModels;
using Xunit;

namespace Armut.Iterable.Client.Tests.ClientTests.CommerceClientTest
{
    public class TrackPurchaseAsyncTests : BaseTestClass
    {
        private readonly ICommerceClient _commerceClient;

        public TrackPurchaseAsyncTests()
        {
            _commerceClient = new CommerceClient(MockRestClient.Object);
        }

        [Fact]
        public async Task Should_Throw_Should_Throw_ArgumentException_If_Model_Is_Null()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _commerceClient.TrackPurchaseAsync(null)).ConfigureAwait(false);
        }

        [Fact]
        public async Task Should_Purchase_With_Valid_Value()
        {
            string path = "/api/commerce/trackPurchase";

            var request = new TrackPurchaseRequest()
            {
                Id = "1234",
                Items = new List<CommerceItem>(),
                User = new UpdateUserRequest()
                {
                    Email = "info@armut.com",
                    UserId = "info@armut.com",
                    DataFields = new ExpandoObject()
                }
            };

            MockRestClient.Setup(m => m.PostAsync<TrackPurchaseResponse>(It.Is<string>(a => a == path), It.IsAny<TrackPurchaseRequest>())).ReturnsAsync(new ApiResponse<TrackPurchaseResponse>
            {
                UrlPath = path,
                HttpStatusCode = HttpStatusCode.OK,
                Model = new TrackPurchaseResponse
                {
                    Code = "Success"
                }
            });

            ApiResponse<TrackPurchaseResponse> response = await _commerceClient.TrackPurchaseAsync(request).ConfigureAwait(false);

            Assert.NotNull(response);
            Assert.NotNull(response.Model);
            Assert.Equal("Success", response.Model.Code);
            Assert.Equal(HttpStatusCode.OK, response.HttpStatusCode);
            Assert.Equal(path, response.UrlPath);

            MockRestClient.Verify(m => m.PostAsync<TrackPurchaseResponse>(It.Is<string>(a => a == path), It.IsAny<TrackPurchaseRequest>()), Times.Once);
        }


    }
}
