using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using UltricoCalendarContracts.Entities;
using UltricoCalendarContracts.Interfaces.Repository;
using UltricoCalendarService.Persistance;

namespace UltricoCalendarService.Repositories
{
    public class CalendarRepository : ISingleEventRepository, IEventSeriesRepository, IEventFromSeriesRepository
    {
        public int AddEventFromSeries(EventFromSeries eventFromSeries)
        {
            using (var db = new CalendarDbContext())
            {
                var entity = db.EditedSeriesEvents.Add(eventFromSeries);
                db.SaveChanges();
                return entity.Entity.Id;
            }
        }

        public EventFromSeries GetEventFromSeries(int id)
        {
            using (var db = new CalendarDbContext())
            {
                return db.EditedSeriesEvents.First(x => x.Id == id);
            }
        }

        public EventFromSeries UpdateEventFromSeries(EventFromSeries eventFromSeries)
        {
            using (var db = new CalendarDbContext())
            {
                var entity = db.EditedSeriesEvents.Update(eventFromSeries);
                db.SaveChanges();
                return entity.Entity;
            }
        }

        public bool DeleteEventFromSeries(int id)
        {
            using (var db = new CalendarDbContext())
            {
                var editedSeriesEventToDelete = db.EventSeries.First(x => x.Id == id);
                db.EventSeries.Remove(editedSeriesEventToDelete);
                return db.SaveChanges() > 0;
            }
        }

        public List<EventFromSeries> GetEventFromSeries(DateTime from, DateTime to)
        {
            using (var db = new CalendarDbContext())
            {
                var editedSeries = db.EditedSeriesEvents.Where(x => x.Start > from && x.Start < to);
                return editedSeries.ToList();
            }
        }

        public int ExcludeEventFromSeries(int seriesId, EventFromSeries eventToExclude)
        {
            using (var db = new CalendarDbContext())
            {
                var eventSeries = db.EventSeries.Include(x => x.EditedEvents).First(x => x.Id == seriesId);
                eventSeries.EditedEvents.Add(eventToExclude);
                db.Update(eventSeries);
                db.SaveChanges();
                return eventToExclude.Id;
            }
        }

        public bool DeleteEventSeries(int id)
        {
            using (var db = new CalendarDbContext())
            {
                var eventToDelete = db.EventSeries.First(x => x.Id == id);
                db.EventSeries.Remove(eventToDelete);
                return db.SaveChanges() > 0;
            }
        }

        public List<EventSeries> GetEventSeries(DateTime from, DateTime to)
        {
            using (var db = new CalendarDbContext())
            {
                var eventSeries = db.EventSeries.Where(x => x.Start > from && x.Start < to).Include(x => x.EditedEvents)
                    .ToList();
                return eventSeries;
            }
        }

        public int AddEventSeries(EventSeries eventSeries)
        {
            using (var db = new CalendarDbContext())
            {
                var entity = db.EventSeries.Add(eventSeries);
                db.SaveChanges();
                return entity.Entity.Id;
            }
        }

        public EventSeries GetEventSeries(int id)
        {
            using (var db = new CalendarDbContext())
            {
                var eventSeries = db.EventSeries.Include(x => x.EditedEvents).First(x => x.Id == id);
                return eventSeries;
            }
        }

        public EventSeries UpdateEventSeries(EventSeries editedEventSeries)
        {
            using (var db = new CalendarDbContext())
            {
                var addedEntity = db.EventSeries.Update(editedEventSeries);
                db.SaveChanges();
                return addedEntity.Entity;
            }
        }

        public int AddSingleEvent(SingleEvent singleEvent)
        {
            using (var db = new CalendarDbContext())
            {
                var entity = db.SingleEvents.Add(singleEvent);
                db.SaveChanges();
                return entity.Entity.Id;
            }
        }

        public SingleEvent GetSingleEvent(int id)
        {
            using (var db = new CalendarDbContext())
            {
                return db.SingleEvents.First(x => x.Id == id);
            }
        }

        public SingleEvent UpdateSingleEvent(SingleEvent editedSingleEvent)
        {
            using (var db = new CalendarDbContext())
            {
                var entity = db.SingleEvents.Update(editedSingleEvent);
                db.SaveChanges();
                return entity.Entity;
            }
        }

        public bool DeleteSingleEvent(int id)
        {
            using (var db = new CalendarDbContext())
            {
                var eventToDelete = db.SingleEvents.First(x => x.Id == id);
                db.SingleEvents.Remove(eventToDelete);
                return db.SaveChanges() > 0;
            }
        }

        public List<SingleEvent> GetSingleEvents(DateTime from, DateTime to)
        {
            using (var db = new CalendarDbContext())
            {
                var singleEvents = db.SingleEvents.Where(x => x.Start > from && x.Start < to);
                return singleEvents.ToList();
            }
        }
    }
}