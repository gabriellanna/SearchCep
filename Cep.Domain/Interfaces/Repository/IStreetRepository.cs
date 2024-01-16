using Cep.Domain.Models;

namespace Cep.Domain.Interfaces.Repository
{
    public interface IStreetRepository : IBaseRepository<Street>
    {
        public Task<Street> GetByNameAsync(string name, string neighborhood, string city, string state);
        public Task<Street> GetByCepAsync(string cep);
    }
}