using System;
using UltricoCalendarContracts.Entities;
using UltricoCalendarContracts.Interfaces;
using UltricoCalendarContracts.Interfaces.Repository;
using UltricoCalendarContracts.Models;

namespace UltricoCalendarService.Service
{
    public class CalendarService : ISingleEventService, IEventSeriesService, IEditedSeriesEventService
    {
        public ISingleEventRepository SingleEventRepository { get; }
        
        public IEventSeriesRepository EventSeriesRepository { get; }
        
        public IEditedSeriesEventRepository EditedSeriesEventRepository { get; }
        
        public void AddEvent(ScheduleEvent newEvent)
        {
        }

        public ScheduleEvent GetEvent(int id)
        {
            throw new NotImplementedException();
        }

        public void EditEvent(int id, ScheduleEvent newModel)
        {
            throw new NotImplementedException();
        }

        public void DeleteEvent(int id)
        {
            throw new NotImplementedException();
        }

        public void AddEventSeries(ScheduleEventSeries newEvent)
        {
            throw new NotImplementedException();
        }

        public ScheduleEventSeries GetEventSeries(int id)
        {
            throw new NotImplementedException();
        }

        public void EditEventSeries(int id, ScheduleEventSeries newModel)
        {
            throw new NotImplementedException();
        }

        public void EditEventFromSeries(int id, ScheduleEvent newModel)
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
    }
}