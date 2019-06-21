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
                // TODO: HIGH : Better modeled as classes perhaps. You can map incoming model / DB entity to class.
                case RepeatPeriod.Day:
                    return previousDate.AddDays(1);
                // TODO: HIGH: Weekly shceduling not complete - explicit days of week was the interesting scenario as mentioned in the task.
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