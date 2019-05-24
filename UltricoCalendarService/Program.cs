using System;
using UltricoCalendarCommon;
using UltricoCalendarContracts;

namespace UltricoCalendarService
{
    class Program
    {
        private const string ServiceName = "UltricoCalendarService";
        static void Main(string[] args)
        {
            Console.Title = ServiceName;
            new UltricoService(ServiceName, new UltricoCalendarModule(), new UltricoServiceSettings()).RegisterService();
        }
    }
}
