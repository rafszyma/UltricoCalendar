using System;
using System.Collections.Generic;
using System.Net.Mail;
using UltricoCalendarContracts.Extensions;

namespace UltricoCalendarContracts.Models
{
    public class ScheduleEvent
    {
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public DateTime Start { get; set; }
        
        public TimeSpan Duration { get; set; }
        
        public List<MailAddress> MailAddresses { get; set; }
    }
}