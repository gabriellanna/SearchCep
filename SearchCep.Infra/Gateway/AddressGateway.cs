using Newtonsoft.Json;
using SearchCep.Domain.Interfaces.Gateway;
using SearchCep.Domain.Models.Gateways.BrasilApi;

namespace SearchCep.Infra.Gateway
{
    public class BrasilApiGateway : IBrasilApiGateway
    {
        private readonly HttpClient _client;
        public BrasilApiGateway(HttpClient client)
        {
            _client = client;
        }
        public async Task<ResponseApi> ResponseAddressByCep(string cep)
        {

            var responseApi = await _client.GetAsync($"https://brasilapi.com.br/api/cep/v1/{cep}");
            if (responseApi.IsSuccessStatusCode)
            {
                var contentResponse = await responseApi.Content.ReadAsStringAsync();
                var objResponse = JsonConvert.DeserializeObject<ResponseApi>(contentResponse);

                return objResponse;
            }

            return new();

        }
    }
}