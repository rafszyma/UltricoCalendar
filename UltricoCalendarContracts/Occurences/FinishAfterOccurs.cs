using System;
using System.Collections.Generic;
using UltricoCalendarContracts.Enums;

namespace UltricoCalendarContracts
{
    public class FinishAfterOccurs : FinishClass
    {
        private int _occurs;

        public FinishAfterOccurs(int occurs)
        {
            _occurs = occurs;
        }

        public override List<DateTime> Occur(RepeatPeriod repeatPeriod, DateTime repeatFrom, DateTime repeatTill)
        {
            var latestDate = repeatFrom;
            var occurrences = new List<DateTime>();
            while (_occurs > 0 && latestDate <= repeatTill)
            {
                occurrences.Add(latestDate);
                latestDate = latestDate.Add(repeatPeriod.NextOccurence(latestDate).Subtract(latestDate));
                _occurs--;
            }

            return occurrences;
        }
    }
}