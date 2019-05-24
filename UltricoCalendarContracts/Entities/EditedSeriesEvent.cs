using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace UltricoCalendarContracts.Entities
{
    public class EditedSeriesEvent : CalendarEvent
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public TimeSpan Duration { get; set; }
        public List<MailAddress> MailAddresses { get; set; }
        public int ParentSeriesId { get; set; }
    }
}