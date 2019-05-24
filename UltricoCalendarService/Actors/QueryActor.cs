using System;
using Akka.Actor;
using Autofac.Features.OwnedInstances;
using UltricoCalendarService.Persistance;

namespace UltricoCalendarService.Actors
{
    public class QueryActor : ReceiveActor
    {
        public static Props Props => Props.Create(() => new CalendarActor());
        
        public Func<Owned<CalendarDbContext>> Factory { get; set; }
        
        public QueryActor()
        {
            
        }
        
        public static void Create(ActorSystem system)
        {
            system.ActorOf(Props,"query-actor");
        }
    }
}