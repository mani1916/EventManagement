using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EventManagement.Model;
using EventManagement.Services;

namespace EventManagement.Controllers
{
    [Route("api/attendees")]
    [ApiController]
    public class AttendeeController : ControllerBase
    {
        private readonly IAttendeeService _attendeeService;

        public AttendeeController(IAttendeeService attendeeService)
        {
            _attendeeService = attendeeService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> CreateAttendee(IFormFile file)
        {
            try
            {
                var result = await _attendeeService.CreateAttendeeAsync(file);
                return CreatedAtAction(nameof(CreateAttendee), result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during CreateAttendee: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Attendee>>> GetAllAttendees()
        {
            try
            {
                var attendees = await _attendeeService.GetAllAttendeesAsync();
                return Ok(attendees);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during GetAllAttendees: {ex.Message}");
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
                await _attendeeService.DeleteAllAttendeesAsync();
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
