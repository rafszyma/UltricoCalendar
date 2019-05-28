using System;
using System.Collections.Generic;

namespace UltricoCalendarContracts.Models
{
    public class EventMetadata
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public IEnumerable<DateTime> Start { get; set; }

        public TimeSpan Duration { get; set; }
    }
}