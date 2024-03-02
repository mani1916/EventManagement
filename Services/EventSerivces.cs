using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManagement.Model;
using EventManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.Services
{
    public class EventSerivces : IEventServices
    {
        private readonly IEventManagementService<Event> _eventManagementService;

        public EventSerivces(IEventManagementService<Event> eventManagementService)
        {
            _eventManagementService = eventManagementService;
        }

        public async Task<bool> CreateEventAsync(Event record)
        {
            try
            {
                return await _eventManagementService.CreateAsync(record);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during CreateEventAsync: {ex.Message}");
                throw new InvalidOperationException("Error creating event. See logs for details.", ex);
            }
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            try
            {
                return await _eventManagementService.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during GetAllEventsAsync: {ex.Message}");
                throw new InvalidOperationException("Error retrieving all events. See logs for details.", ex);
            }
        }

        public async Task<List<Event>> GetPastEventsAsync()
        {
            try
            {
                IEnumerable<Event> allEvents = await _eventManagementService.GetAllAsync();
                List<Event> pastEvents = allEvents.Where(e => e.EventDate < DateTime.Now).ToList();
                return pastEvents;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during GetPastEventsAsync: {ex.Message}");
                throw new InvalidOperationException("Error retrieving past events. See logs for details.", ex);
            }
        }

        public async Task<List<Event>> GetFutureEventsAsync()
        {
            try
            {
                IEnumerable<Event> allEvents = await _eventManagementService.GetAllAsync();
                var futureEvents = allEvents.Where(e => e.EventDate >= DateTime.Now).ToList();
                return futureEvents;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during GetFutureEventsAsync: {ex.Message}");
                throw new InvalidOperationException("Error retrieving future events. See logs for details.", ex);
            }
        }

        public async Task DeleteAllEventsAsync()
        {
            try
            {
                await _eventManagementService.DeleteAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during DeleteAllEventsAsync: {ex.Message}");
                throw new InvalidOperationException("Error deleting all events. See logs for details.", ex);
            }
        }
    }
}