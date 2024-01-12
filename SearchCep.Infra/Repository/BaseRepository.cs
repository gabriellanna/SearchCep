using Microsoft.EntityFrameworkCore;
using SearchCep.Domain.Interfaces.Repository;
using SearchCep.Domain.Models;
using SearchCep.Infra.Context;

namespace SearchCep.Infra.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly SearchCepContext _context;
        protected DbSet<T> _dataSet;

        public BaseRepository(SearchCepContext context)
        {
            _context = context;
            _dataSet = _context.Set<T>();
        }

        public async Task<bool> DelteAsync(int id)
        {
            try
            {
                var itemDb = await _dataSet.FirstOrDefaultAsync(item => item.Id == id);
                _dataSet.Remove(itemDb);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IList<T>> GetAllAsync()
        {
            try
            {
                var itemsDb = await _dataSet.ToListAsync();
                return itemsDb;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T> InsertAsync(T item)
        {
            try
            {
                _dataSet.Add(item);
                await _context.SaveChangesAsync();
                
                return item;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T> UpdateAsync(T item)
        {
            try
            {
                _dataSet.Update(item);
                await _context.SaveChangesAsync();

                return item;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}