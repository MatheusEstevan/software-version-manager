using Microsoft.EntityFrameworkCore;
using SoftwareVersionManager.Data.Context;
using SoftwareVersionManager.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareVersionManager.Data.Generic
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly SoftwareVersionManagerDbContext _dbContext;

        public BaseRepository(SoftwareVersionManagerDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        public virtual async Task<IEnumerable<T>> ListAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<List<T>> CreateRangeAsync(List<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
            return entities;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<int> DeleteAsync(int id)
        {
            T entity = await _dbContext.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _dbContext.Set<T>().Remove(entity);
                return await _dbContext.SaveChangesAsync();
            }
            return -1;
        }

        public virtual async Task RemoveRangeAsync(ICollection<T> items)
        {
            _dbContext.Set<T>().RemoveRange(items);
            await _dbContext.SaveChangesAsync();
        }
    }
}
