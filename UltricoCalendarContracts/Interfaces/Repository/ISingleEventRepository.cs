using System;
using System.Collections.Generic;
using UltricoCalendarContracts.Entities;

namespace UltricoCalendarContracts.Interfaces.Repository
{
    public interface ISingleEventRepository : IRepository
    {
        void AddSingleEvent(SingleEvent singleEvent);
        SingleEvent GetSingleEvent(int id);
        void UpdateSingleEvent(SingleEvent editedSingleEvent);
        void DeleteSingleEvent(int id);
        List<SingleEvent> GetSingleEvents(DateTime from, DateTime to);
    }
}