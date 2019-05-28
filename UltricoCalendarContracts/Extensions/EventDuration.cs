using System;
using Newtonsoft.Json;
using UltricoCalendarContracts.Enums;

namespace UltricoCalendarContracts.Extensions
{
    public class EventDuration
    {
        private EventDuration(TimeSpan eventDurationTimeSpan, DurationType eventDurationType)
        {
            EventDurationType = eventDurationType;
            EventDurationTimeSpan = eventDurationTimeSpan;
        }

        public TimeSpan EventDurationTimeSpan { get; }

        public DurationType EventDurationType { get; }

        public static EventDuration FullDayDuration()
        {
            return new EventDuration(TimeSpan.FromHours(24), DurationType.FullDay);
        }

        public static EventDuration TimeSpanDuration(TimeSpan timeSpan)
        {
            return new EventDuration(timeSpan, DurationType.TimeSpan);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static EventDuration FromJson(string json)
        {
            return JsonConvert.DeserializeObject<EventDuration>(json);
        }
    }
}