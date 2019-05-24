using System;
using System.Collections.Generic;
using UltricoCalendarContracts.Enums;

namespace UltricoCalendarContracts
{
    public class NeverFinish : FinishClass
    {
        public override List<DateTime> Occur(RepeatPeriod repeatPeriod, DateTime repeatFrom, DateTime repeatTill)
        {
            var occurrences = new List<DateTime>();
            var latestTime = repeatFrom;
            while (latestTime < repeatTill)
            {
                occurrences.Add(latestTime);
                latestTime = latestTime.Add(repeatPeriod.NextOccurence(latestTime).Subtract(latestTime));
            }

            return occurrences;
        }
    }
}