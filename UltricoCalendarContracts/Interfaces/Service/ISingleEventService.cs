using UltricoCalendarContracts.Entities;

namespace UltricoCalendarContracts.Interfaces
{
    public interface ISingleEventService
    {
        void AddEvent(SingleEvent newEvent);
        
        SingleEvent GetEvent(int id);

        void EditEvent(int id, SingleEvent newModel);

        void DeleteEvent(int id);
    }
}