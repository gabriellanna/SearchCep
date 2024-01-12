using SearchCep.Domain.Models;

namespace SearchCep.Domain.Interfaces.Repository
{
    public interface INeighborhoodRepository : IBaseRepository<Neighborhood>
    {
        public Task<Neighborhood> GetByNameAsync(string name);
    }
}