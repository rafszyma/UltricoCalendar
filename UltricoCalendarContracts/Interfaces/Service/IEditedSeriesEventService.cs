using System;

namespace UltricoCalendarContracts.Interfaces.Service
{
    public interface IEditedSeriesEventService
    {
        void EditEventFromSeries(int id, ICalendarEvent newEventModel);
        
        ICalendarEvent GetEditedEventFromSeries(int id);
        
        void DeleteEventFromSeries(int seriesId, DateTime dateTime);
    }
}