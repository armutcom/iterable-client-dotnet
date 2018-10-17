using Armut.Iterable.Client.Contracts;
using Armut.Iterable.Client.Core.Responses;
using Armut.Iterable.Client.Models.UserModels;
using Armut.Iterable.Client.Tests.Base;
using Moq;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Armut.Iterable.Client.Tests.ClientTests.UserClientTests
{
    public class BulkUpdateAsyncTests : BaseTestClass
    {
        private readonly IUserClient _userClient;

        public BulkUpdateAsyncTests()
        {
            _userClient = new UserClient(MockRestClient.Object);
        }

        [Fact]
        public async Task Should_Throw_Should_Throw_ArgumentException_If_Path_Is_Null_Or_Empty()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _userClient.BulkUpdateAsync(null)).ConfigureAwait(false);
        }

        [Fact]
        public async Task Should_Update_User()
        {
            string path = "/api/users/bulkUpdate";
            
            var request = new BulkUpadateUserRequest
            {
                Users = new UserModel[1]
                {
                    new UserModel
                    {
                        Email = "info@armut.com",
                        UserId = "info@armut.com"
                    }
                }
            };

            MockRestClient.Setup(m => m.PostAsync<BulkUpdateUserResponse>(It.Is<string>(a => a == path), It.IsAny<UserModel[]>())).ReturnsAsync(new ApiResponse<BulkUpdateUserResponse>
            {
                UrlPath = path,
                HttpStatusCode = HttpStatusCode.OK,
                Model = new BulkUpdateUserResponse
                {
                    SuccessCount = 5
                }
            });

            ApiResponse<BulkUpdateUserResponse> response = await _userClient.BulkUpdateAsync(request).ConfigureAwait(false);

            Assert.NotNull(response);
            Assert.NotNull(response.Model);
            Assert.Equal(5, response.Model.SuccessCount);
            Assert.Equal(HttpStatusCode.OK, response.HttpStatusCode);
            Assert.Equal(path, response.UrlPath);

            MockRestClient.Verify(m => m.PostAsync<BulkUpdateUserResponse>(It.Is<string>(a => a == path), It.IsAny<UserModel[]>()), Times.Once);
        }
    }
}