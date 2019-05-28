using Akka.Actor;
using Autofac;
using UltricoCalendarCommon;
using UltricoCalendarContracts;
using UltricoCalendarContracts.Interfaces.Service;
using UltricoCalendarContracts.Models;

namespace UltricoCalendarService.Actors
{
    public class CalendarActor : ReceiveActor
    {
        private readonly IEventFromSeriesService _eventFromSeriesService =
            UltricoModule.IoCContainer.Resolve<IEventFromSeriesService>();

        private readonly IEventSeriesService _eventSeriesService =
            UltricoModule.IoCContainer.Resolve<IEventSeriesService>();

        private readonly ISingleEventService _singleEventService =
            UltricoModule.IoCContainer.Resolve<ISingleEventService>();

        public CalendarActor()
        {
            Receive<Commands.EventCommands<SingleEventModel>.Add>(command =>
            {
                _singleEventService.AddEvent(command.Data);
            });

            Receive<Commands.EventCommands<SingleEventModel>.Update>(command =>
            {
                _singleEventService.EditEvent(command.Id, command.Data);
            });

            Receive<Commands.EventCommands<SingleEventModel>.Delete>(command =>
            {
                _singleEventService.DeleteEvent(command.Id);
            });

            Receive<Commands.EventCommands<EventSeriesModel>.Add>(command =>
            {
                _eventSeriesService.AddEventSeries(command.Data);
            });

            Receive<Commands.EventCommands<EventSeriesModel>.Update>(command =>
            {
                _eventSeriesService.EditEventSeries(command.Id, command.Data);
            });

            Receive<Commands.EventCommands<EventSeriesModel>.Delete>(command =>
            {
                _eventSeriesService.DeleteEventSeries(command.Id);
            });

            Receive<Commands.EditEventFromSeriesCommands.ExcludeEventFromSeries>(command =>
            {
                _eventFromSeriesService.ExcludeEventFromSeries(command.SeriesId, command.EventFromSeriesModelData);
            });

            Receive<Commands.EditEventFromSeriesCommands.DeleteEventOccurenceFromSeries>(command =>
            {
                _eventFromSeriesService.DeleteEventOccurenceFromSeries(command.SeriesId, command.DateTime);
            });

            Receive<Commands.EventCommands<EventFromSeriesModel>.Update>(command =>
            {
                _eventFromSeriesService.EditEventFromSeries(command.Id, command.Data);
            });

            Receive<Commands.EventCommands<EventFromSeriesModel>.Delete>(command =>
            {
                _eventFromSeriesService.DeleteEventFromSeries(command.Id);
            });
        }

        private static Props Props => Props.Create(() => new CalendarActor());

        public static void Create(ActorSystem system)
        {
            system.ActorOf(Props, "calendar-actor");
        }
    }
}