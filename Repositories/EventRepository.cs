using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManagement.Model;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Repositories
{
    public class EventRepository<T> : IEventRepository<T> where T : class
    {
        private readonly EventDbContext _eventDbContext;
        private readonly EventDbContext1 _eventDbContext1;
        public EventRepository(EventDbContext1 eventDbContext1)
        {
            _eventDbContext1 = eventDbContext1;
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _eventDbContext1.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _eventDbContext1.Set<T>().ToListAsync();
        }

        public async Task<bool> CreateAsync(T record)
        {
            _eventDbContext1.Set<T>().Add(record);
            await _eventDbContext1.SaveChangesAsync();
            return true;
        }
        public async Task UpdateAsync(T record)
        {
            _eventDbContext1.Set<T>().Update(record);
            await _eventDbContext1.SaveChangesAsync();
        }

        public async Task DeleteAsync(T record)
        {
            _eventDbContext1.Set<T>().Remove(record);
            await _eventDbContext1.SaveChangesAsync();
        }

        public async Task DeleteAllAsync()
        {
            var entities = await _eventDbContext1.Set<T>().ToListAsync();
            _eventDbContext1.Set<T>().RemoveRange(entities);
            await _eventDbContext1.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var primaryKeyProperty = typeof(T).GetProperties()
             .FirstOrDefault(prop => prop.Name.ToLower() == "id" || prop.Name.ToLower() == $"{typeof(T).Name.ToLower()}id");

            if (primaryKeyProperty == null)
            {
                throw new InvalidOperationException($"Entity does not have a property named '{typeof(T).Name}Id' or 'Id'.");
            }

            return await _eventDbContext1.Set<T>().AnyAsync(e => (int)primaryKeyProperty.GetValue(e) == id);
        }
    }
}