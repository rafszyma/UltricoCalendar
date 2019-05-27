using UltricoCalendarContracts.Entities;

namespace UltricoCalendarContracts.Interfaces.Repository
{
    public interface IEventSeriesRepository
    {
        void Add(EventSeries editedSeriesEvent);
        EventSeries Get(int id);
        void Update(EventSeries editedSeriesEvent);
        void Delete(int id);
    }
}