using Microsoft.EntityFrameworkCore;
using Cep.Domain.Interfaces.Repository;
using Cep.Domain.Models;
using Cep.Infra.Context;

namespace Cep.Infra.Repository
{
    public class StateRepository : BaseRepository<State>, IStateRepository
    {
        public StateRepository(CepContext options) : base(options)
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