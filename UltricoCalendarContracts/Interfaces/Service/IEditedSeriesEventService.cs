using System;
using UltricoCalendarContracts.Entities;
using UltricoCalendarContracts.Models;

namespace UltricoCalendarContracts.Interfaces
{
    public interface IEditedSeriesEventService
    {
        void EditEventFromSeries(int id, ScheduleEvent newModel);
        
        void GetEditedEventFromSeries(int id);
        
        void DeleteEventFromSeries(int seriesId, DateTime dateTime);
    }
}