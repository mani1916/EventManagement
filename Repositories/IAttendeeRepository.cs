using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManagement.Model;

namespace EventManagement.Repositories
{
    public interface IAttendeeRepository
    {
        Task<bool> GetConflictsAsync(int eventId, int userId, DateTime eventDate);
    }
}