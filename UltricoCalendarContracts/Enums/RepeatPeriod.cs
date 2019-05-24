using System;

namespace UltricoCalendarContracts.Enums
{
    public enum RepeatPeriod
    {
        Day,
        Week,
        Month,
        Year
    }

    public static class RepeatPeriodExtension
    {
        public static DateTime NextOccurence(this RepeatPeriod period, DateTime previousDate)
        {
            switch (period)
            {
                case RepeatPeriod.Day:
                    return previousDate.AddDays(1);
                case RepeatPeriod.Week:
                    return previousDate.AddDays(7);
                case RepeatPeriod.Month:
                    return previousDate.AddMonths(1);
                case RepeatPeriod.Year:
                    return previousDate.AddYears(1);
                default:
                    throw new ArgumentOutOfRangeException(nameof(period), period, null);
            }
        }
    }
}