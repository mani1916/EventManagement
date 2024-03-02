using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.DTO
{
    public class EventDTO
    {
        public int EventId { get; set; }

        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
    }
}