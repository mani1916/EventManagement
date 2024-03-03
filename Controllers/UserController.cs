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
       /// <summary>
       /// This C# function asynchronously creates a user using a file input and handles exceptions by
       /// logging errors.
       /// </summary>
       /// <param name="IFormFile">`IFormFile` is an interface used in ASP.NET Core to represent a file
       /// sent with the HttpRequest. It provides properties and methods to access information about the
       /// file such as the file name, content type, and content. In the provided code snippet, the
       /// `CreateUser` method takes an `</param>
       /// <returns>
       /// The `CreateUser` method is returning an `ActionResult<bool>`. Inside the method, it is trying
       /// to create a user asynchronously by calling the `_userService.CreateUserAsync(file)` method.
       /// If successful, it returns the result of the creation action using `CreatedAtAction` with the
       /// name of the `CreateUser` method. If an exception occurs during the user creation process, it
       /// catches the exception,
       /// </returns>
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
