using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using UltricoCalendarContracts.Enums;
using UltricoCalendarContracts.Extensions;
using UltricoCalendarContracts.Interfaces;
using UltricoCalendarContracts.Models;

namespace UltricoCalendarContracts.Entities
{
    public class EventSeries : CalendarEvent, ICalendarDisplayable
    {
        public RepeatPeriod RepeatPeriod { get; set; }
        
        public FinishClass Finish { get; set; }
        
        public virtual List<EditedSeriesEvent> EditedEvents { get; set; }
        
        public List<DateTime> DeletedOccurrences { get; set; }
        
        public EventMetadata ToMetadata(DateTime @from, DateTime to)
        {
            var allEventOccurrences = Finish.Occur(RepeatPeriod, from, to).ToList();
            allEventOccurrences.RemoveAll(x => DeletedOccurrences.Contains(x));
            allEventOccurrences.RemoveAll(x => EditedEvents.Select(y => y.OldStartDate).Contains(x));
            if (Duration.EventDurationType == DurationType.FullDay)
            {
                allEventOccurrences = allEventOccurrences.Select(x => x.Date).ToList();
            }
            return new EventMetadata
            {
                Id = Id,
                Title = Title,
                Duration = Duration.EventDurationTimeSpan,
                Start = allEventOccurrences
            };
        }

        public override BaseEventModel ToBaseModel()
        {
            return new ScheduleEventModelSeries
            {
                Title = Title,
                Description = Description,
                Start = Start,
                Duration = Duration.EventDurationTimeSpan.ToString(),
                MailAddresses = MailAddresses,
                RepeatPeriod = RepeatPeriod,
                FinishEnum = Finish.GetFinishValue(),
                OccursAmount = Finish.GetFinishValue() == FinishEnum.AfterOccurs ? ((FinishAfterOccurs)Finish).TimesToOccur : null,
                FinishDate = Finish.GetFinishValue() == FinishEnum.AfterDate ? ((FinishAfterDate)Finish).TimeWhenFinished : null
            };
        }
    }
}