using Cep.Domain.Models;

namespace Cep.Domain.Interfaces.Repository
{
    public interface ICityRepository : IBaseRepository<City>
    {
        public Task<City> GetByNameAsync(string name);
    }
}