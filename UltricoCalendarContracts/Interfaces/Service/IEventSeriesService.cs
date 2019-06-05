using System;

namespace UltricoCalendarContracts.Interfaces.Service
{
    public interface IEventSeriesService
    {
        int AddEventSeries(ICalendarEvent newEventModel);

        ICalendarEvent GetEventSeries(int id);

        void EditEventSeries(int id, ICalendarEvent newEventModel);

        bool DeleteEventSeries(int id);

        int ExcludeEventFromSeries(int id, ICalendarEvent newEventModel);

        void DeleteEventOccurenceFromSeries(int seriesId, DateTime dateTime);
    }
}