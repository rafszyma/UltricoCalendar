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
            
            // TODO: HIGH : Not very readable code. I would try to hint the reader that we are trying to find the start time
            // for generating occurances. Possibly encapsulate the logic in RepeatPeriod to find the closest occurance to a given date and start from there.
            
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