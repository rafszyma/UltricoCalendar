using System;
using System.Net;
using System.Runtime.CompilerServices;
using UltricoCalendarContracts.Entities;
using UltricoCalendarContracts.Enums;
using UltricoCalendarContracts.Extensions;
using UltricoCalendarContracts.Interfaces;

namespace UltricoCalendarContracts.Models
{
    public class ScheduleEventSeries : BaseEvent
    {
        public RepeatPeriod RepeatPeriod { get; set; }
        
        public FinishEnum FinishEnum { get; set; }

        public int OccursAmount { get; set; }
        
        public DateTime FinishDate { get; set; }
        
        public override CalendarEvent ToEntity()
        {
            FinishClass finishClass;
            
            // This looks very ugly and is against SOLID but can't think of anything better
            switch (FinishEnum)
            {
                case FinishEnum.AfterDate:
                    finishClass = FinishEnum.GetOccurenceClass(FinishDate);
                    break;
                case FinishEnum.AfterOccurs:
                    finishClass = FinishEnum.GetOccurenceClass(OccursAmount);
                    break;
                case FinishEnum.NeverFinish:
                    finishClass = FinishEnum.GetOccurenceClass();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return new EventSeries
            {
                Title = Title,
                Description = Description,
                Start = Start,
                Duration = Duration == TimeSpan.Zero
                    ? EventDuration.FullDayDuration()
                    : EventDuration.TimeSpanDuration(Duration),
                MailAddresses = MailAddresses,
                RepeatEvery = RepeatPeriod,
                Finish = finishClass
            };
        }
    }
}