using Microsoft.EntityFrameworkCore;
using Cep.Domain.Interfaces.Repository;
using Cep.Domain.Models;
using Cep.Infra.Context;

namespace Cep.Infra.Repository
{
    public class StreetRepository : BaseRepository<Street>, IStreetRepository
    {
        public StreetRepository(CepContext options) : base(options)
        {
        }

        public async Task<Street> GetByCepAsync(string cep)
        {
            return await _dataSet
                .Include(x => x.Neighborhood)
                    .ThenInclude(x => x.City)
                        .ThenInclude(x => x.State)
                .FirstOrDefaultAsync(street => street.Cep == cep);
        }

        public async Task<Street> GetByNameAsync(string name, string neighborhood, string city, string state)
        {
            return await _dataSet
                .Include(x => x.Neighborhood)
                        .ThenInclude(x => x.City)
                            .ThenInclude(x => x.State)
                .FirstOrDefaultAsync(street =>
                    street.Name == name && 
                    street.Neighborhood.Name == neighborhood &&
                    street.Neighborhood.City.Name == city &&
                    street.Neighborhood.City.State.Name == state
                );
        }
    }
}