using SearchCep.Domain.Models;

namespace SearchCep.Domain.Interfaces.Repository
{
    public interface IStreetRepository : IBaseRepository<Street>
    {
        public Task<Street> GetByNameAsync(string name);
    }
}