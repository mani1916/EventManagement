using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManagement.Model;

namespace EventManagement.Services
{
    public interface IEventServices
    {
        Task<bool> CreateEventAsync(Event record);
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<IEnumerable<Event>> GetPastEventsAsync();
        Task<IEnumerable<Event>> GetFutureEventsAsync();
        Task DeleteAllEventsAsync();
    }
}