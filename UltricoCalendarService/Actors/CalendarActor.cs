using System;
using Akka.Actor;
using Akka.DI.Core;
using Akka.Routing;
using Autofac;
using Serilog;
using UltricoCalendarCommon;
using UltricoCalendarContracts;
using UltricoCalendarContracts.Interfaces;
using UltricoCalendarContracts.Interfaces.Repository;
using UltricoCalendarContracts.Interfaces.Service;

namespace UltricoCalendarService.Actors
{
    public class CalendarActor : ReceiveActor
    {
        private static Props Props => Props.Create(() => new CalendarActor());
        
        private readonly ISingleEventService _singleEventService = UltricoModule.IoCContainer.Resolve<ISingleEventService>();

        private readonly IEventSeriesService _eventSeriesService = UltricoModule.IoCContainer.Resolve<IEventSeriesService>();

        private readonly IEditedSeriesEventService _editedSeriesEventService = UltricoModule.IoCContainer.Resolve<IEditedSeriesEventService>();

        public CalendarActor()
        {
            Receive<Commands.SingleEventCommands.Add>(command =>
            {
                _singleEventService.AddEvent(command.Data);
            });
            
            Receive<Commands.SingleEventCommands.Update>(command =>
            {
                _singleEventService.EditEvent(command.Id, command.Data);
            });
            
            Receive<Commands.SingleEventCommands.Delete>(command =>
            {
                _singleEventService.DeleteEvent(command.Id);
            });
            
            Receive<Commands.EventSeriesCommands.Add>(command =>
            {
                _eventSeriesService.AddEventSeries(command.Data);
            });
            
            Receive<Commands.EventSeriesCommands.Update>(command =>
            {
                _eventSeriesService.EditEventSeries(command.Id, command.Data);
            });
            
            Receive<Commands.EventSeriesCommands.Delete>(command =>
            {
                _eventSeriesService.DeleteEventSeries(command.Id);
            });
            
            Receive<Commands.EditEventFromSeriesCommands.EditEventFromSeries>(command =>
            {
                _editedSeriesEventService.EditEventFromSeries(command.SeriesId, command.EventFromSeriesModelData);
            });
            
            Receive<Commands.EditEventFromSeriesCommands.DeleteEventFromSeries>(command =>
            {
                _editedSeriesEventService.DeleteEventFromSeries(command.SeriesId, command.DateTime);
            });
        }
        
        public static void Create(ActorSystem system)
        {
            system.ActorOf(Props,"calendar-actor");
        }
    }
}