using System;
using System.Collections.Generic;
using UltricoCalendarContracts.Entities;
using UltricoCalendarContracts.Interfaces;

namespace UltricoCalendarContracts.Models
{
    public abstract class BaseEventModel : ICalendarEvent
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; }

        public string Duration { get; set; }

        public List<string> MailAddresses { get; set; }

        public abstract CalendarEvent ToEntity();
    }
}