using UltricoCalendarContracts.Entities;

namespace UltricoCalendarContracts.Interfaces.Repository
{
    public interface ISingleEventRepository
    {
        void Add(SingleEvent editedSeriesEvent);
        SingleEvent Get(int id);
        void Update(SingleEvent editedSeriesEvent);
        void Delete(int id);
    }
}