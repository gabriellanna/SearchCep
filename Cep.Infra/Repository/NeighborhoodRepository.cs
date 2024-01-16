using Microsoft.EntityFrameworkCore;
using Cep.Domain.Interfaces.Repository;
using Cep.Domain.Models;
using Cep.Infra.Context;

namespace Cep.Infra.Repository
{
    public class NeighborhoodRepository : BaseRepository<Neighborhood>, INeighborhoodRepository
    {
        public NeighborhoodRepository(CepContext options) : base(options)
        {
        }
        public async Task<Neighborhood> GetByNameAsync(string name, string cityName, string stateName)
        {
            var neighborhoodDb = await _dataSet
                .Include(x => x.City)
                    .ThenInclude(x => x.State)
                .FirstOrDefaultAsync(neighborhood =>
                    neighborhood.Name == name &&
                    neighborhood.City.Name == cityName &&
                    neighborhood.City.State.Name == stateName);
            return neighborhoodDb;
        }
    }
}