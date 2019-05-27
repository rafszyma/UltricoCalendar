using System;
using UltricoCalendarContracts.Entities;
using UltricoCalendarContracts.Interfaces;

namespace UltricoCalendarService.Service
{
    public class CalendarService : ISingleEventService, IEventSeriesService, IEditedSeriesEventService
    {
        public void AddEvent(SingleEvent newEvent)
        {
            throw new NotImplementedException();
        }

        public SingleEvent GetEvent(int id)
        {
            throw new NotImplementedException();
        }

        public void EditEvent(int id, SingleEvent newModel)
        {
            throw new NotImplementedException();
        }

        public void DeleteEvent(int id)
        {
            throw new NotImplementedException();
        }

        public void AddEventSeries(EventSeries newEvent)
        {
            throw new NotImplementedException();
        }

        public EventSeries GetEventSeries(int id)
        {
            throw new NotImplementedException();
        }

        void IEventSeriesService.EditEventSeries(int id, EventSeries newModel)
        {
            throw new NotImplementedException();
        }

        public void GetEditedEventFromSeries(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteEventFromSeries(int seriesId, DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public void DeleteEventSeries(int id)
        {
            throw new NotImplementedException();
        }

        void IEditedSeriesEventService.EditEventSeries(int id, EventSeries newModel)
        {
            throw new NotImplementedException();
        }
    }
}