using System;
using System.Collections.Generic;
using System.Linq;
using UltricoCalendarContracts.Entities;
using UltricoCalendarContracts.Interfaces;
using UltricoCalendarContracts.Interfaces.Repository;
using UltricoCalendarContracts.Interfaces.Service;
using UltricoCalendarContracts.Models;

namespace UltricoCalendarService.Service
{
    public class CalendarService : ISingleEventService, IEventSeriesService, IEventFromSeriesService, IMetadataService
    {
        private readonly IEventFromSeriesRepository _eventFromSeriesRepository;
        private readonly IEventSeriesRepository _eventSeriesRepository;
        private readonly ISingleEventRepository _singleEventRepository;

        public CalendarService(ISingleEventRepository singleEventRepository,
            IEventSeriesRepository eventSeriesRepository, IEventFromSeriesRepository eventFromSeriesRepository)
        {
            _singleEventRepository = singleEventRepository;
            _eventSeriesRepository = eventSeriesRepository;
            _eventFromSeriesRepository = eventFromSeriesRepository;
        }

        public ICalendarEvent GetEventFromSeries(int id)
        {
            return (EventFromSeriesModel) _eventFromSeriesRepository.GetEventFromSeries(id).ToBaseModel();
        }

        public void EditEventFromSeries(int id, ICalendarEvent newEventModel)
        {
            var entity = (EventFromSeries) newEventModel.ToEntity();
            entity.Id = id;
            _eventFromSeriesRepository.UpdateEventFromSeries(entity);
        }

        public bool DeleteEventFromSeries(int id)
        {
            return _eventFromSeriesRepository.DeleteEventFromSeries(id);
        }

        public int ExcludeEventFromSeries(int seriesId, ICalendarEvent newEventModel)
        {
            return _eventSeriesRepository.ExcludeEventFromSeries(seriesId, (EventFromSeries) newEventModel.ToEntity());
        }

        public void DeleteEventOccurenceFromSeries(int seriesId, DateTime dateTime)
        {
            var series = _eventSeriesRepository.GetEventSeries(seriesId);
            series.DeletedOccurrences.Add(dateTime);
            _eventSeriesRepository.UpdateEventSeries(series);
        }

        public int AddEventSeries(ICalendarEvent newEventModel)
        {
            return _eventSeriesRepository.AddEventSeries((EventSeries) newEventModel.ToEntity());
        }

        public ICalendarEvent GetEventSeries(int id)
        {
            return (EventSeriesModel) _eventSeriesRepository.GetEventSeries(id).ToBaseModel();
        }

        public void EditEventSeries(int id, ICalendarEvent newEventModel)
        {
            var entity = (EventSeries) newEventModel.ToEntity();
            entity.Id = id;
            _eventSeriesRepository.UpdateEventSeries(entity);
        }

        public bool DeleteEventSeries(int id)
        {
            return _eventSeriesRepository.DeleteEventSeries(id);
        }


        public IEnumerable<EventMetadata> GetMetadata(DateTime from, DateTime to)
        {
            var result = new List<EventMetadata>();
            result.AddRange(_singleEventRepository.GetSingleEvents(from, to).Select(x => x.ToMetadata(from, to)));
            result.AddRange(_eventSeriesRepository.GetEventSeries(to).Select(x => x.ToMetadata(from, to)));
            result.AddRange(_eventFromSeriesRepository.GetEventFromSeries(from, to)
                .Select(x => x.ToMetadata(from, to)));

            return result;
        }

        public int AddEvent(ICalendarEvent newEventModel)
        {
            return _singleEventRepository.AddSingleEvent((SingleEvent) newEventModel.ToEntity());
        }

        public ICalendarEvent GetEvent(int id)
        {
            return (SingleEventModel) _singleEventRepository.GetSingleEvent(id).ToBaseModel();
        }

        public void EditEvent(int id, ICalendarEvent newEventModel)
        {
            var entity = (SingleEvent) newEventModel.ToEntity();
            entity.Id = id;
            _singleEventRepository.UpdateSingleEvent(entity);
        }

        public bool DeleteEvent(int id)
        {
            return _singleEventRepository.DeleteSingleEvent(id);
        }
    }
}