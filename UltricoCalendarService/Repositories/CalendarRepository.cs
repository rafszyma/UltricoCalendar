using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Features.OwnedInstances;
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
        public Func<Owned<CalendarDbContext>> Factory { get; set; }
        
        public void AddSingleEvent(SingleEvent singleEvent)
        {
            using (var db = Factory.Invoke().Value)
            {
                db.SingleEvents.Add(singleEvent);
                db.SaveChanges();
            }
        }

        public SingleEvent GetSingleEvent(int id)
        {
            using (var db = Factory.Invoke().Value)
            {
                return db.SingleEvents.First(x => x.Id == id);
            }
        }

        public void UpdateSingleEvent(SingleEvent editedSingleEvent)
        {
            using (var db = Factory.Invoke().Value)
            {
                db.SingleEvents.Update(editedSingleEvent);
                db.SaveChanges();
            }
        }

        public void DeleteSingleEvent(int id)
        {
            using (var db = Factory.Invoke().Value)
            {
                var eventToDelete = db.SingleEvents.First(x => x.Id == id);
                db.SingleEvents.Remove(eventToDelete);
                db.SaveChanges();
            }
        }

        public IEnumerable<SingleEvent> GetSingleEvents(DateTime @from, DateTime to)
        {
            using (var db = Factory.Invoke().Value)
            {
                var singleEvents = db.SingleEvents.Where(x => (x.Start > @from && x.Start < to));
                return singleEvents;
            }
        }

        public void DeleteEventSeries(int id)
        {
            using (var db = Factory.Invoke().Value)
            {
                var eventToDelete = db.EventSeries.First(x => x.Id == id);
                db.EventSeries.Remove(eventToDelete);
                db.SaveChanges();
            }
        }

        public IEnumerable<EventSeries> GetEventSeries(DateTime @from, DateTime to)
        {
            using (var db = Factory.Invoke().Value)
            {
                var eventSeries = db.EventSeries.Where(x => x.Start < to);
                return eventSeries;
            }
        }

        public void AddEventSeries(EventSeries eventSeries)
        {
            using (var db = Factory.Invoke().Value)
            {
                db.EventSeries.Add(eventSeries);
                db.SaveChanges();
            }
        }

        public EventSeries GetEventSeries(int id)
        {
            using (var db = Factory.Invoke().Value)
            {
                var eventSeries = db.EventSeries.First(x => x.Id == id);
                return eventSeries;
            }
        }

        public void UpdateEventSeries(EventSeries editedEventSeries)
        {
            using (var db = Factory.Invoke().Value)
            {
                db.EventSeries.Update(editedEventSeries);
            }
        }

        public void AddEditedSeriesEvent(EditedSeriesEvent editedSeriesEvent)
        {
            using (var db = Factory.Invoke().Value)
            {
                db.EditedSeriesEvents.Add(editedSeriesEvent);
                db.SaveChanges();
            }
        }

        public EditedSeriesEvent GetEditedSeriesEvent(int id)
        {
            using (var db = Factory.Invoke().Value)
            {
                return db.EditedSeriesEvents.First(x => x.Id == id);;
            }
        }

        public void UpdateEditedSeriesEvent(EditedSeriesEvent editedSeriesEvent)
        {
            using (var db = Factory.Invoke().Value)
            {
                db.EditedSeriesEvents.Update(editedSeriesEvent);
            }
        }

        public void DeleteEditedSeriesEvent(int id)
        {
            using (var db = Factory.Invoke().Value)
            {
                var editedSeriesEventToDelete = db.EventSeries.First(x => x.Id == id);
                db.EventSeries.Remove(editedSeriesEventToDelete);
                db.SaveChanges();
            }
        }
    }
}