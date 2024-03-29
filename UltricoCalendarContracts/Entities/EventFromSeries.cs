using System;
using System.Collections.Generic;
using UltricoCalendarContracts.Enums;
using UltricoCalendarContracts.Interfaces;
using UltricoCalendarContracts.Models;

namespace UltricoCalendarContracts.Entities
{
    public class EventFromSeries : CalendarEvent, ICalendarDisplayable
    {
        public int EventSeriesId { get; set; }

        public virtual EventSeries EventSeries { get; set; }

        public DateTime OldStartDate { get; set; }

        public EventMetadata ToMetadata(DateTime from, DateTime to)
        {
            return new EventMetadata
            {
                Id = Id,
                Title = Title,
                Duration = Duration.EventDurationTimeSpan,
                Start = new List<DateTime> {Duration.EventDurationType == DurationType.FullDay ? Start.Date : Start}
            };
        }

        public override BaseEventModel ToBaseModel()
        {
            return new EventFromSeriesModel(OldStartDate)
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