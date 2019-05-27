using System;
using UltricoCalendarContracts.Models;

namespace UltricoCalendarContracts.Interfaces
{
    public interface ICalendarDisplayable
    {
        EventMetadata ToMetadata(DateTime from, DateTime to);
    }
}