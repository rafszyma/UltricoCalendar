using System;
using System.Collections.Generic;
using System.Net.Mail;
using UltricoCalendarContracts.Entities;
using UltricoCalendarContracts.Extensions;
using UltricoCalendarContracts.Interfaces;

namespace UltricoCalendarContracts.Models
{
    public class ScheduleEvent : BaseEvent 
    {

        public override CalendarEvent ToEntity()
        {
            return new SingleEvent
            {
                Title = Title,
                Description = Description,
                Start = Start,
                Duration = Duration == TimeSpan.Zero ? EventDuration.FullDayDuration() : EventDuration.TimeSpanDuration(Duration),
                MailAddresses = MailAddresses
            };
        }

        
    }
}