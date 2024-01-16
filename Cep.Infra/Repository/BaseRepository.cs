using Microsoft.EntityFrameworkCore;
using Cep.Domain.Interfaces.Repository;
using Cep.Domain.Models;
using Cep.Infra.Context;

namespace Cep.Infra.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly CepContext _context;
        protected DbSet<T> _dataSet;

        public BaseRepository(CepContext context)
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
                var itemDb = await _dataSet.FirstOrDefaultAsync(entity => entity.Id == item.Id);
                _dataSet.Entry(itemDb).CurrentValues.SetValues(item);
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