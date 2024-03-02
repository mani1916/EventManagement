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
