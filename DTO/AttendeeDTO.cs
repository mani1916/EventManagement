using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.DTO
{
    public class AttendeeDTO
    {
        public int AttendeeId { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public DateTime EventDate { get; set; }
    }
}