using Microsoft.EntityFrameworkCore;
using SearchCep.Domain.Interfaces.Repository;
using SearchCep.Domain.Models;
using SearchCep.Infra.Context;

namespace SearchCep.Infra.Repository
{
    public class StateRepository : BaseRepository<State>, IStateRepository
    {
        public StateRepository(SearchCepContext options) : base(options)
        {
        }
        public async Task<State> GetByNameAsync(string name)
        {
            return await _dataSet.FirstOrDefaultAsync(state => state.Name == name);
        }
        public async Task<IList<State>> GetAllStateAsync()
        {
            return await _dataSet.Include(state => state.Cities).ToListAsync();
        }
    }
}