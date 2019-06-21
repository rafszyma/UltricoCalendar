using System;
using System.Collections.Generic;
using UltricoCalendarContracts.Enums;

namespace UltricoCalendarContracts.Occurences
{
    // TODO : GOOD : Nice encapsulation of logic in classes.
    public class FinishAfterDate : FinishClass
    {
        public FinishAfterDate(DateTime timeWhenFinished)
        {
            TimeWhenFinished = timeWhenFinished;
        }

        // TODO: MEDIUM : Naming - finish after date  then we switch to time.
        public DateTime TimeWhenFinished { get; }

        // TODO: HIGH: I think the decision to go with events / series etc makes it impossible to use entities here (or 
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