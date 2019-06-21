using System;
using System.Collections.Generic;
using UltricoCalendarContracts.Extensions;
using UltricoCalendarContracts.Models;

namespace UltricoCalendarContracts.Entities
{
    // TODO : HIGH : Looks like external contracts and internal domain concepts are mixed in one repository that is shared
    // everywhere?
    public abstract class CalendarEvent
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; }

        public EventDuration Duration { get; set; }

        public List<string> MailAddresses { get; set; }

        public abstract BaseEventModel ToBaseModel();
    }
}