using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftwareVersionManager.Domain.Interfaces
{
    public interface IService<T> where T : class
    {
        Task<IEnumerable<T>> ListAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<int> DeleteAsync(int id);
    }
}
