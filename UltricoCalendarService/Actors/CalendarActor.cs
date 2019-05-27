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
                SingleEventService.AddEvent(command.Data);
            });
            
            Receive<Commands.SingleEventCommands.Update>(command =>
            {
                SingleEventService.EditEvent(command.Id, command.Data);
            });
            
            Receive<Commands.SingleEventCommands.Delete>(command =>
            {
                SingleEventService.DeleteEvent(command.Id);
            });
            
            Receive<Commands.EventSeriesCommands.Add>(command =>
            {
                EventSeriesService.AddEventSeries(command.Data);
            });
            
            Receive<Commands.EventSeriesCommands.Update>(command =>
            {
                EventSeriesService.EditEventSeries(command.Id, command.Data);
            });
            
            Receive<Commands.EventSeriesCommands.Delete>(command =>
            {
                EventSeriesService.DeleteEventSeries(command.Id);
            });
            
            Receive<Commands.EditEventFromSeriesCommands.EditEventFromSeries>(command =>
            {
                EditedSeriesEventService.EditEventFromSeries(command.SeriesId, command.EventData);
            });
            
            Receive<Commands.EditEventFromSeriesCommands.DeleteEventFromSeries>(command =>
            {
                EditedSeriesEventService.DeleteEventFromSeries(command.SeriesId, command.DateTime);
            });
        }
        
        public static void Create(ActorSystem system)
        {
            system.ActorOf(Props,"calendar-actor");
        }
    }
}