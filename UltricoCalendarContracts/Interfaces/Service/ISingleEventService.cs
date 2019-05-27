using UltricoCalendarContracts.Entities;
using UltricoCalendarContracts.Models;

namespace UltricoCalendarContracts.Interfaces
{
    public interface ISingleEventService
    {
        void AddEvent(ScheduleEvent newEvent);
        
        ScheduleEvent GetEvent(int id);

        void EditEvent(int id, ScheduleEvent newModel);

        void DeleteEvent(int id);
    }
}