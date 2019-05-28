using System;
using System.Net.Sockets;
using UltricoCalendarContracts.Entities;
using UltricoCalendarContracts.Extensions;
using UltricoCalendarContracts.Models;

namespace UltricoCalendarContracts
{
    public class Commands
    {
        public abstract class EventCommands<T> where T : BaseEventModel
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
        
        public class SingleEventCommands : EventCommands<SingleEventModel>{}
        
        public class EventSeriesCommands : EventCommands<EventSeriesModel>{}
        
        public class EventFromSeriesCommands : EventCommands<EventFromSeriesModel>{}

        public class EditEventFromSeriesCommands
        {
            public class ExcludeEventFromSeries
            {
                public EventFromSeriesModel EventFromSeriesModelData { get; }
                
                public int SeriesId { get; }

                public ExcludeEventFromSeries(EventFromSeriesModel eventFromSeriesModelData, int seriesId)
                {
                    EventFromSeriesModelData = eventFromSeriesModelData;
                    SeriesId = seriesId;
                }
            }

            public class DeleteEventOccurenceFromSeries
            {
                public int SeriesId { get; }
                
                public DateTime DateTime { get; }

                public DeleteEventOccurenceFromSeries(int seriesId, DateTime dateTime)
                {
                    SeriesId = seriesId;
                    DateTime = dateTime;
                }
            }
        }
    }
}