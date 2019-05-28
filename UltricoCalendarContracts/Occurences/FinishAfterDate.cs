using System;
using System.Collections.Generic;
using UltricoCalendarContracts.Enums;

namespace UltricoCalendarContracts.Occurences
{
    public class FinishAfterDate : FinishClass
    {
        public DateTime TimeWhenFinished { get; }
        
        public override IEnumerable<DateTime> Occur(RepeatPeriod repeatPeriod, DateTime repeatFrom, DateTime repeatTill)
        {
            var occurrences = new List<DateTime>();
            var latestTime = repeatFrom;
            DateTime earlierEnd;
            earlierEnd = DateTime.Compare(repeatTill, TimeWhenFinished) < 0 ? repeatTill : TimeWhenFinished;
            
            while (latestTime < TimeWhenFinished || latestTime < earlierEnd)
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