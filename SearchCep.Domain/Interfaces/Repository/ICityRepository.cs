using SearchCep.Domain.Models;

namespace SearchCep.Domain.Interfaces.Repository
{
    public interface ICityRepository : IBaseRepository<City>
    {
        public Task<City> GetByNameAsync(string name);
    }
}