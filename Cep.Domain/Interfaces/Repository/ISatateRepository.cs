using Cep.Domain.Models;

namespace Cep.Domain.Interfaces.Repository
{
    public interface IStateRepository : IBaseRepository<State>
    {
        public Task<State> GetByNameAsync(string name);
        public Task<IList<State>> GetAllStateAsync();
    }
}