using Cep.Domain.Models;

namespace Cep.Domain.Interfaces.Repository
{
    public interface INeighborhoodRepository : IBaseRepository<Neighborhood>
    {
        public Task<Neighborhood> GetByNameAsync(string name);
    }
}