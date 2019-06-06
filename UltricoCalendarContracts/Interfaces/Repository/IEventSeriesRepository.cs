using System;
using System.Collections.Generic;
using UltricoCalendarContracts.Entities;

namespace UltricoCalendarContracts.Interfaces.Repository
{
    public interface IEventSeriesRepository : IRepository
    {
        int AddEventSeries(EventSeries eventSeries);
        EventSeries GetEventSeries(int id);
        EventSeries UpdateEventSeries(EventSeries editedEventSeries);
        int ExcludeEventFromSeries(int seriesId, EventFromSeries eventToExclude);
        bool DeleteEventSeries(int id);
        List<EventSeries> GetEventSeries(DateTime to);
    }
}