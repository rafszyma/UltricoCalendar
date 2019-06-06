using System;
using System.Collections.Generic;

namespace UltricoCalendarContracts.Interfaces.Service
{
    public interface IEmailService
    {
        bool SendNotificationEmailsFromPeriod(DateTime from, DateTime to);

        bool SendEmailNotification(List<string> address, string title, DateTime start);


    }
}