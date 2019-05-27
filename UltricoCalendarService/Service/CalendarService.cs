using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using UltricoCalendarCommon;
using UltricoCalendarContracts.Entities;
using UltricoCalendarContracts.Interfaces;
using UltricoCalendarContracts.Interfaces.Repository;
using UltricoCalendarContracts.Models;

namespace UltricoCalendarService.Service
{
    public class CalendarService : ISingleEventService, IEventSeriesService, IEditedSeriesEventService, IMetadataService
    {
        private readonly ISingleEventRepository _singleEventRepository = UltricoModule.IoCContainer.Resolve<ISingleEventRepository>();
        
        private readonly IEventSeriesRepository _eventSeriesRepository = UltricoModule.IoCContainer.Resolve<IEventSeriesRepository>();
        
        private readonly IEditedSeriesEventRepository _editedSeriesEventRepository = UltricoModule.IoCContainer.Resolve<IEditedSeriesEventRepository>();
        
        public void AddEvent(ICalendarEvent newEventModel)
        {
            _singleEventRepository.AddSingleEvent((SingleEvent)newEventModel.ToEntity());
        }

        public ICalendarEvent GetEvent(int id)
        {
            return (ScheduleEventModel)_singleEventRepository.GetSingleEvent(id).ToBaseModel();
        }

        public void EditEvent(int id, ICalendarEvent newEventModel)
        {
            var entity = (SingleEvent)newEventModel.ToEntity();
            entity.Id = id;
            _singleEventRepository.UpdateSingleEvent(entity);
        }

        public void DeleteEvent(int id)
        {
            _singleEventRepository.DeleteSingleEvent(id);
        }

        public void AddEventSeries(ICalendarEvent newEventModel)
        {
            _eventSeriesRepository.AddEventSeries((EventSeries)newEventModel.ToEntity());
        }

        public ICalendarEvent GetEventSeries(int id)
        {
            return (ScheduleEventModelSeries) _eventSeriesRepository.GetEventSeries(id).ToBaseModel();
        }

        public void EditEventSeries(int id, ICalendarEvent newEventModel)
        {
            var entity = (EventSeries)newEventModel.ToEntity();
            entity.Id = id;
            _eventSeriesRepository.UpdateEventSeries(entity);
        }
        
        public void DeleteEventSeries(int id)
        {
            _eventSeriesRepository.DeleteEventSeries(id);
        }

        public void EditEventFromSeries(int seriesId, ICalendarEvent newEventModel)
        {
            var series = _eventSeriesRepository.GetEventSeries(seriesId);
            var editedEvent = (EditedSeriesEvent) newEventModel.ToEntity();
            series.EditedEvents.Add(editedEvent);
            _eventSeriesRepository.UpdateEventSeries(series);
            _editedSeriesEventRepository.AddEditedSeriesEvent(editedEvent);
        }

        public ICalendarEvent GetEditedEventFromSeries(int id)
        {
            return (ScheduleEventModelFromSeries) _editedSeriesEventRepository.GetEditedSeriesEvent(id).ToBaseModel();
        }

        public void DeleteEventFromSeries(int seriesId, DateTime dateTime)
        {
            var series = _eventSeriesRepository.GetEventSeries(seriesId);
            series.DeletedOccurrences.Add(dateTime);
            _eventSeriesRepository.UpdateEventSeries(series);
        }


        public IEnumerable<EventMetadata> GetMetadata(DateTime @from, DateTime to)
        {
            var result = new List<EventMetadata>();
            var singleEvents = _singleEventRepository.GetSingleEvents(from, to);
            var eventSeries = _eventSeriesRepository.GetEventSeries(from, to);
            var editedEvents = _editedSeriesEventRepository.GetEditedSeriesEvent(from, to);
            
            result.AddRange(singleEvents.Select(x => x.ToMetadata(from, to)));
            result.AddRange(eventSeries.Select(x => x.ToMetadata(from, to)));
            result.AddRange(editedEvents.Select(x => x.ToMetadata(from, to)));

            return result;
        }
    }
}