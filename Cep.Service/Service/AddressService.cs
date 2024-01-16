using Cep.Domain.Dtos.Address;
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
        private const int updateAddress = 3; 

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
            if (streetDbCep != null && streetDbCep.LastUpdate.AddMonths(updateAddress) > DateTime.Now)
                return MapperTo(streetDbCep);



            var responseGateway = await _gateway.ResponseAddressByCep(cep);
            if (responseGateway == null && streetDbCep != null)
                return MapperTo(streetDbCep);

            if (responseGateway == null && streetDbCep == null)
                return null;

            var stateDb = await _repoState.GetByNameAsync(responseGateway.State);
            var cityDb = await _repoCity.GetByNameAsync(responseGateway.City, responseGateway.State);
            var neighborhoodDb = await _repoNeighborhood.GetByNameAsync(responseGateway.Neighborhood, responseGateway.City, responseGateway.State);


            if (streetDbCep != null && streetDbCep.LastUpdate.AddMonths(updateAddress) < DateTime.Now)
            {
                var streetUpdate =  new Street(){
                    Id = streetDbCep.Id,
                    Cep = streetDbCep.Cep,
                    Name = responseGateway.Street,
                    LastUpdate =  DateTime.Now,
                    Neighborhood = neighborhoodDb ?? new Neighborhood()
                    {
                        Name = responseGateway.Neighborhood,
                        City = cityDb ?? new City()
                        {
                            Name = responseGateway.City,
                            State = stateDb ?? new State(){
                                Name = responseGateway.State
                            },
                            StateId = stateDb?.Id ?? 0
                        },
                        CityId = cityDb?.Id ?? 0
                    },
                    NeighborhoodId = neighborhoodDb?.Id ?? 0
                };
                streetUpdate = await _repoStreet.UpdateAsync(streetUpdate);
                return MapperTo(streetUpdate);
            }

            var streetDb = await _repoStreet.GetByNameAsync(
                responseGateway.Street, 
                responseGateway.Neighborhood, 
                responseGateway.City, 
                responseGateway.State
            );

            var objAddress = MapperTo(responseGateway);

            if (stateDb == null)
                stateDb = await _repoState.InsertAsync(new State(objAddress.State));

            if (cityDb == null)
                cityDb = await _repoCity.InsertAsync(new City(objAddress.City, stateDb.Id));

            if (neighborhoodDb == null)
                neighborhoodDb = await _repoNeighborhood.InsertAsync(new Neighborhood(objAddress.Neighborhood, cityDb.Id));

            if (streetDb == null)
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

        private AddressResponseDto MapperTo(Street street)
        {
            return new AddressResponseDto()
            {
                City = street.Neighborhood.City.Name,
                CityId = street.Neighborhood.City.Id,
                State = street.Neighborhood.City.State.Name,
                StateId = street.Neighborhood.City.State.Id,
                Neighborhood = street.Neighborhood.Name,
                NeighborhoodId = street.Neighborhood.Id,
                Street = street.Name,
                StreetId = street.Id,
                Cep = street.Cep,
                LastUpdate = street.LastUpdate
            };
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

        private async Task<AddressResponseDto> ValidCepDb(string cep, IStreetRepository repoStreet)
        {
            var streetDbCep = await repoStreet.GetByCepAsync(cep.Replace("-", ""));
            if (streetDbCep != null)
            {
                return new AddressResponseDto()
                {
                    City = streetDbCep.Neighborhood.City.Name,
                    State = streetDbCep.Neighborhood.City.State.Name,
                    Neighborhood = streetDbCep.Neighborhood.Name,
                    Street = streetDbCep.Name,
                    Cep = streetDbCep.Cep,
                };
            }

            return new();

        }
    }
}