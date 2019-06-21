using System;
using System.Collections.Generic;
using System.Linq;
using UltricoCalendarContracts.Enums;
using UltricoCalendarContracts.Interfaces;
using UltricoCalendarContracts.Models;
using UltricoCalendarContracts.Occurences;

namespace UltricoCalendarContracts.Entities
{
    // TODO : HIGH : No constructors / everything public instead
    
    public class EventSeries : CalendarEvent, ICalendarDisplayable
    {
        public EventSeries()
        {
            DeletedOccurrences = new List<DateTime>();
        }

        // TODO : HIGH : Anemic domain model. Logic in extension methods.
        public RepeatPeriod RepeatPeriod { get; set; }
        

        public FinishClass Finish { get; set; }

        public virtual List<EventFromSeries> EditedEvents { get; set; }

        // TODO: HIGH : Public lists exposed. If things need to be added to collection a domain specific method can be used.
        public List<DateTime> DeletedOccurrences { get; set; }

        public EventMetadata ToMetadata(DateTime from, DateTime to)
        {
            var allEventOccurrences = Finish.Occur(RepeatPeriod, Start, from, to).ToList();
            allEventOccurrences.RemoveAll(x => DeletedOccurrences.Contains(x));
            allEventOccurrences.RemoveAll(x => EditedEvents.Select(y => y.OldStartDate).Contains(x));
            return new EventMetadata
            {
                Id = Id,
                Title = Title,
                Duration = Duration.EventDurationTimeSpan,
                Start = allEventOccurrences
            };
        }

        // TODO : HIGH : 
        public override BaseEventModel ToBaseModel()
        {
            return new EventSeriesModel
            {
                Title = Title,
                Description = Description,
                Start = Start,
                Duration = Duration.EventDurationTimeSpan.ToString(),
                MailAddresses = MailAddresses,
                RepeatPeriod = RepeatPeriod,
                FinishEnum = Finish.GetFinishValue(),
                OccursAmount = Finish.GetFinishValue() == FinishEnum.AfterOccurs
                    ? (int?) ((FinishAfterOccurs) Finish).MaxTimesToOccur
                    : null,
                FinishDate = Finish.GetFinishValue() == FinishEnum.AfterDate
                    ? (DateTime?) ((FinishAfterDate) Finish).TimeWhenFinished
                    : null
            };
        }
    }
}