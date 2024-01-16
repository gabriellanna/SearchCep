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
        public async Task<City> GetByNameAsync(string name, string stateName)
        {
            var cityDb = await _dataSet.Include(x => x.State).FirstOrDefaultAsync(city => city.Name == name && city.State.Name == stateName);
            return cityDb;
        }

        public async Task<IList<City>> GetCitiesByStateId(int stateId)
        {
            return await _dataSet.Where(x => x.StateId == stateId).ToListAsync();
        }
    }
}