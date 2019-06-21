namespace UltricoCalendarCommon.Settings
{
    //TODO : HIGH : public setters - here and everywhere else. This shouldn't be mutable.
    public class UltricoServiceSettings
    {
        public string LogEnvironment { get; set; }

        public string AkkaSystemName { get; set; }

        public string HoconPath { get; set; }
    }
}