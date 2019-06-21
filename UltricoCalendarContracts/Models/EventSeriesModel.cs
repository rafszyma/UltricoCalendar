using System;
using System.ComponentModel.DataAnnotations;
using UltricoCalendarContracts.Entities;
using UltricoCalendarContracts.Enums;
using UltricoCalendarContracts.Extensions;
using UltricoCalendarContracts.Occurences;

namespace UltricoCalendarContracts.Models
{

    public class EventSeriesModel : BaseEventModel
    {
        [Required] public RepeatPeriod RepeatPeriod { get; set; }

        [Required] public FinishEnum FinishEnum { get; set; }

        public int? OccursAmount { get; set; }

        public DateTime? FinishDate { get; set; }

        public override CalendarEvent ToEntity()
        {
            FinishClass finishClass = new NeverFinish();
            
            // TODO: HIGH : Yes, it's ugly :) I think I would:
            // 1) put it outside of the model - factory perhaps
            // 2) have the factory accept a mapping of enum -> class / use Di container to register mapping using reflection on app start (implementations would have some interface - we search for implementation with name matching enum)
            // 3) write good tests that cover this and implementation that don't match any enum - this way noone would add an implementation without adding an enum / remove implementation and leave enum / change names independently.
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