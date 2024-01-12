using SearchCep.Domain.Models.Gateways.BrasilApi;

namespace SearchCep.Domain.Interfaces.Gateway
{
    public interface IBrasilApiGateway
    {
        public Task<ResponseApi> ResponseAddressByCep(string cep);
    }
}