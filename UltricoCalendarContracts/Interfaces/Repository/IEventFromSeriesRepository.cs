using System;
using System.Collections.Generic;
using UltricoCalendarContracts.Entities;

namespace UltricoCalendarContracts.Interfaces.Repository
{
    public interface IEventFromSeriesRepository : IRepository
    {
        // TODO: HIGH : Why not return the object with ID initialized?
        // When I see this I'm not sure if I need to set ID myself.
        int AddEventFromSeries(EventFromSeries eventFromSeries);
        EventFromSeries GetEventFromSeries(int id);
        EventFromSeries UpdateEventFromSeries(EventFromSeries eventFromSeries);
        bool DeleteEventFromSeries(int id);
        List<EventFromSeries> GetEventFromSeries(DateTime from, DateTime to);
    }
}