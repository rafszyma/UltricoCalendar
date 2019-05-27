using System;
using System.Collections.Generic;
using UltricoCalendarContracts.Entities;
using UltricoCalendarContracts.Models;

namespace UltricoCalendarContracts.Interfaces.Repository
{
    public interface ISingleEventRepository
    {
        void AddSingleEvent(SingleEvent singleEvent);
        SingleEvent GetSingleEvent(int id);
        void UpdateSingleEvent(SingleEvent editedSingleEvent);
        void DeleteSingleEvent(int id);
        List<SingleEvent> GetSingleEvents(DateTime from, DateTime to);
    }
}