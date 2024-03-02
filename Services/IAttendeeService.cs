using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManagement.Model;

namespace EventManagement.Services
{
    public interface IAttendeeService
    {
        Task<List<Attendee>> CreateAttendeeAsync(IFormFile file);
        Task<IEnumerable<Attendee>> GetAllAttendeesAsync();
        Task DeleteAllAttendeesAsync();
    }
}