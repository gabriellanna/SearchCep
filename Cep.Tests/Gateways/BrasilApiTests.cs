using System.Net;
using Cep.Infra.Gateway;
using Cep.Tests.Doubles;
using Xunit;

namespace Cep.Tests.Gateways
{
    public class BrasilApiTests
    {
        [Fact]
        public async Task Buscar_cep_deve_retornar_endereco()
        {
            var cep = "26255350";
            var client = GetHttpClient("Payloads/ResponseBrasilApi.json");
            var brasilApi = new BrasilApiGateway(client);
            var response = await brasilApi.ResponseAddressByCep(cep);
            Assert.Equal(response.Cep, cep);
        }
        private HttpClient GetHttpClient(string responseFile)
        {
            var handler = new HttpMessageHandlerMock().SetupSendAsync("/api/cep/v1/", HttpResponseMessage(responseFile)).Object;
            return new HttpClient(handler){BaseAddress = new Uri("https://brasilapi.com.br/")};
        }

        public HttpResponseMessage HttpResponseMessage(string filePath, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        var body = File.ReadAllText(filePath);
        return new HttpResponseMessage()
        {
            StatusCode = statusCode,
            Content = new StringContent(body),
        };
    }
    }
}