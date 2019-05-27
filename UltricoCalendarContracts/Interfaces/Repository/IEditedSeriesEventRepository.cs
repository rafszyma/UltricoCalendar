using System;
using System.Collections.Generic;
using UltricoCalendarContracts.Entities;
using UltricoCalendarContracts.Models;

namespace UltricoCalendarContracts.Interfaces.Repository
{
    public interface IEditedSeriesEventRepository
    {
        void AddEditedSeriesEvent(EditedSeriesEvent editedSeriesEvent);
        EditedSeriesEvent GetEditedSeriesEvent(int id);
        void UpdateEditedSeriesEvent(EditedSeriesEvent editedSeriesEvent);
        void DeleteEditedSeriesEvent(int id);
        List<EditedSeriesEvent> GetEditedSeriesEvent(DateTime from, DateTime to);
    }
}