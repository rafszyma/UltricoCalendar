using System;
using System.Collections.Generic;
using System.Net.Mail;
using UltricoCalendarContracts.Enums;
using UltricoCalendarContracts.Extensions;

namespace UltricoCalendarContracts.Entities
{
    public class EventSeries : CalendarEvent
    {
        public RepeatPeriod RepeatEvery { get; set; }
        
        public FinishClass Finish { get; set; }
        
        public List<EditedSeriesEvent> EditedEvents { get; set; }
        
        public List<DateTime> DeletedOccurrences { get; set; }
    }
}