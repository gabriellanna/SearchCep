using Cep.Domain.Dtos.Address;
using Cep.Domain.Dtos.State;
using Cep.Domain.Interfaces.Gateway;
using Cep.Domain.Interfaces.Repository;
using Cep.Domain.Interfaces.Service;
using Cep.Domain.Models;
using Cep.Domain.Models.Gateways.BrasilApi;

namespace Cep.Service.Service
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
            var streetDbCep = await _repoStreet.GetByCepAsync(cep.Replace("-", ""));
            if(streetDbCep != null && streetDbCep.LastUpdate.AddMonths(3) > DateTime.Now)
            {
                return new AddressResponseDto()
                {
                    City = streetDbCep.Neighborhood.City.Name,
                    CityId = streetDbCep.Neighborhood.City.Id,
                    State = streetDbCep.Neighborhood.City.State.Name,
                    StateId = streetDbCep.Neighborhood.City.State.Id,
                    Neighborhood = streetDbCep.Neighborhood.Name,
                    NeighborhoodId = streetDbCep.Neighborhood.Id,
                    Street = streetDbCep.Name,
                    StreetId = streetDbCep.Id,
                    Cep = streetDbCep.Cep,
                    LastUpdate = streetDbCep.LastUpdate
                };
            }

            if(streetDbCep != null && streetDbCep.LastUpdate.AddMonths(3) < DateTime.Now)
            {
                await _repoStreet.DelteAsync(streetDbCep.Id);
            }
            

            var objAddress = MapperTo(await _gateway.ResponseAddressByCep(cep));
            var stateDb = await _repoState.GetByNameAsync(objAddress.State);
            var cityDb = await _repoCity.GetByNameAsync(objAddress.City);
            var neighborhoodDb = await _repoNeighborhood.GetByNameAsync(objAddress.Neighborhood);
            var streetDb = await _repoStreet.GetByNameAsync(objAddress.Street);

            if(stateDb == null)
                stateDb = await _repoState.InsertAsync(new State(objAddress.State));

            if(cityDb == null)
                cityDb = await _repoCity.InsertAsync(new City(objAddress.City, stateDb.Id));

            if(neighborhoodDb == null)
                neighborhoodDb = await _repoNeighborhood.InsertAsync(new Neighborhood(objAddress.Neighborhood, cityDb.Id));
            
            if(streetDb == null)
                streetDb = await _repoStreet.InsertAsync(new Street(objAddress.Street, objAddress.Cep, neighborhoodDb.Id, DateTime.Now));
                


            objAddress.StateId = stateDb.Id;
            objAddress.CityId = cityDb.Id;
            objAddress.NeighborhoodId = neighborhoodDb.Id;
            objAddress.StreetId = streetDb.Id;
            objAddress.Cep = streetDb.Cep;
            objAddress.LastUpdate = streetDb.LastUpdate;

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

        // private async Task<AddressResponseDto> ValidCepDb(string cep, IStreetRepository repoStreet)
        // {
        //     var streetDbCep = await repoStreet.GetByCepAsync(cep.Replace("-", ""));
        //     if(streetDbCep != null)
        //     {
        //         return new AddressResponseDto()
        //         {
        //             City = streetDbCep.Neighborhood.City.Name,
        //             State = streetDbCep.Neighborhood.City.State.Name,
        //             Neighborhood = streetDbCep.Neighborhood.Name,
        //             Street = streetDbCep.Name,
        //             Cep = streetDbCep.Cep,
        //         };
        //     }
            
        // }
    }
}