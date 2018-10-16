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
    public class GetByEmailAsync : BaseTestClass
    {
        private readonly IUserClient _userClient;

        public GetByEmailAsync()
        {
            _userClient = new UserClient(MockRestClient.Object);
        }

        [Fact]
        public async Task Should_Throw_Should_Throw_ArgumentException_If_Path_Is_Null_Or_Empty()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _userClient.GetByEmailAsync(null)).ConfigureAwait(false);
            await Assert.ThrowsAsync<ArgumentException>(() => _userClient.GetByEmailAsync(string.Empty)).ConfigureAwait(false);
        }

        [Fact]
        public async Task Should_Retrieve_User()
        {
            const string email = "info@armut.com";
            string path = $"/api/users/{email}";

            MockRestClient.Setup(m => m.GetAsync<RetrieveUserResponse>(It.Is<string>(a => a == path))).ReturnsAsync(new ApiResponse<RetrieveUserResponse>
            {
                UrlPath = path,
                HttpStatusCode = HttpStatusCode.OK,
                Model = new RetrieveUserResponse
                {
                    User = new UserModel
                    {
                        Email = email,
                        UserId = email
                    }
                }
            });

            ApiResponse<RetrieveUserResponse> response = await _userClient.GetByEmailAsync(email).ConfigureAwait(false);

            Assert.NotNull(response);
            Assert.NotNull(response.Model);
            Assert.NotNull(response.Model.User);
            Assert.Equal(email, response.Model.User.Email);
            Assert.Equal(email, response.Model.User.UserId);
            Assert.Equal(HttpStatusCode.OK, response.HttpStatusCode);
            Assert.Equal(path, response.UrlPath);

            MockRestClient.Verify(m => m.GetAsync<RetrieveUserResponse>(It.Is<string>(a => a == path)), Times.Once);
        }
    }
}