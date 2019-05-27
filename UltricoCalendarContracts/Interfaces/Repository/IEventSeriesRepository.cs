using System;
using System.Collections.Generic;
using UltricoCalendarContracts.Entities;
using UltricoCalendarContracts.Models;

namespace UltricoCalendarContracts.Interfaces.Repository
{
    public interface IEventSeriesRepository
    {
        void AddEventSeries(EventSeries eventSeries);
        EventSeries GetEventSeries(int id);
        void UpdateEventSeries(EventSeries editedEventSeries);
        void DeleteEventSeries(int id);
        IEnumerable<EventSeries> GetEventSeries(DateTime from, DateTime to);
    }
}