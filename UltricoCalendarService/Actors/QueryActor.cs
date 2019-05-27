using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using Autofac;
using Autofac.Features.OwnedInstances;
using Serilog;
using UltricoCalendarCommon;
using UltricoCalendarContracts;
using UltricoCalendarContracts.Interfaces;
using UltricoCalendarContracts.Interfaces.Repository;
using UltricoCalendarContracts.Models;
using UltricoCalendarService.Persistance;

namespace UltricoCalendarService.Actors
{
    public class QueryActor : ReceiveActor
    {
        private static Props Props => Props.Create(() => new QueryActor());

        private readonly ISingleEventService _singleEventService = UltricoModule.IoCContainer.Resolve<ISingleEventService>();

        private readonly IEventSeriesService _eventSeriesService = UltricoModule.IoCContainer.Resolve<IEventSeriesService>();

        private readonly IEditedSeriesEventService _editedSeriesEventService = UltricoModule.IoCContainer.Resolve<IEditedSeriesEventService>();

        private readonly IMetadataService _metadataService = UltricoModule.IoCContainer.Resolve<IMetadataService>();
        
        public QueryActor()
        {
            
            Receive<Queries.GetEventMetadata>(query =>
            {
                var result = _metadataService.GetMetadata(query.From, query.To);
                Context.Sender.Tell(result);
            });
            
            Receive<Queries.SingleEventQueries.Get>(query =>
            {
                var result = _singleEventService.GetEvent(query.Id);
                Context.Sender.Tell(result);
            });
            
            Receive<Queries.EventSeriesQueries.Get>(query =>
            {
                var result = _eventSeriesService.GetEventSeries(query.Id);
                Context.Sender.Tell(result);
            });
            
            Receive<Queries.EditEventFromSeriesQueries.Get>(query =>
            {
                var result = _editedSeriesEventService.GetEditedEventFromSeries(query.Id);
                Context.Sender.Tell(result);
            });
        }
        
        public static void Create(ActorSystem system)
        {
            system.ActorOf(Props,"query-actor");
        }
    }
}