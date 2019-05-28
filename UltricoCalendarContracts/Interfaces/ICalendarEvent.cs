using UltricoCalendarContracts.Entities;

namespace UltricoCalendarContracts.Interfaces
{
    public interface ICalendarEvent
    {
        CalendarEvent ToEntity();
    }
}