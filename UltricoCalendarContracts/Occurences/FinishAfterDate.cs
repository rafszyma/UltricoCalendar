using System;
using System.Collections.Generic;
using UltricoCalendarContracts.Enums;

namespace UltricoCalendarContracts
{
    public class FinishAfterDate : FinishClass
    {
        public DateTime? TimeWhenFinished { get; }
        
        public override IEnumerable<DateTime> Occur(RepeatPeriod repeatPeriod, DateTime repeatFrom, DateTime repeatTill)
        {
            var occurrences = new List<DateTime>();
            var latestTime = repeatFrom;
            while (latestTime < TimeWhenFinished || DateTime.Now < repeatTill)
            {
                occurrences.Add(latestTime);
                latestTime = latestTime.Add(repeatPeriod.NextOccurence(latestTime).Subtract(latestTime));
            }

            return occurrences;
        }

        public override FinishEnum GetFinishValue()
        {
            return FinishEnum.AfterDate;
        }

        public FinishAfterDate(DateTime timeWhenFinished)
        {
            TimeWhenFinished = timeWhenFinished;
        }
    }
}