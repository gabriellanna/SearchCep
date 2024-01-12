using SearchCep.Domain.Dtos.Address;
using SearchCep.Domain.Interfaces.Gateway;
using SearchCep.Domain.Interfaces.Repository;
using SearchCep.Domain.Interfaces.Service;
using SearchCep.Domain.Models;
using SearchCep.Domain.Models.Gateways.BrasilApi;

namespace SearchCep.Service.Service
{
    public class AddressService : IAddressService
    {
        private readonly IBrasilApiGateway _gateway;
        private readonly IStateRepository _repoState;
        private readonly ICityRepository _repoCity;
        private readonly INeighborhoodRepository _repoNeighborhood;
        private readonly IStreetRepository _repoStreet;

        public AddressService(IBrasilApiGateway gateway, IStateRepository repoState, ICityRepository repoCity, INeighborhoodRepository repoNeighborhood, IStreetRepository repoStreet)
        {
            _gateway = gateway;
            _repoState = repoState;
            _repoCity = repoCity;
            _repoNeighborhood = repoNeighborhood;
            _repoStreet = repoStreet;
        }

        public async Task<AddressResponseDto> GetCep(string cep)
        {
            var objAddress = MapperTo(await _gateway.ResponseAddressByCep(cep));
            var stateDb = await _repoState.GetByNameAsync(objAddress.State);
            var cityDb = await _repoCity.GetByNameAsync(objAddress.City);
            var neighborhoodDb = await _repoNeighborhood.GetByNameAsync(objAddress.Neighborhood);
            var streetDb = await _repoStreet.GetByNameAsync(objAddress.Street);

            if(stateDb == null)
                stateDb = await _repoState.InsertAsync(new State(objAddress.State));

            if(cityDb == null)
                cityDb = await _repoCity.InsertAsync(new City(objAddress.City));

            if(neighborhoodDb == null)
                neighborhoodDb = await _repoNeighborhood.InsertAsync(new Neighborhood(objAddress.Neighborhood));
            
            if(streetDb == null)
                streetDb = await _repoStreet.InsertAsync(new Street(objAddress.Street, objAddress.Cep));
                


            objAddress.StateId = stateDb.Id;
            objAddress.CityId = cityDb.Id;
            objAddress.NeighborhoodId = neighborhoodDb.Id;
            objAddress.StreetId = streetDb.Id;
            objAddress.Cep = streetDb.Cep;

            return objAddress;
        }
        public async Task<IList<State>> GetAllState()
        {
            return await _repoState.GetAllStateAsync();
        }

        public Task<IList<City>> GetAllCity()
        {
            throw new NotImplementedException();
        }

        public Task<IList<Neighborhood>> GetAllNeighborhood()
        {
            throw new NotImplementedException();
        }
        public Task<IList<Street>> GetAllStreet()
        {
            throw new NotImplementedException();
        }

        private AddressResponseDto MapperTo(ResponseApi response)
        {
            return new AddressResponseDto()
            {
                City = response.City,
                State = response.State,
                Neighborhood = response.Neighborhood,
                Street = response.Street,
                Cep = response.Cep,
            };
        }
    }
}