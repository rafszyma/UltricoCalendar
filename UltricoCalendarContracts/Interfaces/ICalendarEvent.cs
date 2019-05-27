using UltricoCalendarContracts.Entities;
using UltricoCalendarContracts.Models;

namespace UltricoCalendarContracts.Interfaces
{
    public interface ICalendarEvent
    {
        CalendarEvent ToEntity();

        EventMetadata ToMetadata();
    }
}