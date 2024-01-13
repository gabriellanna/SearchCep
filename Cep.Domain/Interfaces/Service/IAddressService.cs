using Cep.Domain.Dtos.Address;
using Cep.Domain.Models;

namespace Cep.Domain.Interfaces.Service
{
    public interface IAddressService
    {
        public Task<AddressResponseDto> GetCep(string cep);
        public Task<IList<State>> GetAllState();
        public Task<IList<City>> GetAllCity();
        public Task<IList<Neighborhood>> GetAllNeighborhood();
        public Task<IList<Street>> GetAllStreet();
    }
}