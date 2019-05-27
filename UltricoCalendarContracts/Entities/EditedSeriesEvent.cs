using System;
using System.Collections.Generic;
using System.Net.Mail;
using UltricoCalendarContracts.Extensions;

namespace UltricoCalendarContracts.Entities
{
    public class EditedSeriesEvent : CalendarEvent
    {
        public int EventSeriesId { get; set; }
        
        public virtual EventSeries EventSeries { get; set; }
    }
}