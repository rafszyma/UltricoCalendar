using System;
using System.Collections.Generic;
using UltricoCalendarContracts.Entities;

namespace UltricoCalendarContracts.Interfaces.Repository
{
    public interface ISingleEventRepository : IRepository
    {
        int AddSingleEvent(SingleEvent singleEvent);
        SingleEvent GetSingleEvent(int id);
        SingleEvent UpdateSingleEvent(SingleEvent editedSingleEvent);
        bool DeleteSingleEvent(int id);
        List<SingleEvent> GetSingleEvents(DateTime from, DateTime to);
    }
}