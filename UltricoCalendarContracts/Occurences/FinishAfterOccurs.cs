using System;
using System.Collections.Generic;
using UltricoCalendarContracts.Enums;

namespace UltricoCalendarContracts
{
    public class FinishAfterOccurs : FinishClass
    {
        public int? TimesToOccur { get; private set; }

        public FinishAfterOccurs(int timesToOccur)
        {
            TimesToOccur = timesToOccur;
        }

        public override IEnumerable<DateTime> Occur(RepeatPeriod repeatPeriod, DateTime repeatFrom, DateTime repeatTill)
        {
            var latestDate = repeatFrom;
            var occurrences = new List<DateTime>();
            while (TimesToOccur > 0 && latestDate <= repeatTill)
            {
                occurrences.Add(latestDate);
                latestDate = latestDate.Add(repeatPeriod.NextOccurence(latestDate).Subtract(latestDate));
                TimesToOccur--;
            }

            return occurrences;
        }

        public override FinishEnum GetFinishValue()
        {
            return FinishEnum.AfterOccurs;
        }
    }
}