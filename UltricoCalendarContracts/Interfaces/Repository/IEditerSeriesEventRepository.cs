using UltricoCalendarContracts.Entities;

namespace UltricoCalendarContracts.Interfaces.Repository
{
    public interface IEditerSeriesEventRepository
    {
        void Add(EditedSeriesEvent editedSeriesEvent);
        EditedSeriesEvent Get(int id);
        void Update(EditedSeriesEvent editedSeriesEvent);
        void Delete(int id);
    }
}