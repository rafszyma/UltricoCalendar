using System;
using System.Net.Sockets;
using UltricoCalendarContracts.Entities;
using UltricoCalendarContracts.Extensions;
using UltricoCalendarContracts.Models;

namespace UltricoCalendarContracts
{
    public class Commands
    {
        public abstract class EventCommands<T> where T : BaseEvent
        {
            public class Add
            {
                public T Data { get; }

                public Add(T data)
                {
                    Data = data;
                }
            }
            
            public class Update
            {
                public T Data { get; }
                
                public int Id { get;  }

                public Update(int id, T data)
                {
                    Id = id;
                    Data = data;
                }
            }
            
            public class Delete
            {
                public int Id { get; }

                public Delete(int id)
                {
                    Id = id;
                }
            }

        }
        
        public class SingleEventCommands : EventCommands<ScheduleEvent>{}
        
        public class EventSeriesCommands : EventCommands<ScheduleEventSeries>{}

        public class EditEventFromSeriesCommands
        {
            public class EditEventFromSeries
            {
                public ScheduleEvent EventData { get; }
                
                public int SeriesId { get; }

                public EditEventFromSeries(ScheduleEvent eventData, int seriesId)
                {
                    EventData = eventData;
                    SeriesId = seriesId;
                }
            }

            public class DeleteEventFromSeries
            {
                public int SeriesId { get; }
                
                public DateTime DateTime { get; }

                public DeleteEventFromSeries(int seriesId, DateTime dateTime)
                {
                    SeriesId = seriesId;
                    DateTime = dateTime;
                }
            }
        }
    }
}