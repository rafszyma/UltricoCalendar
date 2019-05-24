using System;
using System.Collections.Generic;
using System.Net.Mail;
using UltricoCalendarContracts.Enums;

namespace UltricoCalendarContracts.Entities
{
    public class EventSeries<T> : CalendarEvent where T : FinishClass
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public RepeatPeriod RepeatEvery { get; set; }
        public DateTime Start { get; set; }
        public TimeSpan Duration { get; set; }
        public T Finish { get; set; }
        public List<MailAddress> MailAddresses { get; set; }
    }
}