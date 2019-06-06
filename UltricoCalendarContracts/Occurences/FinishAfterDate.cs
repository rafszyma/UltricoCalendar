using System;
using System.Collections.Generic;
using UltricoCalendarContracts.Enums;

namespace UltricoCalendarContracts.Occurences
{
    public class FinishAfterDate : FinishClass
    {
        public FinishAfterDate(DateTime timeWhenFinished)
        {
            TimeWhenFinished = timeWhenFinished;
        }

        public DateTime TimeWhenFinished { get; }

        public override IEnumerable<DateTime> Occur(RepeatPeriod repeatPeriod, DateTime eventStart, DateTime repeatFrom, DateTime repeatTill)
        {
            var occurrences = new List<DateTime>();
            var latestTime = eventStart;
            var effectiveEnd = DateTime.Compare(repeatTill, TimeWhenFinished) < 0 ? repeatTill : TimeWhenFinished;

            while (latestTime < effectiveEnd)
            {
                if (latestTime > repeatFrom)
                {
                    occurrences.Add(latestTime);
                }

                latestTime = latestTime.Add(repeatPeriod.NextOccurence(latestTime).Subtract(latestTime));
            }

            return occurrences;
        }

        public override FinishEnum GetFinishValue()
        {
            return FinishEnum.AfterDate;
        }
    }
}