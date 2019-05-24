using System;
using System.Collections.Generic;
using UltricoCalendarContracts.Enums;

namespace UltricoCalendarContracts
{
    public class FinishAfterDate : FinishClass
    {
        private readonly DateTime _timeWhenFinished;
        
        public override List<DateTime> Occur(RepeatPeriod repeatPeriod, DateTime repeatFrom, DateTime repeatTill)
        {
            var occurrences = new List<DateTime>();
            var latestTime = repeatFrom;
            while (latestTime < _timeWhenFinished || DateTime.Now < repeatTill)
            {
                occurrences.Add(latestTime);
                latestTime = latestTime.Add(repeatPeriod.NextOccurence(latestTime).Subtract(latestTime));
            }

            return occurrences;
        }

        public FinishAfterDate(DateTime timeWhenFinished)
        {
            _timeWhenFinished = timeWhenFinished;
        }
    }
}