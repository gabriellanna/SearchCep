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
        public async Task<Neighborhood> GetByNameAsync(string name)
        {
            var neighborhoodDb = await _dataSet.FirstOrDefaultAsync(neighborhood => neighborhood.Name == name);
            return neighborhoodDb;
        }
    }
}