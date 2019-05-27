using System;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using UltricoCalendarContracts.Entities;
using UltricoCalendarContracts.Extensions;

namespace UltricoCalendarContracts.Models
{
    public class ScheduleEventModelFromSeries : ScheduleEventModel
    {
        public DateTime OldStartDate { get; set; }
        public override CalendarEvent ToEntity()
        {
            return new EditedSeriesEvent
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