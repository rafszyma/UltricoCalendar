using System;

namespace UltricoCalendarContracts.Interfaces.Service
{
    public interface IEventFromSeriesService
    {
        void ExcludeEventFromSeries(int id, ICalendarEvent newEventModel);


        void DeleteEventOccurenceFromSeries(int seriesId, DateTime dateTime);

        ICalendarEvent GetEditedEventFromSeries(int id);

        void EditEventFromSeries(int id, ICalendarEvent newEventModel);
        void DeleteEventFromSeries(int id);
    }
}