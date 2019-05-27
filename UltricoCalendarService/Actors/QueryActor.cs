using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using Autofac.Features.OwnedInstances;
using Serilog;
using UltricoCalendarContracts;
using UltricoCalendarContracts.Interfaces.Repository;
using UltricoCalendarContracts.Models;
using UltricoCalendarService.Persistance;

namespace UltricoCalendarService.Actors
{
    public class QueryActor : ReceiveActor
    {
        public static Props Props => Props.Create(() => new QueryActor());
        
        public ISingleEventRepository SingleEventRepository { get; }
        
        public IEventSeriesRepository EventSeriesRepository { get; }
        
        public IEditedSeriesEventRepository EditedSeriesEventRepository { get; }
        
        public QueryActor()
        {
            Receive<Queries.GetEventMetadata>(query =>
            {
                var singleEvents = SingleEventRepository.GetSingleEvents(query.From, query.To).ToList();
                var eventSeries = EventSeriesRepository.GetEventSeries(query.From, query.To).ToList();
                
                // TODO return value
            });
            
            Receive<Queries.SingleEventQueries.Get>(query =>
            {
                SingleEventRepository.GetSingleEvent(query.Id);
                // TODO Return value
            });
            
            Receive<Queries.EventSeriesQueries.Get>(query =>
            {
                EventSeriesRepository.GetEventSeries(query.Id);
                // TODO Return value
            });
            
            Receive<Queries.EditEventFromSeriesQueries.Get>(query =>
            {
                EditedSeriesEventRepository.GetEditedSeriesEvent(query.Id);
                // TODO Return value
            });
        }
        
        public static void Create(ActorSystem system)
        {
            system.ActorOf(Props,"query-actor");
        }
    }
}