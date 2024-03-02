using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManagement.Repositories;

namespace EventManagement.Services
{
    public class EventManagementService<T> : IEventManagementService<T> where T : class
    {
        public readonly IEventRepository<T> _eventRepository;
        public readonly IAttendeeRepository _attendeeRepository;

        public EventManagementService(IEventRepository<T> eventRepository,IAttendeeRepository attendeeRepository)
        {
            _eventRepository = eventRepository;
            _attendeeRepository=attendeeRepository;
        }
        public async Task<T> GetByIdAsync(int id)
        {
            try
            {
                return await _eventRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during GetByIdAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await _eventRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during GetAllAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> CreateAsync(T entity)
        {
            try
            {
                return await _eventRepository.CreateAsync(entity);
            
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during CreateAsync: {ex.Message}");
                throw;
            }
        }
        public async Task DeleteAllAsync()
        {
            try
            {
                await _eventRepository.DeleteAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during DeleteAllAsync: {ex.Message}");
                throw;
            }
        }
        public async Task<bool> UserExistsAsync(int userId)
        {
            try
            {
                return await _eventRepository.ExistsAsync(userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during UserExistsAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> EventExistsAsync(int eventId)
        {
            try
            {
                return await _eventRepository.ExistsAsync(eventId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during EventExistsAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> GetConflictsAsync(int eventId, int userId, DateTime eventDate)
        {
            return await _attendeeRepository.GetConflictsAsync(eventId,userId,eventDate);
        }
    }

}