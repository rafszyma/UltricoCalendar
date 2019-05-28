using System;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using UltricoCalendarContracts.Entities;
using UltricoCalendarContracts.Extensions;

namespace UltricoCalendarContracts.Models
{
    public class EventFromSeriesModel : SingleEventModel
    {
        public EventFromSeriesModel(DateTime oldStartDate)
        {
            OldStartDate = oldStartDate;
        }

        private DateTime OldStartDate { get; }
        
        public override CalendarEvent ToEntity()
        {
            return new EventFromSeries
            {
                Title = Title,
                Description = Description,
                Start = Start,
                Duration = TimeSpan.Parse(Duration) == TimeSpan.Zero ? EventDuration.FullDayDuration() : EventDuration.TimeSpanDuration(TimeSpan.Parse(Duration)),
                MailAddresses = MailAddresses,
                OldStartDate = OldStartDate
            };
        }
    }
}