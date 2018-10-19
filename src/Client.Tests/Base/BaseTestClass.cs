using Armut.Iterable.Client.Contracts;
using Armut.Iterable.Client.Core;
using Moq;
using Moq.Protected;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Armut.Iterable.Client.Tests.Base
{
    public class BaseTestClass
    {
        public Mock<IRestClient> MockRestClient { get; } = new Mock<IRestClient>();

        public Mock<HttpMessageHandler> MockHttpMessageHandler { get; set; }

        public IRestClient CreateRestClient()
        {
            return new RestClient(new HttpClient
            {
                BaseAddress = new Uri("https://api.iterable.com/")
            });
        }

        public IRestClient CreateRestClient(HttpResponseMessage httpResponseMessage)
        {
            MockHttpMessageHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            MockHttpMessageHandler
                .Protected()
                // Setup the PROTECTED method to mock
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                // prepare the expected response of the mocked http call
                .ReturnsAsync(httpResponseMessage)
                .Verifiable();

            var httpClient = new HttpClient(MockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("https://api.iterable.com/")
            };

            return new RestClient(httpClient);
        }

        public void VerifyRestClient(Times times, HttpMethod method)
        {
            MockHttpMessageHandler
                .Protected()
                .Verify("SendAsync", times,
                    ItExpr.Is<HttpRequestMessage>(message => message.Method == method),
                    ItExpr.IsAny<CancellationToken>());
        }
        
        public void VerifyRestClient(Times times, HttpMethod method, string path)
        {
            MockHttpMessageHandler
                .Protected()
                .Verify("SendAsync", times,
                    ItExpr.Is<HttpRequestMessage>(message => message.Method == method && message.RequestUri.ToString().Contains(path)),
                    ItExpr.IsAny<CancellationToken>());
        }
    }
}