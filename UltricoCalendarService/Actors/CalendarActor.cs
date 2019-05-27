using System;
using Akka.Actor;
using Akka.DI.Core;
using Akka.Routing;
using Serilog;
using UltricoCalendarContracts;
using UltricoCalendarContracts.Interfaces;

namespace UltricoCalendarService.Actors
{
    public class CalendarActor : ReceiveActor
    {
        public ISingleEventService SingleEventService;

        public IEventSeriesService EventSeriesService;

        public IEditedSeriesEventService EditedSeriesEventService;
        
        public static Props Props => Props.Create(() => new CalendarActor());
        
        public CalendarActor()
        {
            Receive<Commands.SingleEventCommands.Add>(command =>
            {
                Log.Information("Hey I got AddEvent command");
            });
            
            Receive<Commands.SingleEventCommands.Update>(command =>
            {
                Log.Information("Hey I got AddEvent command");
            });
            
            Receive<Commands.SingleEventCommands.Delete>(command =>
            {
                Log.Information("Hey I got AddEvent command");
            });
            
            Receive<Commands.EventSeriesCommands.Add>(command =>
            {
                Log.Information("Hey I got AddEvent command");
            });
            
            Receive<Commands.EventSeriesCommands.Update>(command =>
            {
                Log.Information("Hey I got AddEvent command");
            });
            
            Receive<Commands.EventSeriesCommands.Delete>(command =>
            {
                Log.Information("Hey I got AddEvent command");
            });
            
            Receive<Commands.EditEventFromSeriesCommands.EditEventFromSeries>(command =>
            {
                Log.Information("Hey I got AddEvent command");
            });
            
            Receive<Commands.EditEventFromSeriesCommands.DeleteEventFromSeries>(command =>
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