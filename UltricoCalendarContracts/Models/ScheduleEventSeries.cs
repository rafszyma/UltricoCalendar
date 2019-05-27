using System;
using System.Net;
using System.Runtime.CompilerServices;
using UltricoCalendarContracts.Enums;

namespace UltricoCalendarContracts.Models
{
    public class ScheduleEventSeries : ScheduleEvent
    {
        public RepeatPeriod RepeatPeriod { get; set; }
        
        public FinishEnum FinishEnum { get; set; }

        public int OccursAmount { get; set; }
        
        public DateTime FinishDate { get; set; }
    }
}