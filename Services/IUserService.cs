using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManagement.Model;

namespace EventManagement.Services
{
    public interface IUserService
    {
        Task<bool> CreateUserAsync(IFormFile file);
        Task DeleteUserAsync();
        Task<IEnumerable<User>> GetAllUsersAsync();
    }
}