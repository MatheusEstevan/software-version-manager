using System.Collections.Generic;
using System.Threading.Tasks;
using SoftwareVersionManager.Domain.Interfaces;

namespace SoftwareVersionManager.Domain.Generic
{
    public abstract class BaseService<TEntity> : IService<TEntity>
        where TEntity : class
    {
        protected readonly IRepository<TEntity> _repository;

        public BaseService(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public virtual async Task<IEnumerable<TEntity>> ListAsync() => await _repository.ListAsync();
        public virtual async Task<TEntity> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);
        public virtual async Task<TEntity> CreateAsync(TEntity entity) => await _repository.CreateAsync(entity);
        public virtual async Task<TEntity> UpdateAsync(TEntity entity) => await _repository.UpdateAsync(entity);
        public virtual async Task<int> DeleteAsync(int id) => await _repository.DeleteAsync(id);
    }
}
