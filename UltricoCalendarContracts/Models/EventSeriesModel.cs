using System;
using System.Globalization;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UltricoCalendarContracts.Entities;
using UltricoCalendarContracts.Enums;
using UltricoCalendarContracts.Extensions;
using UltricoCalendarContracts.Interfaces;
using UltricoCalendarContracts.Occurences;

namespace UltricoCalendarContracts.Models
{
    public class EventSeriesModel : BaseEventModel
    {
        public RepeatPeriod RepeatPeriod { get; set; }
        
        public FinishEnum FinishEnum { get; set; }

        public int? OccursAmount { get; set; }
        
        public DateTime? FinishDate { get; set; }
        
        public override CalendarEvent ToEntity()
        {
            FinishClass finishClass = new NeverFinish();
            
            // This looks very ugly and is against SOLID but can't think of anything better, there is way to assign logic to Enum value in Java, but can't think of anything better in C#
            switch (FinishEnum)
            {
                case FinishEnum.AfterDate:
                    if (FinishDate != null) finishClass = FinishEnum.GetOccurenceClass(FinishDate.Value);
                    break;
                case FinishEnum.AfterOccurs:
                    if (OccursAmount != null) finishClass = FinishEnum.GetOccurenceClass(OccursAmount.Value);
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
                Duration = TimeSpan.Parse(Duration) == TimeSpan.Zero
                    ? EventDuration.FullDayDuration()
                    : EventDuration.TimeSpanDuration(TimeSpan.Parse(Duration)),
                MailAddresses = MailAddresses,
                RepeatPeriod = RepeatPeriod,
                Finish = finishClass
            };
        }
    }
}