using UltricoCalendarContracts.Entities;
using UltricoCalendarContracts.Models;

namespace UltricoCalendarContracts.Interfaces
{
    public interface IEventSeriesService
    {
        void AddEventSeries(ICalendarEvent newEventModel);
        
        ICalendarEvent GetEventSeries(int id);

        void EditEventSeries(int id, ICalendarEvent newEventModel);

        void DeleteEventSeries(int id);
    }
}