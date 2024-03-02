using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManagement.Model;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Repositories
{
    public class AttendeeRepository : IAttendeeRepository
    {
        private readonly EventDbContext1 _eventDbContext1;
        public AttendeeRepository(EventDbContext1 eventDbContext1)
        {
            _eventDbContext1 = eventDbContext1;
        }
        public async Task<bool> GetConflictsAsync(int eventId, int userId, DateTime eventDate)
        {
            return await _eventDbContext1.Attendees.AnyAsync(a => a.UserId == userId && a.EventDate == eventDate);
        }

    }
}