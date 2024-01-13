using Cep.Domain.Models.Gateways.BrasilApi;

namespace Cep.Domain.Interfaces.Gateway
{
    public interface IBrasilApiGateway
    {
        public Task<ResponseApi> ResponseAddressByCep(string cep);
    }
}