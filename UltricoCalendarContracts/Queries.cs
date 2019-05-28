using System;
using UltricoCalendarContracts.Entities;

namespace UltricoCalendarContracts
{
    public class Queries
    {
        public class GetEventMetadata
        {
            public GetEventMetadata(DateTime from, DateTime to)
            {
                From = from;
                To = to;
            }

            public DateTime From { get; }

            public DateTime To { get; }
        }

        public abstract class EventQueries<T> where T : CalendarEvent
        {
            public class Get
            {
                public Get(int id)
                {
                    Id = id;
                }

                public int Id { get; }
            }
        }

        public class SingleEventQueries : EventQueries<SingleEvent>
        {
        }

        public class EventSeriesQueries : EventQueries<EventSeries>
        {
        }

        public class EventFromSeriesQueries : EventQueries<EventFromSeries>
        {
        }
    }
}