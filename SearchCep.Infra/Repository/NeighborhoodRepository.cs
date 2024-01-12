using Microsoft.EntityFrameworkCore;
using SearchCep.Domain.Interfaces.Repository;
using SearchCep.Domain.Models;
using SearchCep.Infra.Context;

namespace SearchCep.Infra.Repository
{
    public class NeighborhoodRepository : BaseRepository<Neighborhood>, INeighborhoodRepository
    {
        public NeighborhoodRepository(SearchCepContext options) : base(options)
        {
        }
        public async Task<Neighborhood> GetByNameAsync(string name)
        {
            var neighborhoodDb = await _dataSet.FirstOrDefaultAsync(neighborhood => neighborhood.Name == name);
            return neighborhoodDb;
        }
    }
}