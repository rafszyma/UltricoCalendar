using System;
using System.Collections.Generic;
using System.Linq;
using UltricoCalendarContracts.Interfaces.Repository;
using UltricoCalendarContracts.Interfaces.Service;
using UltricoCalendarContracts.Models;

namespace UltricoCalendarService.Service
{
    public class EmailService : IEmailService
    {        
        private readonly IEventFromSeriesRepository _eventFromSeriesRepository;
        private readonly IEventSeriesRepository _eventSeriesRepository;
        private readonly ISingleEventRepository _singleEventRepository;

        public EmailService(ISingleEventRepository singleEventRepository, IEventSeriesRepository eventSeriesRepository, IEventFromSeriesRepository eventFromSeriesRepository)
        {
            _singleEventRepository = singleEventRepository;
            _eventSeriesRepository = eventSeriesRepository;
            _eventFromSeriesRepository = eventFromSeriesRepository;
        }

        public bool SendNotificationEmailsFromPeriod(DateTime @from, DateTime to)
        {
            var singleEvents = _singleEventRepository.GetSingleEvents(from, to);
            var eventsSeries = _eventSeriesRepository.GetEventSeries(to);
            var eventSeriesOccurrences = eventsSeries.SelectMany(x => x.Finish.Occur(x.RepeatPeriod, x.Start, from, to)).ToList();
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
                // TODO test it
                SendEmailNotification(eventSeries.MailAddresses, eventSeries.Title, eventSeriesOccurrences.ElementAt(eventsSeries.IndexOf(eventSeries)));
            }
            return true;
        }

        public bool SendEmailNotification(List<string> address, string title, DateTime start)
        {
            throw new NotImplementedException();
        }
    }
}