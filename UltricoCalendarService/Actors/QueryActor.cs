using System;
using Akka.Actor;
using Autofac.Features.OwnedInstances;
using Serilog;
using UltricoCalendarContracts;
using UltricoCalendarService.Persistance;

namespace UltricoCalendarService.Actors
{
    public class QueryActor : ReceiveActor
    {
        public static Props Props => Props.Create(() => new QueryActor());
        
        public Func<Owned<CalendarDbContext>> Factory { get; set; }
        
        public QueryActor()
        {
            Receive<Queries.GetEventMetadata>(query =>
            {
                Log.Information("Hey I got AddEvent command");
            });
            
            Receive<Queries.SingleEventQueries.Get>(query =>
            {
                Log.Information("Hey I got AddEvent command");
            });
            
            Receive<Queries.EventSeriesQueries.Get>(query =>
            {
                Log.Information("Hey I got AddEvent command");
            });
            
            Receive<Queries.EditEventFromSeriesQueries.Get>(query =>
            {
                Log.Information("Hey I got AddEvent command");
            });
        }
        
        public static void Create(ActorSystem system)
        {
            system.ActorOf(Props,"query-actor");
        }
    }
}