using System;
using UltricoCalendarContracts.Models;

namespace UltricoCalendarContracts
{
    // TODO: MEDIUM : Nested classes intead of namespace - I don't see any value, only cost - Uber class that's hard to navigate.
    public class Commands
    {
        public abstract class EventCommands<T> where T : BaseEventModel
        {
            public class Add
            {
                public Add(T data)
                {
                    Data = data;
                }

                public T Data { get; }
            }

            public class Update
            {
                public Update(int id, T data)
                {
                    Id = id;
                    Data = data;
                }

                public T Data { get; }

                public int Id { get; }
            }

            public class Delete
            {
                public Delete(int id)
                {
                    Id = id;
                }

                public int Id { get; }
            }
        }

        public class SingleEventCommands : EventCommands<SingleEventModel>
        {
        }

        public class EventSeriesCommands : EventCommands<EventSeriesModel>
        {
        }

        public class EventFromSeriesCommands : EventCommands<EventFromSeriesModel>
        {
        }

        public class EditEventFromSeriesCommands
        {
            public class ExcludeEventFromSeries
            {
                public ExcludeEventFromSeries(EventFromSeriesModel eventFromSeriesModelData, int seriesId)
                {
                    EventFromSeriesModelData = eventFromSeriesModelData;
                    SeriesId = seriesId;
                }

                public EventFromSeriesModel EventFromSeriesModelData { get; }

                public int SeriesId { get; }
            }

            public class DeleteEventOccurenceFromSeries
            {
                public DeleteEventOccurenceFromSeries(int seriesId, DateTime dateTime)
                {
                    SeriesId = seriesId;
                    DateTime = dateTime;
                }

                public int SeriesId { get; }

                public DateTime DateTime { get; }
            }
        }

        public class SendEmailCommand
        {
        }
    }
}