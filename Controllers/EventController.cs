using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EventManagement.Model;
using EventManagement.Services;

namespace EventManagement.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventServices _eventServices;

        public EventController(IEventServices eventServices)
        {
            _eventServices = eventServices;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        /// <summary>
        /// The function GetAllEvents asynchronously retrieves all events and returns them as an
        /// ActionResult.
        /// </summary>
        /// <returns>
        /// The method `GetAllEvents` is returning an `ActionResult` of type `IEnumerable<Event>`. If
        /// the operation is successful, it will return an `Ok` response with the list of events. If an
        /// exception occurs during the operation, it will return a `500 Internal Server Error` response
        /// with an error message.
        /// </returns>
        public async Task<ActionResult<IEnumerable<Event>>> GetAllEvents()
        {
            try
            {
                var events = await _eventServices.GetAllEventsAsync();
                return Ok(events);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during GetAllEvents: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("past")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      /// <summary>
      /// The GetPastEvents function retrieves past events asynchronously and returns them as an
      /// ActionResult.
      /// </summary>
      /// <returns>
      /// The `GetPastEvents` method returns an `ActionResult` containing a collection of `Event`
      /// objects. If the operation is successful, it returns an HTTP 200 OK response with the past
      /// events. If an exception occurs during the operation, it returns an HTTP 500 Internal Server
      /// Error response with an error message.
      /// </returns>
        public async Task<ActionResult<IEnumerable<Event>>> GetPastEvents()
        {
            try
            {
                var pastEvents = await _eventServices.GetPastEventsAsync();
                return Ok(pastEvents);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during GetPastEvents: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("future")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        /// <summary>
        /// The function `GetFutureEvents` retrieves future events asynchronously and returns them as an
        /// ActionResult, handling exceptions by logging errors and returning an Internal Server Error
        /// status code if needed.
        /// </summary>
        /// <returns>
        /// The `GetFutureEvents` method returns an `ActionResult` containing an `IEnumerable` of
        /// `Event` objects. If the operation is successful, it returns a 200 OK response with the
        /// future events. If an exception occurs during the operation, it returns a 500 Internal Server
        /// Error response.
        /// </returns>
        public async Task<ActionResult<IEnumerable<Event>>> GetFutureEvents()
        {
            try
            {
                var futureEvents = await _eventServices.GetFutureEventsAsync();
                return Ok(futureEvents);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during GetFutureEvents: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        /// <summary>
        /// The CreateEvent function in C# asynchronously creates an event record and handles any
        /// exceptions by logging an error message and returning a 500 status code if an exception
        /// occurs.
        /// </summary>
        /// <param name="Event">The `CreateEvent` method is an asynchronous action method that receives
        /// an `Event` object named `record` in the request body. The method calls the
        /// `_eventServices.CreateEventAsync` method to create an event using the provided `record`. If
        /// an exception occurs during the process, it catches the</param>
        /// <returns>
        /// The method `CreateEvent` is returning a `Task<ActionResult<bool>>`. This means it is
        /// returning a task that will eventually produce an `ActionResult<bool>`.
        /// </returns>
        public async Task<ActionResult<bool>> CreateEvent([FromBody] Event record)
        {
            try
            {
                return await _eventServices.CreateEventAsync(record);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during CreateEvent: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("all")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteAllEvents()
        {
            try
            {
                await _eventServices.DeleteAllEventsAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during DeleteAllEvents: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
