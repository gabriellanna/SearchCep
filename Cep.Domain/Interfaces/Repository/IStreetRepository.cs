using Cep.Domain.Models;

namespace Cep.Domain.Interfaces.Repository
{
    public interface IStreetRepository : IBaseRepository<Street>
    {
        public Task<Street> GetByNameAsync(string name);
        public Task<Street> GetByCepAsync(string cep);
    }
}