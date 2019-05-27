using UltricoCalendarContracts.Entities;

namespace UltricoCalendarContracts.Interfaces
{
    public interface IEventSeriesService
    {
        void AddEventSeries(EventSeries newEvent);
        
        EventSeries GetEventSeries(int id);

        void EditEventSeries(int id, EventSeries newModel);

        void DeleteEventSeries(int id);
    }
}