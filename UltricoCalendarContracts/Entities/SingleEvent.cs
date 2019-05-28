using System;
using System.Collections.Generic;
using System.Net.Mail;
using UltricoCalendarContracts.Enums;
using UltricoCalendarContracts.Extensions;
using UltricoCalendarContracts.Interfaces;
using UltricoCalendarContracts.Models;

namespace UltricoCalendarContracts.Entities
{
    public class SingleEvent: CalendarEvent, ICalendarDisplayable
    {
        public EventMetadata ToMetadata(DateTime @from, DateTime to)
        {
            return new EventMetadata
            {
                Id = Id,
                Title = Title,
                Duration = Duration.EventDurationTimeSpan,
                Start = new List<DateTime> { Duration.EventDurationType == DurationType.FullDay ? Start.Date : Start }
            };
        }

        public override BaseEventModel ToBaseModel()
        {
            return new SingleEventModel
            {
                Title = Title,
                Description = Description,
                Start = Start,
                Duration = Duration.EventDurationTimeSpan.ToString(),
                MailAddresses = MailAddresses
            };
        }
    }
}