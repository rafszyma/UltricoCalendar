using UltricoCalendarContracts.Entities;
using UltricoCalendarContracts.Models;

namespace UltricoCalendarContracts.Interfaces
{
    public interface IEventSeriesService
    {
        void AddEventSeries(ScheduleEventSeries newEvent);
        
        ScheduleEventSeries GetEventSeries(int id);

        void EditEventSeries(int id, ScheduleEventSeries newModel);

        void DeleteEventSeries(int id);
    }
}