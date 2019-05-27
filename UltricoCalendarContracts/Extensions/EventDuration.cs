using System;
using UltricoCalendarContracts.Enums;

namespace UltricoCalendarContracts.Extensions
{
    public class EventDuration
    {
        public TimeSpan EventDurationTimeSpan { get; }

        public DurationType EventDurationType { get; }
        
        private EventDuration()
        {
            EventDurationType = DurationType.FullDay;
        }

        private EventDuration(TimeSpan eventDurationTimeSpan)
        {
            EventDurationTimeSpan = eventDurationTimeSpan;
            EventDurationType = DurationType.TimeSpan;
        }

        public static EventDuration FullDayDuration()
        {
            return new EventDuration();
        }
        
        public static EventDuration TimeSpanDuration(TimeSpan timeSpan)
        {
            return new EventDuration(timeSpan);
        }
    }
    
    
}