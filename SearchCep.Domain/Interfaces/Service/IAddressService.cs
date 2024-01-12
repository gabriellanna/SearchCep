using SearchCep.Domain.Dtos.Address;
using SearchCep.Domain.Models;

namespace SearchCep.Domain.Interfaces.Service
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