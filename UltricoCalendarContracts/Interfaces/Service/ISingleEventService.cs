namespace UltricoCalendarContracts.Interfaces.Service
{
    public interface ISingleEventService
    {
        void AddEvent(ICalendarEvent newEventModel);
        
        ICalendarEvent GetEvent(int id);

        void EditEvent(int id, ICalendarEvent newEventModel);

        void DeleteEvent(int id);
    }
}