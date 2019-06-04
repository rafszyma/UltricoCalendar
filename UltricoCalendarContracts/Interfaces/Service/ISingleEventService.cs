namespace UltricoCalendarContracts.Interfaces.Service
{
    public interface ISingleEventService
    {
        int AddEvent(ICalendarEvent newEventModel);

        ICalendarEvent GetEvent(int id);

        void EditEvent(int id, ICalendarEvent newEventModel);

        bool DeleteEvent(int id);
    }
}