using Moq;
using Moq.Protected;

namespace Cep.Tests.Doubles
{
    public class HttpMessageHandlerMock
    {
        private readonly Mock<HttpMessageHandler> mock = new();
        public HttpMessageHandler Object => mock.Object;

        public HttpMessageHandlerMock SetupSendAsync(string requestUri, HttpResponseMessage returns)
        {
            mock.Protected().Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(m => m.RequestUri!.ToString().Contains(requestUri)),
                ItExpr.IsAny<CancellationToken>()
                ).ReturnsAsync(returns);
            return this;
        }
    }
}