using System;
using System.Collections.Generic;
using UltricoCalendarContracts.Entities;
using UltricoCalendarContracts.Models;

namespace UltricoCalendarContracts.Interfaces.Repository
{
    public interface IEventFromSeriesRepository
    {
        void AddEventFromSeries(EventFromSeries eventFromSeries);
        EventFromSeries GetEventFromSeries(int id);
        void UpdateEventFromSeries(EventFromSeries eventFromSeries);
        void DeleteEventFromSeries(int id);
        List<EventFromSeries> GetEventFromSeries(DateTime from, DateTime to);
    }
}