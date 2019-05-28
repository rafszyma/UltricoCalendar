using System;
using System.Collections.Generic;
using UltricoCalendarContracts.Enums;

namespace UltricoCalendarContracts.Occurences
{
    public class FinishAfterOccurs : FinishClass
    {
        public int MaxTimesToOccur { get; }

        public FinishAfterOccurs(int maxTimesToOccur)
        {
            MaxTimesToOccur = maxTimesToOccur;
        }

        public override IEnumerable<DateTime> Occur(RepeatPeriod repeatPeriod, DateTime repeatFrom, DateTime repeatTill)
        {
            var latestDate = repeatFrom;
            var occurrences = new List<DateTime>();
            var timesToOccur = MaxTimesToOccur;
            while (timesToOccur > 0 && latestDate <= repeatTill)
            {
                occurrences.Add(latestDate);
                latestDate = latestDate.Add(repeatPeriod.NextOccurence(latestDate).Subtract(latestDate));
                timesToOccur--;
            }

            return occurrences;
        }

        public override FinishEnum GetFinishValue()
        {
            return FinishEnum.AfterOccurs;
        }
    }
}