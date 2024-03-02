using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Services
{
    public interface IEventManagementService<T>
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> CreateAsync(T entity);
        Task DeleteAllAsync();
        Task<bool> UserExistsAsync(int userId);
        Task<bool> EventExistsAsync(int eventId);
        Task<bool> GetConflictsAsync(int eventId, int userId, DateTime eventDate);
    }
}