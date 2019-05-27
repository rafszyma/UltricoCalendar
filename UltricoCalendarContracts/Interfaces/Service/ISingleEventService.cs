using UltricoCalendarContracts.Entities;
using UltricoCalendarContracts.Models;

namespace UltricoCalendarContracts.Interfaces
{
    public interface ISingleEventService
    {
        void AddEvent(ICalendarEvent newEventModel);
        
        ICalendarEvent GetEvent(int id);

        void EditEvent(int id, ICalendarEvent newEventModel);

        void DeleteEvent(int id);
    }
}