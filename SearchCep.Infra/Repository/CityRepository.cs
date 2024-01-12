using Microsoft.EntityFrameworkCore;
using SearchCep.Domain.Interfaces.Repository;
using SearchCep.Domain.Models;
using SearchCep.Infra.Context;

namespace SearchCep.Infra.Repository
{
    public class CityRepository : BaseRepository<City>, ICityRepository
    {
        public CityRepository(SearchCepContext options) : base(options)
        {
        }
        public async Task<City> GetByNameAsync(string name)
        {
            var cityDb = await _dataSet.FirstOrDefaultAsync(city => city.Name == name);
            return cityDb;
        }
    }
}