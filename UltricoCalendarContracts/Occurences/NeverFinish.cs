using System;
using System.Collections.Generic;
using UltricoCalendarContracts.Enums;

namespace UltricoCalendarContracts.Occurences
{
    public class NeverFinish : FinishClass
    {
        public override IEnumerable<DateTime> Occur(RepeatPeriod repeatPeriod, DateTime eventStart, DateTime repeatFrom, DateTime repeatTill)
        {
            var occurrences = new List<DateTime>();
            var latestTime = eventStart;
            while (latestTime < repeatTill)
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
            return FinishEnum.NeverFinish;
        }
    }
}