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
       /// <summary>
       /// This C# function asynchronously creates an attendee list from a file input and handles
       /// exceptions appropriately.
       /// </summary>
       /// <param name="IFormFile">`IFormFile` is a type in ASP.NET Core used to represent a file sent
       /// with an HTTP request. In the context of the provided code snippet, it is used as a parameter
       /// for the `CreateAttendee` method to receive a file that contains attendee information.</param>
       /// <returns>
       /// The `CreateAttendee` method returns an `ActionResult` containing a `List<Attendee>`. If the
       /// result is `null`, it returns an `Ok` response with the message "There is no User have
       /// conflicts". If there is an exception during the process, it returns a `StatusCode` of 500
       /// with the message "Internal Server Error".
       /// </returns>
        public async Task<ActionResult<List<Attendee>>> CreateAttendee(IFormFile file)
        {
            try
            {
                List<Attendee> result = await _attendeeService.CreateAttendeeAsync(file);
                if(result==null)
                return Ok("There is no User have conflicts");
                return Ok(result);

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
        /// <summary>
        /// The function GetAllAttendees asynchronously retrieves a list of attendees and handles
        /// potential exceptions.
        /// </summary>
        /// <returns>
        /// The `GetAllAttendees` method returns an `ActionResult` containing either an
        /// `IEnumerable<Attendee>` if attendees are retrieved successfully, or a message "The Attendee
        /// List is empty" if the list is empty. If an exception occurs, it returns a status code 500
        /// with the message "Internal Server Error".
        /// </returns>
        public async Task<ActionResult<IEnumerable<Attendee>>> GetAllAttendees()
        {
            try
            {
                IEnumerable<Attendee> attendees = await _attendeeService.GetAllAttendeesAsync();
                if(attendees==null)
                return Ok("The Attendee List is empty");
                return Ok(attendees);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during GetAllAttendees: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // [HttpDelete("all")]
        // [ProducesResponseType(StatusCodes.Status204NoContent)]
        // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        // public async Task<ActionResult> DeleteAllEvents()
        // {
        //     try
        //     {
        //         await _attendeeService.DeleteAllAttendeesAsync();
        //         return NoContent();
        //     }
        //     catch (Exception ex)
        //     {
        //         Console.WriteLine($"Error during DeleteAllEvents: {ex.Message}");
        //         return StatusCode(500, "Internal Server Error");
        //     }
        // }
    }
}
