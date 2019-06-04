using System;
using System.Collections.Generic;
using UltricoCalendarContracts.Entities;

namespace UltricoCalendarContracts.Interfaces.Repository
{
    public interface IEventFromSeriesRepository : IRepository
    {
        int AddEventFromSeries(EventFromSeries eventFromSeries);
        EventFromSeries GetEventFromSeries(int id);
        EventFromSeries UpdateEventFromSeries(EventFromSeries eventFromSeries);
        bool DeleteEventFromSeries(int id);
        List<EventFromSeries> GetEventFromSeries(DateTime from, DateTime to);
    }
}