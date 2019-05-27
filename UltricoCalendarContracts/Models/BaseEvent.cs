using System;
using System.Collections.Generic;
using System.Net.Mail;
using UltricoCalendarContracts.Entities;
using UltricoCalendarContracts.Extensions;
using UltricoCalendarContracts.Interfaces;

namespace UltricoCalendarContracts.Models
{
    public abstract class BaseEvent : ICalendarEvent
    {
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public DateTime Start { get; set; }
        
        public TimeSpan Duration { get; set; }
        
        public List<MailAddress> MailAddresses { get; set; }
        
        public EventMetadata ToMetadata()
        {
            return new EventMetadata
            {
                Title = Title,
                Start = Start,
                Duration = Duration == TimeSpan.Zero ? EventDuration.FullDayDuration() : EventDuration.TimeSpanDuration(Duration),
            };
        }

        public abstract CalendarEvent ToEntity();
    }
}