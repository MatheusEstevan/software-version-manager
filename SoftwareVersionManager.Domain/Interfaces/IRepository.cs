using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareVersionManager.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> ListAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<int> DeleteAsync(int id);
        Task RemoveRangeAsync(ICollection<T> items);
    }
}
