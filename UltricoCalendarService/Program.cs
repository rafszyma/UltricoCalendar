﻿using System;
using UltricoCalendarCommon;

namespace UltricoCalendarService
{
    internal class Program
    {
        private const string ServiceName = "UltricoCalendarService";

        private static void Main(string[] args)
        {
            Console.Title = ServiceName;
            new UltricoService(ServiceName, new UltricoCalendarModule(), new UltricoServiceSettings())
                .RegisterService();
        }
    }
}