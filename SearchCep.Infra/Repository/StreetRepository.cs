using Microsoft.EntityFrameworkCore;
using SearchCep.Domain.Interfaces.Repository;
using SearchCep.Domain.Models;
using SearchCep.Infra.Context;

namespace SearchCep.Infra.Repository
{
    public class StreetRepository : BaseRepository<Street>, IStreetRepository
    {
        public StreetRepository(SearchCepContext options) : base(options)
        {
        }
        public async Task<Street> GetByNameAsync(string name)
        {
            var streetDb = await _dataSet.FirstOrDefaultAsync(street => street.Name == name);
            return streetDb;
        }
    }
}