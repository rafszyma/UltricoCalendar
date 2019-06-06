using Akka.Actor;
using UltricoCalendarCommon.Settings;

namespace UltricoCalendarService
{
    public class CalendarServiceSettings : UltricoServiceSettings
    {
        public int CheckEveryMins { get; set; }
        
        public int MinsBeforeToRemind { get; set; }
        
        public string SmtpAddress { get; set; }
        
        public string SmtpLogin { get; set; }
        
        public string SmtpPassword { get; set; }
        
        public string SmtpHost { get; set; }
        
        public int SmtpPort { get; set; }
    }
}