using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Model
{
    public class Event
    {
        public int EventId { get; set; }

        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
    }
}