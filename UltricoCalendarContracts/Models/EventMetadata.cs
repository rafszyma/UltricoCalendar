using System;
using UltricoCalendarContracts.Extensions;

namespace UltricoCalendarContracts.Models
{
    public class EventMetadata
    {
        public string Title { get; set; }
        
        public DateTime Start { get; set; }
        
        public EventDuration Duration { get; set; }
    }
}