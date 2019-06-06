using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Xml.Xsl;
using Akka.Actor;
using Serilog;
using Serilog.Core;
using UltricoCalendarContracts.Interfaces.Repository;
using UltricoCalendarContracts.Interfaces.Service;
using UltricoCalendarContracts.Models;

namespace UltricoCalendarService.Service
{
    public class EmailNotificationService : IEmailNotificationService
    {        
        private readonly IEventFromSeriesRepository _eventFromSeriesRepository;
        private readonly IEventSeriesRepository _eventSeriesRepository;
        private readonly ISingleEventRepository _singleEventRepository;
        private readonly CalendarServiceSettings _settings;
        

        public EmailNotificationService(ISingleEventRepository singleEventRepository, IEventSeriesRepository eventSeriesRepository, IEventFromSeriesRepository eventFromSeriesRepository, CalendarServiceSettings settings)
        {
            _singleEventRepository = singleEventRepository;
            _eventSeriesRepository = eventSeriesRepository;
            _eventFromSeriesRepository = eventFromSeriesRepository;
            _settings = settings;
        }

        public void SendNotificationEmailsFromPeriod(DateTime dateTimeNow)
        {
            var from = dateTimeNow.AddMinutes(_settings.MinsBeforeToRemind - _settings.CheckEveryMins);
            var to = dateTimeNow.AddMinutes(_settings.MinsBeforeToRemind);
            var singleEvents = _singleEventRepository.GetSingleEvents(from, to);
            var eventsSeries = _eventSeriesRepository.GetEventSeries(to);
            
            var eventsFromSeries =_eventFromSeriesRepository.GetEventFromSeries(from, to);

            foreach (var singleEvent in singleEvents)
            {
                SendEmailNotification(singleEvent.MailAddresses, singleEvent.Title, singleEvent.Start);
            }
            
            foreach (var eventFromSeries in eventsFromSeries)
            {
                SendEmailNotification(eventFromSeries.MailAddresses, eventFromSeries.Title, eventFromSeries.Start);
            }
            
            foreach (var eventSeries in eventsSeries)
            {
                var eventSeriesOccurrences = eventSeries.Finish.Occur(eventSeries.RepeatPeriod, eventSeries.Start, from, to);
                foreach (var eventOccurence in eventSeriesOccurrences)
                {
                    SendEmailNotification(eventSeries.MailAddresses, eventSeries.Title, eventOccurence);
                }
            }
        }

        public void SendEmailNotification(List<string> address, string title, DateTime start)
        {
            var smtp = new SmtpClient
            {
                Host = _settings.SmtpHost,
                Port = _settings.SmtpPort,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_settings.SmtpLogin, _settings.SmtpPassword)
            };

            foreach (var mail in address)
            {
                if (IsValidEmail(mail))
                {
                    using (var message = new MailMessage(_settings.SmtpAddress, mail)
                    {
                        Subject = "Ultrico Event Incoming!",
                        Body =
                            $"Hey\n\nYou have one event incoming which is {title} and starts {start:MM/dd/yyyy HH:mm}\n\nThanks!"
                    })
                    {
                        smtp.Send(message);
                    }
                }
                else
                {
                    Log.Warning($"Email: {mail} is not valid email address, skipping.");
                }
            }
            
        }

        private static bool IsValidEmail(string email)
        {
            try {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch {
                return false;
            }
        }
    }
}