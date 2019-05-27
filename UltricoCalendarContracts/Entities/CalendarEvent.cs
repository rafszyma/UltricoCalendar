using System;
using System.Collections.Generic;
using System.Net.Mail;
using UltricoCalendarContracts.Extensions;

namespace UltricoCalendarContracts.Entities
{
    public abstract class CalendarEvent
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public DateTime Start { get; set; }
        
        public EventDuration Duration { get; set; }
        
        public List<MailAddress> MailAddresses { get; set; }
    }
}