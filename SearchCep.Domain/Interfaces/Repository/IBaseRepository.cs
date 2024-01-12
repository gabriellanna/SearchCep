using SearchCep.Domain.Models;

namespace SearchCep.Domain.Interfaces.Repository
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        public Task<T> InsertAsync(T item);
        public Task<T> UpdateAsync(T item);
        public Task<bool> DelteAsync(int id);
        public Task<IList<T>> GetAllAsync();
    }
}