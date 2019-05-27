using System;
using UltricoCalendarContracts.Entities;

namespace UltricoCalendarContracts
{
    public class Queries
    {
        public class GetEventMetadata
        {
            public DateTime From { get; }
            
            public DateTime To { get; }

            public GetEventMetadata(DateTime from, DateTime to)
            {
                From = from;
                To = to;
            }
        }

        public abstract class EventQueries<T> where T : CalendarEvent
        {
            public class Get
            {
                public int Id { get; }

                public Get(int id)
                {
                    Id = id;
                }
            }
        }
        
        public class SingleEventQueries : EventQueries<SingleEvent>{}
        
        public class EventSeriesQueries : EventQueries<EventSeries>{}
        
        public class EditEventFromSeriesQueries : EventQueries<EditedSeriesEvent>{}
    }
}