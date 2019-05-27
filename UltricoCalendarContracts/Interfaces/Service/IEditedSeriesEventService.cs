using System;
using UltricoCalendarContracts.Entities;

namespace UltricoCalendarContracts.Interfaces
{
    public interface IEditedSeriesEventService
    {
        void EditEventSeries(int id, EventSeries newModel);
        
        void GetEditedEventFromSeries(int id);
        
        void DeleteEventFromSeries(int seriesId, DateTime dateTime);
    }
}