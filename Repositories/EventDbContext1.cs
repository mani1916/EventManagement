using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManagement.Model;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Repositories
{
    public class EventDbContext1 : DbContext
    {
        public EventDbContext1(DbContextOptions<EventDbContext1> options) : base(options)
        {

        }

        
        public DbSet<User> Users { get; set; }
        public DbSet<Attendee> Attendees { get; set; }
        public DbSet<Event> Events { get; set; }


    }
}