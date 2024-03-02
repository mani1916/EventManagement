using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EventManagement.Model;
using EventManagement.Services;

namespace EventManagement.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> CreateUser(IFormFile file)
        {
            try
            {
                System.Console.WriteLine("fii");
                var result = await _userService.CreateUserAsync(file);
                return CreatedAtAction(nameof(CreateUser), result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during CreateUser: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during GetAllUsers: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("all")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteAllUsers()
        {
            try
            {
                await _userService.DeleteUserAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during DeleteAllUsers: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

    }
}
