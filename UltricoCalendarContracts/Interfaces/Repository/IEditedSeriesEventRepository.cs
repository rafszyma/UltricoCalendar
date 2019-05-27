using System;
using System.Collections.Generic;
using UltricoCalendarContracts.Entities;

namespace UltricoCalendarContracts.Interfaces.Repository
{
    public interface IEditedSeriesEventRepository
    {
        void AddEditedSeriesEvent(EditedSeriesEvent editedSeriesEvent);
        EditedSeriesEvent GetEditedSeriesEvent(int id);
        void UpdateEditedSeriesEvent(EditedSeriesEvent editedSeriesEvent);
        void DeleteEditedSeriesEvent(int id);
        IEnumerable<EditedSeriesEvent> GetEditedSeriesEvent(DateTime from, DateTime to);
    }
}