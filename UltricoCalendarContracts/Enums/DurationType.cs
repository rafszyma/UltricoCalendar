namespace UltricoCalendarContracts.Enums
{
    // TODO: MEDIUM: Likely could be modeled as property of duration IsFullDay. It never changes / will only ever have two values.
    // This way conditions would read in a more natural way. fi event duration is full day or if event lasts full day instead of
    // if event type id type full day.
    // Could also be implemented as subclasses of Duration : FullDay, TimeSpan
    public enum DurationType
    {
        TimeSpan,
        FullDay
    }
}