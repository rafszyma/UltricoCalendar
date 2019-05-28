using System;
using UltricoCalendarContracts.Entities;
using UltricoCalendarContracts.Extensions;

namespace UltricoCalendarContracts.Models
{
    public class SingleEventModel : BaseEventModel
    {
        public override CalendarEvent ToEntity()
        {
            return new SingleEvent
            {
                Title = Title,
                Description = Description,
                Start = Start,
                Duration = TimeSpan.Parse(Duration) == TimeSpan.Zero
                    ? EventDuration.FullDayDuration()
                    : EventDuration.TimeSpanDuration(TimeSpan.Parse(Duration)),
                MailAddresses = MailAddresses
            };
        }
    }
}