using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Model
{
    public class Attendee
    {
        public int AttendeeId { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public DateTime EventDate{get; set;}
        
        
    }
}