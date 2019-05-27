using System;
using System.Collections.Generic;
using UltricoCalendarContracts.Models;

namespace UltricoCalendarContracts.Interfaces
{
    public interface IMetadataService
    {
        IEnumerable<EventMetadata> GetMetadata(DateTime from, DateTime to);
    }
}