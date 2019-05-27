using System;
using System.Collections.Generic;
using System.Linq;
using Akka.IO;
using Autofac.Features.OwnedInstances;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using UltricoCalendarContracts.Entities;
using UltricoCalendarContracts.Interfaces;
using UltricoCalendarContracts.Interfaces.Repository;
using UltricoCalendarContracts.Models;
using UltricoCalendarService.Persistance;

namespace UltricoCalendarService.Repositories
{
    public class CalendarRepository : ISingleEventRepository, IEventSeriesRepository, IEditedSeriesEventRepository
    {
        public void AddSingleEvent(SingleEvent singleEvent)
        {
            using (var db = new CalendarDbContext())
            {
                db.SingleEvents.Add(singleEvent);
                db.SaveChanges();
            }
        }

        public SingleEvent GetSingleEvent(int id)
        {
            using (var db = new CalendarDbContext())
            {
                return db.SingleEvents.First(x => x.Id == id);
            }
        }

        public void UpdateSingleEvent(SingleEvent editedSingleEvent)
        {
            using (var db = new CalendarDbContext())
            {
                db.SingleEvents.Update(editedSingleEvent);
                db.SaveChanges();
            }
        }

        public void DeleteSingleEvent(int id)
        {
            using (var db = new CalendarDbContext())
            {
                var eventToDelete = db.SingleEvents.First(x => x.Id == id);
                db.SingleEvents.Remove(eventToDelete);
                db.SaveChanges();
            }
        }

        public List<SingleEvent> GetSingleEvents(DateTime @from, DateTime to)
        {
            using (var db = new CalendarDbContext())
            {
                var singleEvents = db.SingleEvents.Where(x => (x.Start > @from && x.Start < to));
                return singleEvents.ToList();
            }
        }

        public void DeleteEventSeries(int id)
        {
            using (var db = new CalendarDbContext())
            {
                var eventToDelete = db.EventSeries.First(x => x.Id == id);
                db.EventSeries.Remove(eventToDelete);
                db.SaveChanges();
            }
        }

        public List<EventSeries> GetEventSeries(DateTime @from, DateTime to)
        {
            using (var db = new CalendarDbContext())
            {
                var eventSeries = db.EventSeries.Where(x => x.Start < to).Include(x => x.EditedEvents).ToList();
                return eventSeries;
            }
        }

        public void AddEventSeries(EventSeries eventSeries)
        {
            using (var db = new CalendarDbContext())
            {
                db.EventSeries.Add(eventSeries);
                db.SaveChanges();
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

        public void UpdateEventSeries(EventSeries editedEventSeries)
        {
            using (var db = new CalendarDbContext())
            {
                db.EventSeries.Update(editedEventSeries);
                db.SaveChanges();
            }
        }

        public void AddEditedSeriesEvent(EditedSeriesEvent editedSeriesEvent)
        {
            using (var db = new CalendarDbContext())
            {
                db.EditedSeriesEvents.Add(editedSeriesEvent);
                db.SaveChanges();
            }
        }

        public EditedSeriesEvent GetEditedSeriesEvent(int id)
        {
            using (var db = new CalendarDbContext())
            {
                return db.EditedSeriesEvents.First(x => x.Id == id);;
            }
        }

        public void UpdateEditedSeriesEvent(EditedSeriesEvent editedSeriesEvent)
        {
            using (var db = new CalendarDbContext())
            {
                db.EditedSeriesEvents.Update(editedSeriesEvent);
                db.SaveChanges();
            }
        }

        public void DeleteEditedSeriesEvent(int id)
        {
            using (var db = new CalendarDbContext())
            {
                var editedSeriesEventToDelete = db.EventSeries.First(x => x.Id == id);
                db.EventSeries.Remove(editedSeriesEventToDelete);
                db.SaveChanges();
            }
        }

        public List<EditedSeriesEvent> GetEditedSeriesEvent(DateTime @from, DateTime to)
        {
            using (var db = new CalendarDbContext())
            {
                var editedSeries = db.EditedSeriesEvents.Where(x => x.Start > from && x.Start < to);
                return editedSeries.ToList();
            }
        }
    }
}