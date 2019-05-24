using System;
using System.Collections.Generic;
using UltricoCalendarContracts.Enums;

namespace UltricoCalendarContracts
{
    public abstract class FinishClass
    {
        public abstract List<DateTime> Occur(RepeatPeriod repeatPeriod, DateTime repeatFrom, DateTime repeatTill);
    }
}