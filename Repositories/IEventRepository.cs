using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManagement.Model;

namespace EventManagement.Repositories
{
    public interface IEventRepository<T>
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> CreateAsync(T record);
        Task UpdateAsync(T record);
        Task DeleteAsync(T record);
        Task DeleteAllAsync();
        Task<bool> ExistsAsync(int id);
    }
}