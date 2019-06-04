using System;

namespace UltricoCalendarContracts.Interfaces.Service
{
    public interface IEventFromSeriesService
    {
        ICalendarEvent GetEventFromSeries(int id);

        void EditEventFromSeries(int id, ICalendarEvent newEventModel);
        bool DeleteEventFromSeries(int id);
    }
}