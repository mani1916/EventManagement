using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManagement.DTO;
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

        /// <summary>
        /// This C# async method creates an event record and handles any exceptions that occur during
        /// the process.
        /// </summary>
        /// <param name="Event">The `CreateEventAsync` method is an asynchronous method that takes an
        /// `Event` object named `record` as a parameter. The method attempts to create a new event
        /// using the `_eventManagementService` and returns a boolean indicating whether the creation
        /// was successful. If an exception occurs during the creation process</param>
        /// <returns>
        /// The method `CreateEventAsync` is returning a `Task<bool>`.
        /// </returns>
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

       /// <summary>
       /// This C# async method retrieves all events from an event management service and handles any
       /// exceptions that occur.
       /// </summary>
       /// <returns>
       /// The method `GetAllEventsAsync` returns a `Task` that will eventually yield an
       /// `IEnumerable<Event>` containing all events retrieved from the `_eventManagementService`.
       /// </returns>
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

       /// <summary>
       /// This C# function asynchronously retrieves a collection of past events.
       /// </summary>
        public async Task<IEnumerable<Event>> GetPastEventsAsync()
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

        /// <summary>
        /// This C# async method retrieves future events from a list of all events based on the event
        /// date.
        /// </summary>
        /// <returns>
        /// The `GetFutureEventsAsync` method returns a collection of future events (events with a date
        /// greater than or equal to the current date) as an asynchronous operation.
        /// </returns>
        public async Task<IEnumerable<Event>> GetFutureEventsAsync()
        {
            try
            {
                IEnumerable<Event> allEvents = await _eventManagementService.GetAllAsync();
                IEnumerable<Event> futureEvents = allEvents.Where(e => e.EventDate >= DateTime.Now).ToList();

                return futureEvents;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during GetFutureEventsAsync: {ex.Message}");
                throw new InvalidOperationException("Error retrieving future events. See logs for details.", ex);
            }
        }

       /// <summary>
       /// The DeleteAllEventsAsync method deletes all events asynchronously and handles any exceptions
       /// that occur.
       /// </summary>
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