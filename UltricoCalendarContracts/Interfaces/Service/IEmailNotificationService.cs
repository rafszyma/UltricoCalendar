using System;
using System.Collections.Generic;

namespace UltricoCalendarContracts.Interfaces.Service
{
    public interface IEmailNotificationService
    {
        void SendNotificationEmailsFromPeriod(DateTime from);
    }
}