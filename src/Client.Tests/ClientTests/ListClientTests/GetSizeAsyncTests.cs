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
    public class GetSizeAsyncTests : BaseTestClass
    {
        private readonly IListClient _listClient;

        public GetSizeAsyncTests()
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
        public async Task Should_Retrieve_Size()
        {
            const int listId = 14;
            const int size = 309;
            string path = $"/api/lists/{listId}/size";

            MockRestClient.Setup(m => m.GetContentAsync(It.Is<string>(a => a == path))).ReturnsAsync(new ApiResponse
            {
                UrlPath = path,
                HttpStatusCode = HttpStatusCode.OK,
                Content = size.ToString()
            });

            ApiResponse<GetSizeResponse> response = await _listClient.GetSizeAsync(listId).ConfigureAwait(false);

            Assert.NotNull(response);
            Assert.NotNull(response.Model);
            Assert.Equal(size, response.Model.Size);
            Assert.Equal(HttpStatusCode.OK, response.HttpStatusCode);
            Assert.Equal(path, response.UrlPath);

            MockRestClient.Verify(m => m.GetContentAsync(It.Is<string>(a => a == path)), Times.Once);
        }

        [Fact]
        public async Task Should_Not_Return_Size_When_HttpStatusCode_Is_BadRequest()
        {
            const int listId = 14;
            string path = $"/api/lists/{listId}/size";

            MockRestClient.Setup(m => m.GetContentAsync(It.Is<string>(a => a == path))).ReturnsAsync(new ApiResponse
            {
                UrlPath = path,
                HttpStatusCode = HttpStatusCode.BadRequest
            });

            ApiResponse<GetSizeResponse> response = await _listClient.GetSizeAsync(listId).ConfigureAwait(false);

            Assert.NotNull(response);
            Assert.Null(response.Model);
            Assert.Equal(HttpStatusCode.BadRequest, response.HttpStatusCode);
            Assert.Equal(path, response.UrlPath);

            MockRestClient.Verify(m => m.GetContentAsync(It.Is<string>(a => a == path)), Times.Once);
        }

        [Fact]
        public async Task Should_Not_Return_Size_When_HttpStatusCode_Is_Unauthorized()
        {
            const int listId = 14;
            string path = $"/api/lists/{listId}/size";

            MockRestClient.Setup(m => m.GetContentAsync(It.Is<string>(a => a == path))).ReturnsAsync(new ApiResponse
            {
                UrlPath = path,
                HttpStatusCode = HttpStatusCode.Unauthorized
            });

            ApiResponse<GetSizeResponse> response = await _listClient.GetSizeAsync(listId).ConfigureAwait(false);

            Assert.NotNull(response);
            Assert.Null(response.Model);
            Assert.Equal(HttpStatusCode.Unauthorized, response.HttpStatusCode);
            Assert.Equal(path, response.UrlPath);

            MockRestClient.Verify(m => m.GetContentAsync(It.Is<string>(a => a == path)), Times.Once);
        }

        [Fact]
        public async Task Should_Not_Return_Size_When_Content_Is_Null_Or_Empty()
        {
            const int listId = 14;
            string path = $"/api/lists/{listId}/size";

            MockRestClient.Setup(m => m.GetContentAsync(It.Is<string>(a => a == path))).ReturnsAsync(new ApiResponse
            {
                UrlPath = path,
                HttpStatusCode = HttpStatusCode.OK
            });

            ApiResponse<GetSizeResponse> response = await _listClient.GetSizeAsync(listId).ConfigureAwait(false);

            Assert.NotNull(response);
            Assert.Null(response.Model);
            Assert.Equal(HttpStatusCode.OK, response.HttpStatusCode);
            Assert.Equal(path, response.UrlPath);

            MockRestClient.Verify(m => m.GetContentAsync(It.Is<string>(a => a == path)), Times.Once);
        }

        [Fact]
        public async Task Should_Not_Return_Size_When_Content_Is_Not_Integer()
        {
            const int listId = 14;
            string path = $"/api/lists/{listId}/size";

            MockRestClient.Setup(m => m.GetContentAsync(It.Is<string>(a => a == path))).ReturnsAsync(new ApiResponse
            {
                UrlPath = path,
                HttpStatusCode = HttpStatusCode.OK,
                Content = "test content"
            });

            ApiResponse<GetSizeResponse> response = await _listClient.GetSizeAsync(listId).ConfigureAwait(false);

            Assert.NotNull(response);
            Assert.Null(response.Model);
            Assert.Equal(HttpStatusCode.OK, response.HttpStatusCode);
            Assert.Equal(path, response.UrlPath);

            MockRestClient.Verify(m => m.GetContentAsync(It.Is<string>(a => a == path)), Times.Once);
        }
    }
}