using Moq;
using Newtonsoft.Json;
using SearchCep.Domain.Interfaces.Gateway;
using SearchCep.Domain.Models.Gateways.BrasilApi;

namespace SearchCep.Tests.Doubles
{
    public class AddressGatewayMock
    {
        private readonly Mock<IBrasilApiGateway> mock = new();

        public IBrasilApiGateway Object => mock.Object;
        public AddressGatewayMock SetupGetAndInsertCep(string response)
        {
            mock.Setup(x => x.ResponseAddressByCep(It.IsAny<string>()))
                .ReturnsAsync(JsonConvert.DeserializeObject<ResponseApi>(response)?? new());
            return this;
        }
    }
}