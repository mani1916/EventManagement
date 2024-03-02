using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using EventManagement.Model;

namespace EventManagement.Services
{
    public class UserService : IUserService
    {
        private readonly IUploadHandler<User> _useruploadHandler;
        private readonly IEventManagementService<User> _eventManagementService;

        public UserService(IUploadHandler<User> useruploadHandler, IEventManagementService<User> eventManagementService)
        {
            _useruploadHandler = useruploadHandler;
            _eventManagementService = eventManagementService;
        }

        public async Task<bool> CreateUserAsync(IFormFile file)
        {
            try
            {
                
                string pathToFile = _useruploadHandler.UploadFile(file);
                List<User> users = await _useruploadHandler.ReadDetailsFromExcel(pathToFile);

                foreach (User user in users)
                {
                    System.Console.WriteLine(user);
                    await _eventManagementService.CreateAsync(user);
                }

                _useruploadHandler.DeleteFile(pathToFile);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during CreateUserAsync: {ex.Message}");
                throw new InvalidOperationException("Error creating users. See logs for details.", ex);
            }
        }
        public async Task DeleteUserAsync()
        {
            try
            {
                 await _eventManagementService.DeleteAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during DeleteUserAsync: {ex.Message}");
                throw new InvalidOperationException($"Error deleting users details.", ex);
            }
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            try
            {
                return await _eventManagementService.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during GetAllUsersAsync: {ex.Message}");
                throw new InvalidOperationException("Error retrieving all users. See logs for details.", ex);
            }
        }
    }
}
