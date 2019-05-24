using System;
using Akka.Actor;
using Akka.DI.Core;
using Akka.Routing;
using Serilog;
using UltricoCalendarContracts;

namespace UltricoCalendarService.Actors
{
    public class CalendarActor : ReceiveActor
    {
        public static Props Props => Props.Create(() => new CalendarActor());
        
        public CalendarActor()
        {
            Receive<Commands.AddEvent>(command =>
            {
                Log.Information("Hey I got AddEvent command");
            });
        }
        
        public static void Create(ActorSystem system)
        {
            system.ActorOf(Props,"calendar-actor");
        }
    }
}