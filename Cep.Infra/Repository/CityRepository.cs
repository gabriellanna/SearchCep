using Microsoft.EntityFrameworkCore;
using Cep.Domain.Interfaces.Repository;
using Cep.Domain.Models;
using Cep.Infra.Context;

namespace Cep.Infra.Repository
{
    public class CityRepository : BaseRepository<City>, ICityRepository
    {
        public CityRepository(CepContext options) : base(options)
        {
        }
        public async Task<City> GetByNameAsync(string name)
        {
            var cityDb = await _dataSet.FirstOrDefaultAsync(city => city.Name == name);
            return cityDb;
        }

        public Task<IList<City>> GetCitiesByState(string state)
        {
            throw new NotImplementedException();
        }
    }
}