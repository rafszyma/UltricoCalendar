using System;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime OldStartDate { get; set; }

        public override CalendarEvent ToEntity()
        {
            return new EventFromSeries
            {
                Title = Title,
                Description = Description,
                Start = Start,
                Duration = TimeSpan.Parse(Duration) == TimeSpan.Zero
                    ? EventDuration.FullDayDuration()
                    : EventDuration.TimeSpanDuration(TimeSpan.Parse(Duration)),
                MailAddresses = MailAddresses,
                OldStartDate = OldStartDate
            };
        }
    }
}