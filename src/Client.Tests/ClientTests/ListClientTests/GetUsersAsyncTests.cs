using Armut.Iterable.Client.Contracts;
using Armut.Iterable.Client.Core.Responses;
using Armut.Iterable.Client.Models.ListModels;
using Armut.Iterable.Client.Tests.Base;
using Moq;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Armut.Iterable.Client.Tests.ClientTests.ListClientTests
{
    public class GetUsersAsyncTests : BaseTestClass
    {
        private readonly IListClient _listClient;

        public GetUsersAsyncTests()
        {
            _listClient = new ListClient(MockRestClient.Object);
        }

        [Fact]
        public async Task Should_Throw_Should_Throw_ArgumentException_If_ListId_Is_LessThanOrEqualTo_Zero()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _listClient.DeleteAsync(0)).ConfigureAwait(false);
            await Assert.ThrowsAsync<ArgumentException>(() => _listClient.DeleteAsync(-5)).ConfigureAwait(false);
        }

        [Fact]
        public async Task Should_Retrieve_Users()
        {
            const int listId = 14;
            const string users = "info@armut.com\ndestek@armut.com";
            string path = $"/api/lists/getUsers?listId={listId}";

            MockRestClient.Setup(m => m.GetContentAsync(It.Is<string>(a => a == path))).ReturnsAsync(new ApiResponse
            {
                UrlPath = path,
                HttpStatusCode = HttpStatusCode.OK,
                Content = users
            });

            ApiResponse<GetUsersResponse> response = await _listClient.GetUsersAsync(listId).ConfigureAwait(false);

            Assert.NotNull(response);
            Assert.NotNull(response.Model);
            Assert.NotNull(response.Model.UserIds);
            Assert.True(response.Model.UserIds.Any());
            Assert.Equal(2, response.Model.UserIds.Count());
            Assert.Equal(HttpStatusCode.OK, response.HttpStatusCode);
            Assert.Equal(path, response.UrlPath);

            MockRestClient.Verify(m => m.GetContentAsync(It.Is<string>(a => a == path)), Times.Once);
        }

        [Fact]
        public async Task Should_Not_Return_Users_When_HttpStatusCode_Is_BadRequest()
        {
            const int listId = 14;
            string path = $"/api/lists/getUsers?listId={listId}";

            MockRestClient.Setup(m => m.GetContentAsync(It.Is<string>(a => a == path))).ReturnsAsync(new ApiResponse
            {
                UrlPath = path,
                HttpStatusCode = HttpStatusCode.BadRequest
            });

            ApiResponse<GetUsersResponse> response = await _listClient.GetUsersAsync(listId).ConfigureAwait(false);

            Assert.NotNull(response);
            Assert.Null(response.Model);
            Assert.Equal(HttpStatusCode.BadRequest, response.HttpStatusCode);
            Assert.Equal(path, response.UrlPath);

            MockRestClient.Verify(m => m.GetContentAsync(It.Is<string>(a => a == path)), Times.Once);
        }

        [Fact]
        public async Task Should_Not_Return_Users_When_HttpStatusCode_Is_Unauthorized()
        {
            const int listId = 14;
            string path = $"/api/lists/getUsers?listId={listId}";

            MockRestClient.Setup(m => m.GetContentAsync(It.Is<string>(a => a == path))).ReturnsAsync(new ApiResponse
            {
                UrlPath = path,
                HttpStatusCode = HttpStatusCode.Unauthorized
            });

            ApiResponse<GetUsersResponse> response = await _listClient.GetUsersAsync(listId).ConfigureAwait(false);

            Assert.NotNull(response);
            Assert.Null(response.Model);
            Assert.Equal(HttpStatusCode.Unauthorized, response.HttpStatusCode);
            Assert.Equal(path, response.UrlPath);

            MockRestClient.Verify(m => m.GetContentAsync(It.Is<string>(a => a == path)), Times.Once);
        }

        [Fact]
        public async Task Should_Not_Return_Users_When_Content_Is_Null_Or_Empty()
        {
            const int listId = 14;
            string path = $"/api/lists/getUsers?listId={listId}";

            MockRestClient.Setup(m => m.GetContentAsync(It.Is<string>(a => a == path))).ReturnsAsync(new ApiResponse
            {
                UrlPath = path,
                HttpStatusCode = HttpStatusCode.OK
            });

            ApiResponse<GetUsersResponse> response = await _listClient.GetUsersAsync(listId).ConfigureAwait(false);

            Assert.NotNull(response);
            Assert.Null(response.Model);
            Assert.Equal(HttpStatusCode.OK, response.HttpStatusCode);
            Assert.Equal(path, response.UrlPath);

            MockRestClient.Verify(m => m.GetContentAsync(It.Is<string>(a => a == path)), Times.Once);
        }
    }
}